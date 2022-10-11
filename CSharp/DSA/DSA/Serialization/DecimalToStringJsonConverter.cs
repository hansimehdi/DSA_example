using System.Globalization;
using Newtonsoft.Json;

namespace DSA.Serialization;

public class DecimalToStringJsonConverter : JsonConverter
{
    public override bool CanRead => false;

    /// <summary>
    /// Returns true if the type of the object is
    /// - decimal
    /// - double
    /// - int
    /// </summary>
    /// <param name="objectType"></param>
    /// <returns></returns>
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(decimal) || objectType == typeof(decimal?) || objectType == typeof(double) ||
               objectType == typeof(double?) ||
               objectType == typeof(int) || objectType == typeof(int?);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
        JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Convert the object( decimal, double or int) to string
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="serializer"></param>
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        switch (value)
        {
            case int v:
                writer.WriteValue($"{v}");
                break;
            case double d:
                writer.WriteValue(
                    $"{double.Parse(double.Parse(d.ToString("N5")).ToString("G29").ToString(CultureInfo.InvariantCulture))}");
                break;
            case decimal m:
                writer.WriteValue(
                    $"{decimal.Parse(decimal.Parse(m.ToString("N5")).ToString("G29").ToString(CultureInfo.InvariantCulture))}");
                break;
        }
    }
}