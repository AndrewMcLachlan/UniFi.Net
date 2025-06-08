namespace UniFi.Net.Access;

/// <summary>
/// Configuration for UniFi Client.
/// </summary>
public record UniFiConfig
{
    /// <summary>
    /// The host URL of the UniFi controller (e.g., "https://unifi.example.com").
    /// </summary>
    public required string Host { get; init; }

    /// <summary>
    /// The API key for accessing the UniFi controller.
    /// </summary>
    /// <remarks>
    /// API keys can be generated in the UniFi control plane settings.
    /// </remarks>
    public required string ApiKey { get; init; }
}
