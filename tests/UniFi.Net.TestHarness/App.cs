using Microsoft.Extensions.Hosting;
using UniFi.Net.TestHarness.Network;
using UniFi.Net.TestHarness.SiteManager;

namespace UniFi.Net.TestHarness;

internal class App(NetworkClient networkClient, SiteManagerClient siteManagerClient) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var action = PromptOption(
                """
                UniFi Client Test Harness
                Choose your API
                """,
                ["Network API", "Site Manager API", "Access API"],
                "Exit");

            switch (action)
            {
                case 1:
                    await networkClient.StartAsync(cancellationToken);
                    break;
                case 2:
                    await siteManagerClient.StartAsync(cancellationToken);
                    break;
                case 3:
                    WriteLine("Access API is not implemented yet.");
                    PressAnyKeyToContinue();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
