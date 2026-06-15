using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Garage
{
    // ------------------------------------------------------------------------
    // *** Class Car ***
    // ------------------------------------------------------------------------
    internal class Car(string fuelType, string licensePlate, string model, int modelYear, int numberOfWheels, string color)
        : Vehicle(licensePlate, model, modelYear, numberOfWheels, color)
    {
        // --- Properties -----------------------------------------------------
        public string FuelType { get; } = fuelType;

    }
}