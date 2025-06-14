using System.Text.Json.Serialization;

namespace UniFi.Net.Serialization;
internal class SnakeCaseEnumConverter : JsonStringEnumConverter
{
    public SnakeCaseEnumConverter() : base(new SnakeCaseNamingPolicy(), allowIntegerValues: false)
    {
    }
}
