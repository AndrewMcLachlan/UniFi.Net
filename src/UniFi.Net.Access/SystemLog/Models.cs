namespace UniFi.Net.Access.SystemLog;
// Models for System Logs
public record SystemLogEntry(string Event, string Actor, string Target, string Timestamp);