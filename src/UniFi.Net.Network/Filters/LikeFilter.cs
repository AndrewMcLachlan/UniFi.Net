namespace UniFi.Net.Network.Filters;

/// <summary>
/// Represents a filter that checks whether a string value matches a specified pattern.
/// </summary>
/// <remarks>This filter uses the "like" operator to perform pattern matching on string values. It is typically
/// used in scenarios where partial or wildcard matching is required.</remarks>
public class LikeFilter(string field, string value, bool not = false) : SingleValueFilter<string>(field, value)
{
    /// <summary>
    /// Gets a value indicating whether the condition is negated.
    /// </summary>
    public bool Not { get; init; } = not;

    ///<inheritdoc/>
    protected override string Operator => "like";

    private string NotStart => Not ? "not(" : string.Empty;
    private string NotEnd => Not ? ")" : string.Empty;

    /// <summary>
    /// Returns a string representation of the filter in the format "Field.Operator(Value)".
    /// </summary>
    /// <returns></returns>
    public override string ToString() =>
        $"{NotStart}{Field}.{Operator}({GetValueString(Value)}){NotEnd}";
}
