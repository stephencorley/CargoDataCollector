using System.Collections.Generic;

namespace CargoDataCollector.Models
{
    public class FooPartner : IVendor
    {
        #region Public Properties
        public int PartnerId { get; set; }
        public string PartnerName { get; set; }
        public List<Tracker> Trackers { get; set; } 
        #endregion

        public List<ResponeModel> PopulateResults()
        {
            List<ResponeModel> responses = new List<ResponeModel>();
            if (Trackers != null && Trackers.Count > 0)
            {
                foreach (var tracker in Trackers)
                {
                    ResponeModel responseModel = new ResponeModel();
                    responseModel.CompanyId = this.PartnerId;
                    responseModel.CompanyName = this.PartnerName;
                    responseModel.DeviceId = tracker.Id;
                    responseModel.DeviceName = tracker.Model;
                    responseModel.AverageTemperature = tracker.GetAvgTemprature();
                    responseModel.TemperatureCount = tracker.GetTempratureCount();
                    responseModel.AverageHumdity = tracker.GetAvgHumidity();
                    responseModel.HumidityCount = tracker.GetHumidityCount();
                    responseModel.FirstReadingDtm = tracker.GetStartDateTime();
                    responseModel.LastReadingDtm = tracker.GetEndDateTime();
                    responses.Add(responseModel);
                }
            }
            else
            {
                ResponeModel responseModel = new ResponeModel();
                responseModel.CompanyId = this.PartnerId;
                responseModel.CompanyName = this.PartnerName;
                responses.Add(responseModel);
            }
            return responses;
        }
    }
}


