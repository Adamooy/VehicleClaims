using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleClaims.Domain.Common;
using VehicleClaims.Domain.Enums;
using VehicleClaims.Domain.Events;
using VehicleClaims.Domain.Exceptions;
using VehicleClaims.Domain.ValueObjects;

namespace VehicleClaims.Domain.Entities
{
    public class Claim : AggregateRoot
    {
        public string ClaimNumber { get; private set; }
        public ClaimStatus Status { get; private set; }
        public string Description { get; private set; }
        public DateTime IncidentDate { get; private set; }
        public DateTime SubmittedAt { get; private set; }
        public DateTime? ResolvedAt { get; private set; }

        public Guid VehicleId { get; private set; }
        public Vehicle Vehicle { get; private set; } = null!;

        private readonly List<DamageAssessment> _assessments = [];
        public IReadOnlyCollection<DamageAssessment> Assessments => _assessments.AsReadOnly();

        public Money TotalEstimatedCost =>
            _assessments.Aggregate(Money.Zero(), (sum, a) => sum.Add(a.EstimatedRepairCost));

        private Claim() : base(Guid.NewGuid()) { }

        private Claim(Guid id, string claimNumber, Guid vehicleId, string description, DateTime incidentDate) : base(id)
        {
            ClaimNumber = claimNumber;
            VehicleId = vehicleId;
            Description = description;
            IncidentDate = incidentDate;
            Status = ClaimStatus.Submitted;
            SubmittedAt = DateTime.UtcNow;
        }

        public static Claim Submit(Guid vehicleId, string description, DateTime incidentDate)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description is required.");
            if (incidentDate > DateTime.UtcNow)
                throw new ArgumentException("Incident date cannot be in the future.");

            var claim = new Claim(
                Guid.NewGuid(),
                GenerateClaimNumber(),
                vehicleId,
                description,
                incidentDate);

            claim.RaiseDomainEvent(new ClaimSubmittedEvent(claim.Id, vehicleId, claim.SubmittedAt));
            return claim;
        }

        public void StartReview()
        {
            EnsureStatus(ClaimStatus.Submitted, "Only submitted claims can be reviewed.");
            Status = ClaimStatus.UnderReview;
        }

        public void AddAssessment(DamageAssessment assessment)
        {
            if (Status is not (ClaimStatus.UnderReview or ClaimStatus.Assessed))
                throw new InvalidClaimStateException("Assessments can only be added during review.");

            _assessments.Add(assessment);
            Status = ClaimStatus.Assessed;

            RaiseDomainEvent(new DamageAssessedEvent(
                Id, assessment.Id, assessment.Level, assessment.EstimatedRepairCost));
        }

        public void Approve()
        {
            EnsureStatus(ClaimStatus.Assessed, "Only assessed claims can be approved.");

            Status = ClaimStatus.Approved;
            ResolvedAt = DateTime.UtcNow;

            RaiseDomainEvent(new ClaimApprovedEvent(Id, TotalEstimatedCost, ResolvedAt.Value));
        }

        public void Reject(string reason)
        {
            if (Status is ClaimStatus.Closed or ClaimStatus.Approved)
                throw new InvalidClaimStateException("Cannot reject a closed or approved claim.");
            if (string.IsNullOrWhiteSpace(reason))
                throw new ArgumentException("Rejection reason is required.");

            Status = ClaimStatus.Rejected;
            ResolvedAt = DateTime.UtcNow;

            RaiseDomainEvent(new ClaimRejectedEvent(Id, reason, ResolvedAt.Value));
        }

        public void Close()
        {
            if (Status is not (ClaimStatus.Approved or ClaimStatus.Rejected))
                throw new InvalidClaimStateException("Only approved or rejected claims can be closed.");

            Status = ClaimStatus.Closed;
        }
        private void EnsureStatus(ClaimStatus expected, string message)
        {
            if (Status != expected)
                throw new InvalidClaimStateException(message);
        }

        private static string GenerateClaimNumber()
            => $"CLM-{DateTime.UtcNow:yyyyMM}-{Guid.NewGuid().ToString()[..8].ToUpperInvariant()}";
    }
}
