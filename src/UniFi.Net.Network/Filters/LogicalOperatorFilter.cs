namespace UniFi.Net.Network.Filters;

/// <summary>
/// Base class for logical operator filters that combine multiple filters using logical operators like AND or OR.
/// </summary>
public abstract class LogicalOperatorFilter : IFilter
{
    /// <summary>
    /// Gets the array of filters that this logical operator combines.
    /// </summary>
    public Filter[] Filters { get; }

    /// <summary>
    /// Gets the operator used in the logical filter (e.g., "and", "or").
    /// </summary>
    protected abstract string Operator { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LogicalOperatorFilter"/> class with the specified filters.
    /// </summary>
    /// <param name="filters">The collection of filters to be combined using a logical operator.  Must contain at least two filters and cannot
    /// be <see langword="null"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="filters"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown when fewer than two filters are provided in <paramref name="filters"/>.</exception>
    protected LogicalOperatorFilter(params Filter[] filters)
    {
        Filters = filters ?? throw new ArgumentNullException(nameof(filters), "Filters cannot be null.");
        if (filters.Length < 2)
        {
            throw new ArgumentException("At least two filters must be provided.", nameof(filters));
        }
    }

    /// <summary>
    /// Returns a string representation of the current object, including the operator and its associated filters.
    /// </summary>
    /// <remarks>The returned string is formatted as "<c>Operator(Filter1, Filter2, ...)</c>", where
    /// <c>Operator</c> is the operator and <c>Filter1, Filter2, ...</c> are the string representations of the filters.
    /// This method is useful for debugging or logging purposes to understand the state of the object.</remarks>
    /// <returns>A string that represents the current object, including the operator and its filters.</returns>
    public override string ToString() =>
        $"{Operator}({string.Join(", ", Filters.Select(f => f.ToString()))})";
}
