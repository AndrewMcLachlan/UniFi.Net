namespace UniFi.Net.Access.Visitors;

/// <summary>
/// A client  for managing visitor-related operations in the UniFi Access system.
/// </summary>
public interface IVisitorClient
{
    // Visitor Management
    /// <summary>
    /// Registers a new visitor.
    /// </summary>
    /// <param name="firstName">First name of the visitor.</param>
    /// <param name="lastName">Last name of the visitor.</param>
    /// <param name="email">Email of the visitor (optional).</param>
    /// <param name="phone">Phone number of the visitor (optional).</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task RegisterVisitorAsync(string firstName, string lastName, string? email = null, string? phone = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches details of a specific visitor by ID.
    /// </summary>
    /// <param name="visitorId">The ID of the visitor.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation, returning visitor details.</returns>
    Task<VisitorDetails> FetchVisitorAsync(string visitorId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches all visitors with optional pagination.
    /// </summary>
    /// <param name="pageNum">Page number for pagination (optional).</param>
    /// <param name="pageSize">Number of visitors per page (optional).</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation, returning a list of visitors.</returns>
    Task<List<VisitorSummary>> FetchAllVisitorsAsync(int? pageNum = null, int? pageSize = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a visitor by ID.
    /// </summary>
    /// <param name="visitorId">The ID of the visitor to delete.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task DeleteVisitorAsync(string visitorId, CancellationToken cancellationToken = default);
}
