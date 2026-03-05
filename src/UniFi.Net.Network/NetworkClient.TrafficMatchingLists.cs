using UniFi.Net.Network.Filters;
using UniFi.Net.Network.Models;

namespace UniFi.Net.Network;

public partial class NetworkClient
{
    /// <inheritdoc />
    public Task<PagedResponse<TrafficMatchingList>> ListTrafficMatchingLists(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/traffic-matching-lists";
        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<TrafficMatchingList>>(path + query, cancellationToken);
    }

    /// <inheritdoc />
    public Task<TrafficMatchingList> GetTrafficMatchingList(Guid siteId, Guid listId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/traffic-matching-lists/{listId}";

        return GetFromJsonAsync<TrafficMatchingList>(path, cancellationToken);
    }

    /// <inheritdoc />
    public Task<TrafficMatchingList> CreateTrafficMatchingList(Guid siteId, CreateTrafficMatchingListRequest request, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/traffic-matching-lists";

        return PostAsJsonAsync<TrafficMatchingList>(path, request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<TrafficMatchingList> UpdateTrafficMatchingList(Guid siteId, Guid listId, UpdateTrafficMatchingListRequest request, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/traffic-matching-lists/{listId}";

        return PutAsJsonAsync<TrafficMatchingList>(path, request, cancellationToken);
    }

    /// <inheritdoc />
    public Task DeleteTrafficMatchingList(Guid siteId, Guid listId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/traffic-matching-lists/{listId}";

        return DeleteAsync(path, cancellationToken);
    }
}
