namespace UniFi.Net.SiteManager.Models;

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
/// <param name="ReportedState">The device's reported state information.</param>
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
/// User-specific data associated with a host.
/// </summary>
/// <param name="Apps">The user's apps.</param>
/// <param name="ConsoleGroupMembers">The members of the console group.</param>
/// <param name="Controllers">The controllers available on the host.</param>
/// <param name="Email">The user's email address.</param>
/// <param name="Features">The features available to the user.</param>
/// <param name="FullName">The user's full name.</param>
/// <param name="LocalId">The user's local identifier.</param>
/// <param name="Permissions">The user's permissions, keyed by permission name.</param>
/// <param name="Role">The user's role.</param>
/// <param name="RoleId">The identifier of the user's role.</param>
/// <param name="Status">The user's status.</param>
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
/// <param name="Mac">The MAC address of the member.</param>
/// <param name="Role">The role of the member within the group.</param>
/// <param name="RoleAttributes">The attributes of the member's role.</param>
/// <param name="SysId">The system identifier of the member.</param>
public record ConsoleGroupMember(
    string Mac,
    string Role,
    RoleAttributes RoleAttributes,
    int SysId
);

/// <summary>
/// Attributes of a console group member's role.
/// </summary>
/// <param name="Applications">The applications available to the role, keyed by application name.</param>
/// <param name="CandidateRoles">The candidate roles for the member.</param>
/// <param name="ConnectedState">The connected state of the member.</param>
/// <param name="ConnectedStateLastChanged">The time the connected state last changed.</param>
public record RoleAttributes(
    IReadOnlyDictionary<string, Application> Applications,
    IReadOnlyList<string> CandidateRoles,
    string ConnectedState,
    string ConnectedStateLastChanged
);

/// <summary>
/// Application availability for a role.
/// </summary>
/// <param name="Owned">Indicates if the application is owned.</param>
/// <param name="Required">Indicates if the application is required.</param>
/// <param name="Supported">Indicates if the application is supported.</param>
public record Application(
    bool Owned,
    bool Required,
    bool Supported
);

/// <summary>
/// Features available to a user.
/// </summary>
/// <param name="DeviceGroups">Indicates if device groups are available.</param>
/// <param name="Floorplan">Floorplan feature availability.</param>
/// <param name="ManageApplications">Indicates if the user can manage applications.</param>
/// <param name="Notifications">Indicates if notifications are available.</param>
/// <param name="Pion">Indicates if Pion is available.</param>
/// <param name="WebRtc">WebRTC feature availability.</param>
public record Features(
    bool DeviceGroups,
    Floorplan Floorplan,
    bool ManageApplications,
    bool Notifications,
    bool Pion,
    WebRtc WebRtc
);

/// <summary>
/// Floorplan feature availability.
/// </summary>
/// <param name="CanEdit">Indicates if the user can edit floorplans.</param>
/// <param name="CanView">Indicates if the user can view floorplans.</param>
public record Floorplan(
    bool CanEdit,
    bool CanView
);

/// <summary>
/// WebRTC feature availability.
/// </summary>
/// <param name="IceRestart">Indicates if ICE restart is supported.</param>
/// <param name="MediaStreams">Indicates if media streams are supported.</param>
/// <param name="TwoWayAudio">Indicates if two-way audio is supported.</param>
public record WebRtc(
    bool IceRestart,
    bool MediaStreams,
    bool TwoWayAudio
);
