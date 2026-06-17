using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
//using static System.Net.Mime.MediaTypeNames;

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

                // TODO-DONE: The number of parking lots should be set by input from Menu/UI, not PREFIXED_LOTS!
                //_handler = new(????);
                //InitGarageAndPopulateVehicle(_handler);

                // TESTA!
                //DebugAndTest.RunTest(_handler);
            }

            // Run - ShowMenuInit
            int menuResult = UI.ShowMenuInit();
            if (menuResult == 0) EndApplication();

            // Instantiate Handler & Garage, and populate Vehicle
            _handler = new(menuResult);
            InitGarageAndPopulateVehicle(_handler);

            // Run - ShowMenuMain
            menuResult = UI.ShowMenuMain();
            if (menuResult == 0) EndApplication();

            // Run - MAIN MENU LOPP
            while (menuResult != 0)
            {
                switch (menuResult)
                {
                    case 1:
                        Console.Clear();
                        _handler.ListAllVehiclesInGarage();
                        ContinueMainLopp();
                        break;

                    case 2:
                        Console.Clear();
                        _handler.ListAllVehicleTypesInGarage();
                        ContinueMainLopp();
                        break;

                    case 3:
                        //Console.Clear();
                        UI.ShowMenuVehicle();
                        ContinueMainLopp();
                        break;

                    case 6:
                        Console.Clear();
                        _handler.FindVehicleInGarageWithLINQ();
                        ContinueMainLopp();
                        break;

                    default:
                        break;
                }
                menuResult = UI.ShowMenuMain();
            }

            EndApplication();
        }

        // --------------------------------------------------------------------
        // *** ContinueMainLopp ***
        // --------------------------------------------------------------------
        private void ContinueMainLopp()
        {
            Console.WriteLine("Tryck [Enter] för att fortsätta.");
            Console.ReadLine();
        }

        // --------------------------------------------------------------------
        // *** End ***
        // --------------------------------------------------------------------
        private void EndApplication()
        {
            // END Application
            UI.ShowMenuExit();
            Console.WriteLine(); ;
            Console.WriteLine("Tryck [Enter] för att avsluta.");
            Console.ReadLine();
            Environment.Exit(0);
        }


        // --------------------------------------------------------------------
        // *** InitGarageAndPopulateVehicle ***
        // --------------------------------------------------------------------
        private void InitGarageAndPopulateVehicle(Handler handler)
        {
            // TODO DONE: REDESIGN - DETTA ÄR FEL!
            // Init new Garage
            //DebugAndTest.DoDebug(false, "manager.Run -> Kör: _handler.InitGarage()");
            //handler.InitGarage();

            // Populate Vehicles
            DebugAndTest.DoDebug(false, "manager.Run -> Kör: _handler.PopulateVehicle()");
            handler.PopulateVehicle();

            // JUST FOR TEST!
            handler.AddVehicleToGarage("ABC123");
            handler.AddVehicleToGarage("ABC456");
            handler.AddVehicleToGarage("ABC789");
            handler.AddVehicleToGarage("DEF123");
            handler.AddVehicleToGarage("DEF456");
            handler.AddVehicleToGarage("DEF789");
            handler.AddVehicleToGarage("GHI123");
        }
    }
}