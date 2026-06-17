using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Garage
{
    // ------------------------------------------------------------------------
    // *** Class Boat ***
    // ------------------------------------------------------------------------
    internal class Boat(int length, string licensePlate, string model, int modelYear, int numberOfWheels, string color)
        : Vehicle(licensePlate, model, modelYear, numberOfWheels, color)
    {
        // --- Properties -----------------------------------------------------
        public int Length { get; } = length;
    }
}