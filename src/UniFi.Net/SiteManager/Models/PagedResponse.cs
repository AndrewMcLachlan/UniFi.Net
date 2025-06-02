namespace UniFi.Net.SiteManager.Models;

/// <summary>
/// A response model that wraps a paged list of data items of type T.
/// </summary>
public record PagedResponse<T> : DataResponse<IReadOnlyList<T>>
{
    /// <summary>
    /// Gets the next token for pagination.
    /// </summary>
    public string? NextToken { get; init; }
}
