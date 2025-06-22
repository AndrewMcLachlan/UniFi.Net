using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniFi.Net.Network.Filters;

namespace UniFi.Net.Network.Tests;

public class LogicalOperatorFilterTests
{
    [Fact]
    public void AndFilter_ShouldCombineFilters()
    {
        var filter1 = new EqualityFilter<string>("type", "ap");
        var filter2 = new GreaterThanFilter<int>("value", 2);
        var combinedFilter = new AndFilter(filter1, filter2);
        Assert.Equal("and(type.eq('ap'), value.gt(2))", combinedFilter.ToString());
    }

    [Fact]
    public void OrFilter_ShouldCombineFilters()
    {
        var filter1 = new EqualityFilter<string>("type", "ap");
        var filter2 = new LessThanFilter<int>("value", 5);
        var combinedFilter = new OrFilter(filter1, filter2);
        Assert.Equal("or(type.eq('ap'), value.lt(5))", combinedFilter.ToString());
    }
}
