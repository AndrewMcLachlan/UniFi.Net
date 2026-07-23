using UniFi.Net.Network.Models;

namespace UniFi.Net.TestHarness.Network;

public partial class NetworkClient
{
    /// <summary>
    /// A data-driven browser over every read endpoint of the Network API. List endpoints print
    /// their items; get-by-id endpoints fetch the first item from the corresponding list so
    /// every read can be exercised without bespoke selection UI.
    /// </summary>
    private async Task DoReads(CancellationToken cancellationToken)
    {
        (string Name, Func<CancellationToken, Task> Run)[] entries =
        [
            ("Application info", async ct => WriteLine(await uniFiClient.GetApplicationInfo(ct))),
            ("Sites", async ct => PrintPaged(await uniFiClient.ListSites(cancellationToken: ct))),
            ("Devices", async ct => PrintPaged(await uniFiClient.ListDevices(RequireSite(), cancellationToken: ct))),
            ("Device (selected)", async ct => WriteLine(await uniFiClient.GetDevice(RequireSite(), RequireDevice(), ct))),
            ("Device statistics (selected)", async ct => WriteLine(await uniFiClient.GetDeviceStatistics(RequireSite(), RequireDevice(), ct))),
            ("Pending devices", async ct => PrintPaged(await uniFiClient.ListPendingDevices(cancellationToken: ct))),
            ("Clients", async ct => PrintPaged(await uniFiClient.ListClients(RequireSite(), cancellationToken: ct))),
            ("Networks", async ct => PrintPaged(await uniFiClient.ListNetworks(RequireSite(), cancellationToken: ct))),
            ("Network (first)", async ct => WriteLine(await uniFiClient.GetNetwork(RequireSite(), await FirstId(uniFiClient.ListNetworks(RequireSite(), limit: 1, cancellationToken: ct), n => n.Id), ct))),
            ("Network references (first)", async ct => WriteLine(await uniFiClient.GetNetworkReferences(RequireSite(), await FirstId(uniFiClient.ListNetworks(RequireSite(), limit: 1, cancellationToken: ct), n => n.Id), ct))),
            ("WiFi broadcasts", async ct => PrintPaged(await uniFiClient.ListWifiBroadcasts(RequireSite(), cancellationToken: ct))),
            ("WiFi broadcast (first)", async ct => WriteLine(await uniFiClient.GetWifiBroadcast(RequireSite(), await FirstId(uniFiClient.ListWifiBroadcasts(RequireSite(), limit: 1, cancellationToken: ct), w => w.Id), ct))),
            ("LAGs", async ct => PrintPaged(await uniFiClient.ListLags(RequireSite(), cancellationToken: ct))),
            ("LAG (first)", async ct => WriteLine(await uniFiClient.GetLag(RequireSite(), await FirstId(uniFiClient.ListLags(RequireSite(), limit: 1, cancellationToken: ct), l => l.Id), ct))),
            ("MC-LAG domains", async ct => PrintPaged(await uniFiClient.ListMcLagDomains(RequireSite(), cancellationToken: ct))),
            ("MC-LAG domain (first)", async ct => WriteLine(await uniFiClient.GetMcLagDomain(RequireSite(), await FirstId(uniFiClient.ListMcLagDomains(RequireSite(), limit: 1, cancellationToken: ct), d => d.Id), ct))),
            ("Switch stacks", async ct => PrintPaged(await uniFiClient.ListSwitchStacks(RequireSite(), cancellationToken: ct))),
            ("Switch stack (first)", async ct => WriteLine(await uniFiClient.GetSwitchStack(RequireSite(), await FirstId(uniFiClient.ListSwitchStacks(RequireSite(), limit: 1, cancellationToken: ct), s => s.Id), ct))),
            ("Firewall zones", async ct => PrintPaged(await uniFiClient.ListFirewallZones(RequireSite(), cancellationToken: ct))),
            ("Firewall zone (first)", async ct => WriteLine(await uniFiClient.GetFirewallZone(RequireSite(), await FirstId(uniFiClient.ListFirewallZones(RequireSite(), limit: 1, cancellationToken: ct), z => z.Id), ct))),
            ("Firewall policies", async ct => PrintPaged(await uniFiClient.ListFirewallPolicies(RequireSite(), cancellationToken: ct))),
            ("Firewall policy (first)", async ct => WriteLine(await uniFiClient.GetFirewallPolicy(RequireSite(), await FirstId(uniFiClient.ListFirewallPolicies(RequireSite(), limit: 1, cancellationToken: ct), p => p.Id), ct))),
            ("Firewall policy ordering (first two zones)", async ct =>
            {
                var zones = await uniFiClient.ListFirewallZones(RequireSite(), limit: 2, cancellationToken: ct);
                if (zones.Data.Count < 2) throw new InvalidOperationException("Need at least two firewall zones.");
                WriteLine(await uniFiClient.GetFirewallPolicyOrdering(RequireSite(), zones.Data[0].Id, zones.Data[1].Id, ct));
            }),
            ("ACL rules", async ct => PrintPaged(await uniFiClient.ListAclRules(RequireSite(), cancellationToken: ct))),
            ("ACL rule (first)", async ct => WriteLine(await uniFiClient.GetAclRule(RequireSite(), await FirstId(uniFiClient.ListAclRules(RequireSite(), limit: 1, cancellationToken: ct), r => r.Id), ct))),
            ("ACL rule ordering", async ct => WriteLine(await uniFiClient.GetAclRuleOrdering(RequireSite(), ct))),
            ("DNS policies", async ct => PrintPaged(await uniFiClient.ListDnsPolicies(RequireSite(), cancellationToken: ct))),
            ("DNS policy (first)", async ct => WriteLine(await uniFiClient.GetDnsPolicy(RequireSite(), await FirstId(uniFiClient.ListDnsPolicies(RequireSite(), limit: 1, cancellationToken: ct), p => p.Id), ct))),
            ("Traffic matching lists", async ct => PrintPaged(await uniFiClient.ListTrafficMatchingLists(RequireSite(), cancellationToken: ct))),
            ("Traffic matching list (first)", async ct => WriteLine(await uniFiClient.GetTrafficMatchingList(RequireSite(), await FirstId(uniFiClient.ListTrafficMatchingLists(RequireSite(), limit: 1, cancellationToken: ct), l => l.Id), ct))),
            ("WAN interfaces", async ct => PrintPaged(await uniFiClient.ListWanInterfaces(RequireSite(), cancellationToken: ct))),
            ("Site-to-site VPN tunnels", async ct => PrintPaged(await uniFiClient.ListSiteToSiteVpnTunnels(RequireSite(), cancellationToken: ct))),
            ("VPN servers", async ct => PrintPaged(await uniFiClient.ListVpnServers(RequireSite(), cancellationToken: ct))),
            ("RADIUS profiles", async ct => PrintPaged(await uniFiClient.ListRadiusProfiles(RequireSite(), cancellationToken: ct))),
            ("Device tags", async ct => PrintPaged(await uniFiClient.ListDeviceTags(RequireSite(), cancellationToken: ct))),
            ("Vouchers", async ct => PrintPaged(await uniFiClient.ListVouchers(RequireSite(), cancellationToken: ct))),
            ("DPI categories", async ct => PrintPaged(await uniFiClient.ListDpiCategories(cancellationToken: ct))),
            ("DPI applications", async ct => PrintPaged(await uniFiClient.ListDpiApplications(cancellationToken: ct))),
            ("Countries", async ct => PrintPaged(await uniFiClient.ListCountries(cancellationToken: ct))),
        ];

        while (!cancellationToken.IsCancellationRequested)
        {
            var title = "All Read Endpoints" + (SelectedSite is not null ? $"\nCurrent site is {SelectedSite.Name}" : "");
            var choice = PromptOption(title, [.. entries.Select(e => e.Name)]);

            if (choice == 0)
            {
                return;
            }

            Clear();
            WriteLine(entries[choice - 1].Name);
            WriteLine(new string('-', entries[choice - 1].Name.Length));

            try
            {
                await entries[choice - 1].Run(cancellationToken);
            }
            catch (Exception ex)
            {
                WriteError($"Error: {ex.Message}");
            }

            PressAnyKeyToContinue();
        }
    }

    private Guid RequireSite() =>
        SelectedSite?.Id ?? throw new InvalidOperationException("No site selected. Select a site from the Sites menu first.");

    private Guid RequireDevice() =>
        SelectedDevice?.Id ?? throw new InvalidOperationException("No device selected. Select a device from the Devices menu first.");

    private static async Task<Guid> FirstId<T>(Task<PagedResponse<T>> list, Func<T, Guid> id)
    {
        var page = await list;
        return page.Data.Count > 0 ? id(page.Data[0]) : throw new InvalidOperationException("No items found to get by ID.");
    }

    private static void PrintPaged<T>(PagedResponse<T> page)
    {
        foreach (var item in page.Data)
        {
            WriteLine(item);
        }

        WriteLine();
        WriteLine($"{page.Count} of {page.TotalCount} item(s) returned (offset {page.Offset}, limit {page.Limit}).");
    }
}
