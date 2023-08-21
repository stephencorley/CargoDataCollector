using CargoDataCollector.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoDataCollector.Tests
{
    public class DeviceTest
    {
        private FooCompany _company;
        private string _companyMockData = "{   \"CompanyId\": 2,   \"Company\": \"Foo2\",   \"Devices\": [     {       \"DeviceID\": 1,       \"Name\": \"XYZ-100\",       \"StartDateTime\": \"08-18-2020 10:30:00\",       \"SensorData\": [         {           \"SensorType\": \"TEMP\",           \"DateTime\": \"08-18-2020 10:35:00\",           \"Value\": 32.15         },         {           \"SensorType\": \"TEMP\",           \"DateTime\": \"08-18-2020 10:40:00\",           \"Value\": 33.15         },         {           \"SensorType\": \"TEMP\",           \"DateTime\": \"08-18-2020 10:45:00\",           \"Value\": 34.15         },         {           \"SensorType\": \"HUM\",           \"DateTime\": \"08-18-2020 10:35:00\",           \"Value\": 90.5         },         {           \"SensorType\": \"HUM\",           \"DateTime\": \"08-18-2020 10:40:00\",           \"Value\": 91.5         },         {           \"SensorType\": \"HUM\",           \"DateTime\": \"08-18-2020 10:45:00\",           \"Value\": 92.5         }       ]     },     {       \"DeviceID\": 2,       \"Name\": \"XYZ-200\",       \"StartDateTime\": \"08-19-2020 10:30:00\",       \"SensorData\": [         {           \"SensorType\": \"TEMP\",           \"DateTime\": \"08-19-2020 10:35:00\",           \"Value\": 42.15         },         {           \"SensorType\": \"TEMP\",           \"DateTime\": \"08-19-2020 10:40:00\",           \"Value\": 43.15         },         {           \"SensorType\": \"TEMP\",           \"DateTime\": \"08-19-2020 10:45:00\",           \"Value\": 44.15         },         {           \"SensorType\": \"HUM\",           \"DateTime\": \"08-19-2020 10:35:00\",           \"Value\": 91.5         },         {           \"SensorType\": \"HUM\",           \"DateTime\": \"08-19-2020 10:40:00\",           \"Value\": 92.5         },         {           \"SensorType\": \"HUM\",           \"DateTime\": \"08-19-2020 10:45:00\",           \"Value\": 93.5         }       ]     }   ] } ";

        [SetUp]
        public void Setup()
        {
            _company = JsonConvert.DeserializeObject<FooCompany>(_companyMockData);
        }

        [Test]
        public void DeviceShouldCalculateValidAverageHumidity()
        {
            var avgHumidity = _company.Devices[0].GetAvgHumidity();
            Assert.AreEqual(91.5, avgHumidity);
        }

        [Test]
        public void DeviceShouldCalculateValidAverageTemprature()
        {
            var avg = _company.Devices[0].GetAvgTemprature();
            Assert.AreEqual(33.15, avg);
        }

        [Test]
        public void DeviceShouldCalculateValidHumidityCount()
        {
            var count = _company.Devices[0].GetHumidityCount();
            Assert.AreEqual(3, count);
        }

        [Test]
        public void DeviceShouldCalculateValidTempratureCount()
        {
            var count = _company.Devices[0].GetTempratureCount();
            Assert.AreEqual(3, count);
        }


        [Test]
        public void DeviceShouldNotCalculateAvgHumidityOnNullSensor()
        {
            _company.Devices[0].SensorData = null;
            var avgHumidity = _company.Devices[0].GetAvgHumidity();
            Assert.AreEqual(null, avgHumidity);
        }

        [Test]
        public void DeviceShouldNotCalculateAvgTempratureOnNullSensor()
        {
            _company.Devices[0].SensorData = null;
            var avg = _company.Devices[0].GetAvgTemprature();
            Assert.AreEqual(null, avg);
        }

        [Test]
        public void DeviceShouldNotCalculateHumidityCountOnNullSensor()
        {
            _company.Devices[0].SensorData = null;
            var count = _company.Devices[0].GetHumidityCount();
            Assert.AreEqual(null, count);
        }

        [Test]
        public void DeviceShouldNotCalculateTempratureCountOnNullSensor()
        {
            _company.Devices[0].SensorData = null;
            var count = _company.Devices[0].GetTempratureCount();
            Assert.AreEqual(null, count);
        }


        [TearDown]
        public void TearDown()
        { _company = null; }

    }
}
