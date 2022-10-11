using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DSA.Serialization;

public class OrderedContractResolver : DefaultContractResolver
{
    protected override System.Collections.Generic.IList<JsonProperty> CreateProperties(System.Type type,
        MemberSerialization memberSerialization) => base.CreateProperties(type, memberSerialization)
        .OrderBy(p => p.PropertyName).ToList();
}