namespace UniFi.Net.Network.Models;

/// <summary>
/// Represents the authorization details for a client, including the authorization time, method, expiration, and usage
/// limits.
/// </summary>
/// <remarks>This record encapsulates information about a client's authorization, such as when it was authorized,
/// the method used for authorization, and any applicable limits on data usage or transfer rates. It also tracks the
/// client's current usage statistics.</remarks>
/// <param name="AuthorizedAt">The date and time when the client was authorized.</param>
/// <param name="AuthorizationMethod">The method used to authorize the client.</param>
/// <param name="ExpiresAt">The date and time when the authorization expires.</param>
/// <param name="DataUsageLimitBytes">The maximum amount of data, in bytes, that the client is allowed to use. A value of <see langword="null"/> indicates
/// no limit.</param>
/// <param name="RxRateLimitKbps">The maximum download rate, in kilobits per second, allowed for the client. A value of <see langword="null"/>
/// indicates no limit.</param>
/// <param name="TxRateLimitKbps">The maximum upload rate, in kilobits per second, allowed for the client. A value of <see langword="null"/> indicates
/// no limit.</param>
/// <param name="Usage">The current usage statistics for the client, including data consumption and rate information.</param>
public record ClientAuthorization(
    DateTimeOffset AuthorizedAt,
    AuthorizationMethod  AuthorizationMethod,
    DateTimeOffset ExpiresAt,
    long? DataUsageLimitBytes,
    long? RxRateLimitKbps,
    long? TxRateLimitKbps,
    ClientUsage Usage);

