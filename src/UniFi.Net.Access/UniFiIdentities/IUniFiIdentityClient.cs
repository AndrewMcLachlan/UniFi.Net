namespace UniFi.Net.Access.UniFiIdentities;

/// <summary>
/// An interface for interacting with UniFi Identity services.
/// </summary>
public interface IUniFiIdentityClient
{
    // UniFi Identity
    /// <summary>
    /// Sends UniFi Identity invitations to users.
    /// </summary>
    /// <param name="invitations">List of invitations containin user IDs and optional emails.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task SendIdentityInvitationsAsync(List<IdentityInvitation> invitations, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches available UniFi Identity resources.
    /// </summary>
    /// <param name="resourceType">Optional resource type filter (e.g., ev_station, vpn, wifi).</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation, returning a list of resources.</returns>
    Task<List<IdentityResource>> FetchAvailableResourcesAsync(string? resourceType = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Assigns resources to a user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="resourceType">The type of resources to assign (e.g., ev_station, vpn, wifi).</param>
    /// <param name="resourceIds">List of resource IDs to assign.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task AssignResourcesToUserAsync(string userId, string resourceType, List<string> resourceIds, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches resources assigned to a user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation, returning a list of assigned resources.</returns>
    Task<List<IdentityResource>> FetchResourcesAssignedToUserAsync(string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Assigns resources to a user group.
    /// </summary>
    /// <param name="groupId">The ID of the user group.</param>
    /// <param name="resourceType">The type of resources to assign (e.g., ev_station, vpn, wifi).</param>
    /// <param name="resourceIds">List of resource IDs to assign.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task AssignResourcesToUserGroupAsync(string groupId, string resourceType, List<string> resourceIds, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches resources assigned to a user group.
    /// </summary>
    /// <param name="groupId">The ID of the user group.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation, returning a list of assigned resources.</returns>
    Task<List<IdentityResource>> FetchResourcesAssignedToUserGroupAsync(string groupId, CancellationToken cancellationToken = default);
}
