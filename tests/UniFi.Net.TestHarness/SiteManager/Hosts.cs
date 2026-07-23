using UniFi.Net.SiteManager.Models;

namespace UniFi.Net.TestHarness.SiteManager;

internal partial class SiteManagerClient
{
    private async Task DoHosts(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var action = PromptOption(Title() + "\nHosts", ["List hosts (and select)", "Get selected host by ID"]);

            switch (action)
            {
                case 0:
                    return;
                case 1:
                    var hosts = await client.ListHostsAsync(cancellationToken: cancellationToken);
                    var selected = SelectItem("Select a host:", hosts.Data, h => $"{h.IpAddress} - {h.Type} ({h.Id})");
                    if (selected is not null)
                    {
                        SelectedHost = selected;
                    }
                    break;
                case 2:
                    if (!SelectedHostCheck()) continue;
                    var host = await client.GetHostAsync(SelectedHost!.Id, cancellationToken);
                    PrintHost(host.Data);
                    PressAnyKeyToContinue();
                    break;
            }
        }
    }

    private static void PrintHost(Host host)
    {
        Clear();
        WriteHeading("Host Details");
        WriteLine($"Id:                {host.Id}");
        WriteLine($"Hardware Id:       {host.HardwareId}");
        WriteLine($"Type:              {host.Type}");
        WriteLine($"IP Address:        {host.IpAddress}");
        WriteLine($"Owner:             {host.Owner}");
        WriteLine($"Blocked:           {host.IsBlocked}");
        WriteLine($"Registered:        {host.RegistrationTime:yyyy-MM-dd HH:mm:ss}");
        WriteLine($"Last State Change: {host.LastConnectionStateChange:yyyy-MM-dd HH:mm:ss}");
        WriteLine($"Latest Backup:     {host.LatestBackupTime:yyyy-MM-dd HH:mm:ss}");
        WriteLine($"User:              {host.UserData.FullName} <{host.UserData.Email}> ({host.UserData.Role})");
        WriteLine($"Apps:              {String.Join(", ", host.UserData.Apps)}");
        WriteLine($"Controllers:       {String.Join(", ", host.UserData.Controllers)}");
        WriteLine($"Reported State:    {(host.ReportedState is null ? "(none)" : "(present)")}");
    }
}
