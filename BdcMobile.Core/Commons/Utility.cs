using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BdcMobile.Core.Commons
{
    public static class Utility
    {
        public static T DeserializeObject<T>(string str)
        {
            var serializeSettings = new JsonSerializerSettings();
            
            serializeSettings.Converters.Add(new InvalidDataFormatJsonConverter());
            var result = JsonConvert.DeserializeObject<T>(str, serializeSettings);            
            return result;
        }


        
    }

    class InvalidDataFormatJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // implement in case you're serializing it back
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var dataString = (string)reader.Value;

            try
            {
                TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById(Constants.TimeZone.HanoiTime);                
                var date = DateTime.Parse(dataString, CultureInfo.InvariantCulture);
                DateTime estTime = TimeZoneInfo.ConvertTimeToUtc(date, estZone);
                return estTime;
            }
            catch (Exception ex)
            {
                return DateTime.MinValue;

            }
            
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}
