namespace UniFi.Net.Access.Users;

/// <summary>
/// A client for managing users in the UniFi Access system.
/// </summary>
public interface IUserClient
{
    // User Management
    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="firstName">First name of the user.</param>
    /// <param name="lastName">Last name of the user.</param>
    /// <param name="email">Email of the user.</param>
    /// <param name="employeeNumber">Employee number of the user.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task RegisterUserAsync(string firstName, string lastName, string? email = null, string? employeeNumber = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches details of a specific user by ID.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation, returning user details.</returns>
    Task<UserDetails> FetchUserAsync(string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches all users with optional pagination.
    /// </summary>
    /// <param name="pageNum">Page number for pagination.</param>
    /// <param name="pageSize">Number of users per page.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation, returning a list of users.</returns>
    Task<List<UserSummary>> FetchAllUsersAsync(int? pageNum = null, int? pageSize = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a user by ID.
    /// </summary>
    /// <param name="userId">The ID of the user to delete.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task DeleteUserAsync(string userId, CancellationToken cancellationToken = default);
}
