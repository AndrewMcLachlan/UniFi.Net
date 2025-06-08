namespace UniFi.Net.SiteManager.Models;

/// <summary>
/// A response model that wraps a single data item of type T.
/// </summary>
/// <typeparam name="T">The type of the data item.</typeparam>
public record DataResponse<T> : Response
{
    /// <summary>
    /// Gets the data item.
    /// </summary>
    public T Data { get; init; } = default!;
}

