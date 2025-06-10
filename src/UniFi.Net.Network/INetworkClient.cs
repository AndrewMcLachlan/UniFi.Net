using UniFi.Net.Network.Models;

namespace UniFi.Net.Network;

/// <summary>
/// The UniFi Client.
/// </summary>
public interface INetworkClient
{
    /// <summary>
    /// Gets the application information.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>Application information.</returns>
    Task<ApplicationInfo> GetApplicationInfo(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a list of sites.
    /// </summary>
    /// <param name="filter">A filter expression.</param>
    /// <param name="offset">The number of sites to offset.</param>
    /// <param name="limit">The maximum number of sites to return (max 200).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of sites.</returns>
    Task<PagedResponse<Site>> ListSites(string? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a list of devices.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="filter">A filter expression.</param>
    /// <param name="offset">The number of devices to offset.</param>
    /// <param name="limit">The maximum number of devices to return (max 200).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of devices.</returns>
    Task<PagedResponse<DeviceSummary>> ListDevices(Guid siteId, string? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific device by its ID.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="deviceId">The device ID.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>Device details.</returns>
    Task<Device> GetDevice(Guid siteId, Guid deviceId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the specified action on a port associated with a device at a given site.
    /// </summary>
    /// <remarks>This method performs an action on a specific port of a device. Ensure that the provided
    /// identifiers for the site and device are valid and that the port index corresponds to an existing port on the
    /// device.</remarks>
    /// <param name="portIdx">The index of the port on which the action will be performed. Must be a valid port index.</param>
    /// <param name="siteId">The unique identifier of the site where the device is located.</param>
    /// <param name="deviceId">The unique identifier of the device associated with the port.</param>
    /// <param name="action">The action to be performed on the port. This determines the operation to execute.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task ExecutePortAction(int portIdx, Guid siteId, Guid deviceId, PortAction action, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the specified action on a device within a site.
    /// </summary>
    /// <remarks>Use this method to perform actions such as restarting, updating, or configuring a device.
    /// Ensure that the <paramref name="siteId"/> and <paramref name="deviceId"/> are valid and correspond to an
    /// existing site and device.</remarks>
    /// <param name="siteId">The unique identifier of the site containing the device.</param>
    /// <param name="deviceId">The unique identifier of the device on which the action will be performed.</param>
    /// <param name="action">The action to execute on the device.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task ExecuteDeviceAction(Guid siteId, Guid deviceId, DeviceAction action, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a list of clients.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="filter">A filter expression.</param>
    /// <param name="offset">The number of clients to offset.</param>
    /// <param name="limit">The maximum number of clients to return (max 200).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of clients.</returns>
    Task<PagedResponse<Client>> ListClients(Guid siteId, string? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific client by its ID.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="clientId">The client ID.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>Client details.</returns>
    Task<Client> GetClient(Guid siteId, Guid clientId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes a specified action on a client within a site, with optional limits on time, data usage, and network
    /// rates.
    /// </summary>
    /// <remarks>Use this method to perform actions such as managing client connectivity or applying
    /// restrictions. Ensure that the provided limits, if specified, are appropriate for the intended action.</remarks>
    /// <param name="siteId">The unique identifier of the site where the client resides.</param>
    /// <param name="clientId">The unique identifier of the client on which the action will be executed.</param>
    /// <param name="action">The action to be performed on the client.</param>
    /// <param name="timeLimitMinutes">An optional time limit, in minutes, for the action. If null, no time limit is applied.</param>
    /// <param name="dataUsageLimitMBytes">An optional data usage limit, in megabytes, for the action. If null, no data usage limit is applied.</param>
    /// <param name="rxRateLimitKbps">An optional receive rate limit, in kilobits per second, for the client. If null, no receive rate limit is
    /// applied.</param>
    /// <param name="txRateLimitKbps">An optional transmit rate limit, in kilobits per second, for the client. If null, no transmit rate limit is
    /// applied.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see
    /// cref="ExecuteClientActionResponse"/> object with details about the outcome of the action.</returns>
    Task<ExecuteClientActionResponse> ExecuteClientAction(Guid siteId, Guid clientId, ClientAction action, long? timeLimitMinutes = null, long? dataUsageLimitMBytes = null, long? rxRateLimitKbps = null, long? txRateLimitKbps = null, CancellationToken cancellationToken = default);
}
