using System;
using System.Collections.Generic;
using System.Linq;

namespace CargoDataCollector.Models
{
    public class Device : IDeviceBase
    {
        #region Public Properties
        public int DeviceID { get; set; }
        public string Name { get; set; }
        public DateTime StartDateTime { get; set; }
        public List<SensorData> SensorData { get; set; }
        #endregion

        #region Overriedden methods for DeviceBase
        public double? GetAvgHumidity()
        {
            if (SensorData != null && SensorData.Count > 0)
            {
                var humidityData = SensorData.Where(x => x.SensorType.ToLower().Equals("hum")).Select(x => x.Value).Average();
                return humidityData;
            }
            else
                return null;
        }
        public int? GetHumidityCount()
        {
            if (SensorData != null && SensorData.Count > 0)
            {
                var count = SensorData.Where(x => x.SensorType.ToLower().Equals("hum")).Count();
                return count;
            }
            else
                return null;
        }
        public int? GetTempratureCount()
        {
            if (SensorData != null && SensorData.Count > 0)
            {
                var count = SensorData.Where(x => x.SensorType.ToLower().Equals("temp")).Count();
                return count;
            }
            else
                return null;
        }
        public double? GetAvgTemprature()
        {
            if (SensorData != null && SensorData.Count > 0)
            {
                var tempratureData = SensorData.Where(x => x.SensorType.ToLower().Equals("temp")).Select(x => x.Value).Average();
                return tempratureData;
            }
            else
                return null;
        }
        public DateTime? GetEndDateTime()
        {
            if (SensorData != null && SensorData.Count > 0)
            {
                SensorData.Sort((x, y) => DateTime.Compare(x.DateTime, y.DateTime));
                return SensorData.LastOrDefault().DateTime;
            }
            return null;
        }
        public DateTime? GetStartDateTime()
        {
            if (SensorData != null && SensorData.Count > 0)
            {
                SensorData.Sort((x, y) => DateTime.Compare(x.DateTime, y.DateTime));
                return SensorData.FirstOrDefault().DateTime;
            }
            return null;
        }
        #endregion
    }
}


