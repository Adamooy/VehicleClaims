using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleClaims.Domain.Common;
using VehicleClaims.Domain.Enums;
using VehicleClaims.Domain.ValueObjects;

namespace VehicleClaims.Domain.Entities
{
    public class DamageAssessment : Entity
    {
        public Guid ClaimId { get; private set; }
        public DamageLevel Level { get; private set; }
        public DamageCategory Category { get; private set; }
        public string Description { get; private set; }
        public Money EstimatedRepairCost { get; private set; }
        public DateTime AssessedAt { get; private set; }
        public string AssessedBy { get; private set; }

        private DamageAssessment() : base(Guid.NewGuid()) { }

        private DamageAssessment(Guid id, Guid claimId, DamageLevel level, DamageCategory category, string description, Money estimatedRepairCost, string assessedBy) : base(id)
        {
            ClaimId = claimId;
            Level = level;
            Category = category;
            Description = description;
            EstimatedRepairCost = estimatedRepairCost;
            AssessedAt = DateTime.UtcNow;
            AssessedBy = assessedBy;
        }

        public static DamageAssessment Create(Guid claimId, DamageLevel level, DamageCategory category, string description, Money estimatedRepairCost, string assessedBy)
        {
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description is required.");
            if (string.IsNullOrWhiteSpace(assessedBy)) throw new ArgumentException("AssessedBy is required.");
            if (estimatedRepairCost.Amount < 0) throw new ArgumentException("Cost cannot be negative.");

            return new DamageAssessment(Guid.NewGuid(), claimId, level, category, description, estimatedRepairCost, assessedBy);
        }
    }
}
