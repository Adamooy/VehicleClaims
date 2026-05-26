using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleClaims.Domain.Common;
using VehicleClaims.Domain.Enums;
using VehicleClaims.Domain.ValueObjects;

namespace VehicleClaims.Domain.Events
{
    public sealed record DamageAssessedEvent(Guid ClaimId, Guid AssessmentId, DamageLevel Level, Money EstimatedCost) : IDomainEvent;
}
