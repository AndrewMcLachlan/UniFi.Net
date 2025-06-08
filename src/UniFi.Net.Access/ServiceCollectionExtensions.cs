using Microsoft.Extensions.Options;
using UniFi.Net;
using UniFi.Net.Access;
using UniFi.Net.Access.AccessPolicies;
using UniFi.Net.Access.Credentials;
using UniFi.Net.Access.Devices;
using UniFi.Net.Access.Spaces;
using UniFi.Net.Access.SystemLogs;
using UniFi.Net.Access.UniFiIdentities;
using UniFi.Net.Access.Users;
using UniFi.Net.Access.Visitors;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extensions for adding the UniFi Client to the service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds all UniFi Access clients to the service collection with the specified configuration.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> object that this method extends.</param>
    /// <param name="configure">An action to configure the client.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that calls can be chained.</returns>
    public static IServiceCollection AddUniFiAccessClients(this IServiceCollection services, Action<UniFiConfig> configure)
    {
        ArgumentNullException.ThrowIfNull(services);

        // TODO: This is suboptimal as the configuration action is applied to each client individually.
        return services.AddUniFiAccessPolicyClient(configure)
            .AddUniFiUserClient(configure)
            .AddUniFiSpaceClient(configure)
            .AddUniFiVisitorClient(configure)
            .AddUniFiCredentialClient(configure)
            .AddUniFiSystemLogClient(configure)
            .AddUniFiIdentityClient(configure)
            .AddUniFiDeviceClient(configure);
    }

    /// <summary>
    /// Adds the UniFi Access Policy Client to the service collection with the specified configuration.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> object that this method extends.</param>
    /// <param name="configure">An action to configure the client.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that calls can be chained.</returns>
    public static IServiceCollection AddUniFiAccessPolicyClient(this IServiceCollection services, Action<UniFiConfig> configure)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.Configure(configure);

        services.AddHttpClient<AccessPolicyClient>("AccessClient", (provider, client) =>
        {
            var config = provider.GetRequiredService<IOptions<UniFiConfig>>().Value;
            HttpClientConfigurator.ConfigureHttpClient(client, new Uri(config.Host), config.ApiKey);
        });

        services.AddSingleton<IAccessPolicyClient, AccessPolicyClient>();

        return services;
    }

    /// <summary>
    /// Adds the UniFi User Client to the service collection with the specified configuration.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> object that this method extends.</param>
    /// <param name="configure">An action to configure the client.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that calls can be chained.</returns>
    public static IServiceCollection AddUniFiUserClient(this IServiceCollection services, Action<UniFiConfig> configure)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.Configure(configure);

        services.AddHttpClient<UserClient>("UserClient", (provider, client) =>
        {
            var config = provider.GetRequiredService<IOptions<UniFiConfig>>().Value;
            HttpClientConfigurator.ConfigureHttpClient(client, new Uri(config.Host), config.ApiKey);
        });

        services.AddSingleton<IUserClient, UserClient>();

        return services;
    }

    /// <summary>
    /// Adds the UniFi Space Client to the service collection with the specified configuration.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> object that this method extends.</param>
    /// <param name="configure">An action to configure the client.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that calls can be chained.</returns>
    public static IServiceCollection AddUniFiSpaceClient(this IServiceCollection services, Action<UniFiConfig> configure)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.Configure(configure);

        services.AddHttpClient<SpaceClient>("SpaceClient", (provider, client) =>
        {
            var config = provider.GetRequiredService<IOptions<UniFiConfig>>().Value;
            HttpClientConfigurator.ConfigureHttpClient(client, new Uri(config.Host), config.ApiKey);
        });

        services.AddSingleton<ISpaceClient, SpaceClient>();

        return services;
    }

    /// <summary>
    /// Adds the UniFi Visitor Client to the service collection with the specified configuration.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> object that this method extends.</param>
    /// <param name="configure">An action to configure the client.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that calls can be chained.</returns>
    public static IServiceCollection AddUniFiVisitorClient(this IServiceCollection services, Action<UniFiConfig> configure)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.Configure(configure);

        services.AddHttpClient<VisitorClient>("VisitorClient", (provider, client) =>
        {
            var config = provider.GetRequiredService<IOptions<UniFiConfig>>().Value;
            HttpClientConfigurator.ConfigureHttpClient(client, new Uri(config.Host), config.ApiKey);
        });

        services.AddSingleton<IVisitorClient, VisitorClient>();

        return services;
    }

    /// <summary>
    /// Adds the UniFi Credential Client to the service collection with the specified configuration.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> object that this method extends.</param>
    /// <param name="configure">An action to configure the client.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that calls can be chained.</returns>
    public static IServiceCollection AddUniFiCredentialClient(this IServiceCollection services, Action<UniFiConfig> configure)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.Configure(configure);

        services.AddHttpClient<CredentialClient>("CredentialClient", (provider, client) =>
        {
            var config = provider.GetRequiredService<IOptions<UniFiConfig>>().Value;
            HttpClientConfigurator.ConfigureHttpClient(client, new Uri(config.Host), config.ApiKey);
        });

        services.AddSingleton<ICredentialClient, CredentialClient>();

        return services;
    }

    /// <summary>
    /// Adds the UniFi System Log Client to the service collection with the specified configuration.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> object that this method extends.</param>
    /// <param name="configure">An action to configure the client.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that calls can be chained.</returns>
    public static IServiceCollection AddUniFiSystemLogClient(this IServiceCollection services, Action<UniFiConfig> configure)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.Configure(configure);

        services.AddHttpClient<SystemLogClient>("SystemLogClient", (provider, client) =>
        {
            var config = provider.GetRequiredService<IOptions<UniFiConfig>>().Value;
            HttpClientConfigurator.ConfigureHttpClient(client, new Uri(config.Host), config.ApiKey);
        });

        services.AddSingleton<ISystemLogClient, SystemLogClient>();

        return services;
    }

    /// <summary>
    /// Adds the UniFi Identity Client to the service collection with the specified configuration.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> object that this method extends.</param>
    /// <param name="configure">An action to configure the client.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that calls can be chained.</returns>
    public static IServiceCollection AddUniFiIdentityClient(this IServiceCollection services, Action<UniFiConfig> configure)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.Configure(configure);

        services.AddHttpClient<UniFiIdentityClient>("IdentityClient", (provider, client) =>
        {
            var config = provider.GetRequiredService<IOptions<UniFiConfig>>().Value;
            HttpClientConfigurator.ConfigureHttpClient(client, new Uri(config.Host), config.ApiKey);
        });

        services.AddSingleton<IUniFiIdentityClient, UniFiIdentityClient>();

        return services;
    }

    /// <summary>
    /// Adds the UniFi Device Client to the service collection with the specified configuration.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> object that this method extends.</param>
    /// <param name="configure">An action to configure the client.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that calls can be chained.</returns>
    public static IServiceCollection AddUniFiDeviceClient(this IServiceCollection services, Action<UniFiConfig> configure)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.Configure(configure);

        services.AddHttpClient<DeviceClient>("DeviceClient", (provider, client) =>
        {
            var config = provider.GetRequiredService<IOptions<UniFiConfig>>().Value;
            HttpClientConfigurator.ConfigureHttpClient(client, new Uri(config.Host), config.ApiKey);
        });

        services.AddSingleton<IDeviceClient, DeviceClient>();

        return services;
    }
}
