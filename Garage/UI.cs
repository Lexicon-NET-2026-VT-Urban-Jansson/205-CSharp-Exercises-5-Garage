using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Console.WriteLine(" -------------------------------------------");
//Console.WriteLine("  *** xxxxmeny ***");
//Console.WriteLine(" -------------------------------------------");
//Console.WriteLine(" (Välj genom att skriv in en siffra)" + CRLF);

//Console.WriteLine(" * Menyval 1: Ange antal parkeringsplatser i garaget");
//Console.WriteLine(" * Menyval 2: xxxx");
//Console.WriteLine(" * Menyval 3: xxxx");
//Console.WriteLine(" * Menyval 0: Avsluta");
//Console.WriteLine(" -------------------------------------------");


namespace Garage
{
    // ------------------------------------------------------------------------
    // *** class UI ***
    // ------------------------------------------------------------------------
    internal class UI
    {
        // --- Fields ---------------------------------------------------------
        private const string CRLF = "\r\n"; // <= För Windows (Använd Environment.NewLine för Linux etc)
        private const string MENU_SELECT_NUMBER = " (Välj genom att skriva in en siffra)" + CRLF;
        private const string MENU_SELECT = " Ange det ditt val: ";
        private const string MENU_SELECT_PARKING_LOTS = " Antal parkeringsplatser: ";

        
        // --- Properties -----------------------------------------------------


        // --- Private Methods ------------------------------------------------
        //
        // --------------------------------------------------------------------
        // *** MenuCaption ***
        // --------------------------------------------------------------------
        private static void WriteMenuCaption()
        {
            // Rensa skärmen (samma som CLS)
            Console.Clear();

            Console.WriteLine(" *******************************************");
            Console.WriteLine(" *                                         *");
            Console.WriteLine(" *      Välkommen till Joe's Garage!       *");
            Console.WriteLine(" *         trevlig dag i garaget           *");
            Console.WriteLine(" *                                         *");
            Console.WriteLine(" *        Gör dina val från menyn!         *");
            Console.WriteLine(" *                                         *");
            Console.WriteLine(" *******************************************" + CRLF);
        }
        // --------------------------------------------------------------------
        // *** MenuCaption ***
        // --------------------------------------------------------------------
        private static void WriteSubMenu(string msg, string select)
        {
            Console.WriteLine(" -------------------------------------------");
            Console.WriteLine($"  *** {msg} ***");
            Console.WriteLine(" -------------------------------------------");
            Console.WriteLine($" {select}"); // + CRLF);
        }
        // --------------------------------------------------------------------
        // *** GetInputNumber ***
        // --------------------------------------------------------------------
        private static int GetInputNumber(string msg)
        {
            Console.Write(msg);
            string val = Console.ReadLine()!;
            val = val == null ? "" : val.Trim();

            int i = int.TryParse(val, out i) ? i : -1;
            return i;
        }
        // --------------------------------------------------------------------
        // *** GetMenuNumber ***
        // --------------------------------------------------------------------
        private static int GetMenuNumber(string msg, int maxNumber)
        {
            int inputNumber = -1;
            while (inputNumber < 0 || inputNumber > maxNumber)
            {
                inputNumber = GetInputNumber(msg);
                if (inputNumber < 0 || inputNumber > maxNumber)
                    msg = " Ogiltigt värde. Var god ange en siffra: ";

            }
            return inputNumber;
        }


        // --- Public Methods -------------------------------------------------
        //
        // --------------------------------------------------------------------
        // *** ShowMenuInit ***
        // --------------------------------------------------------------------
        public static int ShowMenuInit()
        {
            WriteMenuCaption();
            WriteSubMenu("Initiering av garaget", MENU_SELECT_NUMBER);
            Console.WriteLine(" * Menyval 1: Ange antal parkeringsplatser");
            Console.WriteLine(" * Menyval 0: Avsluta");
            Console.WriteLine(" -------------------------------------------");
         
            if (GetMenuNumber(MENU_SELECT, 1) == 0) return 0;
            
            return GetMenuNumber(MENU_SELECT_PARKING_LOTS, 1000);
        }


        // --------------------------------------------------------------------
        // *** ShowMenuMain ***
        // --------------------------------------------------------------------
        public static int ShowMenuMain()
        {
            WriteMenuCaption();
            WriteSubMenu("Huvudmeny i garaget", MENU_SELECT_NUMBER);
            Console.WriteLine(" * Menyval 1: Lista samtliga parkerade fordon");
            Console.WriteLine(" * Menyval 2: Lista alla fordonstyper i garaget");
            Console.WriteLine(" * Menyval 3: Parkera ett fordon i garaget");
            Console.WriteLine(" * Menyval 4: Checka ut ett fordon från garaget");
            Console.WriteLine(" * Menyval 5: Sök ett specifikt fordon i garaget");
            Console.WriteLine(" * Menyval 6: Sök efter speciella fordonegenskaper");

            Console.WriteLine(" * Menyval 0: Avsluta");
            Console.WriteLine(" -------------------------------------------");
            return GetMenuNumber(MENU_SELECT, 6);
        }

        // --------------------------------------------------------------------
        // *** ShowMenuVehicle ***
        // --------------------------------------------------------------------
        public static int ShowMenuVehicle()
        {
            WriteMenuCaption();
            WriteSubMenu("Ange fordonstyp", MENU_SELECT_NUMBER);
            Console.WriteLine(" * Menyval 1: Bil");
            Console.WriteLine(" * Menyval 2: Motorcykel");
            Console.WriteLine(" * Menyval 3: Buss");
            Console.WriteLine(" * Menyval 4: Flygplan");
            Console.WriteLine(" * Menyval 5: Båt");
            Console.WriteLine(" * Menyval 0: Avsluta");
            Console.WriteLine(" -------------------------------------------");

            return GetMenuNumber(MENU_SELECT, 5);

            //if (GetMenuNumber(MENU_SELECT, 5) == 0) return 0;

            //return GetMenuNumber(MENU_SELECT_PARKING_LOTS, 1000);
        }

        // --------------------------------------------------------------------
        // *** ShowMenuExit ***
        // --------------------------------------------------------------------
        public static void ShowMenuExit()
        {
            WriteMenuCaption();
            WriteSubMenu("Tack för besöket & välkommen åter!", "");
            //Console.ReadLine()!;
        }
    }
}