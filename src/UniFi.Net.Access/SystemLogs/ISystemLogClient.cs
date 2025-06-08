namespace UniFi.Net.Access.SystemLogs;

/// <summary>
/// Defines methods for interacting with system logs, including fetching and exporting log data.
/// </summary>
/// <remarks>This interface provides functionality to retrieve system logs based on specific criteria and export
/// them in a CSV format. It is designed for use in scenarios where log data needs to be queried or archived for
/// analysis.</remarks>
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
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation, returning a list of system logs.</returns>
    Task<List<SystemLogEntry>> FetchSystemLogsAsync(string topic, long? since = null, long? until = null, string? actorId = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Exports system logs to a CSV file.
    /// </summary>
    /// <param name="topic">The topic of the logs to export.</param>
    /// <param name="since">Start time for log exporting.</param>
    /// <param name="until">End time for log exporting.</param>
    /// <param name="timeZone">TimeZone for formatting time.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation, returning the CSV file as a byte array.</returns>
    Task<byte[]> ExportSystemLogsAsync(string topic, long since, long until, string timeZone, CancellationToken cancellationToken = default);
}
