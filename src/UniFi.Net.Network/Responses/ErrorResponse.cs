using System.Net;

namespace UniFi.Net.Network.Responses;

/// <summary>
/// An error response model for UniFi Network API errors.
/// </summary>
internal record ErrorResponse
{
    /// <summary>
    /// Gets the HTTP status code.
    /// </summary>
    public HttpStatusCode StatusCode { get; init; }

    /// <summary>
    /// Gets the HTTP status description.
    /// </summary>
    public required string StatusName { get; init; }

    /// <summary>
    /// Gets the error message.
    /// </summary>
    public string? Message { get; init; }

    /// <summary>
    /// Gets the error timestamp.
    /// </summary>
    public DateTimeOffset Timestamp { get; init; }

    /// <summary>
    /// Gets the request path that caused the error.
    /// </summary>
    public required string RequestPath { get; init; }

    /// <summary>
    /// Gets the trace ID for the request.
    /// </summary>
    public required Guid RequestId { get; init; }
}
