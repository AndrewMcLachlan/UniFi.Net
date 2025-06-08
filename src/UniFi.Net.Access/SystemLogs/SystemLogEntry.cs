namespace UniFi.Net.Access.SystemLogs;

/// <summary>
/// System log entry representing a single log event in UniFi Access.
/// </summary>
/// <param name="Event">The event description.</param>
/// <param name="Actor">The actor responsible for the event.</param>
/// <param name="Target">The target of the event.</param>
/// <param name="Timestamp">The time when the event occurred.</param>
public record SystemLogEntry(string Event, string Actor, string Target, string Timestamp);