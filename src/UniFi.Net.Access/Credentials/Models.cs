namespace UniFi.Net.Access.Credentials;

/// <summary>
/// Represents a summary of NFC card details.
/// </summary>
public record NfcCardSummary(string Token, string DisplayId, string Status);

/// <summary>
/// Represents detailed information about an NFC card.
/// </summary>
public record NfcCardDetails(string Token, string DisplayId, string Status, string Alias);