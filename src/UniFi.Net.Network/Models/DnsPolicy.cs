using System.Text.Json.Serialization;
using UniFi.Net.Serialization;

namespace UniFi.Net.Network.Models;

/// <summary>
/// The type of DNS policy record.
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter))]
public enum DnsPolicyType
{
    /// <summary>
    /// A record (IPv4 address mapping).
    /// </summary>
    ARecord = 0,

    /// <summary>
    /// AAAA record (IPv6 address mapping).
    /// </summary>
    AaaaRecord = 1,

    /// <summary>
    /// CNAME record (canonical name alias).
    /// </summary>
    CnameRecord = 2,

    /// <summary>
    /// MX record (mail exchange).
    /// </summary>
    MxRecord = 3,

    /// <summary>
    /// TXT record (text).
    /// </summary>
    TxtRecord = 4,

    /// <summary>
    /// SRV record (service locator).
    /// </summary>
    SrvRecord = 5,

    /// <summary>
    /// Forward domain.
    /// </summary>
    ForwardDomain = 6,
}

/// <summary>
/// Represents a DNS policy.
/// </summary>
/// <param name="Id">The unique identifier of the DNS policy.</param>
/// <param name="Type">The type of DNS record.</param>
/// <param name="Enabled">Indicates whether the DNS policy is enabled.</param>
/// <param name="Domain">The domain name for the policy.</param>
/// <param name="Metadata">The metadata for the DNS policy.</param>
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
public record DnsPolicy(
    Guid Id,
    DnsPolicyType Type,
    bool Enabled,
    string Domain,
    ResourceMetadata Metadata,
    string? Ipv4Address,
    string? Ipv6Address,
    string? TargetDomain,
    string? MailServerDomain,
    string? Text,
    string? ServerDomain,
    string? IpAddress,
    int? TtlSeconds,
    int? Priority,
    int? Port,
    int? Weight,
    string? Service,
    string? Protocol
);
