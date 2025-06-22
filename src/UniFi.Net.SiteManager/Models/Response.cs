using System.Net;

namespace UniFi.Net.SiteManager.Models;

/// <summary>
/// A response model.
/// </summary>
public abstract record Response
{
    /// <summary>
    /// Gets the HTTP status code associated with the response.
    /// </summary>
    public HttpStatusCode HttpStatusCode { get; init; }

    /// <summary>
    /// Gets the unique identifier for the request trace.
    /// </summary>
    public required string TraceId { get; init; }
}

