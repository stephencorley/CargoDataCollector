using CargoDataCollector.Services;
using System;
using System.IO;

namespace CargoDataCollector
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Cargo Data Collector");
            
            //settings of application
            //Note: ISettings and specific settings class can be used if useCases involved multiple settings
            //like connection strings of database or broker
            var inputPath = Path.Combine(Environment.CurrentDirectory, "Data");
            var outputPath = Path.Combine(Environment.CurrentDirectory, "Output.json");
            new ServiceRunner(inputPath, outputPath).Process();
            Console.ReadLine();
        }
    }
}
