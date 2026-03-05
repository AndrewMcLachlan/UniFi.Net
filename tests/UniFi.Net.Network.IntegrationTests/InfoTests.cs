using Xunit.Abstractions;

namespace UniFi.Net.Network.IntegrationTests;

[Trait("Category", "Integration")]
public class InfoTests(NetworkClientFixture fixture, ITestOutputHelper output) : IntegrationTestBase(fixture, output)
{
    /// <summary>
    /// Given a configured Network client,
    /// When GetApplicationInfo is called,
    /// Then the response contains a non-null application version.
    /// </summary>
    [Fact]
    public async Task GetApplicationInfo_ReturnsVersion()
    {
        if (SkipIfNotConfigured()) return;

        var info = await Client.GetApplicationInfo();

        Assert.NotNull(info);
        Assert.NotNull(info.ApplicationVersion);
        Output.WriteLine($"Application version: {info.ApplicationVersion}");
    }
}
