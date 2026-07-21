using System.Text.Json;
using UniFi.Net.Network.Models;

namespace UniFi.Net.Network.Tests;

[Trait("Category", "Unit")]
public class SwitchingModelTests
{
    // Mirrors the options used by the client's ReadFromJsonAsync<T> (camelCase, case-insensitive).
    private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web);

    /// <summary>
    /// Given a paged LAG response containing all three discriminator values,
    /// When it is deserialized,
    /// Then each element maps to the correct <see cref="LagType"/> and populates only the matching type-specific identifier.
    /// </summary>
    [Fact]
    public void ListLags_DeserializesFlattenedUnion()
    {
        const string json = """
        {
          "offset": 0,
          "limit": 25,
          "count": 3,
          "totalCount": 3,
          "data": [
            {
              "id": "11111111-1111-1111-1111-111111111111",
              "type": "LOCAL",
              "members": [ { "deviceId": "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa", "portIdxs": [1, 2] } ],
              "metadata": { "origin": "USER_DEFINED", "configurable": true }
            },
            {
              "id": "22222222-2222-2222-2222-222222222222",
              "type": "MULTI_CHASSIS",
              "members": [ { "deviceId": "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb", "portIdxs": [3] } ],
              "metadata": { "origin": "SYSTEM_DEFINED", "configurable": false },
              "mcLagDomainId": "dddddddd-dddd-dddd-dddd-dddddddddddd"
            },
            {
              "id": "33333333-3333-3333-3333-333333333333",
              "type": "SWITCH_STACK",
              "members": [ { "deviceId": "cccccccc-cccc-cccc-cccc-cccccccccccc", "portIdxs": [4, 5, 6] } ],
              "metadata": { "origin": "USER_DEFINED", "configurable": true },
              "switchStackId": "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"
            }
          ]
        }
        """;

        var result = JsonSerializer.Deserialize<PagedResponse<Lag>>(json, Options);

        Assert.NotNull(result);
        Assert.Equal(3, result!.Data.Count);

        var local = result.Data[0];
        Assert.Equal(LagType.Local, local.Type);
        Assert.Null(local.McLagDomainId);
        Assert.Null(local.SwitchStackId);
        Assert.Equal(new[] { 1, 2 }, local.Members[0].PortIdxs);
        Assert.Equal(MetadataOrigin.UserDefined, local.Metadata.Origin);

        var mcLag = result.Data[1];
        Assert.Equal(LagType.MultiChassis, mcLag.Type);
        Assert.Equal(Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), mcLag.McLagDomainId);
        Assert.Null(mcLag.SwitchStackId);

        var stackLag = result.Data[2];
        Assert.Equal(LagType.SwitchStack, stackLag.Type);
        Assert.Equal(Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), stackLag.SwitchStackId);
        Assert.Null(stackLag.McLagDomainId);
    }

    /// <summary>
    /// Given an MC-LAG domain payload,
    /// When it is deserialized,
    /// Then its nested LAGs and peers (including peer roles) map correctly.
    /// </summary>
    [Fact]
    public void GetMcLagDomain_DeserializesPeersAndLags()
    {
        const string json = """
        {
          "id": "dddddddd-dddd-dddd-dddd-dddddddddddd",
          "name": "Domain A",
          "metadata": { "origin": "USER_DEFINED", "configurable": true },
          "lags": [
            {
              "id": "22222222-2222-2222-2222-222222222222",
              "members": [ { "deviceId": "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb", "portIdxs": [3] } ],
              "metadata": { "origin": "SYSTEM_DEFINED", "configurable": false }
            }
          ],
          "peers": [
            { "deviceId": "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb", "linkPortIdxs": [9, 10], "role": "TOP" },
            { "deviceId": "ffffffff-ffff-ffff-ffff-ffffffffffff", "linkPortIdxs": [9, 10], "role": "BOTTOM" }
          ]
        }
        """;

        var domain = JsonSerializer.Deserialize<McLagDomain>(json, Options);

        Assert.NotNull(domain);
        Assert.Equal("Domain A", domain!.Name);
        Assert.Single(domain.Lags);
        Assert.Equal(new[] { 3 }, domain.Lags[0].Members[0].PortIdxs);
        Assert.Equal(2, domain.Peers.Count);
        Assert.Equal(McLagPeerRole.Top, domain.Peers[0].Role);
        Assert.Equal(McLagPeerRole.Bottom, domain.Peers[1].Role);
        Assert.Equal(new[] { 9, 10 }, domain.Peers[0].LinkPortIdxs);
    }

    /// <summary>
    /// Given a switch stack payload,
    /// When it is deserialized,
    /// Then its member devices and nested LAGs map correctly.
    /// </summary>
    [Fact]
    public void GetSwitchStack_DeserializesMembersAndLags()
    {
        const string json = """
        {
          "id": "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee",
          "name": "Stack 1",
          "metadata": { "origin": "USER_DEFINED", "configurable": true },
          "lags": [
            {
              "id": "33333333-3333-3333-3333-333333333333",
              "members": [ { "deviceId": "cccccccc-cccc-cccc-cccc-cccccccccccc", "portIdxs": [4, 5] } ],
              "metadata": { "origin": "USER_DEFINED", "configurable": true }
            }
          ],
          "members": [
            { "deviceId": "cccccccc-cccc-cccc-cccc-cccccccccccc" },
            { "deviceId": "99999999-9999-9999-9999-999999999999" }
          ]
        }
        """;

        var stack = JsonSerializer.Deserialize<SwitchStack>(json, Options);

        Assert.NotNull(stack);
        Assert.Equal("Stack 1", stack!.Name);
        Assert.Equal(2, stack.Members.Count);
        Assert.Equal(Guid.Parse("99999999-9999-9999-9999-999999999999"), stack.Members[1].DeviceId);
        Assert.Single(stack.Lags);
        Assert.Equal(new[] { 4, 5 }, stack.Lags[0].Members[0].PortIdxs);
    }

    /// <summary>
    /// Given a WiFi broadcast payload that includes the v10.4.57 2.4 GHz lock fields,
    /// When it is deserialized,
    /// Then the new optional boolean fields are populated.
    /// </summary>
    [Fact]
    public void WifiBroadcast_DeserializesNewLockFields()
    {
        const string json = """
        {
          "id": "77777777-7777-7777-7777-777777777777",
          "name": "SSID",
          "type": "STANDARD",
          "enabled": true,
          "metadata": { "origin": "USER_DEFINED", "configurable": true },
          "channel2gLockedTo6": true,
          "dtimPeriod2gLockedTo3": false
        }
        """;

        var wifi = JsonSerializer.Deserialize<WifiBroadcast>(json, Options);

        Assert.NotNull(wifi);
        Assert.True(wifi!.Channel2gLockedTo6);
        Assert.False(wifi.DtimPeriod2gLockedTo3);
    }
}
