using System.Collections.Generic;

namespace CargoDataCollector.Models
{
    public class FooCompany : IVendor
    {
        #region Public Properties
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public List<Device> Devices { get; set; } 
        #endregion

        public List<ResponeModel> PopulateResults()
        {
            List<ResponeModel> responses = new List<ResponeModel>();
            

            if (Devices != null && Devices.Count > 0)
            {
                foreach (var device in Devices)
                {
                    ResponeModel responseModel = new ResponeModel();
                    responseModel.CompanyId = this.CompanyId;
                    responseModel.CompanyName = this.Company;
                    responseModel.DeviceId = device.DeviceID;
                    responseModel.DeviceName = device.Name;
                    responseModel.AverageTemperature = device.GetAvgTemprature();
                    responseModel.TemperatureCount = device.GetTempratureCount();
                    responseModel.AverageHumdity = device.GetAvgHumidity();
                    responseModel.HumidityCount = device.GetHumidityCount();
                    responseModel.FirstReadingDtm = device.GetStartDateTime();
                    responseModel.LastReadingDtm = device.GetEndDateTime();
                    responses.Add(responseModel);
                }
            }
            else
            {
                ResponeModel responseModel = new ResponeModel();
                responseModel.CompanyId = this.CompanyId;
                responseModel.CompanyName = this.Company;
                responses.Add(responseModel);
            }
            return responses;
        }
    }
}


