namespace UniFi.Net.Network.Filters;

/// <summary>
/// Base class for filters used in UniFi Network API queries.
/// </summary>
public abstract class Filter(string field) : IFilter
{
    /// <summary>
    /// Gets or sets the name of the field.
    /// </summary>
    public string Field { get; } = field;

    /// <summary>
    /// Gets the operator used in the filter.
    /// </summary>
    protected abstract string Operator { get; }

    /// <summary>
    /// Returns a string representation of the current instance, including the field and operator values.
    /// </summary>
    /// <remarks>The returned string is formatted as "<c>Field.Operator()</c>", where <c>Field</c> and
    /// <c>Operator</c> represent the respective values of the instance. This method is useful for debugging or logging
    /// purposes.</remarks>
    /// <returns>A string that represents the current instance in the format "<c>Field.Operator()</c>".</returns>
    public override string ToString() => $"{Field}.{Operator}()";
}
