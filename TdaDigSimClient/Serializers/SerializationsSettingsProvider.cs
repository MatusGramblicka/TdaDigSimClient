using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NodaTime;
using NodaTime.Serialization.JsonNet;

namespace TdaDigSimClient.Serializers
{
    public static class SerializationsSettingsProvider
    {
        public static JsonSerializerSettings JsonSerializerSettings(bool shouldHandleTypeNames = false)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            //settings.Converters.Add(new StringEnumConverter());
            settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            settings.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
            settings.NullValueHandling = NullValueHandling.Ignore;
            if (shouldHandleTypeNames)
            {
                settings.TypeNameHandling = TypeNameHandling.Objects;
            }

            return settings;
        }
    }
}
