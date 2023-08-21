using System;
using System.Collections.Generic;
using System.Linq;

namespace CargoDataCollector.Models
{
    public class Tracker : IDeviceBase
    {
        #region Public Properties
        public int Id { get; set; }
        public string Model { get; set; }
        public DateTime ShipmentStartDtm { get; set; }
        public List<Sensor> Sensors { get; set; }
        #endregion

        #region Overriedden methods for DeviceBase
        public double? GetAvgHumidity()
        {
            if (Sensors != null && Sensors.Count > 0)
            {
                var sensor = this.Sensors.Where(x => x.Name.ToLower().Equals("humidty")).FirstOrDefault();
                return sensor?.Crumbs.Select(x => x.Value).Average();
            }
            return null;
        }
        public int? GetHumidityCount()
        {
            if (Sensors != null && Sensors.Count > 0)
            {
                var sensors = Sensors.Where(x => x.Name.ToLower().Equals("humidty")).FirstOrDefault();
                return sensors?.Crumbs.Count();
            }
            return null;
        }
        public double? GetAvgTemprature()
        {
            if (Sensors != null && Sensors.Count > 0)
            {
                var sensor = this.Sensors.Where(x => x.Name.ToLower().Equals("temperature")).FirstOrDefault();
                return sensor?.Crumbs.Select(x => x.Value).Average();
            }
            return null;
        }
        public int? GetTempratureCount()
        {
            if (Sensors != null && Sensors.Count > 0)
            {
                var sensors = Sensors.Where(x => x.Name.ToLower().Equals("temperature")).FirstOrDefault();
                return sensors?.Crumbs.Count();
            }
            return null;
        }
        public DateTime? GetEndDateTime()
        {
            if (Sensors != null && Sensors.Count > 0)
            {
                var crumbs = Sensors.SelectMany(x => x.Crumbs).Distinct().ToList();
                crumbs.Sort((x, y) => DateTime.Compare(x.CreatedDtm, y.CreatedDtm));
                return crumbs.LastOrDefault().CreatedDtm;
            }
            return null;
        }
        public DateTime? GetStartDateTime()
        {
            if (Sensors != null && Sensors.Count > 0)
            {
                var crumbs = Sensors.SelectMany(x => x.Crumbs).Distinct().ToList();
                crumbs.Sort((x, y) => DateTime.Compare(x.CreatedDtm, y.CreatedDtm));
                return crumbs.FirstOrDefault().CreatedDtm;
            }
            return null;
        }
        #endregion
    }
}


