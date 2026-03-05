using System.Text.Json.Serialization;
using UniFi.Net.Serialization;

namespace UniFi.Net.Network.Models;

/// <summary>
/// The management type of a network.
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter))]
public enum NetworkManagement
{
    /// <summary>
    /// The network is unmanaged.
    /// </summary>
    Unmanaged = 0,

    /// <summary>
    /// The network is managed by the gateway.
    /// </summary>
    Gateway = 1,

    /// <summary>
    /// The network is managed by the switch.
    /// </summary>
    Switch = 2,
}

/// <summary>
/// Represents a network configuration.
/// </summary>
/// <param name="Id">The unique identifier of the network.</param>
/// <param name="Name">The name of the network.</param>
/// <param name="Management">The management type of the network.</param>
/// <param name="Enabled">Indicates whether the network is enabled.</param>
/// <param name="VlanId">The VLAN ID assigned to the network.</param>
/// <param name="Default">Indicates whether this is the default network.</param>
/// <param name="Metadata">The metadata for the network.</param>
/// <param name="DhcpGuarding">The DHCP guarding configuration.</param>
public record SiteNetwork(
    Guid Id,
    string Name,
    NetworkManagement Management,
    bool Enabled,
    int VlanId,
    bool Default,
    ResourceMetadata Metadata,
    DhcpGuarding? DhcpGuarding
);

/// <summary>
/// Represents DHCP guarding configuration for a network.
/// </summary>
/// <param name="TrustedDhcpServerIpAddresses">The list of trusted DHCP server IP addresses.</param>
public record DhcpGuarding(
    IReadOnlyList<string> TrustedDhcpServerIpAddresses
);

/// <summary>
/// Represents the references for a network.
/// </summary>
/// <param name="ReferenceResources">The list of reference resources.</param>
public record NetworkReferences(
    IReadOnlyList<NetworkReferenceResource> ReferenceResources
);

/// <summary>
/// Represents a reference resource for a network.
/// </summary>
/// <param name="ResourceType">The type of the referenced resource.</param>
/// <param name="ReferenceCount">The number of references.</param>
/// <param name="References">The list of references.</param>
public record NetworkReferenceResource(
    string ResourceType,
    int ReferenceCount,
    IReadOnlyList<NetworkReference>? References
);

/// <summary>
/// Represents a single reference to a network.
/// </summary>
/// <param name="ReferenceId">The unique identifier of the reference.</param>
public record NetworkReference(
    Guid ReferenceId
);
