using UniFi.Net.Network.Models;

namespace UniFi.Net.Network;

/// <summary>
/// Represents a request to create a new network.
/// </summary>
/// <param name="Management">The management type of the network.</param>
/// <param name="Name">The name of the network.</param>
/// <param name="Enabled">Indicates whether the network is enabled.</param>
/// <param name="VlanId">The VLAN ID assigned to the network.</param>
/// <param name="DhcpGuarding">The DHCP guarding configuration.</param>
public record CreateNetworkRequest(
    NetworkManagement Management,
    string Name,
    bool Enabled,
    int VlanId,
    DhcpGuarding? DhcpGuarding = null
);
