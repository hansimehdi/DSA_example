using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DSA.Serialization;

public static class SerializationExtension
{
    /// <summary>
    /// Serialize object
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    public static string ToJsonString(this object o) => JsonConvert.SerializeObject(
        o, new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Culture = CultureInfo.CurrentCulture,
            ContractResolver = new OrderedContractResolver(),
            Converters = new List<JsonConverter> {new DecimalToStringJsonConverter(), new StringEnumConverter()}
        });
    
    /// <summary>
    /// Convert json string to bytes
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    public static byte[] GetJsonBytes(this object o) => Encoding.UTF8.GetBytes(o.ToJsonString());
}