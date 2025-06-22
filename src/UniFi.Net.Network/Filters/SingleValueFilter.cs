namespace UniFi.Net.Network.Filters;

/// <summary>
/// Represents a filter that operates on a single value for a specific field.
/// </summary>
/// <remarks>This abstract class provides functionality for filtering operations based on a single value. Derived
/// classes should specify the field, operator, and value used in the filtering logic.</remarks>
/// <typeparam name="T">The type of the value being filtered. Must be a non-nullable type.</typeparam>
public abstract class SingleValueFilter<T>(string field, T value) : ValueFilter<T>(field, value)
    where T : notnull
{
    /// <summary>
    /// Returns a string representation of the filter in the format "Field.Operator(Value)".
    /// </summary>
    /// <returns>A string representation of the filter.</returns>
    public override string ToString() =>
        $"{Field}.{Operator}({GetValueString(Value)})";
}
