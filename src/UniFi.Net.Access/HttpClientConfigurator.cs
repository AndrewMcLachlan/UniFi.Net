namespace UniFi.Net;
internal static class HttpClientConfigurator
{
    public static HttpClient ConfigureHttpClient(HttpClient client, Uri host, string apiKey)
    {
        client.BaseAddress = host;
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer {apiKey}");
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        client.DefaultRequestHeaders.Add("User-Agent", "UniFi.Net/1.0");

        return client;
    }
}
