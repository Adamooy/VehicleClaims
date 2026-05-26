using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleClaims.Domain.Common;

namespace VehicleClaims.Domain.Events
{
    public sealed record ClaimRejectedEvent(Guid ClaimId, string Reason, DateTime RejectedAt) : IDomainEvent;
}
