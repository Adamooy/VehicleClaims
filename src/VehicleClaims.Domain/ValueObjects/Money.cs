using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleClaims.Domain.ValueObjects
{
    public sealed record Money(decimal Amount, string Currency)
    {
        public static Money Zero(string currency = "PLN") => new(0, currency);

        public Money Add(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException("Cannot add money with different currencies.");
            return new Money(Amount + other.Amount, Currency);
        }

        public override string ToString() => $"{Amount:F2} {Currency}";
    }
}
