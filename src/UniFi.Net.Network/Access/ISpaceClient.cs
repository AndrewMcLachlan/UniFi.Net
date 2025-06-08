using UniFi.Net.Access.Space;

namespace UniFi.Net.Access;

public interface ISpaceClient
{


    // Space Management
    /// <summary>
    /// Fetches all door groups.
    /// </summary>
    /// <returns>Task representing the asynchronous operation, returning a list of door groups.</returns>
    Task<List<DoorGroupSummary>> FetchAllDoorGroupsAsync();

    /// <summary>
    /// Fetches details of a specific door group by ID.
    /// </summary>
    /// <param name="doorGroupId">The ID of the door group.</param>
    /// <returns>Task representing the asynchronous operation, returning door group details.</returns>
    Task<DoorGroupDetails> FetchDoorGroupAsync(string doorGroupId);

    /// <summary>
    /// Creates a new door group.
    /// </summary>
    /// <param name="name">Name of the door group.</param>
    /// <param name="resourceIds">List of door IDs associated with the group.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task CreateDoorGroupAsync(string name, List<string> resourceIds);

    /// <summary>
    /// Deletes a door group by ID.
    /// </summary>
    /// <param name="doorGroupId">The ID of the door group to delete.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task DeleteDoorGroupAsync(string doorGroupId);

    /// <summary>
    /// Fetches all doors.
    /// </summary>
    /// <returns>Task representing the asynchronous operation, returning a list of doors.</returns>
    Task<List<DoorSummary>> FetchAllDoorsAsync();

    /// <summary>
    /// Fetches details of a specific door by ID.
    /// </summary>
    /// <param name="doorId">The ID of the door.</param>
    /// <returns>Task representing the asynchronous operation, returning door details.</returns>
    Task<DoorDetails> FetchDoorAsync(string doorId);

    /// <summary>
    /// Unlocks a door remotely by ID.
    /// </summary>
    /// <param name="doorId">The ID of the door to unlock.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task UnlockDoorAsync(string doorId);
}
