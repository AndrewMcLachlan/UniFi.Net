using System.Net;
using UniFi.Net.Network.Responses;

namespace UniFi.Net.Network.Exceptions;

/// <summary>
/// Exception that represents an error encountered while interacting with the UniFi Network API.
/// </summary>
public class UniFiNetworkException : Exception
{
    /// <summary>
    /// Gets the HTTP status code associated with the error.
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Gets the HTTP status description associated with the error.
    /// </summary>
    public string StatusName { get; }

    /// <summary>
    /// Gets the timestamp when the error occurred.
    /// </summary>
    public DateTimeOffset Timestamp { get; }

    /// <summary>
    /// Gets the request path that caused the error.
    /// </summary>
    public string RequestPath { get; }

    /// <summary>
    /// Gets the trace ID for the request that caused the error.
    /// </summary>
    public Guid RequestId { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UniFiNetworkException"/> class with a specified error message.
    /// </summary>
    /// <param name="errorResponse">The error response containing details about the error.</param>
    internal UniFiNetworkException(ErrorResponse errorResponse) : base(errorResponse.Message)
    {
        ArgumentNullException.ThrowIfNull(errorResponse);
        StatusCode = errorResponse.StatusCode;
        StatusName = errorResponse.StatusName;
        Timestamp = errorResponse.Timestamp;
        RequestPath = errorResponse.RequestPath;
        RequestId = errorResponse.RequestId;
    }
}
