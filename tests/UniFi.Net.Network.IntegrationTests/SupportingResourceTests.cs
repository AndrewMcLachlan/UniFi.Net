using Xunit.Abstractions;

namespace UniFi.Net.Network.IntegrationTests;

[Trait("Category", "Integration")]
public class SupportingResourceTests(NetworkClientFixture fixture, ITestOutputHelper output) : IntegrationTestBase(fixture, output)
{
    // WAN Interfaces

    /// <summary>
    /// Given a site on the controller,
    /// When ListWanInterfaces is called,
    /// Then a paged response is returned with a non-negative total count.
    /// </summary>
    [Fact]
    public async Task ListWanInterfaces_ReturnsPagedResponse()
    {
        if (SkipIfNoSite()) return;

        var result = await Client.ListWanInterfaces(SiteId);

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.TotalCount >= 0);
        Output.WriteLine($"Found {result.TotalCount} WAN interface(s).");

        foreach (var wan in result.Data)
        {
            Output.WriteLine($"  WAN: {wan.Name} ({wan.Id})");
        }
    }

    // VPN Tunnels

    /// <summary>
    /// Given a site on the controller,
    /// When ListSiteToSiteVpnTunnels is called,
    /// Then a paged response is returned with a non-negative total count.
    /// </summary>
    [Fact]
    public async Task ListSiteToSiteVpnTunnels_ReturnsPagedResponse()
    {
        if (SkipIfNoSite()) return;

        var result = await Client.ListSiteToSiteVpnTunnels(SiteId);

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.TotalCount >= 0);
        Output.WriteLine($"Found {result.TotalCount} VPN tunnel(s).");
    }

    // VPN Servers

    /// <summary>
    /// Given a site on the controller,
    /// When ListVpnServers is called,
    /// Then a paged response is returned with a non-negative total count.
    /// </summary>
    [Fact]
    public async Task ListVpnServers_ReturnsPagedResponse()
    {
        if (SkipIfNoSite()) return;

        var result = await Client.ListVpnServers(SiteId);

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.TotalCount >= 0);
        Output.WriteLine($"Found {result.TotalCount} VPN server(s).");
    }

    // RADIUS Profiles

    /// <summary>
    /// Given a site on the controller,
    /// When ListRadiusProfiles is called,
    /// Then a paged response is returned with a non-negative total count.
    /// </summary>
    [Fact]
    public async Task ListRadiusProfiles_ReturnsPagedResponse()
    {
        if (SkipIfNoSite()) return;

        var result = await Client.ListRadiusProfiles(SiteId);

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.TotalCount >= 0);
        Output.WriteLine($"Found {result.TotalCount} RADIUS profile(s).");
    }

    // Device Tags

    /// <summary>
    /// Given a site on the controller,
    /// When ListDeviceTags is called,
    /// Then a paged response is returned with a non-negative total count.
    /// </summary>
    [Fact]
    public async Task ListDeviceTags_ReturnsPagedResponse()
    {
        if (SkipIfNoSite()) return;

        var result = await Client.ListDeviceTags(SiteId);

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.TotalCount >= 0);
        Output.WriteLine($"Found {result.TotalCount} device tag(s).");
    }

    // DPI Categories (not site-scoped)

    /// <summary>
    /// Given a configured Network client,
    /// When ListDpiCategories is called with no filters,
    /// Then a paged response is returned with a non-negative total count.
    /// </summary>
    [Fact]
    public async Task ListDpiCategories_ReturnsPagedResponse()
    {
        if (SkipIfNotConfigured()) return;

        var result = await Client.ListDpiCategories();

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.TotalCount >= 0);
        Output.WriteLine($"Found {result.TotalCount} DPI category/ies.");
    }

    /// <summary>
    /// Given a configured Network client with at least one DPI category,
    /// When ListDpiCategories is called,
    /// Then each category has a non-empty name.
    /// </summary>
    [Fact]
    public async Task ListDpiCategories_EachCategory_HasRequiredFields()
    {
        if (SkipIfNotConfigured()) return;

        var result = await Client.ListDpiCategories(limit: 10);

        foreach (var category in result.Data)
        {
            Assert.False(String.IsNullOrWhiteSpace(category.Name));
            Output.WriteLine($"  DPI Category: {category.Name} (ID: {category.Id})");
        }
    }

    // DPI Applications (not site-scoped)

    /// <summary>
    /// Given a configured Network client,
    /// When ListDpiApplications is called with no filters,
    /// Then a paged response is returned with a non-negative total count.
    /// </summary>
    [Fact]
    public async Task ListDpiApplications_ReturnsPagedResponse()
    {
        if (SkipIfNotConfigured()) return;

        var result = await Client.ListDpiApplications();

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.TotalCount >= 0);
        Output.WriteLine($"Found {result.TotalCount} DPI application(s).");
    }

    /// <summary>
    /// Given a configured Network client with at least one DPI application,
    /// When ListDpiApplications is called,
    /// Then each application has a non-empty name.
    /// </summary>
    [Fact]
    public async Task ListDpiApplications_EachApplication_HasRequiredFields()
    {
        if (SkipIfNotConfigured()) return;

        var result = await Client.ListDpiApplications(limit: 10);

        foreach (var app in result.Data)
        {
            Assert.False(String.IsNullOrWhiteSpace(app.Name));
            Output.WriteLine($"  DPI App: {app.Name} (ID: {app.Id})");
        }
    }

    // Countries (not site-scoped)

    /// <summary>
    /// Given a configured Network client,
    /// When ListCountries is called with no filters,
    /// Then a paged response is returned with a non-negative total count.
    /// </summary>
    [Fact]
    public async Task ListCountries_ReturnsPagedResponse()
    {
        if (SkipIfNotConfigured()) return;

        var result = await Client.ListCountries();

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.TotalCount >= 0);
        Output.WriteLine($"Found {result.TotalCount} country/ies.");
    }

    /// <summary>
    /// Given a configured Network client with at least one country,
    /// When ListCountries is called,
    /// Then each country has a non-empty code and name.
    /// </summary>
    [Fact]
    public async Task ListCountries_EachCountry_HasRequiredFields()
    {
        if (SkipIfNotConfigured()) return;

        var result = await Client.ListCountries(limit: 10);

        foreach (var country in result.Data)
        {
            Assert.False(String.IsNullOrWhiteSpace(country.Code));
            Assert.False(String.IsNullOrWhiteSpace(country.Name));
            Output.WriteLine($"  Country: {country.Name} ({country.Code})");
        }
    }
}
