namespace UniFi.Net.Models;

/// <summary>
/// Represents a site in the UniFi Network API.
/// </summary>
/// <param name="Id">The ID.</param>
/// <param name="InternalReference">The internal reference.</param>
/// <param name="Name">The site's name.</param>
public record Site(
    Guid Id,
    string InternalReference,
    string Name
);