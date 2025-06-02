using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UniFi.Net.SiteManager.Models;

public record ListHostsResponse(
    IReadOnlyList<Host> Data,
    int HttpStatusCode,
    string TraceId,
    string? NextToken
);

public record Host(
    string Id,
    string HardwareId,
    string Type,
    string IpAddress,
    bool Owner,
    bool IsBlocked,
    string RegistrationTime,
    string LastConnectionStateChange,
    string LatestBackupTime,
    UserData UserData,
    object? ReportedState
);

public record UserData(
    IReadOnlyList<string> Apps,
    IReadOnlyList<ConsoleGroupMember> ConsoleGroupMembers,
    IReadOnlyList<string> Controllers,
    string Email,
    Features Features,
    string FullName,
    string LocalId,
    Permissions Permissions,
    string Role,
    string RoleId,
    string Status
);

public record ConsoleGroupMember(
    string Mac,
    string Role,
    RoleAttributes RoleAttributes,
    int SysId
);

public record RoleAttributes(
    Applications Applications,
    IReadOnlyList<string> CandidateRoles,
    string ConnectedState,
    string ConnectedStateLastChanged
);

public record Applications(
    Application Access,
    Application Connect,
    Application Innerspace,
    Application Network,
    Application Protect,
    Application Talk
);

public record Application(
    bool Owned,
    bool Required,
    bool Supported
);

public record Features(
    bool DeviceGroups,
    Floorplan Floorplan,
    bool ManageApplications,
    bool Notifications,
    bool Pion,
    WebRtc WebRtc
);

public record Floorplan(
    bool CanEdit,
    bool CanView
);

public record WebRtc(
    bool IceRestart,
    bool MediaStreams,
    bool TwoWayAudio
);

public record Permissions(
    [property: JsonPropertyName("access.management")] IReadOnlyList<string> AccessManagement,
    [property: JsonPropertyName("connect.management")] IReadOnlyList<string> ConnectManagement,
    [property: JsonPropertyName("innerspace.management")] IReadOnlyList<string> InnerspaceManagement,
    [property: JsonPropertyName("network.management")] IReadOnlyList<string> NetworkManagement,
    [property: JsonPropertyName("protect.management")] IReadOnlyList<string> ProtectManagement,
    [property: JsonPropertyName("system.management.location")] IReadOnlyList<string> SystemManagementLocation,
    [property: JsonPropertyName("system.management.user")] IReadOnlyList<string> SystemManagementUser,
    [property: JsonPropertyName("talk.management")] IReadOnlyList<string> TalkManagement
);
