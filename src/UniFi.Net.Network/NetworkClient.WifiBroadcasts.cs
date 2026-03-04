using UniFi.Net.Network.Filters;
using UniFi.Net.Network.Models;

namespace UniFi.Net.Network;

public partial class NetworkClient
{
    /// <inheritdoc />
    public Task<PagedResponse<WifiBroadcastSummary>> ListWifiBroadcasts(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/wifi/broadcasts";
        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<WifiBroadcastSummary>>(path + query, cancellationToken);
    }

    /// <inheritdoc />
    public Task<WifiBroadcast> GetWifiBroadcast(Guid siteId, Guid wifiBroadcastId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/wifi/broadcasts/{wifiBroadcastId}";

        return GetFromJsonAsync<WifiBroadcast>(path, cancellationToken);
    }

    /// <inheritdoc />
    public Task<WifiBroadcast> CreateWifiBroadcast(Guid siteId, CreateWifiBroadcastRequest request, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/wifi/broadcasts";

        return PostAsJsonAsync<WifiBroadcast>(path, request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<WifiBroadcast> UpdateWifiBroadcast(Guid siteId, Guid wifiBroadcastId, UpdateWifiBroadcastRequest request, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/wifi/broadcasts/{wifiBroadcastId}";

        return PutAsJsonAsync<WifiBroadcast>(path, request, cancellationToken);
    }

    /// <inheritdoc />
    public Task DeleteWifiBroadcast(Guid siteId, Guid wifiBroadcastId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/wifi/broadcasts/{wifiBroadcastId}";

        return DeleteAsync(path, cancellationToken);
    }
}
