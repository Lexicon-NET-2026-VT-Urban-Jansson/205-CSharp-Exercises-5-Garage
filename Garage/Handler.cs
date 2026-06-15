using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        private MyGarage<Vehicle>? _garage;

        // --- Properties -----------------------------------------------------
        public int NumberOfParkingLots { get; } = parkingLots;



        // --------------------------------------------------------------------
        // *** FixLicensePlate ***
        // --------------------------------------------------------------------
        private string FixLicensePlate(string lp)
        {
            return lp.Trim().ToUpper();
        }


        // --------------------------------------------------------------------
        // *** InitGarage ***
        // --------------------------------------------------------------------
        public void InitGarage()
        {
            string preFix = "_handler.InitGarage -> ";
            DebugAndTest.DoDebug(false, preFix + $"Sätter NumberOfParkingLots till {NumberOfParkingLots}");
            DebugAndTest.DoDebug(false, preFix + "Instansierar Garage: _garage = new MyGarage<Vehicle>(NumberOfParkingLots)");

            // Create a new Garage with a number of parking lots
            _garage = new MyGarage<Vehicle>(NumberOfParkingLots);

            DebugAndTest.DoDebug(false, preFix + $"Nya garaget har {_garage.NumberOfParkingLots} parkeringsplatser");
        }


        // --------------------------------------------------------------------
        // *** FindVehicleInGarage ***
        // --------------------------------------------------------------------
        public bool FindVehicleInGarage(string licensePlate)
        {
            licensePlate = FixLicensePlate(licensePlate);
            bool result = _garage!.FindVehicle(licensePlate);

            string preFix = "_handler.FindVehicleInGarage(licensePlate) -> ";
            if (!result) DebugAndTest.DoDebug(false, preFix + $"Fordon {licensePlate} finns inte i garaget");
            
            return result;
        }


        // --------------------------------------------------------------------
        // *** FindVehicleInGarageLINQ ***
        // --------------------------------------------------------------------
        public void FindVehicleInGarageLINQ()
        {
            if (_garage is null) return;
            _garage.FindVehicleLINQ();
        }


        // --------------------------------------------------------------------
        // *** AddVehicleToGarage ***
        // --------------------------------------------------------------------
        public bool AddVehicleToGarage(string licensePlate)
        {
            licensePlate = FixLicensePlate(licensePlate);

            bool result = false;
            string preFix = "_handler.AddVehicleToGarage(licensePlate) -> ";
            
            // Check if _garage is null
            if (_garage is null)
            {
                DebugAndTest.DoDebug(false, preFix + "ERROR! _garage är null");
                return result;
            }
            // Check if _vehicles is null
            if (_vehicles is null)
            {
                DebugAndTest.DoDebug(false, preFix + "ERROR! _vehicles är null");
                return result;
            }
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
            licensePlate = FixLicensePlate(licensePlate);
            bool result = _garage!.RemoveFromGarage(licensePlate);

            string msg = $"_handler.RemoveVehicleFromGarage(licensePlate) -> Fordon {licensePlate} ";
            msg += result ? "har av avregistrerats från garaget" : "kan inte avregistreras från garaget";
            DebugAndTest.DoDebug(false, msg);

            return result;
        }


        // --------------------------------------------------------------------
        // *** VehicleExist ***
        // --------------------------------------------------------------------
        public Vehicle? VehicleExist(string licensePlate)
        {
            licensePlate = FixLicensePlate(licensePlate);
            return _vehicles!.Find(x => x.LicensePlate == licensePlate);
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
            AddToVehicle(new Car("Diesel", licensePlate, "Volvo XC90", 2010, 4, "Black"));
            DebugVehicle(licensePlate);

            licensePlate = "ABC456";
            AddToVehicle(new Airplane(2, licensePlate, "Cessna 404 Titan II", 2011, 2, "Blue"));
            DebugVehicle(licensePlate);

            licensePlate = "ABC789";
            AddToVehicle(new Bus(22, licensePlate, "Scania OmniExpress", 2012, 4, "Green"));
            DebugVehicle(licensePlate);

            licensePlate = "DEF123";
            AddToVehicle(new Motorcycle(125, licensePlate, "Harley-Davidson XL1200", 2013, 2, "Red"));
            DebugVehicle(licensePlate);

            licensePlate = "DEF456";
            AddToVehicle(new Boat(12, licensePlate, "BMW Oracle Racing 90", 2014, 2, "White"));
            DebugVehicle(licensePlate);

            licensePlate = "DEF789";
            AddToVehicle(new Car("Gasoline", licensePlate, "Ferrari SF90 Stradale", 2015, 4, "Yellow"));
            DebugVehicle(licensePlate);

            licensePlate = "GHI123";
            AddToVehicle(new Boat(12, licensePlate, "Candela C-8", 2016, 2, "White"));
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