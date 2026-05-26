using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleClaims.Domain.Exceptions
{
    public sealed class InvalidClaimStateException(string message) : DomainException(message);
}
