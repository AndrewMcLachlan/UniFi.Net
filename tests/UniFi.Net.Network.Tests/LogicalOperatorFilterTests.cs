using UniFi.Net.Network.Filters;

namespace UniFi.Net.Network.Tests;

[Trait("Category", "Unit")]
public class LogicalOperatorFilterTests
{
    /// <summary>
    /// Given two filters,
    /// When combined using an <see cref="AndFilter"/>,
    /// Then the output is an "and" expression containing both filters.
    /// </summary>
    [Fact]
    public void AndFilter_ShouldCombineFilters()
    {
        var filter1 = new EqualityFilter<string>("type", "ap");
        var filter2 = new GreaterThanFilter<int>("value", 2);
        var combinedFilter = new AndFilter(filter1, filter2);
        Assert.Equal("and(type.eq('ap'), value.gt(2))", combinedFilter.ToString());
    }

    /// <summary>
    /// Given two filters,
    /// When combined using an <see cref="OrFilter"/>,
    /// Then the output is an "or" expression containing both filters.
    /// </summary>
    [Fact]
    public void OrFilter_ShouldCombineFilters()
    {
        var filter1 = new EqualityFilter<string>("type", "ap");
        var filter2 = new LessThanFilter<int>("value", 5);
        var combinedFilter = new OrFilter(filter1, filter2);
        Assert.Equal("or(type.eq('ap'), value.lt(5))", combinedFilter.ToString());
    }
}
