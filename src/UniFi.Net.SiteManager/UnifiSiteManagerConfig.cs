namespace UniFi.Net;
/// <summary>
/// Configuration for UniFi Client.
/// </summary>
public record UniFiSiteManagerConfig
{
    /// <summary>
    /// The host URL of the UniFi Site Manager API.
    /// </summary>
    /// <remarks>
    /// Unless specified, defaults to "https://api.ui.com".
    /// </remarks>
    public string Host { get; init; } = "https://api.ui.com";

    /// <summary>
    /// The API key for accessing the UniFi controller.
    /// </summary>
    /// <remarks>
    /// API keys can be generated in the UniFi control plane settings.
    /// </remarks>
    public required string ApiKey { get; init; }
}
