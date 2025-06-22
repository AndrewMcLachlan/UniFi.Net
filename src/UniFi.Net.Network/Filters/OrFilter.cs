namespace UniFi.Net.Network.Filters;

/// <summary>
/// Or filter for combining multiple filters with a logical OR operation.
/// </summary>
/// <param name="filters">The filter inside the or</param>
public class OrFilter(params Filter[] filters) : LogicalOperatorFilter(filters)
{
    /// <inheritdoc/>
    protected override string Operator => "or";
}
