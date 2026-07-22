using System.Text.Json;
using System.Text.Json.Serialization;

namespace UniFi.Net.Serialization;
internal class CamelCaseEnumConverter : JsonStringEnumConverter
{
    public CamelCaseEnumConverter() : base(JsonNamingPolicy.CamelCase, allowIntegerValues: false)
    {
    }
}
