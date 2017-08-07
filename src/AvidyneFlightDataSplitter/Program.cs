using System;
using static System.Console;

namespace MitchelSellers.AvidyneFlightDataSplitter
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            WriteLine("Avidyne Data File Splitter");
            WriteLine("All generated files will be created in the /Output folder");
            try
            {
                //Create the processer
                var splitter = new AvidyneDataFileSplitter("AvidyneLog.csv");
                splitter.ProcessDataFile();
            }
            catch (Exception ex)
            {
                WriteLine($"ERROR!: {ex.Message}");
            }
            WriteLine("Press any key to exit");
            ReadLine();
        }
    }
}
