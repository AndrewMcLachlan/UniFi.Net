namespace UniFi.Net.Access.AccessPolicies;

/// <summary>
/// A client for managing access policies in the UniFi Access system.
/// </summary>
public interface IAccessPolicyClient
{
    // Access Policies
    /// <summary>
    /// Fetches all access policies.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation, returning a list of access policies.</returns>
    Task<List<AccessPolicySummary>> FetchAllAccessPoliciesAsync( CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches details of a specific access policy by ID.
    /// </summary>
    /// <param name="policyId">The ID of the access policy.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation, returning access policy details.</returns>
    Task<AccessPolicyDetails> FetchAccessPolicyAsync(string policyId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new access policy.
    /// </summary>
    /// <param name="name">Name of the access policy.</param>
    /// <param name="resourceIds">List of resource IDs associated with the policy.</param>
    /// <param name="scheduleId">Schedule ID associated with the policy.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task CreateAccessPolicyAsync(string name, List<string> resourceIds, string scheduleId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an access policy by ID.
    /// </summary>
    /// <param name="policyId">The ID of the access policy to delete.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    Task DeleteAccessPolicyAsync(string policyId, CancellationToken cancellationToken = default);
}
