using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleClaims.Domain.ValueObjects
{
    public sealed record VehicleVin
    {
        public string Value { get; }

        public VehicleVin(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 17)
                throw new ArgumentException("VIN must be exactly 17 characters.", nameof(value));

            Value = value.ToUpperInvariant();
        }

        public override string ToString() => Value;
    }
}
