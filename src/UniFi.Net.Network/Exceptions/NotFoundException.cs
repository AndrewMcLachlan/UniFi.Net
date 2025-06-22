using UniFi.Net.Network.Responses;

namespace UniFi.Net.Network.Exceptions;

/// <summary>
/// An exception that represents a "Not Found" error encountered while interacting with the UniFi Network API.
/// </summary>
public class NotFoundException : UniFiNetworkException
{
    internal NotFoundException(ErrorResponse errorResponse) : base(errorResponse)
    {
    }
}
