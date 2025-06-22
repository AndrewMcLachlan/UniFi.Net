namespace UniFi.Net.Network.Filters;

/// <summary>
/// Check if a field is equal to or not equal to a value.
/// </summary>
/// <typeparam name="T">The type of the value to filter by.</typeparam>
public class EqualityFilter<T>(string field, T value, bool not = false) : SingleValueFilter<T>(field, value) where T : notnull
{
    /// <summary>
    /// Gets or sets a value indicating whether the condition is negated.
    /// </summary>
    public bool Not { get; init; } = not;

    /// <inheritdoc />
    protected override string Operator => Not ? "ne" : "eq";
}
