namespace UniFi.Net.Network.Models;

/// <summary>
/// Represents the response of executing a client action, including details about the action performed and any changes
/// to client authorizations.
/// </summary>
/// <param name="RevokedAuthorization">The client authorization that was revoked as a result of the action, if any; otherwise, <see langword="null"/>.</param>
/// <param name="GrantedAuthorization">The client authorization that was granted as a result of the action, if any; otherwise, <see langword="null"/>.</param>
public record AuthorizeClientGuestAccessResponse(ClientAuthorization? RevokedAuthorization, ClientAuthorization? GrantedAuthorization);
