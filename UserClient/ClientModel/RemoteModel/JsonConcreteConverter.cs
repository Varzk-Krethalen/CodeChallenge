using Newtonsoft.Json;
using System;

namespace ClientModels.RemoteModelObjects
{
    public class JsonConcreteConverter<I, T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(I) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<T>(reader);
        }

        public override void WriteJson(JsonWriter writer,
            object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
