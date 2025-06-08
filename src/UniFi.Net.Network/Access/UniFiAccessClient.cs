using System.Text;
using System.Text.Json;
using UniFi.Net.Access.AccessPolicies;
using UniFi.Net.Access.Credentials;
using UniFi.Net.Access.Devices;
using UniFi.Net.Access.Space;
using UniFi.Net.Access.SystemLog;
using UniFi.Net.Access.Users;
using UniFi.Net.Access.Visitors;

namespace UniFi.Net.Access // Updated namespace to reflect new folder structure
{
    /// <summary>
    /// Implementation of the UniFi Access API client.
    /// </summary>
    public class UniFiAccessClient : IUniFiAccessClient, IUserClient, IVisitorClient, IAccessPolicyClient, ICredentialClient, ISpaceClient, IDeviceClient, ISystemLogClient, IUniFiIdentityClient
    {
        private readonly HttpClient _httpClient;

        public UniFiAccessClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // User Management
        public async Task RegisterUserAsync(string firstName, string lastName, string? email = null, string? employeeNumber = null)
        {
            var payload = new
            {
                first_name = firstName,
                last_name = lastName,
                user_email = email,
                employee_number = employeeNumber
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/v1/developer/users", content);

            response.EnsureSuccessStatusCode();
        }

        public async Task<UserDetails> FetchUserAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"/api/v1/developer/users/{userId}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserDetails>(json) ?? throw new InvalidOperationException("Failed to deserialize user details.");
        }

        public async Task<List<UserSummary>> FetchAllUsersAsync(int? pageNum = null, int? pageSize = null)
        {
            var query = new List<string>();
            if (pageNum.HasValue) query.Add($"page_num={pageNum.Value}");
            if (pageSize.HasValue) query.Add($"page_size={pageSize.Value}");

            var queryString = query.Count > 0 ? "?" + string.Join("&", query) : string.Empty;
            var response = await _httpClient.GetAsync($"/api/v1/developer/users{queryString}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<UserSummary>>(json) ?? new List<UserSummary>();
        }

        public async Task DeleteUserAsync(string userId)
        {
            var response = await _httpClient.DeleteAsync($"/api/v1/developer/users/{userId}");

            response.EnsureSuccessStatusCode();
        }

        // Visitor Management
        public async Task RegisterVisitorAsync(string firstName, string lastName, string? email = null, string? phone = null)
        {
            var payload = new
            {
                first_name = firstName,
                last_name = lastName,
                email = email,
                mobile_phone = phone
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/v1/developer/visitors", content);

            response.EnsureSuccessStatusCode();
        }

        public async Task<VisitorDetails> FetchVisitorAsync(string visitorId)
        {
            var response = await _httpClient.GetAsync($"/api/v1/developer/visitors/{visitorId}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<VisitorDetails>(json) ?? throw new InvalidOperationException("Failed to deserialize visitor details.");
        }

        public async Task<List<VisitorSummary>> FetchAllVisitorsAsync(int? pageNum = null, int? pageSize = null)
        {
            var query = new List<string>();
            if (pageNum.HasValue) query.Add($"page_num={pageNum.Value}");
            if (pageSize.HasValue) query.Add($"page_size={pageSize.Value}");

            var queryString = query.Count > 0 ? "?" + string.Join("&", query) : string.Empty;
            var response = await _httpClient.GetAsync($"/api/v1/developer/visitors{queryString}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<VisitorSummary>>(json) ?? new List<VisitorSummary>();
        }

        public async Task DeleteVisitorAsync(string visitorId)
        {
            var response = await _httpClient.DeleteAsync($"/api/v1/developer/visitors/{visitorId}");

            response.EnsureSuccessStatusCode();
        }

        // Access Policies
        public async Task<List<AccessPolicySummary>> FetchAllAccessPoliciesAsync()
        {
            var response = await _httpClient.GetAsync("/api/v1/developer/access_policies");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<AccessPolicySummary>>(json) ?? new List<AccessPolicySummary>();
        }

        public async Task<AccessPolicyDetails> FetchAccessPolicyAsync(string policyId)
        {
            var response = await _httpClient.GetAsync($"/api/v1/developer/access_policies/{policyId}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AccessPolicyDetails>(json) ?? throw new InvalidOperationException("Failed to deserialize access policy details.");
        }

        public async Task CreateAccessPolicyAsync(string name, List<string> resourceIds, string scheduleId)
        {
            var payload = new
            {
                name = name,
                resources = resourceIds,
                schedule_id = scheduleId
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/v1/developer/access_policies", content);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAccessPolicyAsync(string policyId)
        {
            var response = await _httpClient.DeleteAsync($"/api/v1/developer/access_policies/{policyId}");

            response.EnsureSuccessStatusCode();
        }

        // Credentials
        public async Task<string> GeneratePinCodeAsync()
        {
            var response = await _httpClient.PostAsync("/api/v1/developer/credentials/pin_codes", null);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<string>(json) ?? throw new InvalidOperationException("Failed to deserialize PIN code.");
        }

        public async Task EnrollNfcCardAsync(string deviceId)
        {
            var payload = new { device_id = deviceId };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/v1/developer/credentials/nfc_cards/sessions", content);

            response.EnsureSuccessStatusCode();
        }

        public async Task<NfcCardDetails> FetchNfcCardAsync(string cardToken)
        {
            var response = await _httpClient.GetAsync($"/api/v1/developer/credentials/nfc_cards/{cardToken}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<NfcCardDetails>(json) ?? throw new InvalidOperationException("Failed to deserialize NFC card details.");
        }

        public async Task<List<NfcCardSummary>> FetchAllNfcCardsAsync(int? pageNum = null, int? pageSize = null)
        {
            var query = new List<string>();
            if (pageNum.HasValue) query.Add($"page_num={pageNum.Value}");
            if (pageSize.HasValue) query.Add($"page_size={pageSize.Value}");

            var queryString = query.Count > 0 ? "?" + string.Join("&", query) : string.Empty;
            var response = await _httpClient.GetAsync($"/api/v1/developer/credentials/nfc_cards{queryString}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<NfcCardSummary>>(json) ?? new List<NfcCardSummary>();
        }

        public async Task DeleteNfcCardAsync(string cardToken)
        {
            var response = await _httpClient.DeleteAsync($"/api/v1/developer/credentials/nfc_cards/{cardToken}");

            response.EnsureSuccessStatusCode();
        }

        // Space Management
        public async Task<List<DoorGroupSummary>> FetchAllDoorGroupsAsync()
        {
            var response = await _httpClient.GetAsync("/api/v1/developer/door_groups");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<DoorGroupSummary>>(json) ?? new List<DoorGroupSummary>();
        }

        public async Task<DoorGroupDetails> FetchDoorGroupAsync(string doorGroupId)
        {
            var response = await _httpClient.GetAsync($"/api/v1/developer/door_groups/{doorGroupId}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<DoorGroupDetails>(json) ?? throw new InvalidOperationException("Failed to deserialize door group details.");
        }

        public async Task CreateDoorGroupAsync(string name, List<string> resourceIds)
        {
            var payload = new
            {
                group_name = name,
                resources = resourceIds
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/v1/developer/door_groups", content);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteDoorGroupAsync(string doorGroupId)
        {
            var response = await _httpClient.DeleteAsync($"/api/v1/developer/door_groups/{doorGroupId}");

            response.EnsureSuccessStatusCode();
        }

        public async Task<List<DoorSummary>> FetchAllDoorsAsync()
        {
            var response = await _httpClient.GetAsync("/api/v1/developer/doors");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<DoorSummary>>(json) ?? new List<DoorSummary>();
        }

        public async Task<DoorDetails> FetchDoorAsync(string doorId)
        {
            var response = await _httpClient.GetAsync($"/api/v1/developer/doors/{doorId}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<DoorDetails>(json) ?? throw new InvalidOperationException("Failed to deserialize door details.");
        }

        public async Task UnlockDoorAsync(string doorId)
        {
            var response = await _httpClient.PutAsync($"/api/v1/developer/doors/{doorId}/unlock", null);

            response.EnsureSuccessStatusCode();
        }

        // Device Management
        public async Task<List<DeviceSummary>> FetchAllDevicesAsync()
        {
            var response = await _httpClient.GetAsync("/api/v1/developer/devices");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<DeviceSummary>>(json) ?? new List<DeviceSummary>();
        }

        // System Logs
        public async Task<List<SystemLogEntry>> FetchSystemLogsAsync(string topic, long? since = null, long? until = null, string? actorId = null)
        {
            var payload = new
            {
                topic = topic,
                since = since,
                until = until,
                actor_id = actorId
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/v1/developer/system/logs", content);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<SystemLogEntry>>(json) ?? new List<SystemLogEntry>();
        }

        public async Task<byte[]> ExportSystemLogsAsync(string topic, long since, long until, string timezone)
        {
            var payload = new
            {
                topic = topic,
                since = since,
                until = until,
                timezone = timezone
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/v1/developer/system/logs/export", content);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsByteArrayAsync();
        }

        // UniFi Identity
        public async Task SendIdentityInvitationsAsync(List<IdentityInvitation> invitations)
        {
            var payload = invitations.Select(invitation => new
            {
                user_id = invitation.UserId,
                email = invitation.Email
            });

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/v1/developer/users/identity/invitations", content);

            response.EnsureSuccessStatusCode();
        }

        public async Task<List<IdentityResource>> FetchAvailableResourcesAsync(string? resourceType = null)
        {
            var query = string.IsNullOrEmpty(resourceType) ? string.Empty : $"?resource_type={resourceType}";
            var response = await _httpClient.GetAsync($"/api/v1/developer/users/identity/assignments{query}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<IdentityResource>>(json) ?? new List<IdentityResource>();
        }

        public async Task AssignResourcesToUserAsync(string userId, string resourceType, List<string> resourceIds)
        {
            var payload = new
            {
                resource_type = resourceType,
                resource_ids = resourceIds
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"/api/v1/developer/users/{userId}/identity/assignments", content);

            response.EnsureSuccessStatusCode();
        }

        public async Task<List<IdentityResource>> FetchResourcesAssignedToUserAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"/api/v1/developer/users/{userId}/identity/assignments");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<IdentityResource>>(json) ?? new List<IdentityResource>();
        }

        public async Task AssignResourcesToUserGroupAsync(string groupId, string resourceType, List<string> resourceIds)
        {
            var payload = new
            {
                resource_type = resourceType,
                resource_ids = resourceIds
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"/api/v1/developer/user_groups/{groupId}/identity/assignments", content);

            response.EnsureSuccessStatusCode();
        }

        public async Task<List<IdentityResource>> FetchResourcesAssignedToUserGroupAsync(string groupId)
        {
            var response = await _httpClient.GetAsync($"/api/v1/developer/user_groups/{groupId}/identity/assignments");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<IdentityResource>>(json) ?? new List<IdentityResource>();
        }
    }
}