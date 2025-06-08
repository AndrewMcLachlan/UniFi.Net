using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniFi.Net.Access.Users;

namespace UniFi.Net.Access;
public interface IUserClient
{
    // User Management
    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="firstName">First name of the user.</param>
    /// <param name="lastName">Last name of the user.</param>
    /// <param name="email">Email of the user (optional).</param>
    /// <param name="employeeNumber">Employee number of the user (optional).</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task RegisterUserAsync(string firstName, string lastName, string? email = null, string? employeeNumber = null);

    /// <summary>
    /// Fetches details of a specific user by ID.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>Task representing the asynchronous operation, returning user details.</returns>
    Task<UserDetails> FetchUserAsync(string userId);

    /// <summary>
    /// Fetches all users with optional pagination.
    /// </summary>
    /// <param name="pageNum">Page number for pagination (optional).</param>
    /// <param name="pageSize">Number of users per page (optional).</param>
    /// <returns>Task representing the asynchronous operation, returning a list of users.</returns>
    Task<List<UserSummary>> FetchAllUsersAsync(int? pageNum = null, int? pageSize = null);

    /// <summary>
    /// Deletes a user by ID.
    /// </summary>
    /// <param name="userId">The ID of the user to delete.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task DeleteUserAsync(string userId);
}
