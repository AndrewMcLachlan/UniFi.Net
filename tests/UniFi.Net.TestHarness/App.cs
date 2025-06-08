using Microsoft.Extensions.Hosting;
using UniFi.Net.TestHarness.Network;
using UniFi.Net.TestHarness.SiteManager;

namespace UniFi.Net.TestHarness;

internal class App(NetworkClient networkClient, SiteManagerClient siteManagerClient) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        int action = 0;

        while (action >= 0 && !cancellationToken.IsCancellationRequested)
        {
            action = PrintMenu();

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
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    WriteLine("Invalid option, please try again.");
                    continue;
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private static int PrintMenu()
    {
        Clear();
        WriteLine("UniFi Client Test Harness");
        WriteLine("-------------------------");
        WriteLine("Choose your API");
        WriteLine("1. Network API");
        WriteLine("2. Site Manager API");
        WriteLine("3. Access API ");
        WriteLine("4. Exit");
        Write("Select an option: ");

        var input = ReadKey();

        if (!Int32.TryParse(input.KeyChar.ToString(), out var option) || option > 2)
        {
            WriteLine("Invalid option, please try again.");
            return -1;

        }
        return option;
    }
}
