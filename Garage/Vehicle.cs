using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Garage
{
    // ------------------------------------------------------------------------
    // *** Class Vehicle ***
    // ------------------------------------------------------------------------
    internal class Vehicle(string licensePlate, string model, int modelYear, int numberOfWheels, string color)
    {
        //private List<Vehicle> _vehicles;

        // --- Properties -----------------------------------------------------
        public string LicensePlate { get; set; } = licensePlate;
        public string Model { get; set; } = model;
        public int ModelYear { get; set; } = modelYear; 
        public int NumberOfWheels { get; set; } = numberOfWheels;
        public string Color { get; set; } = color;
    }
}