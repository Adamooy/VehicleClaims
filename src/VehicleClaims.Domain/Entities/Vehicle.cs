using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleClaims.Domain.Common;
using VehicleClaims.Domain.ValueObjects;

namespace VehicleClaims.Domain.Entities
{
    public class Vehicle : Entity
    {
        public VehicleVin Vin { get; private set; }
        public string Make { get; private set; }
        public string Model { get; private set; }
        public int Year { get; private set; }
        public string LicensePlate { get; private set; }

        private Vehicle() : base(Guid.NewGuid()) { }

        private Vehicle(Guid id, VehicleVin vin, string make, string model, int year, string licensePlate) : base(id)
        {
            Vin = vin;
            Make = make;
            Model = model;
            Year = year;
            LicensePlate = licensePlate;
        }

        public static Vehicle Create(string vin, string make, string model, int year, string licensePlate)
        {
            if (string.IsNullOrWhiteSpace(make)) throw new ArgumentException("Make is required.");
            if (string.IsNullOrWhiteSpace(model)) throw new ArgumentException("Model is required.");
            if (year < 1980 || year > DateTime.UtcNow.Year + 1)
                throw new ArgumentOutOfRangeException(nameof(year), "Invalid vehicle year.");

            return new Vehicle(Guid.NewGuid(), new VehicleVin(vin), make, model, year, licensePlate);
        }

        public void UpdateLicensePlate(string licensePlate)
        {
            if (string.IsNullOrWhiteSpace(licensePlate))
                throw new ArgumentException("License plate is required.");
            LicensePlate = licensePlate;
        }
    }
}
