using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FantasticTour
{
    public static class Helpers
    {
        public static string Serialize(Object value)
        {
            return JsonConvert.SerializeObject(
                value,
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateFormatString = "dd/MM/yyyy",
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }
}