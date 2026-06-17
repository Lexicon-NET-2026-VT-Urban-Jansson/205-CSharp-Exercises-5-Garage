using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;

namespace Garage
{
    // ------------------------------------------------------------------------
    // *** Class Handler ***
    // ------------------------------------------------------------------------
    internal class Handler(int parkingLots)
    {
        // --- Fields ---------------------------------------------------------
        private readonly List<Vehicle>? _vehicles = []; // <-- Simplify new expression

        // TODO DONE: REDESIGN - DETTA ÄR FEL! 
        //private MyGarage<Vehicle>? _garage;
        private readonly MyGarage<Vehicle> _garage = new(parkingLots);

        // --- Properties -----------------------------------------------------
        // TODO DONE: REDESIGN - DETTA ÄR FEL!
        //public int NumberOfParkingLots { get; } = parkingLots;



        // --------------------------------------------------------------------
        // *** FixLicensePlate ***
        // --------------------------------------------------------------------
        // TODO DONE: REDESIGN - DETTA ÄR FEL!
        //private string FixLicensePlate(string lp)
        //{
        //    return lp.Trim().ToUpper();
        //}


        // --------------------------------------------------------------------
        // *** InitGarage ***
        // --------------------------------------------------------------------
        // TODO: REDESIGN - DETTA ÄR FEL!
        public void InitGarage()
        {
            // TODO DONE: REDESIGN - DETTA ÄR FEL!
            //string preFix = "_handler.InitGarage -> ";
            //DebugAndTest.DoDebug(false, preFix + $"Sätter NumberOfParkingLots till {NumberOfParkingLots}");
            //DebugAndTest.DoDebug(false, preFix + "Instansierar Garage: _garage = new MyGarage<Vehicle>(NumberOfParkingLots)");

            // Create a new Garage with a number of parking lots
            //_garage = new MyGarage<Vehicle>(NumberOfParkingLots);

            //DebugAndTest.DoDebug(false, preFix + $"Nya garaget har {_garage.NumberOfParkingLots} parkeringsplatser");
        }


        // --------------------------------------------------------------------
        // *** FindVehicleInGarage ***
        // --------------------------------------------------------------------
        public bool FindVehicleInGarage(string licensePlate)
        {
            bool result = _garage.FindVehicle(licensePlate);

            if (result)
            {
                string preFix = "_handler.FindVehicleInGarage(licensePlate) -> ";
                DebugAndTest.DoDebug(false, preFix + $"Fordon {licensePlate} finns inte i garaget");
            } 

            return result;
        }


        // --------------------------------------------------------------------
        // *** FindVehicleInGarageWithLINQ ***
        // --------------------------------------------------------------------
        public void FindVehicleInGarageWithLINQ()
        {
            // TODO: REDESIGN - DETTA ÄR FEL!
            _garage.FindVehiclesWithLINQ();
        }


        // --------------------------------------------------------------------
        // *** TestGetEnumerator ***
        // --------------------------------------------------------------------
        public void TestGetEnumerator()
        {
            // TODO: ENDAST FÖR TEST AV GETENUMERATOR() 
            _garage.TestGetEnumerator();
        }


        // --------------------------------------------------------------------
        // *** AddVehicleToGarage ***
        // --------------------------------------------------------------------
        public bool AddVehicleToGarage(string licensePlate)
        {
            bool result = false;
            string preFix = "_handler.AddVehicleToGarage(licensePlate) -> ";
            
            // TODO DONE: REDESIGN - DETTA ÄR FEL!
            //licensePlate = FixLicensePlate(licensePlate);

            // Check if _garage is null
            //if (_garage is null)
            //{
            //    DebugAndTest.DoDebug(false, preFix + "ERROR! _garage är null");
            //    return result;
            //}
            // Check if _vehicles is null
            //if (_vehicles is null)
            //{
            //    DebugAndTest.DoDebug(false, preFix + "ERROR! _vehicles är null");
            //    return result;
            //}

            // Check if garage is full
            if (_garage.GarageIsFull())
            {
                DebugAndTest.DoDebug(false, preFix + $"Garaget är fullt, kan inte registrera {licensePlate} i garaget");
                return result;
            }
            // Check if vehicle already in garage
            if (FindVehicleInGarage(licensePlate))
            {
                DebugAndTest.DoDebug(false, preFix + $"Fordon {licensePlate} finns redan i garaget");
                return result;
            }


            // Check if vehicle exist, if so: Add vehicle to garage!
            Vehicle? vehicle = VehicleExist(licensePlate);
            if (vehicle != null) result = _garage.AddToGarage(vehicle);
                
            if (result) 
                DebugAndTest.DoDebug(false, preFix + $"Fordon {licensePlate} är registrerat i garaget");
            else
                DebugAndTest.DoDebug(false, preFix + $"Kan inte hitta och registrera {licensePlate} i garaget");
            
            return result;
        }


        // --------------------------------------------------------------------
        // *** RemoveVehicleFromGarage ***
        // --------------------------------------------------------------------
        public bool RemoveVehicleFromGarage(string licensePlate)
        {
            // TODO DONE: REDESIGN - DETTA ÄR FEL!
            //licensePlate = FixLicensePlate(licensePlate);
            
            bool result = _garage.RemoveFromGarage(licensePlate);

            string msg = $"_handler.RemoveVehicleFromGarage(licensePlate) -> Fordon {licensePlate} ";
            msg += result ? "har av avregistrerats från garaget" : "kan inte avregistreras från garaget";
            DebugAndTest.DoDebug(false, msg);

            return result;
        }


        // --------------------------------------------------------------------
        // *** AddToVehicleNEW ***
        // --------------------------------------------------------------------
        public void AddToVehicleNEW(string vehicleType, object typeSpecific, string licensePlate, string model, int modelYear, int numberOfWheels, string color)
        {
            string preFix = "_handler.AddToVehicle(vehicle) -> ";
            // Check if null
            if (_vehicles is null)
            {
                DebugAndTest.DoDebug(false, preFix + "ERROR! _vehicles is null");
                return;
            }

            vehicleType = vehicleType.ToUpper().Trim();
            switch (vehicleType)
            {
                case "BIL" when typeSpecific is string fuelType:
                    _vehicles.Add(new Car(fuelType, licensePlate, model, modelYear, numberOfWheels, color));
                    break;
                case "FLYGPLAN" when typeSpecific is int numberOfEngines:
                    _vehicles.Add(new Airplane(numberOfEngines, licensePlate, model, modelYear, numberOfWheels, color));
                    break;
                case "MOTORCYKEL" when typeSpecific is int cylinderVolume:
                    _vehicles.Add(new Motorcycle(cylinderVolume, licensePlate, model, modelYear, numberOfWheels, color));
                    break;
                case "BUSS" when typeSpecific is int numberOfSeats:
                    _vehicles.Add(new Bus(numberOfSeats, licensePlate, model, modelYear, numberOfWheels, color));
                    break;
                case "BÅT" when typeSpecific is int length:
                    _vehicles.Add(new Boat(length, licensePlate, model, modelYear, numberOfWheels, color));
                    break;
                default:
                    break;
            }
        }

        // --------------------------------------------------------------------
        // *** AddVehicle ***
        // --------------------------------------------------------------------
        public void AddToVehicle(Vehicle vehicle)
        {
            string preFix = "_handler.AddToVehicle(vehicle) -> ";
            // Check if null
            if (_vehicles is null)
                DebugAndTest.DoDebug(false, preFix + "ERROR! _vehicles is null");
            else
                _vehicles.Add(vehicle);

            // new Car("Diesel", licensePlate, "Volvo XC90", 2010, 4, "Black")

            //AddToVehicle(new Airplane(2, licensePlate, "Cessna 404 Titan II", 2011, 2, "Blue"));
            //AddToVehicle(new Car("Diesel", licensePlate, "Volvo XC90", 2010, 4, "Black"));
        }


        // --------------------------------------------------------------------
        // *** VehicleExist ***
        // --------------------------------------------------------------------
        public Vehicle? VehicleExist(string licensePlate)
        {
            // TODO DONE: REDESIGN - DETTA ÄR FEL!
            //licensePlate = FixLicensePlate(licensePlate);
            licensePlate = licensePlate.Trim().ToUpper();

            // Check if null
            //if (_vehicles is null)
            //    DebugAndTest.DoDebug(false, preFix + "ERROR! _vehicles is null");
            //else
            //    _vehicles.Add(vehicle);

            return _vehicles!.Find(x => x.LicensePlate == licensePlate);
        }


        // --------------------------------------------------------------------
        // *** ListAllVehicles ***
        // --------------------------------------------------------------------
        public void ListAllVehiclesInGarage()
        {
            string preFix = "_handler.ListAllVehiclesInGarage() -> ";
            // Check if null
            if (_garage is null)
                DebugAndTest.DoDebug(false, preFix + "ERROR! _garage is null");
            else
            {
                DebugAndTest.DoDebug(false, preFix + "Kör: _garage.ListAllVehicles()");
                _garage.ListAllVehicles();
            }

        }


        // --------------------------------------------------------------------
        // *** ListAllVehicleTypes ***
        // --------------------------------------------------------------------
        public void ListAllVehicleTypesInGarage()
        {
            string preFix = "_handler.ListAllVehicleTypesInGarage() -> ";
            // Check if null
            if (_garage is null)
                DebugAndTest.DoDebug(false, preFix + "ERROR! _garage is null");
            else
            {
                DebugAndTest.DoDebug(false, preFix + "Kör: _garage.ListAllVehicleTypes()");
                _garage.ListAllVehicleTypes();
            }
        }


        // --------------------------------------------------------------------
        // *** PopulateVehicle ***
        // --------------------------------------------------------------------
        public void PopulateVehicle()
        {
            string licensePlate = "ABC123";
            //AddToVehicle(new Car("Diesel", licensePlate, "Volvo XC90", 2010, 4, "Black"));
            AddToVehicleNEW("Bil", "Diesel", licensePlate, "Volvo XC90", 2010, 4, "Black");
            DebugVehicle(licensePlate);

            licensePlate = "ABC456";
            //AddToVehicle(new Airplane(2, licensePlate, "Cessna 404 Titan II", 2011, 2, "Blue"));
            AddToVehicleNEW("Flygplan", 2, licensePlate, "Cessna 404 Titan II", 2011, 2, "Blue");
            DebugVehicle(licensePlate);

            licensePlate = "ABC789";
            //AddToVehicle(new Bus(22, licensePlate, "Scania OmniExpress", 2012, 4, "Green"));
            AddToVehicleNEW("Buss", 22, licensePlate, "Scania OmniExpress", 2012, 4, "Green");
            DebugVehicle(licensePlate);

            licensePlate = "DEF123";
            //AddToVehicle(new Motorcycle(125, licensePlate, "Harley-Davidson XL1200", 2013, 2, "Red"));
            AddToVehicleNEW("Motorcykel", 125, licensePlate, "Harley-Davidson XL1200", 2013, 2, "Red");
            DebugVehicle(licensePlate);

            licensePlate = "DEF456";
            AddToVehicle(new Boat(12, licensePlate, "BMW Oracle Racing 90", 2014, 2, "White"));
            AddToVehicleNEW("Båt", 12, licensePlate, "BMW Oracle Racing 90", 2014, 2, "White");
            DebugVehicle(licensePlate);

            licensePlate = "DEF789";
            //AddToVehicle(new Car("Gasoline", licensePlate, "Ferrari SF90 Stradale", 2015, 4, "Yellow"));
            AddToVehicleNEW("Bil", "Gasoline", licensePlate, "Ferrari SF90 Stradale", 2015, 4, "Yellow");
            DebugVehicle(licensePlate);

            licensePlate = "GHI123";
            //AddToVehicle(new Boat(12, licensePlate, "Candela C-8", 2016, 2, "White"));
            AddToVehicleNEW("Båt", 12, licensePlate, "Candela C-8", 2016, 2, "Green");
            DebugVehicle(licensePlate);
        }
        // --------------------------------------------------------------------
        // *** DebugVehicle ***
        // --------------------------------------------------------------------
        public void DebugVehicle(string licensePlate)
        {
            string preFix = "_handler.PopulateVehicle(licensePlate) -> ";
            DebugAndTest.DoDebug(false, preFix + $"Registrerar fordon: {licensePlate}");
        }
    }
}