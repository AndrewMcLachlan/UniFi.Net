namespace UniFi.Net.Access.UniFiIdentities;
/// <summary>
/// Represents an invitation to UniFi Identity.
/// </summary>
public record IdentityInvitation(string UserId, string? Email);

/// <summary>
/// Represents a UniFi Identity resource.
/// </summary>
public record IdentityResource(string Id, string Name, string ResourceType, bool Deleted);