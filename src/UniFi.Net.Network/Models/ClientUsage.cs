namespace UniFi.Net.Network.Models;

/// <summary>
/// Represents the usage statistics of a client, including duration and data transfer metrics.
/// </summary>
/// <param name="DurationSec">The total duration of the client's activity, in seconds.</param>
/// <param name="RxBytes">The total number of bytes received by the client.</param>
/// <param name="TxBytes">The total number of bytes transmitted by the client.</param>
/// <param name="Bytes">The total number of bytes transferred by the client, including both received and transmitted data.</param>
public record ClientUsage(
    long DurationSec,
    long RxBytes,
    long TxBytes,
    long Bytes);
