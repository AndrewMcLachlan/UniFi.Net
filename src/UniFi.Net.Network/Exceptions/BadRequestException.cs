using UniFi.Net.Network.Responses;

namespace UniFi.Net.Network.Exceptions;

/// <summary>
/// An exception that represents a "Bad Request" error encountered while interacting with the UniFi Network API.
/// </summary>
public class BadRequestException : UniFiNetworkException
{
    internal BadRequestException(ErrorResponse errorResponse) : base(errorResponse)
    {
    }
}
