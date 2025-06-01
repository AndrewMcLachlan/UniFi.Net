using UniFi.Net.Models;

namespace UniFi.Net;

/// <summary>
/// The UniFi Client.
/// </summary>
public interface IUniFiClient
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
}
