namespace UniFi.Net.Network.Filters;

/// <summary>
/// Represents a filter that checks whether a field's value is included in a specified collection.
/// </summary>
/// <remarks>This filter can be used to determine whether a field's value matches any of the values in the
/// provided collection. The filter supports negation, allowing the condition to check for values not in the
/// collection.</remarks>
/// <typeparam name="T">The type of elements in the collection. Must be a non-nullable type.</typeparam>
public class InFilter<T>(string field, IEnumerable<T> value) : ValueFilter<IEnumerable<T>>(field, value) where T : notnull
{
    /// <summary>
    /// Gets or sets a value indicating whether the condition is negated.
    /// </summary>
    public bool Not { get; init; } = false;

    /// <inheritdoc />
    protected override string Operator => Not ? "notIn" : "in";

    /// <summary>
    /// Returns a string representation of the filter in the format "Field.Operator(Value, Value, Value...)".
    /// </summary>
    /// <returns></returns>
    public override string ToString() =>
        $"{Field}.{Operator}({String.Join(", ", Value.Select(v => GetValueString(v)))})";


}
