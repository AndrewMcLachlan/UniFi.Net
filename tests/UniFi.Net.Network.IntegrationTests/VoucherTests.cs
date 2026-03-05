using Xunit.Abstractions;

namespace UniFi.Net.Network.IntegrationTests;

[Trait("Category", "Integration")]
public class VoucherTests(NetworkClientFixture fixture, ITestOutputHelper output) : IntegrationTestBase(fixture, output)
{
    /// <summary>
    /// Given a site on the controller,
    /// When ListVouchers is called with no filters,
    /// Then a paged response is returned with a non-negative total count.
    /// </summary>
    [Fact]
    public async Task ListVouchers_ReturnsPagedResponse()
    {
        if (SkipIfNoSite()) return;

        var result = await Client.ListVouchers(SiteId);

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.TotalCount >= 0);
        Output.WriteLine($"Found {result.TotalCount} voucher(s).");
    }

    /// <summary>
    /// Given a site with at least one voucher,
    /// When GetVoucher is called with the first voucher's ID,
    /// Then the voucher is returned with a matching ID and a non-empty code.
    /// </summary>
    [Fact]
    public async Task GetVoucher_WhenVouchersExist_ReturnsVoucher()
    {
        if (SkipIfNoSite()) return;

        var list = await Client.ListVouchers(SiteId, limit: 1);
        if (list.Data.Count == 0) { SkipBecause("No vouchers found on site."); return; }

        var voucher = await Client.GetVoucher(SiteId, list.Data[0].Id);

        Assert.NotNull(voucher);
        Assert.Equal(list.Data[0].Id, voucher.Id);
        Assert.False(String.IsNullOrWhiteSpace(voucher.Code));
        Output.WriteLine($"  Voucher: {voucher.Name}, Code: {voucher.Code}");
    }
}
