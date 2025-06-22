namespace UniFi.Net.Network.Filters;

/// <summary>
/// Filters by a value.
/// </summary>
public abstract class ValueFilter<T>(string field, T value) : Filter(field) where T : notnull
{
    /// <summary>
    /// Gets the value to filter by.
    /// </summary>
    public T Value { get; } = value;

    /// <summary>
    /// Converts the given value to its string representation suitable for use in filtering operations.
    /// </summary>
    /// <remarks>The method formats the value based on its type:
    /// <list type="bullet">
    /// <item>
    /// <description>For
    /// <see cref="string"/> values, single quotes are added, and any single quotes within the string are
    /// escaped.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// For <see cref="bool"/> values, <see langword="true"/> and
    /// <see langword="false"/> are returned as their string equivalents.</description> </item> <item> <description>For
    /// <see cref="DateTime"/> and <see cref="DateTimeOffset"/> values, the ISO 8601 format is used.</description>
    /// </item>
    /// <item>
    /// <description>For other types, the <see cref="object.ToString"/> method is used.</description>
    /// </item>
    /// </list>
    /// If the value is of an unsupported type or <paramref name="value"/>.ToString() returns <see langword="null"/>, an <see cref="InvalidOperationException"/> is thrown.
    /// </remarks>
    /// <returns>A string representation of the <see cref="Value"/> formatted according to its type.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the <paramref name="value"/> is of an unsupported type or if <paramref name="value"/>.ToString() returns <see
    /// langword="null"/>.</exception>
    protected string GetValueString(object value) =>
        value switch
        {
            string str => $"'{str.Replace("'", "''")}'",
            bool b => b ? "true" : "false",
            DateTimeOffset dt => dt.UtcDateTime.ToString("yyyy-MM-ddTHH:mm:ss'Z'", System.Globalization.CultureInfo.InvariantCulture),
            _ => Value.ToString() ?? throw new InvalidOperationException("Unsupported value type for ValueFilter.")
        };
}
