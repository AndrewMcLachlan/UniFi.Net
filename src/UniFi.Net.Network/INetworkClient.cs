using UniFi.Net.Network.Filters;
using UniFi.Net.Network.Models;

namespace UniFi.Net.Network;

/// <summary>
/// The UniFi Network API client.
/// </summary>
public interface INetworkClient
{
    #region Info

    /// <summary>
    /// Gets the application information.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>Application information.</returns>
    Task<ApplicationInfo> GetApplicationInfo(CancellationToken cancellationToken = default);

    #endregion

    #region Sites

    /// <summary>
    /// Gets a list of sites.
    /// </summary>
    /// <param name="filter">A filter expression.</param>
    /// <param name="offset">The number of sites to offset.</param>
    /// <param name="limit">The maximum number of sites to return (max 200).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of sites.</returns>
    Task<PagedResponse<Site>> ListSites(IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    #endregion

    #region Devices

    /// <summary>
    /// Gets a list of devices.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="filter">A filter expression.</param>
    /// <param name="offset">The number of devices to offset.</param>
    /// <param name="limit">The maximum number of devices to return (max 200).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of devices.</returns>
    Task<PagedResponse<DeviceSummary>> ListDevices(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific device by its ID.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="deviceId">The device ID.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>Device details.</returns>
    Task<Device> GetDevice(Guid siteId, Guid deviceId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Power cycles a port on a device.
    /// </summary>
    /// <param name="portIdx">The index of the port on which the action will be performed.</param>
    /// <param name="siteId">The unique identifier of the site where the device is located.</param>
    /// <param name="deviceId">The unique identifier of the device associated with the port.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task PowerCyclePort(int portIdx, Guid siteId, Guid deviceId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Restarts a device.
    /// </summary>
    /// <param name="siteId">The unique identifier of the site containing the device.</param>
    /// <param name="deviceId">The unique identifier of the device to restart.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task RestartDevice(Guid siteId, Guid deviceId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adopts a device to a site.
    /// </summary>
    /// <param name="siteId">The unique identifier of the site to adopt the device to.</param>
    /// <param name="macAddress">The MAC address of the device to adopt.</param>
    /// <param name="ignoreDeviceLimit">If true, ignores the device limit for the site.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The adopted device.</returns>
    Task<Device> AdoptDevice(Guid siteId, string macAddress, bool? ignoreDeviceLimit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes a device from a site.
    /// </summary>
    /// <param name="siteId">The unique identifier of the site containing the device.</param>
    /// <param name="deviceId">The unique identifier of the device to remove.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task RemoveDevice(Guid siteId, Guid deviceId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the latest statistics for a device.
    /// </summary>
    /// <param name="siteId">The unique identifier of the site containing the device.</param>
    /// <param name="deviceId">The unique identifier of the device.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The latest device statistics.</returns>
    Task<DeviceStatistics> GetDeviceStatistics(Guid siteId, Guid deviceId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a list of pending devices awaiting adoption.
    /// </summary>
    /// <param name="filter">A filter expression.</param>
    /// <param name="offset">The number of devices to offset.</param>
    /// <param name="limit">The maximum number of devices to return (max 200).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of pending devices.</returns>
    Task<PagedResponse<PendingDevice>> ListPendingDevices(IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    #endregion

    #region Clients

    /// <summary>
    /// Gets a list of clients.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="filter">A filter expression.</param>
    /// <param name="offset">The number of clients to offset.</param>
    /// <param name="limit">The maximum number of clients to return (max 200).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of clients.</returns>
    Task<PagedResponse<Client>> ListClients(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific client by its ID.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="clientId">The client ID.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>Client details.</returns>
    Task<Client> GetClient(Guid siteId, Guid clientId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Authorizes guest access for a client.
    /// </summary>
    /// <param name="siteId">The unique identifier of the site where the client resides.</param>
    /// <param name="clientId">The unique identifier of the client.</param>
    /// <param name="timeLimitMinutes">An optional time limit, in minutes. If null, no time limit is applied.</param>
    /// <param name="dataUsageLimitMBytes">An optional data usage limit, in megabytes. If null, no data usage limit is applied.</param>
    /// <param name="rxRateLimitKbps">An optional receive rate limit, in kilobits per second. If null, no receive rate limit is applied.</param>
    /// <param name="txRateLimitKbps">An optional transmit rate limit, in kilobits per second. If null, no transmit rate limit is applied.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The authorization response with details about revoked and granted authorizations.</returns>
    Task<AuthorizeClientGuestAccessResponse> AuthorizeClientGuestAccess(Guid siteId, Guid clientId, long? timeLimitMinutes = null, long? dataUsageLimitMBytes = null, long? rxRateLimitKbps = null, long? txRateLimitKbps = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Unauthorizes guest access for a client.
    /// </summary>
    /// <param name="siteId">The unique identifier of the site where the client resides.</param>
    /// <param name="clientId">The unique identifier of the client.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UnauthorizeClientGuestAccess(Guid siteId, Guid clientId, CancellationToken cancellationToken = default);

    #endregion

    #region Vouchers

    /// <summary>
    /// Retrieves a paginated list of vouchers for the specified site.
    /// </summary>
    /// <param name="siteId">The unique identifier of the site for which vouchers are being retrieved.</param>
    /// <param name="filter">Optional filter criteria to narrow down the results.</param>
    /// <param name="offset">The zero-based index of the first voucher to retrieve.</param>
    /// <param name="limit">The maximum number of vouchers to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A paged response of vouchers.</returns>
    Task<PagedResponse<Voucher>> ListVouchers(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Generates a collection of vouchers for a specified site.
    /// </summary>
    /// <param name="siteId">The unique identifier of the site for which the vouchers are being generated.</param>
    /// <param name="name">The name or label associated with the generated vouchers.</param>
    /// <param name="authorizedGuestLimit">The maximum number of guests authorized to use the vouchers.</param>
    /// <param name="timeLimitMinutes">The time limit, in minutes, for which the vouchers are valid.</param>
    /// <param name="count">The number of vouchers to generate.</param>
    /// <param name="dataUsageLimitMBytes">The maximum data usage limit, in megabytes, for each voucher.</param>
    /// <param name="rxRateLimitKbps">The maximum download rate limit, in kilobits per second, for each voucher.</param>
    /// <param name="txRateLimitKbps">The maximum upload rate limit, in kilobits per second, for each voucher.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The generated vouchers.</returns>
    Task<IEnumerable<Voucher>> GenerateVouchers(Guid siteId, string name, long authorizedGuestLimit, long timeLimitMinutes, int? count = null, long? dataUsageLimitMBytes = null, long? rxRateLimitKbps = null, long? txRateLimitKbps = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes vouchers associated with the specified site that match the given filter criteria.
    /// </summary>
    /// <param name="siteId">The unique identifier of the site whose vouchers are to be deleted.</param>
    /// <param name="filter">The filter criteria used to determine which vouchers to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The total number of vouchers deleted.</returns>
    Task<long> DeleteVouchers(Guid siteId, IFilter filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a voucher associated with the specified site and voucher identifiers.
    /// </summary>
    /// <param name="siteId">The unique identifier of the site to which the voucher belongs.</param>
    /// <param name="voucherId">The unique identifier of the voucher to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The voucher.</returns>
    Task<Voucher> GetVoucher(Guid siteId, Guid voucherId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a specific voucher associated with the specified site.
    /// </summary>
    /// <param name="siteId">The unique identifier of the site whose vouchers are to be deleted.</param>
    /// <param name="voucherId">The unique identifier of the voucher to be deleted.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The total number of vouchers deleted.</returns>
    Task<long> DeleteVoucher(Guid siteId, Guid voucherId, CancellationToken cancellationToken = default);

    #endregion

    #region Networks

    /// <summary>
    /// Gets a list of networks for a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="filter">A filter expression.</param>
    /// <param name="offset">The number of networks to offset.</param>
    /// <param name="limit">The maximum number of networks to return (max 200).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of networks.</returns>
    Task<PagedResponse<SiteNetwork>> ListNetworks(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific network by its ID.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="networkId">The network ID.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The network.</returns>
    Task<SiteNetwork> GetNetwork(Guid siteId, Guid networkId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new network on a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="request">The network creation request.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The created network.</returns>
    Task<SiteNetwork> CreateNetwork(Guid siteId, CreateNetworkRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing network on a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="networkId">The network ID.</param>
    /// <param name="request">The network update request.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The updated network.</returns>
    Task<SiteNetwork> UpdateNetwork(Guid siteId, Guid networkId, UpdateNetworkRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a network from a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="networkId">The network ID.</param>
    /// <param name="force">If true, forces the deletion even if the network has references.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteNetwork(Guid siteId, Guid networkId, bool? force = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the references for a network.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="networkId">The network ID.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The network references.</returns>
    Task<NetworkReferences> GetNetworkReferences(Guid siteId, Guid networkId, CancellationToken cancellationToken = default);

    #endregion

    #region WiFi Broadcasts

    /// <summary>
    /// Gets a list of WiFi broadcasts for a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="filter">A filter expression.</param>
    /// <param name="offset">The number of broadcasts to offset.</param>
    /// <param name="limit">The maximum number of broadcasts to return (max 200).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of WiFi broadcast summaries.</returns>
    Task<PagedResponse<WifiBroadcastSummary>> ListWifiBroadcasts(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific WiFi broadcast by its ID.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="wifiBroadcastId">The WiFi broadcast ID.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The WiFi broadcast.</returns>
    Task<WifiBroadcast> GetWifiBroadcast(Guid siteId, Guid wifiBroadcastId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new WiFi broadcast on a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="request">The WiFi broadcast creation request.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The created WiFi broadcast.</returns>
    Task<WifiBroadcast> CreateWifiBroadcast(Guid siteId, CreateWifiBroadcastRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing WiFi broadcast on a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="wifiBroadcastId">The WiFi broadcast ID.</param>
    /// <param name="request">The WiFi broadcast update request.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The updated WiFi broadcast.</returns>
    Task<WifiBroadcast> UpdateWifiBroadcast(Guid siteId, Guid wifiBroadcastId, UpdateWifiBroadcastRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a WiFi broadcast from a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="wifiBroadcastId">The WiFi broadcast ID.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteWifiBroadcast(Guid siteId, Guid wifiBroadcastId, CancellationToken cancellationToken = default);

    #endregion

    #region Firewall Zones

    /// <summary>
    /// Gets a list of firewall zones for a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="filter">A filter expression.</param>
    /// <param name="offset">The number of zones to offset.</param>
    /// <param name="limit">The maximum number of zones to return (max 200).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of firewall zones.</returns>
    Task<PagedResponse<FirewallZone>> ListFirewallZones(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific firewall zone by its ID.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="zoneId">The firewall zone ID.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The firewall zone.</returns>
    Task<FirewallZone> GetFirewallZone(Guid siteId, Guid zoneId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new firewall zone on a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="request">The firewall zone creation request.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The created firewall zone.</returns>
    Task<FirewallZone> CreateFirewallZone(Guid siteId, CreateFirewallZoneRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing firewall zone on a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="zoneId">The firewall zone ID.</param>
    /// <param name="request">The firewall zone update request.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The updated firewall zone.</returns>
    Task<FirewallZone> UpdateFirewallZone(Guid siteId, Guid zoneId, UpdateFirewallZoneRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a firewall zone from a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="zoneId">The firewall zone ID.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteFirewallZone(Guid siteId, Guid zoneId, CancellationToken cancellationToken = default);

    #endregion

    #region Firewall Policies

    /// <summary>
    /// Gets a list of firewall policies for a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="filter">A filter expression.</param>
    /// <param name="offset">The number of policies to offset.</param>
    /// <param name="limit">The maximum number of policies to return (max 200).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of firewall policies.</returns>
    Task<PagedResponse<FirewallPolicy>> ListFirewallPolicies(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific firewall policy by its ID.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="policyId">The firewall policy ID.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The firewall policy.</returns>
    Task<FirewallPolicy> GetFirewallPolicy(Guid siteId, Guid policyId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new firewall policy on a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="request">The firewall policy creation request.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The created firewall policy.</returns>
    Task<FirewallPolicy> CreateFirewallPolicy(Guid siteId, CreateFirewallPolicyRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing firewall policy on a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="policyId">The firewall policy ID.</param>
    /// <param name="request">The firewall policy update request.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The updated firewall policy.</returns>
    Task<FirewallPolicy> UpdateFirewallPolicy(Guid siteId, Guid policyId, UpdateFirewallPolicyRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Patches an existing firewall policy on a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="policyId">The firewall policy ID.</param>
    /// <param name="request">The firewall policy patch request.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The patched firewall policy.</returns>
    Task<FirewallPolicy> PatchFirewallPolicy(Guid siteId, Guid policyId, PatchFirewallPolicyRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a firewall policy from a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="policyId">The firewall policy ID.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteFirewallPolicy(Guid siteId, Guid policyId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the ordering of firewall policies for a site, filtered by source and destination zones.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="sourceFirewallZoneId">The source firewall zone ID.</param>
    /// <param name="destinationFirewallZoneId">The destination firewall zone ID.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The firewall policy ordering.</returns>
    Task<FirewallPolicyOrdering> GetFirewallPolicyOrdering(Guid siteId, Guid sourceFirewallZoneId, Guid destinationFirewallZoneId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the ordering of firewall policies for a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="request">The firewall policy ordering update request.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The updated firewall policy ordering.</returns>
    Task<FirewallPolicyOrdering> UpdateFirewallPolicyOrdering(Guid siteId, UpdateFirewallPolicyOrderingRequest request, CancellationToken cancellationToken = default);

    #endregion

    #region ACL Rules

    /// <summary>
    /// Gets a list of ACL rules for a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="filter">A filter expression.</param>
    /// <param name="offset">The number of rules to offset.</param>
    /// <param name="limit">The maximum number of rules to return (max 200).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of ACL rules.</returns>
    Task<PagedResponse<AclRule>> ListAclRules(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific ACL rule by its ID.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="ruleId">The ACL rule ID.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The ACL rule.</returns>
    Task<AclRule> GetAclRule(Guid siteId, Guid ruleId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new ACL rule on a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="request">The ACL rule creation request.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The created ACL rule.</returns>
    Task<AclRule> CreateAclRule(Guid siteId, CreateAclRuleRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing ACL rule on a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="ruleId">The ACL rule ID.</param>
    /// <param name="request">The ACL rule update request.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The updated ACL rule.</returns>
    Task<AclRule> UpdateAclRule(Guid siteId, Guid ruleId, UpdateAclRuleRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an ACL rule from a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="ruleId">The ACL rule ID.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteAclRule(Guid siteId, Guid ruleId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the ordering of ACL rules for a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The ACL rule ordering.</returns>
    Task<AclRuleOrdering> GetAclRuleOrdering(Guid siteId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the ordering of ACL rules for a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="request">The ACL rule ordering update request.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The updated ACL rule ordering.</returns>
    Task<AclRuleOrdering> UpdateAclRuleOrdering(Guid siteId, UpdateAclRuleOrderingRequest request, CancellationToken cancellationToken = default);

    #endregion

    #region DNS Policies

    /// <summary>
    /// Gets a list of DNS policies for a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="filter">A filter expression.</param>
    /// <param name="offset">The number of policies to offset.</param>
    /// <param name="limit">The maximum number of policies to return (max 200).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of DNS policies.</returns>
    Task<PagedResponse<DnsPolicy>> ListDnsPolicies(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific DNS policy by its ID.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="policyId">The DNS policy ID.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The DNS policy.</returns>
    Task<DnsPolicy> GetDnsPolicy(Guid siteId, Guid policyId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new DNS policy on a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="request">The DNS policy creation request.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The created DNS policy.</returns>
    Task<DnsPolicy> CreateDnsPolicy(Guid siteId, CreateDnsPolicyRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing DNS policy on a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="policyId">The DNS policy ID.</param>
    /// <param name="request">The DNS policy update request.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The updated DNS policy.</returns>
    Task<DnsPolicy> UpdateDnsPolicy(Guid siteId, Guid policyId, UpdateDnsPolicyRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a DNS policy from a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="policyId">The DNS policy ID.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteDnsPolicy(Guid siteId, Guid policyId, CancellationToken cancellationToken = default);

    #endregion

    #region Traffic Matching Lists

    /// <summary>
    /// Gets a list of traffic matching lists for a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="filter">A filter expression.</param>
    /// <param name="offset">The number of lists to offset.</param>
    /// <param name="limit">The maximum number of lists to return (max 200).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of traffic matching lists.</returns>
    Task<PagedResponse<TrafficMatchingList>> ListTrafficMatchingLists(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific traffic matching list by its ID.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="listId">The traffic matching list ID.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The traffic matching list.</returns>
    Task<TrafficMatchingList> GetTrafficMatchingList(Guid siteId, Guid listId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new traffic matching list on a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="request">The traffic matching list creation request.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The created traffic matching list.</returns>
    Task<TrafficMatchingList> CreateTrafficMatchingList(Guid siteId, CreateTrafficMatchingListRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing traffic matching list on a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="listId">The traffic matching list ID.</param>
    /// <param name="request">The traffic matching list update request.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The updated traffic matching list.</returns>
    Task<TrafficMatchingList> UpdateTrafficMatchingList(Guid siteId, Guid listId, UpdateTrafficMatchingListRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a traffic matching list from a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="listId">The traffic matching list ID.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteTrafficMatchingList(Guid siteId, Guid listId, CancellationToken cancellationToken = default);

    #endregion

    #region Supporting Resources

    /// <summary>
    /// Gets a list of WAN interfaces for a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="offset">The number of interfaces to offset.</param>
    /// <param name="limit">The maximum number of interfaces to return (max 200).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of WAN interfaces.</returns>
    Task<PagedResponse<WanInterface>> ListWanInterfaces(Guid siteId, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a list of site-to-site VPN tunnels for a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="filter">A filter expression.</param>
    /// <param name="offset">The number of tunnels to offset.</param>
    /// <param name="limit">The maximum number of tunnels to return (max 200).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of VPN tunnels.</returns>
    Task<PagedResponse<VpnTunnel>> ListSiteToSiteVpnTunnels(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a list of VPN servers for a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="filter">A filter expression.</param>
    /// <param name="offset">The number of servers to offset.</param>
    /// <param name="limit">The maximum number of servers to return (max 200).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of VPN servers.</returns>
    Task<PagedResponse<VpnServer>> ListVpnServers(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a list of RADIUS profiles for a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="filter">A filter expression.</param>
    /// <param name="offset">The number of profiles to offset.</param>
    /// <param name="limit">The maximum number of profiles to return (max 200).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of RADIUS profiles.</returns>
    Task<PagedResponse<RadiusProfile>> ListRadiusProfiles(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a list of device tags for a site.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="filter">A filter expression.</param>
    /// <param name="offset">The number of tags to offset.</param>
    /// <param name="limit">The maximum number of tags to return (max 200).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of device tags.</returns>
    Task<PagedResponse<DeviceTag>> ListDeviceTags(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a list of DPI categories.
    /// </summary>
    /// <param name="filter">A filter expression.</param>
    /// <param name="offset">The number of categories to offset.</param>
    /// <param name="limit">The maximum number of categories to return (max 200).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of DPI categories.</returns>
    Task<PagedResponse<DpiCategory>> ListDpiCategories(IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a list of DPI applications.
    /// </summary>
    /// <param name="filter">A filter expression.</param>
    /// <param name="offset">The number of applications to offset.</param>
    /// <param name="limit">The maximum number of applications to return (max 200).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of DPI applications.</returns>
    Task<PagedResponse<DpiApplication>> ListDpiApplications(IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a list of countries.
    /// </summary>
    /// <param name="filter">A filter expression.</param>
    /// <param name="offset">The number of countries to offset.</param>
    /// <param name="limit">The maximum number of countries to return (max 200).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of countries.</returns>
    Task<PagedResponse<Country>> ListCountries(IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    #endregion
}
