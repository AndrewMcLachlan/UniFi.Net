namespace UniFi.Net.Network.Models;

/// <summary>
/// Represents a guest voucher with usage and rate limits.
/// </summary>
/// <param name="Id">Unique identifier of the voucher.</param>
/// <param name="CreatedAt">Date and time when the voucher was created (UTC).</param>
/// <param name="Name">Name of the voucher.</param>
/// <param name="Code">Numeric code for the voucher.</param>
/// <param name="AuthorizedGuestLimit">Maximum number of guests authorized by this voucher.</param>
/// <param name="AuthorizedGuestCount">Current number of guests authorized by this voucher.</param>
/// <param name="ActivatedAt">Date and time when the voucher was activated (UTC).</param>
/// <param name="ExpiresAt">Date and time when the voucher expires (UTC).</param>
/// <param name="Expired">Indicates whether the voucher is expired.</param>
/// <param name="TimeLimitMinutes">Time limit in minutes for voucher usage.</param>
/// <param name="DataUsageLimitMBytes">Data usage limit in megabytes.</param>
/// <param name="RxRateLimitKbps">Receive rate limit in kilobits per second.</param>
/// <param name="TxRateLimitKbps">Transmit rate limit in kilobits per second.</param>
public record Voucher(
    Guid Id,
    DateTimeOffset CreatedAt,
    string Name,
    long Code,
    int? AuthorizedGuestLimit,
    int AuthorizedGuestCount,
    DateTimeOffset? ActivatedAt,
    DateTimeOffset? ExpiresAt,
    bool Expired,
    int TimeLimitMinutes,
    int? DataUsageLimitMBytes,
    int? RxRateLimitKbps,
    int? TxRateLimitKbps
);
