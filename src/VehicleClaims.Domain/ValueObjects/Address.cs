using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleClaims.Domain.ValueObjects
{
    public sealed record Address(
        string Street,
        string City,
        string PostalCode,
        string Country);
}
