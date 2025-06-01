using Microsoft.Extensions.Options;
using UniFi.Net;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extensions for adding the UniFi Client to the service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the UniFi Client to the service collection with the specified configuration.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> object that this method extends.</param>
    /// <param name="configure">An action to configure the client.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that calls can be chained.</returns>
    public static IServiceCollection AddUniFiClient(this IServiceCollection services, Action<UniFiConfig> configure)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.Configure(configure);

        services.AddHttpClient<UniFiClient>("UniFiClient", (services, client) =>
        {
            var config = services.GetRequiredService<IOptions<UniFiConfig>>().Value;
            client.BaseAddress = new Uri(config.Host);
            client.DefaultRequestHeaders.Add("X-API-KEY", config.ApiKey);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("User-Agent", "UniFi.Client/1.0");
        });

        services.AddSingleton<IUniFiClient, UniFiClient>();

        return services;
    }
}
