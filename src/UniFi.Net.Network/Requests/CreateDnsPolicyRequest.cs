using UniFi.Net.Network.Models;

namespace UniFi.Net.Network;

/// <summary>
/// Represents a request to create a new DNS policy.
/// </summary>
/// <param name="Type">The type of DNS record.</param>
/// <param name="Enabled">Indicates whether the DNS policy is enabled.</param>
/// <param name="Domain">The domain name for the policy.</param>
/// <param name="Ipv4Address">The IPv4 address (for A records).</param>
/// <param name="Ipv6Address">The IPv6 address (for AAAA records).</param>
/// <param name="TargetDomain">The target domain (for CNAME records).</param>
/// <param name="MailServerDomain">The mail server domain (for MX records).</param>
/// <param name="Text">The text value (for TXT records).</param>
/// <param name="ServerDomain">The server domain (for SRV records).</param>
/// <param name="IpAddress">The IP address (for forward domain).</param>
/// <param name="TtlSeconds">The time-to-live in seconds.</param>
/// <param name="Priority">The priority (for MX and SRV records).</param>
/// <param name="Port">The port (for SRV records).</param>
/// <param name="Weight">The weight (for SRV records).</param>
/// <param name="Service">The service name (for SRV records).</param>
/// <param name="Protocol">The protocol (for SRV records).</param>
public record CreateDnsPolicyRequest(
    DnsPolicyType Type,
    bool Enabled,
    string Domain,
    string? Ipv4Address = null,
    string? Ipv6Address = null,
    string? TargetDomain = null,
    string? MailServerDomain = null,
    string? Text = null,
    string? ServerDomain = null,
    string? IpAddress = null,
    int? TtlSeconds = null,
    int? Priority = null,
    int? Port = null,
    int? Weight = null,
    string? Service = null,
    string? Protocol = null
);
