using System.Diagnostics;

namespace Garage
{
    // ------------------------------------------------------------------------
    // *** Class Program ***
    // ------------------------------------------------------------------------
    internal class Program
    {
        // --------------------------------------------------------------------
        // *** Main ***
        // --------------------------------------------------------------------
        static void Main()
        {
            // Start Debug
            DebugAndTest.StartDebug();

            // Instantiate Manager
            DebugAndTest.DoDebug(false, "Program -> Instansierar Manager: manager = new()");
            Manager manager = new();

            // RUN Application
            DebugAndTest.DoDebug(false, "Program -> Kör programmet: manager.Run()");
            manager.Run();

            // END Application
            Console.WriteLine(); ;
            Console.WriteLine("Tryck [Enter] för att avsluta.");
            Console.ReadLine();
        }
    }
}
