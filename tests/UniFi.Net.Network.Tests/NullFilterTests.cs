using UniFi.Net.Network.Filters;

namespace UniFi.Net.Network.Tests;
public class NullFilterTests
{
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
