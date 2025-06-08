namespace UniFi.Net.Access;

public interface IUniFiIdentityClient
{
    // UniFi Identity
    /// <summary>
    /// Sends UniFi Identity invitations to users.
    /// </summary>
    /// <param name="invitations">List of invitations containing user IDs and optional emails.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task SendIdentityInvitationsAsync(List<IdentityInvitation> invitations);

    /// <summary>
    /// Fetches available UniFi Identity resources.
    /// </summary>
    /// <param name="resourceType">Optional resource type filter (e.g., ev_station, vpn, wifi).</param>
    /// <returns>Task representing the asynchronous operation, returning a list of resources.</returns>
    Task<List<IdentityResource>> FetchAvailableResourcesAsync(string? resourceType = null);

    /// <summary>
    /// Assigns resources to a user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="resourceType">The type of resources to assign (e.g., ev_station, vpn, wifi).</param>
    /// <param name="resourceIds">List of resource IDs to assign.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task AssignResourcesToUserAsync(string userId, string resourceType, List<string> resourceIds);

    /// <summary>
    /// Fetches resources assigned to a user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>Task representing the asynchronous operation, returning a list of assigned resources.</returns>
    Task<List<IdentityResource>> FetchResourcesAssignedToUserAsync(string userId);

    /// <summary>
    /// Assigns resources to a user group.
    /// </summary>
    /// <param name="groupId">The ID of the user group.</param>
    /// <param name="resourceType">The type of resources to assign (e.g., ev_station, vpn, wifi).</param>
    /// <param name="resourceIds">List of resource IDs to assign.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task AssignResourcesToUserGroupAsync(string groupId, string resourceType, List<string> resourceIds);

    /// <summary>
    /// Fetches resources assigned to a user group.
    /// </summary>
    /// <param name="groupId">The ID of the user group.</param>
    /// <returns>Task representing the asynchronous operation, returning a list of assigned resources.</returns>
    Task<List<IdentityResource>> FetchResourcesAssignedToUserGroupAsync(string groupId);
}
