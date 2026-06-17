using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;


/*
-------------------------------------------------------------------------------
  Modifier  | Description
-------------------------------------------------------------------------------
* public    | The code is accessible for all classes
* private   | The code is only accessible within the same class
* protected | The code is accessible within the same class, 
            | or in a class that is inherited from that class.
* internal  | The code is only accessible within its own assembly, 
            | but not from another assembly.
-------------------------------------------------------------------------------
*/

// ------------------------------------------------------------------------
// *** Class XXX ***
// ------------------------------------------------------------------------

// --- Fields ---------------------------------------------------------
// --- Properties -----------------------------------------------------

// --- Private Methods ------------------------------------------------
// --- Public Methods -------------------------------------------------

// --------------------------------------------------------------------
// *** Method ***
// --------------------------------------------------------------------




namespace Garage
{
    // ------------------------------------------------------------------------
    // *** Class DebugAndTest ***
    // ------------------------------------------------------------------------
    internal class DebugAndTest
    {
        // --- Fields ---------------------------------------------------------
#if DEBUG
        internal const bool DO_DEBUG = true;
#else
        internal const bool DO_DEBUG = false;
#endif
        internal const bool DEBUG_TO_CONSOLE = false;
        internal const bool DO_TEST = false;

        
        // --------------------------------------------------------------------
        // *** StartDebug ***
        // --------------------------------------------------------------------
        internal static void StartDebug()
        {
            if (DO_DEBUG) Console.WriteLine();
            DoDebug(false, "");
            DoDebug(false, "=============== KÖR APP - GARAGE ===============");
        }


        // --------------------------------------------------------------------
        // *** DoDebug ***
        // --------------------------------------------------------------------
        internal static void DoDebug(bool doConsole, string msg)
        {
            // Write all to Console?
            if (DEBUG_TO_CONSOLE) doConsole = true;

            if (DO_DEBUG)
            {
                msg = msg.Trim();
                if (msg != "") msg = "*** " + msg + " ***";

                if (doConsole)
                    Console.WriteLine(msg);
                else
                    Debug.WriteLine(msg);
            }
        }


        // --------------------------------------------------------------------
        // *** AddOrRemoveVehicle ***
        // --------------------------------------------------------------------
        internal static void AddOrRemoveVehicle(string msg, string vehicle, Handler handler, bool addVehicle) 
        {
            int type = addVehicle ? 1 : 2;
            if (msg.Trim() != "") DoDebug(false, msg);
            WriteDebug(type, vehicle);
            DebugVehicle(handler, vehicle, addVehicle);
            DoDebug(false, "");
        }


        // --------------------------------------------------------------------
        // *** DebugVehicle ***
        // --------------------------------------------------------------------
        internal static void DebugVehicle(Handler handler, string vehicle, bool addVehicle)
        {
            bool result;
            string msg;
            if (addVehicle)
            {
                result = handler.AddVehicleToGarage(vehicle);
                msg = result ? "är registrerat i garaget" : "kan inte registreras i garaget";
            }
            else
            {
                result = handler.RemoveVehicleFromGarage(vehicle);
                msg = result ? "har checkats ut från garaget" : "kan inte checkas ut från garaget";
            }
            DoDebug(true, $"Fordon {vehicle} " + msg);
        }


        // --------------------------------------------------------------------
        // *** WriteDebug ***
        // --------------------------------------------------------------------
        internal static void WriteDebug(int type, string vehicle)
        {
            string msg = type switch
            {
                1 => $"manager.Run -> KÖR TEST: _handler.AddVehicleToGarage(\"{vehicle}\") ->",
                2 => $"manager.Run -> KÖR TEST: _handler.RemoveVehicleFromGarage(\"{vehicle}\") ->",
                3 =>  "manager.Run -> KÖR TEST: _handler.ListAllVehiclesInGarage()",
                4 =>  "manager.Run -> KÖR TEST: _handler.ListAllVehicleTypesInGarage()",
                _ => "",
            };
            DoDebug(false, msg);
        }


        // --------------------------------------------------------------------
        // *** List Garage ***
        // --------------------------------------------------------------------
        internal static void ListGarage(int type, string msg, Handler _handler)
        {
            DoDebug(false, msg);
            WriteDebug(type, "");
            
            if (type == 3) 
                _handler.ListAllVehiclesInGarage();
            else if (type == 4)
                _handler.ListAllVehicleTypesInGarage();

            DoDebug(false, "");
            if (type == 3) DoDebug(true, "");
        }


        // --------------------------------------------------------------------
        // *** RunTest ***
        // --------------------------------------------------------------------
        internal static void RunTest(Handler _handler)
        {
            if (_handler is null) return;

            string vehicle;
            string msg;
            string msgTestStart = "=============== KÖR TEST - START ===============";
            string msgTestStopp = "=============== KÖR TEST - STOPP ===============";
            string preFix = "manager.Run -> ";

            if (!DEBUG_TO_CONSOLE) Console.WriteLine(msgTestStart);
            DoDebug(false, "");
            DoDebug(false, msgTestStart);

            // List Garage Test (_currentCount == 0)
            ListGarage(3, preFix + "TESTA ATT LISTA GARAGET OM DET ÄR TOMT... ", _handler);

            // Add To Garage Test
            vehicle = "BUG999";
            msg = preFix + "TESTA ATT REGGA ETT FORDON I GARAGET, SOM INTE FINNS...";
            AddOrRemoveVehicle(msg, vehicle, _handler, true);

            // Add To Garage Test
            vehicle = "ABC123";
            msg = preFix + "TESTA ATT REGGA TVÅ EXISTERANDE FORDON I GARAGET...";
            AddOrRemoveVehicle(msg, vehicle, _handler, true);
            vehicle = "ABC456";
            msg = "";
            AddOrRemoveVehicle(msg, vehicle, _handler, true);

            // Add To Garage Test
            vehicle = "ABC123";
            msg = preFix + "TESTA ATT REGGA ETT FORDON SOM REDAN FINNS I GARAGET...";
            AddOrRemoveVehicle(msg, vehicle, _handler, true);

            // Add To Garage Test
            vehicle = "ABC789";
            msg = preFix + "TESTA ATT REGGA DET TREDJE FORDONET I GARAGET...";
            AddOrRemoveVehicle(msg, vehicle, _handler, true);

            // Add To Garage Test
            vehicle = "DEF456"; // OBS! DETTA GÄLLER BARA OM MAN KÖR MED PREFIXED_LOTS = 3
            msg = preFix + "TESTA ATT REGGA ETT FORDON I GARAGET, NÄR DET ÄR FULLT... (GÄLLER FÖR PREFIXED_LOTS = 3)";
            AddOrRemoveVehicle(msg, vehicle, _handler, true);

            // List Garage Test
            ListGarage(3, preFix + "TESTA ATT LISTA GARAGET...", _handler);

            // Remove From Garage Test
            vehicle = "ABC456";
            msg = preFix + "TESTA ATT TA BORT ETT FORDON I GARAGET...";
            AddOrRemoveVehicle(msg, vehicle, _handler, false);

            // List Garage Test
            ListGarage(3, preFix + "TESTA ATT LISTA GARAGET...", _handler);

            // Add To Garage Test
            vehicle = "DEF789";
            msg = preFix + "TESTA ATT REGGA NYTT FORDONET I GARAGET...";
            AddOrRemoveVehicle(msg, vehicle, _handler, true);

            // List Garage Test
            ListGarage(3, preFix + "TESTA ATT LISTA GARAGET...", _handler);

            // Add To Garage Test
            vehicle = "GHI123";
            msg = preFix + "TESTA ATT REGGA NYTT FORDONET I GARAGET...";
            AddOrRemoveVehicle(msg, vehicle, _handler, true);

            // List Garage Test
            ListGarage(3, preFix + "TESTA ATT LISTA GARAGET...", _handler);

            // List Vehicle Types Test
            ListGarage(4, preFix + "TESTA ATT LISTA FORDONSTYPER...", _handler);

            // TESTA LINQ!
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine(" TESTAR LINQ!");
            Console.WriteLine("-----------------------------------------");
            _handler.FindVehicleInGarageWithLINQ();
            Console.WriteLine("-----------------------------------------");
            

            // ENDAST FÖR TEST AV GETENUMERATOR() !
            _handler.TestGetEnumerator();


            DoDebug(false, msgTestStopp);
            DoDebug(false, "");
            if (!DEBUG_TO_CONSOLE)
            {
                Console.WriteLine();
                Console.WriteLine(msgTestStopp);
                Console.WriteLine();
            } 

            // Clear Test
            Console.WriteLine("Tryck [Enter] för att avsluta Test.");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine();
        }
    }
}