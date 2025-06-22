using UniFi.Net.Network.Filters;

namespace UniFi.Net.Network.Tests;

public class EqualityFilterTests
{
    [Theory]
    [InlineData("Test Device", "name.eq('Test Device')")]
    [InlineData("Test Device", "name.ne('Test Device')", true)]
    [InlineData("Someone's Device", "name.eq('Someone''s Device')")]
    public void EqualityFilter(string value, string expected, bool not = false)
    {
        var filter = new EqualityFilter<string>("name", value)
        {
            Not = not
        };

        var filterString = filter.ToString();

        Assert.Equal(expected, filterString);
    }

    [Theory]
    [InlineData("2025-01-01Z", "name.eq(2025-01-01T00:00:00Z)")]
    [InlineData("2025-01-01Z", "name.ne(2025-01-01T00:00:00Z)", true)]
    public void EqualityFilterDateTime(DateTimeOffset dateTimeOffset, string expected, bool not = false)
    {
        var filter = new EqualityFilter<DateTimeOffset>("name", dateTimeOffset)
        {
            Not = not
        };
        var filterString = filter.ToString();
        Assert.Equal(expected, filterString);
    }

    [Theory]
    [InlineData(42, "name.eq(42)")]
    [InlineData(42, "name.ne(42)", true)]
    public void EqualityFilterInteger(int value, string expected, bool not = false)
    {
        var filter = new EqualityFilter<int>("name", value)
        {
            Not = not
        };
        var filterString = filter.ToString();
        Assert.Equal(expected, filterString);
    }

    [Theory]
    [InlineData("C3ACDE0E-645E-47B9-957F-A1F35AD8A0B6", "name.eq(c3acde0e-645e-47b9-957f-a1f35ad8a0b6)")]
    [InlineData("c3acde0e-645e-47b9-957f-a1f35ad8a0b6", "name.ne(c3acde0e-645e-47b9-957f-a1f35ad8a0b6)", true)]
    public void EqualityFilterGuid(Guid value, string expected, bool not = false)
    {
        var filter = new EqualityFilter<Guid>("name", value)
        {
            Not = not
        };
        var filterString = filter.ToString();
        Assert.Equal(expected, filterString);
    }
}
