using UniFi.Net.Network.Models;

namespace UniFi.Net.Network;

public partial class NetworkClient
{
    /// <inheritdoc />
    public Task<ApplicationInfo> GetApplicationInfo(CancellationToken cancellationToken = default) =>
        GetFromJsonAsync<ApplicationInfo>($"{PathPrefix}info", cancellationToken);
}
