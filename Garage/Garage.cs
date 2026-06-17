using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Security;
using System.Text;

namespace Garage
{
    // ------------------------------------------------------------------------
    // *** Class Garage ***
    // ------------------------------------------------------------------------
    internal class MyGarage<T>(int numberOfParkingLots) : IEnumerable<T> where T : Vehicle
    {
        // --- Fields ---------------------------------------------------------
        private readonly T[] _parkingLots = new T[numberOfParkingLots];
        private int _currentCount = 0;

        // --- Properties -----------------------------------------------------
        // TODO DONE: REDESIGN - DETTA ÄR FEL!
        //public int NumberOfParkingLots { get; } = numberOfParkingLots;


        // --- Private Methods ------------------------------------------------
        //
        // --------------------------------------------------------------------
        // *** FixLicensePlate ***
        // --------------------------------------------------------------------
        private string FixLicensePlate(string licensePlate)
        {
            return licensePlate.Trim().ToUpper();
        }
        // --------------------------------------------------------------------
        // *** GetVehicleIndex ***
        // --------------------------------------------------------------------
        private int GetVehicleIndex(string licensePlate)
        {
            for (int i = 0; i < _parkingLots.Length; i++)
                if (_parkingLots[i] != null && _parkingLots[i]?.LicensePlate == licensePlate) return i;
            
            return -1;
        }
        // --------------------------------------------------------------------
        // *** GetFreeParkingLotIndex ***
        // --------------------------------------------------------------------
        private int GetFreeParkingLotIndex()
        {
            for (int i = 0; i < _parkingLots.Length; i++)
                if (_parkingLots[i] == null) return i;

            return -1;
        }
        // --------------------------------------------------------------------
        // *** GetVehicleType ***
        // --------------------------------------------------------------------
        private string GetVehicleType(T vehicle)
        {
            return vehicle switch
            {
                Airplane => "Flygplan",
                Motorcycle => "Motorcykel",
                Car => "Bil",
                Bus => "Buss",
                Boat => "Båt",
                _ => "-",
            };
        }



        // --- Public Methods -------------------------------------------------
        //
        // --------------------------------------------------------------------
        // *** GarageIsFull ***
        // --------------------------------------------------------------------
        public bool GarageIsFull()
        {
            return _currentCount >= _parkingLots.Length;
        }


        // --------------------------------------------------------------------
        // *** FindVehicle ***
        // --------------------------------------------------------------------
        public bool FindVehicle(string licensePlate)
        {
            if (_currentCount == 0) return false;
            
            licensePlate = FixLicensePlate(licensePlate);
            foreach (T vehicle in _parkingLots) 
                if (vehicle != null && vehicle.LicensePlate == licensePlate) return true;
            
            return false;
        }


        // --------------------------------------------------------------------
        // *** FindVehiclesWithLINQ ***
        // --------------------------------------------------------------------
        public void FindVehiclesWithLINQ()
        {
            //var parkingLots = _parkingLots
            //.Where(item => item?.NumberOfWheels == 2);
            //.Select(item => item?.Model);

            // TODO: REDESIGN - DETTA ÄR FEL! = SKA VARA HELT VALFRIA URVAL!
            // ENDAST FÖR TEST!
            //var parkingLots = _parkingLots
            //    .Where(item => (
            //        (
            //        (item?.LicensePlate == "ABC123") ||
            //        (item?.LicensePlate == "DEF789") ||
            //        (item?.LicensePlate == "DEF456") ||
            //        (item?.LicensePlate == "GHI123")
            //        )
            //        &&
            //        (item?.NumberOfWheels == 2)
            //        ));

            var parkingLots = _parkingLots
                .Where(item => (item?.NumberOfWheels == 2));


            // TODO: REDESIGN - DETTA ÄR FEL! = ALLA UTSKRIFTER I UI!
            // ENDAST FÖR TEST!
            string oneLine = "------------------------------------------";
            Console.WriteLine(oneLine);
            Console.WriteLine(" Test av LINQ – Alla fordon med två hjul");
            Console.WriteLine(oneLine);

            foreach (T vehicle in parkingLots)
                if (vehicle != null) Console.WriteLine($" {vehicle.LicensePlate}: {vehicle.Model}");
        
            Console.WriteLine(oneLine);
        }


        // --------------------------------------------------------------------
        // *** AddToGarage ***
        // --------------------------------------------------------------------
        public bool AddToGarage(T vehicle)
        {
            bool result = false;
            if (!GarageIsFull() && !FindVehicle(vehicle.LicensePlate))
            {
                // TODO-DONE: FIXA EN FOR-LOOP!! DETTA KOMMER INTE FUNKA!
                //_parkingLots[_currentCount] = vehicle;
                
                int FirstFreeLot = GetFreeParkingLotIndex();
                if (FirstFreeLot > -1) 
                { 
                    _parkingLots[FirstFreeLot] = vehicle;
                    _currentCount++;
                    result = true;
                }
            }
            return result;
        }


        // --------------------------------------------------------------------
        // *** RemoveFromGarage ***
        // --------------------------------------------------------------------
        public bool RemoveFromGarage(string licensePlate)
        {
            bool result = false;
            licensePlate = FixLicensePlate(licensePlate);

            int vehicleIndex = GetVehicleIndex(licensePlate);
            if (vehicleIndex > -1) 
            {
                // TODO DONE: REDESIGN - DETTA ÄR FEL!
                //_parkingLots[vehicleIndex] = null;

                _parkingLots[vehicleIndex] = default!;
                _currentCount--;
                result = true;
            }
            return result;
        }


        // --------------------------------------------------------------------
        // *** ListAllVehicles ***
        // --------------------------------------------------------------------
        public void ListAllVehicles() 
        {
            // TODO: REDESIGN - DETTA ÄR FEL! = ALLA UTSKRIFTER I UI!
            string oneLine = "-----------------------------------------";
            //Console.WriteLine();
            Console.WriteLine(oneLine);
            Console.WriteLine(" Registrerade fordon i garaget");
            Console.WriteLine(oneLine);

            if (_currentCount == 0)
                Console.WriteLine(" Garaget är tomt!");
            else
            {
                string vehicleType;
                foreach (var vehicle in _parkingLots)
                {
                    if (vehicle != null)
                    {
                        vehicleType = GetVehicleType(vehicle);
                        Console.WriteLine($" {vehicle.LicensePlate}: {vehicleType} - {vehicle.Model}");
                    }
                }

                // TODO DONE: REDESIGN - DETTA ÄR FEL!
                //foreach (var vehicle in _parkingLots)
                //{
                //    if (vehicle != null)
                //    {
                //        vehicleType = GetVehicleType(vehicle);
                //        Console.WriteLine($" {vehicle.LicensePlate}: {vehicleType} - {vehicle.Model}");
                //    }
                //}
            }
            Console.WriteLine(oneLine);
        }


        // --------------------------------------------------------------------
        // *** ListAllVehicleTypes ***
        // --------------------------------------------------------------------
        public void ListAllVehicleTypes()
        {
            Dictionary<string, int> types = [];

            string vehicleType;
            foreach (var vehicle in _parkingLots)
            {
                if (vehicle != null)
                {
                    vehicleType = GetVehicleType(vehicle);
                    if (types.ContainsKey(vehicleType))
                        types[vehicleType]++;
                    else
                        types.Add(vehicleType, 1);
                }

                // TODO DONE: REDESIGN - DETTA ÄR FEL!
                //if (vehicle != null)
                //{
                //    vehicleType = GetVehicleType(vehicle);
                //    if (types.ContainsKey(vehicleType))
                //        types[vehicleType]++;
                //    else
                //        types.Add(vehicleType, 1);
                //}
            }

            // TODO: REDESIGN - DETTA ÄR FEL! = ALLA UTSKRIFTER I UI!
            string oneLine = "-----------------------------------------";
            //Console.WriteLine();
            Console.WriteLine(oneLine);
            Console.WriteLine(" Registrerade fordonstyper i garaget");
            Console.WriteLine(oneLine);

            foreach (var type in types)
                Console.WriteLine($" {type.Value} st - {type.Key}");

            Console.WriteLine(oneLine);
        }


        // --------------------------------------------------------------------
        // *** GetEnumerator ***
        // --------------------------------------------------------------------
        //
        // Lånat 'lite' från NETGame! 😉
        //
        // TODO: Check GetEnumerator()
        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in _parkingLots) if (item != null) yield return item; 
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        // --------------------------------------------------------------------
        // *** TestGetEnumerator ***
        // --------------------------------------------------------------------
        public void TestGetEnumerator()
        {
            // TODO: ENDAST FÖR TEST AV GETENUMERATOR() 
            DebugAndTest.DoDebug(true, "");
            DebugAndTest.DoDebug(true, "==================================================");
            DebugAndTest.DoDebug(true, " KÖR TEST: _garage.TestGetEnumerator()");
            DebugAndTest.DoDebug(true, " TESTAR: foreach (var item in _parkingLots)");
            DebugAndTest.DoDebug(true, "==================================================");

            foreach (var item in _parkingLots)
            {
                if (item != null)
                    DebugAndTest.DoDebug(true, " TestGetEnumerator() Funkar!");
                else
                    DebugAndTest.DoDebug(true, " ERROR! item == null");
            }

            DebugAndTest.DoDebug(true, "==================================================");
        }
    }
}