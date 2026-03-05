namespace UniFi.Net.Network;

/// <summary>
/// Represents a request to update the ordering of ACL rules.
/// </summary>
/// <param name="OrderedAclRuleIds">The ordered list of ACL rule IDs.</param>
public record UpdateAclRuleOrderingRequest(
    IReadOnlyList<Guid> OrderedAclRuleIds
);
