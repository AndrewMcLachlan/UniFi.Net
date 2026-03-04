using UniFi.Net.Network.Filters;
using UniFi.Net.Network.Models;

namespace UniFi.Net.Network;

public partial class NetworkClient
{
    /// <inheritdoc />
    public Task<PagedResponse<Site>> ListSites(IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites";
        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<Site>>(path + query, cancellationToken);
    }
}
