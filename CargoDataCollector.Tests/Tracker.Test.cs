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
    public class TrackerTests
    {
        private FooPartner _partner;
        private string _partnerMockData = "{ \"PartnerId\": 1,   \"PartnerName\": \"Foo1\",   \"Trackers\": [     {       \"Id\": 1,       \"Model\": \"ABC-100\",       \"ShipmentStartDtm\": \"08-17-2020 10:30:00\",       \"Sensors\": [         {           \"Id\": 100,           \"Name\": \"Temperature\",           \"Crumbs\": [             {               \"CreatedDtm\": \"08-17-2020 10:35:00\",               \"Value\": 22.15             },             {               \"CreatedDtm\": \"08-17-2020 10:40:00\",               \"Value\": 23.15             },             {               \"CreatedDtm\": \"08-17-2020 10:45:00\",               \"Value\": 24.15             }           ]         },         {           \"Id\": 101,           \"Name\": \"Humidty\",           \"Crumbs\": [             {               \"CreatedDtm\": \"08-17-2020 10:35:00\",               \"Value\": 80.5             },             {               \"CreatedDtm\": \"08-17-2020 10:40:00\",               \"Value\": 81.5             },             {               \"CreatedDtm\": \"08-17-2020 10:45:00\",               \"Value\": 82.5             }           ]         }       ]     },     {       \"Id\": 2,       \"Model\": \"ABC-200\",       \"ShipmentStartDtm\": \"08-18-2020 10:30:00\",       \"Sensors\": [         {           \"Id\": 200,           \"Name\": \"Temperature\",           \"Crumbs\": [             {               \"CreatedDtm\": \"08-18-2020 10:35:00\",               \"Value\": 23.15             },             {               \"CreatedDtm\": \"08-18-2020 10:40:00\",               \"Value\": 24.15             },             {               \"CreatedDtm\": \"08-18-2020 10:45:00\",               \"Value\": 25.15             }           ]         },         {           \"Id\": 201,           \"Name\": \"Humidty\",           \"Crumbs\": [             {               \"CreatedDtm\": \"08-18-2020 10:35:00\",               \"Value\": 81.5             },             {               \"CreatedDtm\": \"08-18-2020 10:40:00\",               \"Value\": 82.5             },             {               \"CreatedDtm\": \"08-18-2020 10:45:00\",               \"Value\": 83.5             }           ]         }       ]     }   ] } ";

        [SetUp]
        public void Setup()
        {
            _partner = JsonConvert.DeserializeObject<FooPartner>(_partnerMockData);        
        }

        [Test]
        public void TrackerShouldCalculateValidAverageHumidity()
        {
            var avgHumidity = _partner.Trackers[0].GetAvgHumidity();
            Assert.AreEqual(81.5, avgHumidity);
        }

        [Test]
        public void TrackerShouldCalculateValidAverageTemprature()
        {
            var avg = _partner.Trackers[0].GetAvgTemprature();
            Assert.AreEqual(23.15, Math.Round(avg.Value,2));
        }

        [Test]
        public void TrackerShouldCalculateValidHumidityCount()
        {
            var count = _partner.Trackers[0].GetHumidityCount();
            Assert.AreEqual(3, count);
        }

        [Test]
        public void TrackerShouldCalculateValidTempratureCount()
        {
            var count = _partner.Trackers[0].GetTempratureCount();
            Assert.AreEqual(3, count);
        }


        [Test]
        public void TrackerShouldNotCalculateAvgHumidityOnNullSensor()
        {
            _partner.Trackers[0].Sensors = null;
            var avgHumidity = _partner.Trackers[0].GetAvgHumidity();
            Assert.AreEqual(null, avgHumidity);
        }

        [Test]
        public void TrackerShouldNotCalculateAvgTempratureOnNullSensor()
        {
            _partner.Trackers[0].Sensors = null;
            var avg = _partner.Trackers[0].GetAvgTemprature();
            Assert.AreEqual(null, avg);
        }

        [Test]
        public void TrackerShouldNotCalculateHumidityCountOnNullSensor()
        {
            _partner.Trackers[0].Sensors = null;
            var count = _partner.Trackers[0].GetHumidityCount();
            Assert.AreEqual(null, count);
        }

        [Test]
        public void TrackerShouldNotCalculateTempratureCountOnNullSensor()
        {
            _partner.Trackers[0].Sensors = null;
            var count = _partner.Trackers[0].GetTempratureCount();
            Assert.AreEqual(null, count);
        }


        [TearDown]
        public void TearDown()
        { _partner = null; }
    }
}
