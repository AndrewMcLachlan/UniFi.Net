using UniFi.Net.Access.AccessPolicies;

namespace UniFi.Net.Access;

public interface IAccessPolicyClient
{
    // Access Policies
    /// <summary>
    /// Fetches all access policies.
    /// </summary>
    /// <returns>Task representing the asynchronous operation, returning a list of access policies.</returns>
    Task<List<AccessPolicySummary>> FetchAllAccessPoliciesAsync();

    /// <summary>
    /// Fetches details of a specific access policy by ID.
    /// </summary>
    /// <param name="policyId">The ID of the access policy.</param>
    /// <returns>Task representing the asynchronous operation, returning access policy details.</returns>
    Task<AccessPolicyDetails> FetchAccessPolicyAsync(string policyId);

    /// <summary>
    /// Creates a new access policy.
    /// </summary>
    /// <param name="name">Name of the access policy.</param>
    /// <param name="resourceIds">List of resource IDs associated with the policy.</param>
    /// <param name="scheduleId">Schedule ID associated with the policy.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task CreateAccessPolicyAsync(string name, List<string> resourceIds, string scheduleId);

    /// <summary>
    /// Deletes an access policy by ID.
    /// </summary>
    /// <param name="policyId">The ID of the access policy to delete.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task DeleteAccessPolicyAsync(string policyId);
}
