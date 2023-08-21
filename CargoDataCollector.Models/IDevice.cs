using System;

namespace CargoDataCollector.Models
{
    public interface IDeviceBase
    {
        public double? GetAvgHumidity();
        public int? GetHumidityCount();
        public double? GetAvgTemprature();
        public int? GetTempratureCount();
        public DateTime? GetStartDateTime();
        public DateTime? GetEndDateTime();
    }
}


