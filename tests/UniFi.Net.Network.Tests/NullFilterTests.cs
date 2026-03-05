using UniFi.Net.Network.Filters;

namespace UniFi.Net.Network.Tests;

[Trait("Category", "Unit")]
public class NullFilterTests
{
    /// <summary>
    /// Given a field name and optional negation,
    /// When a <see cref="NullFilter"/> is converted to a string,
    /// Then the output is an "isNull" or "isNotNull" expression for the field.
    /// </summary>
    [Theory]
    [InlineData("name", "name.isNull()")]
    [InlineData("name", "name.isNotNull()", true)]
    public void IsNullFilter(string field, string expected, bool not = false)
    {
        var filter = new NullFilter(field)
        {
            Not = not,
        };

        Assert.Equal(expected, filter.ToString());
    }
}
