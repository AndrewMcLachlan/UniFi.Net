namespace UniFi.Net.Access.Credentials;

/// <summary>
/// A client for managing credentials-related operations in the UniFi Access system.
/// </summary>
public interface ICredentialClient
{
    // Credentials
    /// <summary>
    /// Generates a new PIN code.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation, returning the generated PIN code.</returns>
    Task<string> GeneratePinCodeAsync( CancellationToken cancellationToken = default);

    /// <summary>
    /// Enrols a new NFC card.
    /// </summary>
    /// <param name="deviceId">The ID of the device to enrol the NFC card to.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task EnrolNfcCardAsync(string deviceId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches details of a specific NFC card by token.
    /// </summary>
    /// <param name="cardToken">The token of the NFC card.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation, returning NFC card details.</returns>
    Task<NfcCardDetails> FetchNfcCardAsync(string cardToken, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches all NFC cards with optional pagination.
    /// </summary>
    /// <param name="pageNum">Page number for pagination (optional).</param>
    /// <param name="pageSize">Number of NFC cards per page (optional).</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation, returning a list of NFC cards.</returns>
    Task<List<NfcCardSummary>> FetchAllNfcCardsAsync(int? pageNum = null, int? pageSize = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an NFC card by token.
    /// </summary>
    /// <param name="cardToken">The token of the NFC card to delete.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task DeleteNfcCardAsync(string cardToken, CancellationToken cancellationToken = default);
}
