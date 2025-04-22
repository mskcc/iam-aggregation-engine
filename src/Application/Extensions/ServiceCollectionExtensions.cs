using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Http;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PingOne;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.TokenService;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;
using Polly;
using Polly.Extensions.Http;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add Ping Identity services to the service collection.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddPingIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddSingleton<ITokenService, PingOneTokenService>();

        services.AddTransient<IPingOneService, PingOneService>();

        return services;
    }

    /// <summary>
    /// Add Ping Identity HTTP clients to the service collection.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddPingIdentityHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddPingOneHttpClients(configuration);

        return services;
    }

    private static IServiceCollection AddPingOneHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        var logger = services.BuildServiceProvider().GetService<ILogger<ServiceCollection>>();
        ArgumentNullException.ThrowIfNull(logger);

        services
            .AddTransient<PingOneHttpHandler>()
            .AddHttpClient(HttpClientNames.PingOneClient, (services, client) =>
            {
                var pingOneOptionsMonitor = services.GetRequiredService<IOptionsMonitor<PingOneOptions>>();
                var pingOneOptions = pingOneOptionsMonitor.CurrentValue;
                var apiBaseUrl = pingOneOptions.ApiBaseUrl;
                ArgumentNullException.ThrowIfNullOrEmpty(apiBaseUrl);

                client.BaseAddress = new Uri(apiBaseUrl);
            })
            .ConfigurePrimaryHttpMessageHandler((services) =>
            {
                var pingOneOptionsMonitor = services.GetRequiredService<IOptionsMonitor<PingOneOptions>>();
                var pingOneOptions = pingOneOptionsMonitor.CurrentValue;
                var handler = new HttpClientHandler();

                if (pingOneOptions.TrustServerSslCertificate is true)
                {
                    handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                }

                return handler;
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            .AddHttpMessageHandler<PingOneHttpHandler>()
            .AddPolicyHandler(GetCircuitBreakerPolicy(logger));

        services
            .AddHttpClient(HttpClientNames.PingOneOauthClient, (services, client) =>
            {
                var pingOneOptionsMonitor = services.GetRequiredService<IOptionsMonitor<PingOneOptions>>();
                var pingOneOptions = pingOneOptionsMonitor.CurrentValue;
                var authority = pingOneOptions.Authority;
                ArgumentNullException.ThrowIfNullOrEmpty(authority);

                client.BaseAddress = new Uri(authority);
            });

        return services;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(ILogger<ServiceCollection> logger)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking: 5,
                durationOfBreak: TimeSpan.FromSeconds(30), 
                onBreak: (response, timespan) =>
                {
                    logger.LogInformation("Circuit broken! Failing requests for {TotalSeconds} seconds.", timespan.TotalSeconds);
                },
                onReset: () =>
                {
                    logger.LogInformation("Circuit reset! Normal operation resumed.");
                },
                onHalfOpen: () =>
                {
                    logger.LogInformation("Circuit in half-open state. Next request determines full recovery or break.");
                }
            );
    }
}