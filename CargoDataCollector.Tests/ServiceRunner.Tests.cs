using CargoDataCollector.Services;
using NUnit.Framework;
using System;
using System.IO;

namespace CargoDataCollector.Tests
{
    public class ServiceRunnerTests
    {
        private string _input;
        private string _output;
        private ServiceRunner _serviceRunner;
        private string _partnerMockData = "{ \"PartnerId\": 1,   \"PartnerName\": \"Foo1\",   \"Trackers\": [     {       \"Id\": 1,       \"Model\": \"ABC-100\",       \"ShipmentStartDtm\": \"08-17-2020 10:30:00\",       \"Sensors\": [         {           \"Id\": 100,           \"Name\": \"Temperature\",           \"Crumbs\": [             {               \"CreatedDtm\": \"08-17-2020 10:35:00\",               \"Value\": 22.15             },             {               \"CreatedDtm\": \"08-17-2020 10:40:00\",               \"Value\": 23.15             },             {               \"CreatedDtm\": \"08-17-2020 10:45:00\",               \"Value\": 24.15             }           ]         },         {           \"Id\": 101,           \"Name\": \"Humidty\",           \"Crumbs\": [             {               \"CreatedDtm\": \"08-17-2020 10:35:00\",               \"Value\": 80.5             },             {               \"CreatedDtm\": \"08-17-2020 10:40:00\",               \"Value\": 81.5             },             {               \"CreatedDtm\": \"08-17-2020 10:45:00\",               \"Value\": 82.5             }           ]         }       ]     },     {       \"Id\": 2,       \"Model\": \"ABC-200\",       \"ShipmentStartDtm\": \"08-18-2020 10:30:00\",       \"Sensors\": [         {           \"Id\": 200,           \"Name\": \"Temperature\",           \"Crumbs\": [             {               \"CreatedDtm\": \"08-18-2020 10:35:00\",               \"Value\": 23.15             },             {               \"CreatedDtm\": \"08-18-2020 10:40:00\",               \"Value\": 24.15             },             {               \"CreatedDtm\": \"08-18-2020 10:45:00\",               \"Value\": 25.15             }           ]         },         {           \"Id\": 201,           \"Name\": \"Humidty\",           \"Crumbs\": [             {               \"CreatedDtm\": \"08-18-2020 10:35:00\",               \"Value\": 81.5             },             {               \"CreatedDtm\": \"08-18-2020 10:40:00\",               \"Value\": 82.5             },             {               \"CreatedDtm\": \"08-18-2020 10:45:00\",               \"Value\": 83.5             }           ]         }       ]     }   ] } ";
        private string _companyMockData = "{   \"CompanyId\": 2,   \"Company\": \"Foo2\",   \"Devices\": [     {       \"DeviceID\": 1,       \"Name\": \"XYZ-100\",       \"StartDateTime\": \"08-18-2020 10:30:00\",       \"SensorData\": [         {           \"SensorType\": \"TEMP\",           \"DateTime\": \"08-18-2020 10:35:00\",           \"Value\": 32.15         },         {           \"SensorType\": \"TEMP\",           \"DateTime\": \"08-18-2020 10:40:00\",           \"Value\": 33.15         },         {           \"SensorType\": \"TEMP\",           \"DateTime\": \"08-18-2020 10:45:00\",           \"Value\": 34.15         },         {           \"SensorType\": \"HUM\",           \"DateTime\": \"08-18-2020 10:35:00\",           \"Value\": 90.5         },         {           \"SensorType\": \"HUM\",           \"DateTime\": \"08-18-2020 10:40:00\",           \"Value\": 91.5         },         {           \"SensorType\": \"HUM\",           \"DateTime\": \"08-18-2020 10:45:00\",           \"Value\": 92.5         }       ]     },     {       \"DeviceID\": 2,       \"Name\": \"XYZ-200\",       \"StartDateTime\": \"08-19-2020 10:30:00\",       \"SensorData\": [         {           \"SensorType\": \"TEMP\",           \"DateTime\": \"08-19-2020 10:35:00\",           \"Value\": 42.15         },         {           \"SensorType\": \"TEMP\",           \"DateTime\": \"08-19-2020 10:40:00\",           \"Value\": 43.15         },         {           \"SensorType\": \"TEMP\",           \"DateTime\": \"08-19-2020 10:45:00\",           \"Value\": 44.15         },         {           \"SensorType\": \"HUM\",           \"DateTime\": \"08-19-2020 10:35:00\",           \"Value\": 91.5         },         {           \"SensorType\": \"HUM\",           \"DateTime\": \"08-19-2020 10:40:00\",           \"Value\": 92.5         },         {           \"SensorType\": \"HUM\",           \"DateTime\": \"08-19-2020 10:45:00\",           \"Value\": 93.5         }       ]     }   ] } ";
        
        [SetUp]
        public void Setup()
        {
            _input = Path.Combine(Environment.CurrentDirectory, "Data");
            _output = Path.Combine(Environment.CurrentDirectory, "Output.json");
            CreateMockData();
            _serviceRunner = new ServiceRunner(_input, _output);
        }

        [Test]        
        public void ProcessShouldGenerateOutputFile()
        {
            _serviceRunner.Process();
            Assert.IsTrue(File.Exists(_output));
        }

        [Test]
        public void ProcessShouldThrowsExceptionOnInvalidPath()
        {
            //Initialization with invalid path
            _serviceRunner = new ServiceRunner(Path.Combine(_input,"test"), _output);
            
            //ServiceRunner should throws exception
            Assert.Throws<FileNotFoundException>(() => _serviceRunner.Process());
        }

        //TODO: write test case for this
        [Test]
        [TestCase("This is simple text")]
        [TestCase("{\"name\": \"Joe\", \"age\": null]")]
        [TestCase("{\"name\": \"Joe\", \"age\": }")]
        [TestCase("{{}}")]
        public void DeserializeDataShouldThrowExceptionOnInvalidJsonData(string data)
        {            
            //ServiceRunner should throws exception
            Assert.Throws<FormatException>(() => _serviceRunner.DeserializeData(data));
        }


        [Test]
        [TestCase("{\"PartnerId\": 1,   \"PartnerName\": \"Foo1\"}")]
        [TestCase("{\"CompanyId\": 2,   \"Company\": \"Foo2\"}")]
        [TestCase("{\"PartnerId\": 1,   \"PartnerName\": \"Foo1\",   \"Trackers\": [{}]}")]
        [TestCase("{\"CompanyId\": 2,   \"Company\": \"Foo2\",   \"Devices\": [{}]}")]
        [TestCase("{\"CompanyId\": 2,   \"Company\": \"Foo2\",   \"Devices\": [     {       \"DeviceID\": 1,       \"Name\": \"XYZ-100\",       \"StartDateTime\": \"08-18-2020 10:30:00\",       \"SensorData\": [         {           \"SensorType\": \"TEMP\",           \"DateTime\": \"08-18-2020 10:35:00\",           \"Value\": 32.15         },         {           \"SensorType\": \"TEMP\",           \"DateTime\": \"08-18-2020 10:40:00\",           \"Value\": 33.15         },         {           \"SensorType\": \"TEMP\",           \"DateTime\": \"08-18-2020 10:45:00\",           \"Value\": 34.15         },         {           \"SensorType\": \"HUM\",           \"DateTime\": \"08-18-2020 10:35:00\",           \"Value\": 90.5         },         {           \"SensorType\": \"HUM\",           \"DateTime\": \"08-18-2020 10:40:00\",           \"Value\": 91.5         },         {           \"SensorType\": \"HUM\",           \"DateTime\": \"08-18-2020 10:45:00\",           \"Value\": 92.5         }       ]     },     {       \"DeviceID\": 2,       \"Name\": \"XYZ-200\",       \"StartDateTime\": \"08-19-2020 10:30:00\",       \"SensorData\": [         {           \"SensorType\": \"TEMP\",           \"DateTime\": \"08-19-2020 10:35:00\",           \"Value\": 42.15         },         {           \"SensorType\": \"TEMP\",           \"DateTime\": \"08-19-2020 10:40:00\",           \"Value\": 43.15         },         {           \"SensorType\": \"TEMP\",           \"DateTime\": \"08-19-2020 10:45:00\",           \"Value\": 44.15         },         {           \"SensorType\": \"HUM\",           \"DateTime\": \"08-19-2020 10:35:00\",           \"Value\": 91.5         },         {           \"SensorType\": \"HUM\",           \"DateTime\": \"08-19-2020 10:40:00\",           \"Value\": 92.5         },         {           \"SensorType\": \"HUM\",           \"DateTime\": \"08-19-2020 10:45:00\",           \"Value\": 93.5         }       ]     }   ] } ")]
        [TestCase("{\"PartnerId\": 1,   \"PartnerName\": \"Foo1\",   \"Trackers\": [     {       \"Id\": 1,       \"Model\": \"ABC-100\",       \"ShipmentStartDtm\": \"08-17-2020 10:30:00\",       \"Sensors\": [         {           \"Id\": 100,           \"Name\": \"Temperature\",           \"Crumbs\": [             {               \"CreatedDtm\": \"08-17-2020 10:35:00\",               \"Value\": 22.15             },             {               \"CreatedDtm\": \"08-17-2020 10:40:00\",               \"Value\": 23.15             },             {               \"CreatedDtm\": \"08-17-2020 10:45:00\",               \"Value\": 24.15             }           ]         },         {           \"Id\": 101,           \"Name\": \"Humidty\",           \"Crumbs\": [             {               \"CreatedDtm\": \"08-17-2020 10:35:00\",               \"Value\": 80.5             },             {               \"CreatedDtm\": \"08-17-2020 10:40:00\",               \"Value\": 81.5             },             {               \"CreatedDtm\": \"08-17-2020 10:45:00\",               \"Value\": 82.5             }           ]         }       ]     },     {       \"Id\": 2,       \"Model\": \"ABC-200\",       \"ShipmentStartDtm\": \"08-18-2020 10:30:00\",       \"Sensors\": [         {           \"Id\": 200,           \"Name\": \"Temperature\",           \"Crumbs\": [             {               \"CreatedDtm\": \"08-18-2020 10:35:00\",               \"Value\": 23.15             },             {               \"CreatedDtm\": \"08-18-2020 10:40:00\",               \"Value\": 24.15             },             {               \"CreatedDtm\": \"08-18-2020 10:45:00\",               \"Value\": 25.15             }           ]         },         {           \"Id\": 201,           \"Name\": \"Humidty\",           \"Crumbs\": [             {               \"CreatedDtm\": \"08-18-2020 10:35:00\",               \"Value\": 81.5             },             {               \"CreatedDtm\": \"08-18-2020 10:40:00\",               \"Value\": 82.5             },             {               \"CreatedDtm\": \"08-18-2020 10:45:00\",               \"Value\": 83.5             }           ]         }       ]     }   ] } ")]
        public void DeserializeDataShouldAcceptValidJsonData(string data)
        {
            //ServiceRunner should throws exception
            Assert.DoesNotThrow(() => _serviceRunner.DeserializeData(data));
        }

        [TearDown]
        public void Cleanup()
        {
            Directory.Delete(_input,true);
            File.Delete(_output);
        }
        private void CreateMockData()
        {
            FileWriting(_input, "partner.json", _partnerMockData);
            FileWriting(_input, "company.json", _companyMockData);
        }
        private void FileWriting(string path, string fileName, string data)
        {
            if (!Directory.Exists(path))
            { Directory.CreateDirectory(path); }
            using (var f = new FileStream(Path.Combine(path,fileName), FileMode.OpenOrCreate))
            {
                var dataArray = System.Text.Encoding.UTF8.GetBytes(data);
                f.Write(dataArray, 0, dataArray.Length);
                f.Flush();
            }
        }
    }
}