using Microsoft.Extensions.Configuration;
using UniFi.Net.Network;
using UniFi.Net.Network.Models;

namespace UniFi.Net.Network.IntegrationTests;

/// <summary>
/// Shared fixture that creates a <see cref="NetworkClient"/> and discovers the first site.
/// Configuration is read from (in order of precedence):
///   1. Environment variables: UNIFI__NETWORK__HOST, UNIFI__NETWORK__APIKEY
///   2. User secrets (secret id: UniFi.Net.Network.IntegrationTests)
///   3. appsettings.json
/// </summary>
public class NetworkClientFixture : IAsyncLifetime
{
    public INetworkClient Client { get; private set; } = null!;

    /// <summary>
    /// The first site discovered on the controller. Null if no sites exist.
    /// </summary>
    public Site? Site { get; private set; }

    /// <summary>
    /// Indicates whether the fixture is configured with a valid host and API key.
    /// When false, all integration tests should be skipped.
    /// </summary>
    public bool IsConfigured { get; private set; }

    public async Task InitializeAsync()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
            .AddUserSecrets<NetworkClientFixture>(optional: true)
            .AddEnvironmentVariables()
            .Build();

        var host = configuration["UniFi:Network:Host"];
        var apiKey = configuration["UniFi:Network:ApiKey"];

        if (String.IsNullOrWhiteSpace(host) || String.IsNullOrWhiteSpace(apiKey))
        {
            IsConfigured = false;
            return;
        }

        IsConfigured = true;
        Client = new NetworkClient(new Uri(host), apiKey);

        // Discover the first site for use by all tests
        var sites = await Client.ListSites(limit: 1);
        Site = sites.Data.FirstOrDefault();
    }

    public Task DisposeAsync() => Task.CompletedTask;
}

[CollectionDefinition("Integration")]
public class IntegrationCollection : ICollectionFixture<NetworkClientFixture>
{
}
