
namespace UniFi.Net.Models;

/// <summary>
/// Represents a paged response from the UniFi API.
/// </summary>
/// <typeparam name="T">The type of data being returned.</typeparam>
/// <param name="Offset">The amount of records offset.</param>
/// <param name="Limit">The maximum amount returned.</param>
/// <param name="Count">The count of records returned.</param>
/// <param name="TotalCount">The total number of available records.</param>
/// <param name="Data">The data.</param>
public record PagedResponse<T>(
    int Offset,
    int Limit,
    int Count,
    int TotalCount,
    IReadOnlyList<T> Data
);