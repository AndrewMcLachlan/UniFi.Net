namespace UniFi.Net.Access.Users;

/// <summary>
/// Represents a summary of user details.
/// </summary>
public record UserSummary(string Id, string FirstName, string LastName, string Email);

/// <summary>
/// Represents detailed information about a user.
/// </summary>
public record UserDetails(string Id, string FirstName, string LastName, string Email, string Status);