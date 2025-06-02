using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniFi.Net.SiteManager.Models;

public record IspMetric(
    string MetricType,
    IReadOnlyList<IspMetricPeriod> Periods,
    string HostId,
    string SiteId
);

public record IspMetricPeriod(
    IspMetricData Data,
    string MetricTime,
    string Version
);

public record IspMetricData(
    IspMetricWan Wan
);

public record IspMetricWan(
    int AvgLatency,
    int DownloadKbps,
    int Downtime,
    string IspAsn,
    string IspName,
    int MaxLatency,
    int PacketLoss,
    int UploadKbps,
    int Uptime
);
