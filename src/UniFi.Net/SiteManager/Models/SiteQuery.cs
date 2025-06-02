namespace UniFi.Net.SiteManager.Models;
public record SiteQuery
{
    public required DateTimeOffset BeginTimestamp { get; init; }

    public required DateTimeOffset EndTimestamp { get; init; }

    public required string HostId { get; init; }

    public required string SiteId { get; init; }
}
