namespace UniFi.Net.Access.Spaces;

/// <summary>
/// A client for managing spaces in the UniFi Access system.
/// </summary>
public interface ISpaceClient
{
    // Space Management
    /// <summary>
    /// Fetches all door groups.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation, returning a list of door groups.</returns>
    Task<List<DoorGroupSummary>> FetchAllDoorGroupsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches details of a specific door group by ID.
    /// </summary>
    /// <param name="doorGroupId">The ID of the door group.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation, returning door group details.</returns>
    Task<DoorGroupDetails> FetchDoorGroupAsync(string doorGroupId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new door group.
    /// </summary>
    /// <param name="name">Name of the door group.</param>
    /// <param name="resourceIds">List of door IDs associated with the group.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task CreateDoorGroupAsync(string name, List<string> resourceIds, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a door group by ID.
    /// </summary>
    /// <param name="doorGroupId">The ID of the door group to delete.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task DeleteDoorGroupAsync(string doorGroupId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches all doors.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation, returning a list of doors.</returns>
    Task<List<DoorSummary>> FetchAllDoorsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches details of a specific door by ID.
    /// </summary>
    /// <param name="doorId">The ID of the door.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation, returning door details.</returns>
    Task<DoorDetails> FetchDoorAsync(string doorId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Unlocks a door remotely by ID.
    /// </summary>
    /// <param name="doorId">The ID of the door to unlock.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task UnlockDoorAsync(string doorId, CancellationToken cancellationToken = default);
}
