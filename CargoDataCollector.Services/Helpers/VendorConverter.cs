using CargoDataCollector.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace CargoDataCollector.Services.Helpers
{
    class VendorConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IVendor);
        }
        public override bool CanWrite => base.CanWrite;
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            try
            {
                if (jsonObject.ContainsKey("CompanyId"))
                    return jsonObject.ToObject<FooCompany>(serializer);
                else if (jsonObject.ContainsKey("PartnerId"))
                {
                    return jsonObject.ToObject<FooPartner>(serializer);
                }
                throw new NotSupportedException($"Unsupported type");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
