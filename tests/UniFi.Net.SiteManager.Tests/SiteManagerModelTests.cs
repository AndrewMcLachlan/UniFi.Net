using System.Net;
using System.Text.Json;
using UniFi.Net.SiteManager.Models;

namespace UniFi.Net.SiteManager.Tests;

/// <summary>
/// Verifies that Site Manager API models deserialize spec-shaped JSON using the same
/// web defaults (camelCase, case-insensitive) that <c>ReadFromJsonAsync</c> applies.
/// </summary>
public class SiteManagerModelTests
{
    private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web);

    [Fact]
    public void ListHosts_DeserializesHostAndUserData()
    {
        const string json = """
        {
            "data": [
                {
                    "id": "900A6F00301E000000000626123456780000000004018100000000078A5941F7",
                    "hardwareId": "eae0f123-0000-5111-b111-f833f56eade5",
                    "type": "console",
                    "ipAddress": "192.168.1.226",
                    "owner": true,
                    "isBlocked": false,
                    "registrationTime": "2024-04-17T07:27:14Z",
                    "lastConnectionStateChange": "2024-06-23T03:59:52Z",
                    "latestBackupTime": "2024-06-22T11:55:10Z",
                    "userData": {
                        "apps": ["users"],
                        "consoleGroupMembers": [
                            {
                                "mac": "F4E2C6C23F13",
                                "role": "UNADOPTED",
                                "roleAttributes": {
                                    "applications": {
                                        "network": { "owned": false, "required": true, "supported": true }
                                    },
                                    "candidateRoles": ["PRIMARY"],
                                    "connectedState": "CONNECTED",
                                    "connectedStateLastChanged": "2024-06-23T03:59:52Z"
                                },
                                "sysId": 58368
                            }
                        ],
                        "controllers": ["network", "protect"],
                        "email": "user@example.com",
                        "features": {
                            "deviceGroups": true,
                            "floorplan": { "canEdit": true, "canView": true },
                            "manageApplications": true,
                            "notifications": true,
                            "pion": true,
                            "webrtc": { "iceRestart": true, "mediaStreams": true, "twoWayAudio": true }
                        },
                        "fullName": "UniFi User",
                        "localId": "c74d461f-9a3b-4a48-b64f-3f9ae1d1341a",
                        "permissions": { "network.management": ["admin"] },
                        "role": "own",
                        "roleId": "eb0ad29d-2eb2-4bd5-9dd1-a5b8bfba1b58",
                        "status": "ACTIVE"
                    },
                    "reportedState": null
                }
            ],
            "httpStatusCode": 200,
            "traceId": "a7dc15e0eb4527142d7823515b15f87d",
            "nextToken": "eyJ2IjoxfQ"
        }
        """;

        var response = JsonSerializer.Deserialize<PagedResponse<Host>>(json, Options);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.HttpStatusCode);
        Assert.Equal("a7dc15e0eb4527142d7823515b15f87d", response.TraceId);
        Assert.Equal("eyJ2IjoxfQ", response.NextToken);
        var host = Assert.Single(response.Data);
        Assert.Equal("console", host.Type);
        Assert.True(host.Owner);
        Assert.Equal(new DateTimeOffset(2024, 4, 17, 7, 27, 14, TimeSpan.Zero), host.RegistrationTime);
        Assert.Equal("user@example.com", host.UserData.Email);
        var member = Assert.Single(host.UserData.ConsoleGroupMembers);
        Assert.Equal(58368, member.SysId);
        Assert.True(member.RoleAttributes.Applications["network"].Required);
        Assert.True(host.UserData.Features.Floorplan.CanView);
        Assert.Equal(["admin"], host.UserData.Permissions["network.management"]);
    }

    [Fact]
    public void ListDevices_DeserializesHostWithDevices()
    {
        const string json = """
        {
            "data": [
                {
                    "hostId": "900A6F00301E000000000626123456780000000004018100000000078A5941F7",
                    "hostName": "Dream Machine Special Edition",
                    "devices": [
                        {
                            "id": "F4E2C6C23F13",
                            "mac": "F4E2C6C23F13",
                            "name": "Dream Machine Special Edition",
                            "model": "UDM SE",
                            "shortname": "UDMPROSE",
                            "ip": "192.168.1.226",
                            "productLine": "network",
                            "status": "online",
                            "version": "4.0.180",
                            "firmwareStatus": "upToDate",
                            "updateAvailable": null,
                            "isConsole": true,
                            "isManaged": true,
                            "startupTime": "2024-06-19T13:41:43Z",
                            "adoptionTime": null,
                            "note": null,
                            "uidb": {
                                "guid": "0fd8c390-a0e8-4cb2-b93a-7b3051c83c46",
                                "id": "e85485da-54c3-4906-8f19-3cef4116ff02",
                                "images": { "default": "3008400039c483c496f4ad820242c447" }
                            }
                        }
                    ],
                    "updatedAt": "2024-06-23T03:59:52Z"
                }
            ],
            "httpStatusCode": 200,
            "traceId": "a7dc15e0eb4527142d7823515b15f87d",
            "nextToken": null
        }
        """;

        var response = JsonSerializer.Deserialize<PagedResponse<HostWithDevices>>(json, Options);

        Assert.NotNull(response);
        var host = Assert.Single(response.Data);
        Assert.Equal("Dream Machine Special Edition", host.HostName);
        Assert.Equal(new DateTimeOffset(2024, 6, 23, 3, 59, 52, TimeSpan.Zero), host.UpdatedAt);
        var device = Assert.Single(host.Devices);
        Assert.Equal("UDMPROSE", device.ShortName);
        Assert.True(device.IsConsole);
        Assert.Null(device.UpdateAvailable);
        Assert.Equal("3008400039c483c496f4ad820242c447", device.Uidb.Images["default"]);
    }

    [Fact]
    public void ListSites_DeserializesSite()
    {
        const string json = """
        {
            "data": [
                {
                    "siteId": "661900ae6aec8f548d49fd54",
                    "hostId": "900A6F00301E000000000626123456780000000004018100000000078A5941F7",
                    "meta": {
                        "desc": "Default",
                        "gatewayMac": "f4:e2:c6:c2:3f:13",
                        "name": "default",
                        "timezone": "Europe/Lisbon"
                    },
                    "statistics": {
                        "counts": {
                            "criticalNotificationCount": 0,
                            "gatewayDevice": 1,
                            "guestClient": 0,
                            "totalDevice": 5,
                            "wifiClient": 24
                        },
                        "gateway": {
                            "hardwareId": "eae0f123-0000-5111-b111-f833f56eade5",
                            "inspectionState": "off",
                            "ipsMode": "detection",
                            "ipsSignature": { "rulesCount": 43416, "type": "ET" },
                            "shortname": "UDMPROSE"
                        },
                        "internetIssues": [],
                        "ispInfo": { "name": "Vodafone", "organization": "Vodafone Portugal" },
                        "percentages": { "txRetry": 2, "wanUptime": 99 }
                    },
                    "permission": "admin",
                    "isOwner": false
                }
            ],
            "httpStatusCode": 200,
            "traceId": "0e4d2f34a41b8b371abd0f8305506327",
            "nextToken": null
        }
        """;

        var response = JsonSerializer.Deserialize<PagedResponse<Site>>(json, Options);

        Assert.NotNull(response);
        var site = Assert.Single(response.Data);
        Assert.Equal("661900ae6aec8f548d49fd54", site.SiteId);
        Assert.Equal("Europe/Lisbon", site.Meta.TimeZone);
        Assert.Equal(5, site.Statistics.Counts["totalDevice"]);
        Assert.Equal(43416, site.Statistics.Gateway.IpsSignature.RulesCount);
        Assert.Equal("Vodafone", site.Statistics.IspInfo.Name);
        Assert.Equal(99, site.Statistics.Percentages["wanUptime"]);
        Assert.Equal("admin", site.Permission);
    }

    [Fact]
    public void GetIspMetrics_DeserializesSnakeCaseKbpsFields()
    {
        const string json = """
        {
            "data": [
                {
                    "metricType": "5m",
                    "periods": [
                        {
                            "data": {
                                "wan": {
                                    "avgLatency": 5,
                                    "download_kbps": 118000,
                                    "downtime": 0,
                                    "ispAsn": "3352",
                                    "ispName": "Vodafone",
                                    "maxLatency": 9,
                                    "packetLoss": 0,
                                    "upload_kbps": 61000,
                                    "uptime": 100
                                }
                            },
                            "metricTime": "2024-06-23T09:00:00Z",
                            "version": "1"
                        }
                    ],
                    "hostId": "900A6F00301E000000000626123456780000000004018100000000078A5941F7",
                    "siteId": "661900ae6aec8f548d49fd54"
                }
            ],
            "httpStatusCode": 200,
            "traceId": "d54e6a6f7fe37b558f30947db2b09b45"
        }
        """;

        var response = JsonSerializer.Deserialize<DataResponse<IReadOnlyList<IspMetric>>>(json, Options);

        Assert.NotNull(response);
        var metric = Assert.Single(response.Data);
        Assert.Equal("5m", metric.MetricType);
        var wan = Assert.Single(metric.Periods).Data.Wan;
        // Regression: these fields are snake_case on the wire and need explicit mappings.
        Assert.Equal(118000, wan.DownloadKbps);
        Assert.Equal(61000, wan.UploadKbps);
        Assert.Equal(5, wan.AvgLatency);
        Assert.Equal("Vodafone", wan.IspName);
    }

    [Fact]
    public void QueryIspMetrics_DeserializesMetricsWrapper()
    {
        const string json = """
        {
            "data": {
                "metrics": [
                    {
                        "metricType": "1h",
                        "periods": [],
                        "hostId": "900A6F00301E000000000626123456780000000004018100000000078A5941F7",
                        "siteId": "661900ae6aec8f548d49fd54"
                    }
                ],
                "message": "partial data returned",
                "status": "partialSuccess"
            },
            "httpStatusCode": 200,
            "traceId": "d54e6a6f7fe37b558f30947db2b09b45"
        }
        """;

        var response = JsonSerializer.Deserialize<DataResponse<IspMetricsQueryResult>>(json, Options);

        Assert.NotNull(response);
        var metric = Assert.Single(response.Data.Metrics);
        Assert.Equal("1h", metric.MetricType);
        Assert.Equal("partial data returned", response.Data.Message);
        Assert.Equal("partialSuccess", response.Data.Status);
    }

    [Fact]
    public void GetSDWanConfig_DeserializesCamelCaseEnums()
    {
        const string json = """
        {
            "data": {
                "id": "6572628f-4be3-4e73-a233-a684dab3ac96",
                "name": "SD-WAN config",
                "type": "sdwan-hbsp",
                "variant": "distributed",
                "settings": {
                    "hubsInterconnect": true,
                    "spokeToHubTunnelsMode": "maxResiliency",
                    "spokesAutoScaleAndNatEnabled": true,
                    "spokesAutoScaleAndNatRange": "172.16.0.0/12",
                    "spokesIsolate": false,
                    "spokeStandardSettingsEnabled": false,
                    "spokeStandardSettingsValues": null,
                    "spokeToHubRouting": "geo"
                },
                "hubs": [
                    {
                        "id": "6572628f-4be3-4e73-a233-a684dab3ac97",
                        "hostId": "900A6F00301E000000000626123456780000000004018100000000078A5941F7",
                        "siteId": "661900ae6aec8f548d49fd54",
                        "networkIds": ["6633a11c9a71ed6b4f6ab432"],
                        "routes": ["10.0.0.0/24"],
                        "primaryWan": "WAN",
                        "wanFailover": true
                    }
                ],
                "spokes": [
                    {
                        "id": "6572628f-4be3-4e73-a233-a684dab3ac98",
                        "hostId": "900A6F00301E000000000626123456780000000004018100000000078A5941F8",
                        "siteId": "661900ae6aec8f548d49fd55",
                        "networkIds": [],
                        "routes": [],
                        "primaryWan": "WAN2",
                        "wanFailover": false,
                        "hubsPriority": ["6572628f-4be3-4e73-a233-a684dab3ac97"]
                    }
                ]
            },
            "httpStatusCode": 200,
            "traceId": "1a2b3c4d5e6f7a8b9c0d1e2f3a4b5c6d"
        }
        """;

        var response = JsonSerializer.Deserialize<DataResponse<SDWanConfig>>(json, Options);

        Assert.NotNull(response);
        var config = response.Data;
        Assert.Equal(SDWanVariant.Distributed, config.Variant);
        Assert.Equal(SpokeToHubTunnelsMode.MaxResiliency, config.Settings.SpokeToHubTunnelsMode);
        Assert.Equal(SpokeToHubRouting.Geo, config.Settings.SpokeToHubRouting);
        var hub = Assert.Single(config.Hubs);
        Assert.True(hub.WanFailover);
        var spoke = Assert.Single(config.Spokes);
        Assert.Equal(["6572628f-4be3-4e73-a233-a684dab3ac97"], spoke.HubsPriority);
    }

    [Fact]
    public void GetSDWanConfigStatus_DeserializesStatusEnums()
    {
        const string json = """
        {
            "data": {
                "id": "6572628f-4be3-4e73-a233-a684dab3ac96",
                "fingerprint": "029b46b1cf46ba7f",
                "updatedAt": 1719140392,
                "hubs": [
                    {
                        "id": "6572628f-4be3-4e73-a233-a684dab3ac97",
                        "hostId": "900A6F00301E000000000626123456780000000004018100000000078A5941F7",
                        "siteId": "661900ae6aec8f548d49fd54",
                        "name": "Hub 1",
                        "primaryWanStatus": { "ip": "10.0.0.1", "latency": 5, "internetIssues": [], "wanId": "wan" },
                        "secondaryWanStatus": { "ip": "10.0.0.2", "latency": null, "internetIssues": null, "wanId": "wan2" },
                        "errors": [],
                        "warnings": [],
                        "numberOfTunnelsUsedByOtherFeatures": 0,
                        "networks": [
                            { "networkId": "6633a11c9a71ed6b4f6ab432", "name": "Default", "errors": [], "warnings": [] }
                        ],
                        "routes": [],
                        "applyStatus": "createFailed"
                    }
                ],
                "spokes": [
                    {
                        "id": "6572628f-4be3-4e73-a233-a684dab3ac98",
                        "hostId": "900A6F00301E000000000626123456780000000004018100000000078A5941F8",
                        "siteId": "661900ae6aec8f548d49fd55",
                        "name": "Spoke 1",
                        "primaryWanStatus": { "ip": "10.0.1.1", "latency": 12, "internetIssues": [], "wanId": "wan" },
                        "secondaryWanStatus": { "ip": "10.0.1.2", "latency": null, "internetIssues": [], "wanId": "wan2" },
                        "errors": [],
                        "warnings": [],
                        "numberOfTunnelsUsedByOtherFeatures": 1,
                        "networks": [],
                        "routes": [
                            { "routeValue": "10.0.0.0/24", "errors": [], "warnings": [] }
                        ],
                        "connections": [
                            {
                                "hubId": "6572628f-4be3-4e73-a233-a684dab3ac97",
                                "tunnels": [
                                    { "spokeWanId": "wan", "hubWanId": "wan", "status": "connected" }
                                ]
                            }
                        ],
                        "applyStatus": "ok"
                    }
                ],
                "lastGeneratedAt": 1719140300,
                "generateStatus": "GENERATE_FAILED",
                "errors": [],
                "warnings": []
            },
            "httpStatusCode": 200,
            "traceId": "1a2b3c4d5e6f7a8b9c0d1e2f3a4b5c6d"
        }
        """;

        var response = JsonSerializer.Deserialize<DataResponse<SDWanConfigStatus>>(json, Options);

        Assert.NotNull(response);
        var status = response.Data;
        Assert.Equal(GenerateStatus.GenerateFailed, status.GenerateStatus);
        var hub = Assert.Single(status.Hubs);
        Assert.Equal(ApplyStatus.CreateFailed, hub.ApplyStatus);
        Assert.Null(hub.SecondaryWanStatus.Latency);
        var spoke = Assert.Single(status.Spokes);
        Assert.Equal(ApplyStatus.Ok, spoke.ApplyStatus);
        var connection = Assert.Single(spoke.Connections);
        var tunnel = Assert.Single(connection.Tunnels);
        Assert.Equal(TunnelStatus.Connected, tunnel.Status);
        Assert.Equal("10.0.0.0/24", Assert.Single(spoke.Routes).RouteValue);
    }

    [Fact]
    public void ListSDWanConfigs_DeserializesBasicConfig()
    {
        const string json = """
        {
            "data": [
                { "id": "6572628f-4be3-4e73-a233-a684dab3ac96", "name": "SD-WAN config", "type": "sdwan-hbsp" }
            ],
            "httpStatusCode": 200,
            "traceId": "1a2b3c4d5e6f7a8b9c0d1e2f3a4b5c6d"
        }
        """;

        var response = JsonSerializer.Deserialize<DataResponse<IReadOnlyList<BasicSDWanConfig>>>(json, Options);

        Assert.NotNull(response);
        var config = Assert.Single(response.Data);
        Assert.Equal(Guid.Parse("6572628f-4be3-4e73-a233-a684dab3ac96"), config.Id);
        Assert.Equal("sdwan-hbsp", config.Type);
    }

    [Fact]
    public void ErrorResponse_Deserializes()
    {
        const string json = """
        {
            "httpStatusCode": 401,
            "code": "UNAUTHORIZED",
            "message": "unauthorized",
            "traceId": "b30d1554e0873f4d70595cd772e0f4aa"
        }
        """;

        var response = JsonSerializer.Deserialize<ErrorResponse>(json, Options);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Unauthorized, response.HttpStatusCode);
        Assert.Equal("UNAUTHORIZED", response.Code);
        Assert.Equal("unauthorized", response.Message);
        Assert.Equal("b30d1554e0873f4d70595cd772e0f4aa", response.TraceId);
    }

    [Fact]
    public void IspMetricsQueryRequest_SerializesSitesWrapper()
    {
        var request = new IspMetricsQueryRequest(
        [
            new IspMetricsQuery
            {
                BeginTimestamp = new DateTimeOffset(2024, 6, 22, 0, 0, 0, TimeSpan.Zero),
                EndTimestamp = new DateTimeOffset(2024, 6, 23, 0, 0, 0, TimeSpan.Zero),
                HostId = "900A6F00301E000000000626123456780000000004018100000000078A5941F7",
                SiteId = "661900ae6aec8f548d49fd54"
            }
        ]);

        var json = JsonSerializer.Serialize(request, Options);

        using var document = JsonDocument.Parse(json);
        var sites = document.RootElement.GetProperty("sites");
        var site = Assert.Single(sites.EnumerateArray().ToArray());
        Assert.Equal("661900ae6aec8f548d49fd54", site.GetProperty("siteId").GetString());
        Assert.Equal("2024-06-22T00:00:00+00:00", site.GetProperty("beginTimestamp").GetString());
    }
}
