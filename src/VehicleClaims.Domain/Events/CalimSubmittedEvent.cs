using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleClaims.Domain.Common;

namespace VehicleClaims.Domain.Events
{
    public sealed record ClaimSubmittedEvent(Guid ClaimId, Guid VehicleId, DateTime SubmittedAt) : IDomainEvent;
}
