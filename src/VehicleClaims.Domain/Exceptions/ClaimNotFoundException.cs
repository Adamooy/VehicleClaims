using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleClaims.Domain.Exceptions
{
    public sealed class ClaimNotFoundException(Guid claimId) : DomainException($"Claim with ID '{claimId}' was not found.");
}
