using System.Text.Json;

namespace UniFi.Net.Serialization;

internal class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        if (String.IsNullOrEmpty(name)) return name;
        var sb = new System.Text.StringBuilder();
        sb.Append(Char.ToLowerInvariant(name[0]));
        for (int i = 1; i < name.Length; i++)
        {
            var c = name[i];
            if (Char.IsUpper(c))
            {
                sb.Append('_');
                sb.Append(Char.ToLowerInvariant(c));
            }
            else
            {
                sb.Append(c);
            }
        }
        return sb.ToString().ToUpperInvariant();
    }
}
