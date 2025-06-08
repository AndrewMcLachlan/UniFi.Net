namespace UniFi.Net.Access;

public interface ICredentialClient
{
    // Credentials
    /// <summary>
    /// Generates a new PIN code.
    /// </summary>
    /// <returns>Task representing the asynchronous operation, returning the generated PIN code.</returns>
    Task<string> GeneratePinCodeAsync();

    /// <summary>
    /// Enrolls a new NFC card.
    /// </summary>
    /// <param name="deviceId">The ID of the device to enroll the NFC card to.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task EnrollNfcCardAsync(string deviceId);

    /// <summary>
    /// Fetches details of a specific NFC card by token.
    /// </summary>
    /// <param name="cardToken">The token of the NFC card.</param>
    /// <returns>Task representing the asynchronous operation, returning NFC card details.</returns>
    Task<NfcCardDetails> FetchNfcCardAsync(string cardToken);

    /// <summary>
    /// Fetches all NFC cards with optional pagination.
    /// </summary>
    /// <param name="pageNum">Page number for pagination (optional).</param>
    /// <param name="pageSize">Number of NFC cards per page (optional).</param>
    /// <returns>Task representing the asynchronous operation, returning a list of NFC cards.</returns>
    Task<List<NfcCardSummary>> FetchAllNfcCardsAsync(int? pageNum = null, int? pageSize = null);

    /// <summary>
    /// Deletes an NFC card by token.
    /// </summary>
    /// <param name="cardToken">The token of the NFC card to delete.</param>
    /// <returns>Task representing the asynchronous operation.</returns>
    Task DeleteNfcCardAsync(string cardToken);
}
