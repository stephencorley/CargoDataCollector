using CargoDataCollector.Models;
using CargoDataCollector.Services.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace CargoDataCollector.Services
{
    /// <summary>
    /// IServiceRunner can be created, if use cases involved multiple implementations of ServiceRunner 
    /// like data source is database or broker etc.
    /// </summary>
    public class ServiceRunner
    {
        private readonly string _inputPath;
        private readonly string _outputPath;

        public ServiceRunner(string inputPath, string outpuPath)
        {
            _outputPath = outpuPath;
            _inputPath = inputPath;
        }
        public void SerializeResponseData(List<ResponeModel> responeModels)
        {
            if (responeModels.Count > 0)
            {
                Console.WriteLine($"Writing {responeModels.Count} records.");
                var responseJson = JsonConvert.SerializeObject(responeModels, Formatting.Indented);
                File.WriteAllText(_outputPath, responseJson);
                Console.WriteLine($"Process completed successfully. Find processed record in file: {_outputPath}.{Environment.NewLine}Press any key to exit.");
            }
            else
            {
                Console.WriteLine("No records processed.");
            }
        }
        
        public List<ResponeModel> DeserializeData(string data)
        {
            List<ResponeModel> responeModels = new List<ResponeModel>();

            if (!IsValidJson(data))
                throw new FormatException("Incorrect data format.");

            Console.WriteLine($"Devices data found from file.");
            var settings = new JsonSerializerSettings
            {
                Converters = { new VendorConverter() },
            };

            // Deserialize the JSON
            IVendor vendor = JsonConvert.DeserializeObject<IVendor>(data, settings);
            var results = vendor.PopulateResults();
            Console.WriteLine($"Devices data successfully processed.");
            responeModels.AddRange(results);

            return responeModels;
        }
        public void Process()
        {
            Console.WriteLine($"Checking input file path: {_inputPath}");
            if (Directory.Exists(_inputPath))
            {
                try
                {
                    List<ResponeModel> responses = new List<ResponeModel>();

                    Console.WriteLine("Path exists. Getting Devices data.");
                    var files = Directory.GetFiles(_inputPath);
                    foreach (var file in files)
                    {
                        try
                        {
                            var data = File.ReadAllText(file);
                            responses.AddRange(DeserializeData(data));
                        }
                        catch (FormatException)
                        { Console.WriteLine("Incorrect data format."); }
                        catch (Exception)
                        { Console.WriteLine("Something went wrong."); }
                    }
                    SerializeResponseData(responses);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Process failed: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Path not found, please input valid file path: {_inputPath}");
                throw new FileNotFoundException("Invalid path.");
            }
        }
        private bool IsValidJson(string text)
        {
            try
            {
                var json = JContainer.Parse(text);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
