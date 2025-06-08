using UniFi.Net.Access.SystemLog;

namespace UniFi.Net.Access;

public interface ISystemLogClient
{
    // System Logs
    /// <summary>
    /// Fetches system logs.
    /// </summary>
    /// <param name="topic">The topic of the logs to fetch.</param>
    /// <param name="since">Start time for log fetching (optional).</param>
    /// <param name="until">End time for log fetching (optional).</param>
    /// <param name="actorId">The ID of the actor (optional).</param>
    /// <returns>Task representing the asynchronous operation, returning a list of system logs.</returns>
    Task<List<SystemLogEntry>> FetchSystemLogsAsync(string topic, long? since = null, long? until = null, string? actorId = null);

    /// <summary>
    /// Exports system logs to a CSV file.
    /// </summary>
    /// <param name="topic">The topic of the logs to export.</param>
    /// <param name="since">Start time for log exporting.</param>
    /// <param name="until">End time for log exporting.</param>
    /// <param name="timezone">Timezone for formatting time.</param>
    /// <returns>Task representing the asynchronous operation, returning the CSV file as a byte array.</returns>
    Task<byte[]> ExportSystemLogsAsync(string topic, long since, long until, string timezone);
}
