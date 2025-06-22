using UniFi.Net.SiteManager.Models;

namespace UniFi.Net.SiteManager.Exceptions;

/// <summary>
/// Represents an exception that is thrown when an operation fails due to unauthorized access.
/// </summary>
/// <remarks>This exception is typically used to indicate that the caller does not have the necessary permissions
/// to perform the requested operation. It is specific to scenarios involving UniFi SiteManager operations.</remarks>
public class UnauthorizedException : UniFiSiteManagerException
{
    internal UnauthorizedException(ErrorResponse errorResponse) : base(errorResponse)
    {
    }
}
