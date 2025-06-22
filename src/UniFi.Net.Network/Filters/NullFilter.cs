namespace UniFi.Net.Network.Filters;

/// <summary>
/// Filters by whether a value is null or not.
/// </summary>
public class NullFilter(string field, bool not = false) : Filter(field)
{
    /// <summary>
    /// Gets or sets a value indicating whether the condition is negated.
    /// </summary>
    public bool Not { get; init; } = not;

    /// <inheritdoc />
    protected override string Operator => Not ? "isNotNull" : "isNull";
}
