namespace UniFi.Net.Network.Models;

/// <summary>
/// Represents the latest statistics for a device.
/// </summary>
/// <param name="UptimeSec">The device uptime in seconds.</param>
/// <param name="LastHeartbeatAt">The timestamp of the last heartbeat.</param>
/// <param name="NextHeartbeatAt">The expected timestamp of the next heartbeat.</param>
/// <param name="LoadAverage1Min">The 1-minute load average.</param>
/// <param name="LoadAverage5Min">The 5-minute load average.</param>
/// <param name="LoadAverage15Min">The 15-minute load average.</param>
/// <param name="CpuUtilizationPct">The CPU utilization percentage.</param>
/// <param name="MemoryUtilizationPct">The memory utilization percentage.</param>
/// <param name="Uplink">The uplink statistics.</param>
/// <param name="Interfaces">The interface statistics.</param>
public record DeviceStatistics(
    long UptimeSec,
    DateTimeOffset LastHeartbeatAt,
    DateTimeOffset NextHeartbeatAt,
    double LoadAverage1Min,
    double LoadAverage5Min,
    double LoadAverage15Min,
    double CpuUtilizationPct,
    double MemoryUtilizationPct,
    DeviceStatisticsUplink? Uplink,
    DeviceStatisticsInterfaces? Interfaces
);

/// <summary>
/// Represents uplink statistics for a device.
/// </summary>
/// <param name="TxRateBps">The transmit rate in bits per second.</param>
/// <param name="RxRateBps">The receive rate in bits per second.</param>
public record DeviceStatisticsUplink(
    long TxRateBps,
    long RxRateBps
);

/// <summary>
/// Represents interface statistics for a device.
/// </summary>
/// <param name="Radios">The radio interface statistics.</param>
public record DeviceStatisticsInterfaces(
    IReadOnlyList<DeviceStatisticsRadio>? Radios
);

/// <summary>
/// Represents radio statistics for a device interface.
/// </summary>
/// <param name="FrequencyGHz">The operating frequency in GHz.</param>
/// <param name="TxRetriesPct">The percentage of transmit retries.</param>
public record DeviceStatisticsRadio(
    float FrequencyGHz,
    double TxRetriesPct
);
