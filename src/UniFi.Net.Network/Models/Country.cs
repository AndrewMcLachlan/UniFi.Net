namespace UniFi.Net.Network.Models;

/// <summary>
/// Represents a country.
/// </summary>
/// <param name="Code">The country code.</param>
/// <param name="Name">The name of the country.</param>
public record Country(
    string Code,
    string Name
);
