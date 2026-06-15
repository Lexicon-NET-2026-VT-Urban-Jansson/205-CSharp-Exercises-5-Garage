using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Garage
{
    // ------------------------------------------------------------------------
    // *** Class Bus ***
    // ------------------------------------------------------------------------
    internal class Bus(int numberOfSeats, string licensePlate, string model, int modelYear, int numberOfWheels, string color)
    : Vehicle(licensePlate, model, modelYear, numberOfWheels, color)
    {
        // --- Properties -----------------------------------------------------
        public int NumberOfSeats { get; } = numberOfSeats;
    }
}