using Xunit.Abstractions;

namespace UniFi.Net.Network.IntegrationTests;

/// <summary>
/// Base class for integration tests. Provides skip logic when the fixture is not configured.
/// </summary>
[Collection("Integration")]
public abstract class IntegrationTestBase
{
    protected NetworkClientFixture Fixture { get; }
    protected INetworkClient Client => Fixture.Client;
    protected Guid SiteId => Fixture.Site?.Id ?? Guid.Empty;
    protected bool HasSite => Fixture.Site is not null;
    protected ITestOutputHelper Output { get; }

    protected IntegrationTestBase(NetworkClientFixture fixture, ITestOutputHelper output)
    {
        Fixture = fixture;
        Output = output;
    }

    /// <summary>
    /// Returns true (and logs a skip message) if the fixture is not configured.
    /// Usage: if (SkipIfNotConfigured()) return;
    /// </summary>
    protected bool SkipIfNotConfigured()
    {
        if (!Fixture.IsConfigured)
        {
            Output.WriteLine("SKIPPED: Integration tests require UniFi:Network:Host and UniFi:Network:ApiKey to be configured.");
            return true;
        }
        return false;
    }

    /// <summary>
    /// Returns true (and logs a skip message) if the fixture has no site.
    /// </summary>
    protected bool SkipIfNoSite()
    {
        if (SkipIfNotConfigured()) return true;
        if (!HasSite)
        {
            Output.WriteLine("SKIPPED: No sites found on the controller.");
            return true;
        }
        return false;
    }

    /// <summary>
    /// Logs a skip message and returns true.
    /// </summary>
    protected bool SkipBecause(string reason)
    {
        Output.WriteLine($"SKIPPED: {reason}");
        return true;
    }
}
