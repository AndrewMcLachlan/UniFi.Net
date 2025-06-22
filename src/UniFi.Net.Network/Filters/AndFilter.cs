namespace UniFi.Net.Network.Filters;

/// <summary>
/// Represents a logical "AND" filter that combines multiple filters.
/// </summary>
/// <remarks>This filter evaluates to <see langword="true"/> only if all the provided filters evaluate to <see
/// langword="true"/>. Use this filter to enforce that multiple conditions must be satisfied simultaneously.</remarks>
/// <param name="filters">The filters to be combined.</param>
public class AndFilter(params Filter[] filters) : LogicalOperatorFilter(filters)
{
    /// <inheritdoc />
    protected override string Operator => "and";
}
