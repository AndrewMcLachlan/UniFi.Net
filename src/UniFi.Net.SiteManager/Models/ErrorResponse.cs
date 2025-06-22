namespace UniFi.Net.SiteManager.Models;

/// <summary>
/// Error response.
/// </summary>
public record ErrorResponse : Response
{
    /// <summary>
    /// Gets the HTTP error code.
    /// </summary>
    public required string Code { get; init; }

    /// <summary>
    /// Gets the error message.
    /// </summary>
    public required string Message { get; init; }
}
