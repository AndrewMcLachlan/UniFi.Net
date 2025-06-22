using System.Diagnostics;
using System.Net;
using UniFi.Net.SiteManager.Models;

namespace UniFi.Net.SiteManager.Exceptions;

/// <summary>
/// Exception that represents an error encountered while interacting with the UniFi Network API.
/// </summary>
public class UniFiSiteManagerException : Exception
{
    /// <summary>
    /// Gets the HTTP status code associated with the error.
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Gets the HTTP status description associated with the error.
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// Gets the trace ID for the request that caused the error.
    /// </summary>
    public string TraceId { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UniFiSiteManagerException"/> class with a specified error message.
    /// </summary>
    /// <param name="errorResponse">The error response containing details about the error.</param>
    internal UniFiSiteManagerException(ErrorResponse errorResponse) : base(errorResponse.Message)
    {
        ArgumentNullException.ThrowIfNull(errorResponse);
        StatusCode = errorResponse.HttpStatusCode;
        Code = errorResponse.Code;
        TraceId = errorResponse.TraceId;
    }
}
