namespace UniFi.Net;
internal static class HttpClientConfigurator
{
    public static HttpClient ConfigureHttpClient(HttpClient client, Uri host, string apiKey)
    {
        client.BaseAddress = host;
        client.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        client.DefaultRequestHeaders.Add("User-Agent", "UniFi.Client/1.0");

        return client;
    }
}
