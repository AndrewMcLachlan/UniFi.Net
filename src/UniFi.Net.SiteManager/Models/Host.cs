namespace UniFi.Net.SiteManager.Models;

/// <summary>
/// Represents the response containing a list of hosts.
/// </summary>
/// <param name="Data">List of host devices.</param>
/// <param name="HttpStatusCode">HTTP status code of the response.</param>
/// <param name="TraceId">Trace identifier for the request.</param>
/// <param name="NextToken">Token for fetching the next set of results, if available.</param>
public record ListHostsResponse(
    IReadOnlyList<Host> Data,
    int HttpStatusCode,
    string TraceId,
    string? NextToken
);

/// <summary>
/// Represents a host device.
/// </summary>
/// <param name="Id">Unique identifier of the host device.</param>
/// <param name="HardwareId">Hardware identifier of the device.</param>
/// <param name="Type">Type of the device (console, network-server).</param>
/// <param name="IpAddress">Current IP address of the device.</param>
/// <param name="Owner">Indicates if the current user is the owner of this device.</param>
/// <param name="IsBlocked">Indicates if the device is blocked from cloud access.</param>
/// <param name="RegistrationTime">Time in RFC3339 format when the device was registered to the cloud.</param>
/// <param name="LastConnectionStateChange">Time in RFC3339 format when the connection state last changed.</param>
/// <param name="LatestBackupTime">Time in RFC3339 format of the latest device backup.</param>
/// <param name="UserData">User-specific data associated with the device including permissions and role information.</param>
/// <param name="ReportedState">Device’s reported state information.</param>
public record Host(
    string Id,
    string HardwareId,
    string Type,
    string IpAddress,
    bool Owner,
    bool IsBlocked,
    DateTimeOffset RegistrationTime,
    DateTimeOffset LastConnectionStateChange,
    DateTimeOffset LatestBackupTime,
    UserData UserData,
    object? ReportedState
);

/// <summary>
/// Use information.
/// </summary>
/// <param name="Apps">The user's apps.</param>
/// <param name="ConsoleGroupMembers"></param>
/// <param name="Controllers"></param>
/// <param name="Email"></param>
/// <param name="Features"></param>
/// <param name="FullName"></param>
/// <param name="LocalId"></param>
/// <param name="Permissions"></param>
/// <param name="Role"></param>
/// <param name="RoleId"></param>
/// <param name="Status"></param>
public record UserData(
    IReadOnlyList<string> Apps,
    IReadOnlyList<ConsoleGroupMember> ConsoleGroupMembers,
    IReadOnlyList<string> Controllers,
    string Email,
    Features Features,
    string FullName,
    string LocalId,
    IReadOnlyDictionary<string, IReadOnlyList<string>> Permissions,
    string Role,
    string RoleId,
    string Status
);

/// <summary>
/// Represents a console group member.
/// </summary>
/// <param name="Mac"></param>
/// <param name="Role"></param>
/// <param name="RoleAttributes"></param>
/// <param name="SysId"></param>
public record ConsoleGroupMember(
    string Mac,
    string Role,
    RoleAttributes RoleAttributes,
    int SysId
);

public record RoleAttributes(
    IReadOnlyDictionary<string, Application> Applications,
    IReadOnlyList<string> CandidateRoles,
    string ConnectedState,
    string ConnectedStateLastChanged
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

