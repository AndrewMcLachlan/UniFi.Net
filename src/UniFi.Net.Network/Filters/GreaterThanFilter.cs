namespace UniFi.Net.Network.Filters;

/// <summary>
/// Represents a filter that matches values greater than a specified threshold.
/// </summary>
/// <remarks>This filter can be configured to include or exclude the threshold value in the comparison by setting
/// the <see cref="Inclusive"/> property.</remarks>
/// <typeparam name="T">The type of the value being filtered. Must be a non-nullable type.</typeparam>
public class GreaterThanFilter<T>(string field, T value, bool inclusive = false) : SingleValueFilter<T>(field, value)
    where T : notnull
{
    /// <summary>
    /// Gets or sets a value indicating whether the range is inclusive of its boundaries.
    /// </summary>
    public bool Inclusive { get; init; } = inclusive;

    /// <inheritdoc/>
    protected override string Operator => Inclusive ? "ge" : "gt";
}
