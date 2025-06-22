using UniFi.Net.Network.Responses;

namespace UniFi.Net.Network.Exceptions;

/// <summary>
/// Represents an exception that is thrown when an operation fails due to unauthorized access.
/// </summary>
/// <remarks>This exception is typically used to indicate that the caller does not have the necessary permissions
/// to perform the requested operation. It is specific to scenarios involving UniFi network operations.</remarks>
public class UnauthorizedException : UniFiNetworkException
{
    internal UnauthorizedException(ErrorResponse errorResponse) : base(errorResponse)
    {
    }
}
