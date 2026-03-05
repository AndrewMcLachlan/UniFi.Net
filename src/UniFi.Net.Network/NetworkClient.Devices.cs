using System.Net.Http.Json;
using UniFi.Net.Network.Filters;
using UniFi.Net.Network.Models;

namespace UniFi.Net.Network;

public partial class NetworkClient
{
    /// <inheritdoc />
    public Task<PagedResponse<DeviceSummary>> ListDevices(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/devices";
        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<DeviceSummary>>(path + query, cancellationToken);
    }

    /// <inheritdoc />
    public Task<Device> GetDevice(Guid siteId, Guid deviceId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/devices/{deviceId}";

        return GetFromJsonAsync<Device>(path, cancellationToken);
    }

    /// <inheritdoc />
    public Task PowerCyclePort(int portIdx, Guid siteId, Guid deviceId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/devices/{deviceId}/interfaces/ports/{portIdx}/actions";
        var request = new HttpRequestMessage(HttpMethod.Post, path)
        {
            Content = JsonContent.Create(new { action = PortAction.PowerCycle })
        };

        return SendAsync(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task RestartDevice(Guid siteId, Guid deviceId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/devices/{deviceId}/actions";
        var request = new HttpRequestMessage(HttpMethod.Post, path)
        {
            Content = JsonContent.Create(new { action = DeviceAction.Restart })
        };

        return SendAsync(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<Device> AdoptDevice(Guid siteId, string macAddress, bool? ignoreDeviceLimit = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/devices";
        var requestBody = new
        {
            macAddress,
            ignoreDeviceLimit
        };

        return PostAsJsonAsync<Device>(path, requestBody, cancellationToken);
    }

    /// <inheritdoc />
    public Task RemoveDevice(Guid siteId, Guid deviceId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/devices/{deviceId}";

        return DeleteAsync(path, cancellationToken);
    }

    /// <inheritdoc />
    public Task<DeviceStatistics> GetDeviceStatistics(Guid siteId, Guid deviceId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/devices/{deviceId}/statistics/latest";

        return GetFromJsonAsync<DeviceStatistics>(path, cancellationToken);
    }

    /// <inheritdoc />
    public Task<PagedResponse<PendingDevice>> ListPendingDevices(IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}pending-devices";
        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<PendingDevice>>(path + query, cancellationToken);
    }
}
