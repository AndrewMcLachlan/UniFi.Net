using UniFi.Net.SiteManager.Models;

namespace UniFi.Net.TestHarness.SiteManager;

internal partial class SiteManagerClient
{
    private async Task DoSDWanConfigs(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var action = PromptOption(
                Title() + "\nSD-WAN Configs",
                [
                    "List SD-WAN configs (and select)",
                    "Get selected SD-WAN config by ID",
                    "Get selected SD-WAN config status",
                ]);

            switch (action)
            {
                case 0:
                    return;
                case 1:
                    var configs = await client.ListSDWanConfigsAsync(cancellationToken);
                    var selected = SelectItem("Select an SD-WAN config:", configs.Data, c => $"{c.Name} - {c.Type} ({c.Id})");
                    if (selected is not null)
                    {
                        SelectedSDWanConfig = selected;
                    }
                    break;
                case 2:
                    if (!SelectedSDWanConfigCheck()) continue;
                    var config = await client.GetSDWanConfigAsync(SelectedSDWanConfig!.Id, cancellationToken);
                    PrintConfig(config.Data);
                    PressAnyKeyToContinue();
                    break;
                case 3:
                    if (!SelectedSDWanConfigCheck()) continue;
                    var status = await client.GetSDWanConfigStatusAsync(SelectedSDWanConfig!.Id, cancellationToken);
                    PrintStatus(status.Data);
                    PressAnyKeyToContinue();
                    break;
            }
        }
    }

    private bool SelectedSDWanConfigCheck()
    {
        if (SelectedSDWanConfig == null)
        {
            WriteError("No SD-WAN config selected. Please select one first (List SD-WAN configs).");
            PressAnyKeyToContinue();
            return false;
        }
        return true;
    }

    private static void PrintConfig(SDWanConfig config)
    {
        Clear();
        WriteHeading("SD-WAN Config");
        WriteLine($"Id:      {config.Id}");
        WriteLine($"Name:    {config.Name}");
        WriteLine($"Type:    {config.Type}");
        WriteLine($"Variant: {config.Variant}");
        WriteLine($"Settings:");
        WriteLine($"  Hubs interconnect:   {config.Settings.HubsInterconnect}");
        WriteLine($"  Tunnels mode:        {config.Settings.SpokeToHubTunnelsMode}");
        WriteLine($"  Routing:             {config.Settings.SpokeToHubRouting}");
        WriteLine($"  Auto-scale and NAT:  {config.Settings.SpokesAutoScaleAndNatEnabled} ({config.Settings.SpokesAutoScaleAndNatRange})");
        WriteLine($"  Isolate spokes:      {config.Settings.SpokesIsolate}");
        WriteLine($"Hubs ({config.Hubs.Count}):");
        foreach (var hub in config.Hubs)
        {
            WriteLine($"  {hub.Id}: site {hub.SiteId}, primary WAN {hub.PrimaryWan}, failover {hub.WanFailover}");
        }
        WriteLine($"Spokes ({config.Spokes.Count}):");
        foreach (var spoke in config.Spokes)
        {
            WriteLine($"  {spoke.Id}: site {spoke.SiteId}, primary WAN {spoke.PrimaryWan}, failover {spoke.WanFailover}");
        }
    }

    private static void PrintStatus(SDWanConfigStatus status)
    {
        Clear();
        WriteHeading("SD-WAN Config Status");
        WriteLine($"Id:              {status.Id}");
        WriteLine($"Fingerprint:     {status.Fingerprint}");
        WriteLine($"Generate status: {status.GenerateStatus}");
        WriteLine($"Updated at:      {DateTimeOffset.FromUnixTimeSeconds(status.UpdatedAt):yyyy-MM-dd HH:mm:ss}");
        WriteLine($"Errors:          {(status.Errors.Count > 0 ? String.Join("; ", status.Errors) : "(none)")}");
        WriteLine($"Warnings:        {(status.Warnings.Count > 0 ? String.Join("; ", status.Warnings) : "(none)")}");
        WriteLine($"Hubs ({status.Hubs.Count}):");
        foreach (var hub in status.Hubs)
        {
            WriteLine($"  {hub.Name}: apply {hub.ApplyStatus}, primary WAN {hub.PrimaryWanStatus?.Ip} ({hub.PrimaryWanStatus?.Latency} ms)");
        }
        WriteLine($"Spokes ({status.Spokes.Count}):");
        foreach (var spoke in status.Spokes)
        {
            WriteLine($"  {spoke.Name}: apply {spoke.ApplyStatus}, primary WAN {spoke.PrimaryWanStatus?.Ip} ({spoke.PrimaryWanStatus?.Latency} ms)");
            foreach (var connection in spoke.Connections)
            {
                foreach (var tunnel in connection.Tunnels)
                {
                    WriteLine($"    Tunnel to hub {connection.HubId}: {tunnel.SpokeWanId} -> {tunnel.HubWanId} ({tunnel.Status})");
                }
            }
        }
    }
}
