using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleClaims.Domain.Common;
using VehicleClaims.Domain.ValueObjects;

namespace VehicleClaims.Domain.Events
{
    public sealed record ClaimApprovedEvent(Guid ClaimId, Money ApprovedAmount, DateTime ApprovedAt) : IDomainEvent;
}
