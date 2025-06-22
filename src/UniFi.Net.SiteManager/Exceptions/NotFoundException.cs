using UniFi.Net.SiteManager.Models;

namespace UniFi.Net.SiteManager.Exceptions;

/// <summary>
/// An exception that represents a "Not Found" error encountered while interacting with the UniFi SiteManager API.
/// </summary>
public class NotFoundException : UniFiSiteManagerException
{
    internal NotFoundException(ErrorResponse errorResponse) : base(errorResponse)
    {
    }
}
