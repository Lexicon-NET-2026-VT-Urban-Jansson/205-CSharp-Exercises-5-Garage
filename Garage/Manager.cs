using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Garage
{
    // ------------------------------------------------------------------------
    // *** Class Manager ***
    // ------------------------------------------------------------------------
    internal class Manager
    {
        // --- Fields ---------------------------------------------------------
        private const int PREFIXED_LOTS = 6;
        private Handler? _handler;


        // --- Properties -----------------------------------------------------


        // --------------------------------------------------------------------
        // *** Run ***
        // --------------------------------------------------------------------
        public void Run()
        {
            // Run Test?
            if (DebugAndTest.DO_TEST) 
            { 
                // Create a new Garage with PREFIXED_LOTS (number of parking lots)
                DebugAndTest.DoDebug(false, "manager.Run -> Instansierar Handler: _handler = new Handler(PREFIXED_LOTS)");
                _handler = new(PREFIXED_LOTS);
                InitGarageAndPopulateVehicle(_handler);
                DebugAndTest.RunTest(_handler);
            }

            // Run Menu
            Console.WriteLine("Här ska huvumenyn visas...");

            // TODO : The number of parking lots should be set by input from Menu/UI, not PREFIXED_LOTS!
            //_handler = new(????);
            //InitGarageAndPopulateVehicle(_handler);
        }

        // --------------------------------------------------------------------
        // *** InitGarageAndPopulateVehicle ***
        // --------------------------------------------------------------------
        private void InitGarageAndPopulateVehicle(Handler handler)
        {
            // Init new Garage
            DebugAndTest.DoDebug(false, "manager.Run -> Kör: _handler.InitGarage()");
            handler.InitGarage();

            // Populate Vehicles
            DebugAndTest.DoDebug(false, "manager.Run -> Kör: _handler.PopulateVehicle()");
            handler.PopulateVehicle();
        }
    }
}