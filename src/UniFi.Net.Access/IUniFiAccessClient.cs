using System.Threading.Tasks;
using System.Collections.Generic;

namespace UniFi.Net.Access // Updated namespace to reflect new folder structure
{
    /// <summary>
    /// Interface for interacting with the UniFi Access API.
    /// </summary>
    public interface IUniFiAccessClient
    {
        // User Management
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="firstName">First name of the user.</param>
        /// <param name="lastName">Last name of the user.</param>
        /// <param name="email">Email of the user (optional).</param>
        /// <param name="employeeNumber">Employee number of the user (optional).</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task RegisterUserAsync(string firstName, string lastName, string? email = null, string? employeeNumber = null);

        /// <summary>
        /// Fetches details of a specific user by ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>Task representing the asynchronous operation, returning user details.</returns>
        Task<UserDetails> FetchUserAsync(string userId);

        /// <summary>
        /// Fetches all users with optional pagination.
        /// </summary>
        /// <param name="pageNum">Page number for pagination (optional).</param>
        /// <param name="pageSize">Number of users per page (optional).</param>
        /// <returns>Task representing the asynchronous operation, returning a list of users.</returns>
        Task<List<UserSummary>> FetchAllUsersAsync(int? pageNum = null, int? pageSize = null);

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task DeleteUserAsync(string userId);

        // Visitor Management
        /// <summary>
        /// Registers a new visitor.
        /// </summary>
        /// <param name="firstName">First name of the visitor.</param>
        /// <param name="lastName">Last name of the visitor.</param>
        /// <param name="email">Email of the visitor (optional).</param>
        /// <param name="phone">Phone number of the visitor (optional).</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task RegisterVisitorAsync(string firstName, string lastName, string? email = null, string? phone = null);

        /// <summary>
        /// Fetches details of a specific visitor by ID.
        /// </summary>
        /// <param name="visitorId">The ID of the visitor.</param>
        /// <returns>Task representing the asynchronous operation, returning visitor details.</returns>
        Task<VisitorDetails> FetchVisitorAsync(string visitorId);

        /// <summary>
        /// Fetches all visitors with optional pagination.
        /// </summary>
        /// <param name="pageNum">Page number for pagination (optional).</param>
        /// <param name="pageSize">Number of visitors per page (optional).</param>
        /// <returns>Task representing the asynchronous operation, returning a list of visitors.</returns>
        Task<List<VisitorSummary>> FetchAllVisitorsAsync(int? pageNum = null, int? pageSize = null);

        /// <summary>
        /// Deletes a visitor by ID.
        /// </summary>
        /// <param name="visitorId">The ID of the visitor to delete.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task DeleteVisitorAsync(string visitorId);

        // Access Policies
        /// <summary>
        /// Fetches all access policies.
        /// </summary>
        /// <returns>Task representing the asynchronous operation, returning a list of access policies.</returns>
        Task<List<AccessPolicySummary>> FetchAllAccessPoliciesAsync();

        /// <summary>
        /// Fetches details of a specific access policy by ID.
        /// </summary>
        /// <param name="policyId">The ID of the access policy.</param>
        /// <returns>Task representing the asynchronous operation, returning access policy details.</returns>
        Task<AccessPolicyDetails> FetchAccessPolicyAsync(string policyId);

        /// <summary>
        /// Creates a new access policy.
        /// </summary>
        /// <param name="name">Name of the access policy.</param>
        /// <param name="resourceIds">List of resource IDs associated with the policy.</param>
        /// <param name="scheduleId">Schedule ID associated with the policy.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task CreateAccessPolicyAsync(string name, List<string> resourceIds, string scheduleId);

        /// <summary>
        /// Deletes an access policy by ID.
        /// </summary>
        /// <param name="policyId">The ID of the access policy to delete.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task DeleteAccessPolicyAsync(string policyId);

        // Credentials
        /// <summary>
        /// Generates a new PIN code.
        /// </summary>
        /// <returns>Task representing the asynchronous operation, returning the generated PIN code.</returns>
        Task<string> GeneratePinCodeAsync();

        /// <summary>
        /// Enrolls a new NFC card.
        /// </summary>
        /// <param name="deviceId">The ID of the device to enroll the NFC card to.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task EnrollNfcCardAsync(string deviceId);

        /// <summary>
        /// Fetches details of a specific NFC card by token.
        /// </summary>
        /// <param name="cardToken">The token of the NFC card.</param>
        /// <returns>Task representing the asynchronous operation, returning NFC card details.</returns>
        Task<NfcCardDetails> FetchNfcCardAsync(string cardToken);

        /// <summary>
        /// Fetches all NFC cards with optional pagination.
        /// </summary>
        /// <param name="pageNum">Page number for pagination (optional).</param>
        /// <param name="pageSize">Number of NFC cards per page (optional).</param>
        /// <returns>Task representing the asynchronous operation, returning a list of NFC cards.</returns>
        Task<List<NfcCardSummary>> FetchAllNfcCardsAsync(int? pageNum = null, int? pageSize = null);

        /// <summary>
        /// Deletes an NFC card by token.
        /// </summary>
        /// <param name="cardToken">The token of the NFC card to delete.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task DeleteNfcCardAsync(string cardToken);

        // Space Management
        /// <summary>
        /// Fetches all door groups.
        /// </summary>
        /// <returns>Task representing the asynchronous operation, returning a list of door groups.</returns>
        Task<List<DoorGroupSummary>> FetchAllDoorGroupsAsync();

        /// <summary>
        /// Fetches details of a specific door group by ID.
        /// </summary>
        /// <param name="doorGroupId">The ID of the door group.</param>
        /// <returns>Task representing the asynchronous operation, returning door group details.</returns>
        Task<DoorGroupDetails> FetchDoorGroupAsync(string doorGroupId);

        /// <summary>
        /// Creates a new door group.
        /// </summary>
        /// <param name="name">Name of the door group.</param>
        /// <param name="resourceIds">List of door IDs associated with the group.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task CreateDoorGroupAsync(string name, List<string> resourceIds);

        /// <summary>
        /// Deletes a door group by ID.
        /// </summary>
        /// <param name="doorGroupId">The ID of the door group to delete.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task DeleteDoorGroupAsync(string doorGroupId);

        /// <summary>
        /// Fetches all doors.
        /// </summary>
        /// <returns>Task representing the asynchronous operation, returning a list of doors.</returns>
        Task<List<DoorSummary>> FetchAllDoorsAsync();

        /// <summary>
        /// Fetches details of a specific door by ID.
        /// </summary>
        /// <param name="doorId">The ID of the door.</param>
        /// <returns>Task representing the asynchronous operation, returning door details.</returns>
        Task<DoorDetails> FetchDoorAsync(string doorId);

        /// <summary>
        /// Unlocks a door remotely by ID.
        /// </summary>
        /// <param name="doorId">The ID of the door to unlock.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task UnlockDoorAsync(string doorId);

        // Device Management
        /// <summary>
        /// Fetches all devices.
        /// </summary>
        /// <returns>Task representing the asynchronous operation, returning a list of devices.</returns>
        Task<List<DeviceSummary>> FetchAllDevicesAsync();

        // System Logs
        /// <summary>
        /// Fetches system logs.
        /// </summary>
        /// <param name="topic">The topic of the logs to fetch.</param>
        /// <param name="since">Start time for log fetching (optional).</param>
        /// <param name="until">End time for log fetching (optional).</param>
        /// <param name="actorId">The ID of the actor (optional).</param>
        /// <returns>Task representing the asynchronous operation, returning a list of system logs.</returns>
        Task<List<SystemLogEntry>> FetchSystemLogsAsync(string topic, long? since = null, long? until = null, string? actorId = null);

        /// <summary>
        /// Exports system logs to a CSV file.
        /// </summary>
        /// <param name="topic">The topic of the logs to export.</param>
        /// <param name="since">Start time for log exporting.</param>
        /// <param name="until">End time for log exporting.</param>
        /// <param name="timezone">Timezone for formatting time.</param>
        /// <returns>Task representing the asynchronous operation, returning the CSV file as a byte array.</returns>
        Task<byte[]> ExportSystemLogsAsync(string topic, long since, long until, string timezone);

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

    /// <summary>
    /// Represents a summary of user details.
    /// </summary>
    public record UserSummary(string Id, string FirstName, string LastName, string Email);

    /// <summary>
    /// Represents detailed information about a user.
    /// </summary>
    public record UserDetails(string Id, string FirstName, string LastName, string Email, string Status);

    /// <summary>
    /// Represents a summary of visitor details.
    /// </summary>
    public record VisitorSummary(string Id, string FirstName, string LastName, string Email);

    /// <summary>
    /// Represents detailed information about a visitor.
    /// </summary>
    public record VisitorDetails(string Id, string FirstName, string LastName, string Email, string Status);

    /// <summary>
    /// Represents a summary of access policy details.
    /// </summary>
    public record AccessPolicySummary(string Id, string Name);

    /// <summary>
    /// Represents detailed information about an access policy.
    /// </summary>
    public record AccessPolicyDetails(string Id, string Name, List<string> ResourceIds, string ScheduleId);

    /// <summary>
    /// Represents a summary of NFC card details.
    /// </summary>
    public record NfcCardSummary(string Token, string DisplayId, string Status);

    /// <summary>
    /// Represents detailed information about an NFC card.
    /// </summary>
    public record NfcCardDetails(string Token, string DisplayId, string Status, string Alias);

    // Models for Space Management
    public record DoorGroupSummary(string Id, string Name);
    public record DoorGroupDetails(string Id, string Name, List<string> ResourceIds);
    public record DoorSummary(string Id, string Name, string Status);
    public record DoorDetails(string Id, string Name, string Status, string Type);

    // Models for Device Management
    public record DeviceSummary(string Id, string Name, string Type, string Alias);

    // Models for System Logs
    public record SystemLogEntry(string Event, string Actor, string Target, string Timestamp);

    /// <summary>
    /// Represents an invitation to UniFi Identity.
    /// </summary>
    public record IdentityInvitation(string UserId, string? Email);

    /// <summary>
    /// Represents a UniFi Identity resource.
    /// </summary>
    public record IdentityResource(string Id, string Name, string ResourceType, bool Deleted);
}