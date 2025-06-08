namespace UniFi.Net.Access.AccessPolicies;

/// <summary>
/// Represents a summary of access policy details.
/// </summary>
public record AccessPolicySummary(string Id, string Name);

/// <summary>
/// Represents detailed information about an access policy.
/// </summary>
public record AccessPolicyDetails(string Id, string Name, List<string> ResourceIds, string ScheduleId);