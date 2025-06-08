using Microsoft.Extensions.Options;
using UniFi.Net;
using UniFi.Net.SiteManager;

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
    public static IServiceCollection AddUniFiSiteManagerClient(this IServiceCollection services, Action<UniFiSiteManagerConfig> configure)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.Configure(configure);

        services.AddHttpClient<SiteManagerClient>("SiteManagerClient", (provider, client) =>
        {
            var config = provider.GetRequiredService<IOptions<UniFiSiteManagerConfig>>().Value;
            HttpClientConfigurator.ConfigureHttpClient(client, new Uri(config.Host), config.ApiKey);
        });

        services.AddSingleton<ISiteManagerClient, SiteManagerClient>();

        return services;
    }
}
