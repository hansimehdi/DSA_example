using System.Globalization;
using Newtonsoft.Json;

namespace DSA.Serialization;

public class DecimalJsonConverter : JsonConverter<decimal>
{
    public override void WriteJson(JsonWriter writer,
        decimal value, JsonSerializer serializer)
    {
        writer.WriteRawValue(value.ToString(CultureInfo.InvariantCulture));
    }

    public override decimal ReadJson(JsonReader reader, Type objectType, decimal existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}