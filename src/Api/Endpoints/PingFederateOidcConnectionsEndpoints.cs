using Flurl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PingFederate;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.Pagination;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.UseCases;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Api.Endpoints;

/// <summary>
/// Endpoints for PingFederate connections API.
/// </summary>
public static class PingFederateOidcConnectionsEndpoints
{
    /// <summary>
    /// Maps the PingFederate Connections endpoints for the API.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapPingFederateOidcConnectionsEndpoints(this IEndpointRouteBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        var optionsMonitor = app.ServiceProvider.GetService<IOptionsMonitor<ApiOptions>>();
        var options = optionsMonitor?.CurrentValue;

        ArgumentNullException.ThrowIfNull(options);

        var oidcConnectionsEndpoint = options.BaseEndpoint.AppendPathSegment(options.OidcConnectionsEndpoint);
        var oidcConnectionsAggregationEndpoint = options.BaseEndpoint.AppendPathSegment(options.OidcConnectionsAggregateEndpoint);
        var oidcConnectionsPurgeEndpoint = options.BaseEndpoint.AppendPathSegment(options.OidcConnectionsPurgeEndpoint);

        app.MapGet(oidcConnectionsEndpoint, GetOidcConnectionsAsync)
            .WithName("Get OIDC Connections")
            .WithOpenApi(options => {
                options.Description = "Use this to get a list of OIDC objects that represent all aggregated OIDC connections from Ping Federate.";
                options.Summary ="OIDC connections";
                options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "OIDC Connections" } };

                return options;
            })
            .RequireAuthorization();

        app.MapPost(oidcConnectionsAggregationEndpoint, AggregateOidcConnectionsAsync)
            .WithName("Run OIDC Connections Aggregation")
            .WithOpenApi(options => {
                options.Description = "Use this to aggregate all currently configured OIDC connections in Ping Federate.";
                options.Summary = "Aggregate OIDC connections";
                options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "OIDC Connections" } };

                return options;
            })
            .RequireAuthorization();

        app.MapPost(oidcConnectionsPurgeEndpoint, PurgeOidcConnectionsAsync)
            .WithName("Run OIDC Connections Purge")
            .WithOpenApi(options => {
                options.Description = "Use this to purge all OIDC connections in the configured databased.";
                options.Summary = "Purge OIDC connections";
                options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "OIDC Connections" } };

                return options;
            })
            .RequireAuthorization();

        return app;
    }

    /// <summary>
    /// Purges data from table by notifying <see cref="PurgeOidcConnectionColleague"/>
    /// </summary>
    /// <param name="pingFederateDbContext"></param>
    /// <returns><see cref="IResult"/></returns>
    public static async Task<IResult> PurgeOidcConnectionsAsync(
        [FromServices] IOptionsMonitor<ApiOptions> apiOptions,
        [FromServices] NotifyPurgeOidcColleague notifyPurgeOidcColleague)
    {
        ArgumentNullException.ThrowIfNull(apiOptions);
        ArgumentNullException.ThrowIfNull(notifyPurgeOidcColleague);

        var options = apiOptions.CurrentValue;
        var oidcConnectionsEndpoint = options.BaseEndpoint.AppendPathSegment(options.OidcConnectionsEndpoint);

        var response = await notifyPurgeOidcColleague.Notify();

        return Results.Accepted(oidcConnectionsEndpoint, response);
    }

    /// <summary>
    /// Gets all configured OIDC connections in PIng Federate using <see cref="PingFederateService"/>
    /// </summary>
    /// <param name="pingFederateService"></param>
    /// <returns><see cref="Task"/> of type <see cref="IResult"/></returns>
    public static async Task<IResult> GetOidcConnectionsAsync(
        [FromServices] NotifyGetOidcConnectionsColleague notifyGetOidcConnectionsColleague,
        [FromServices] IPaginationService paginationService,
        [FromQuery] int pageNumber = 0,
        [FromQuery] int pageSize = 0)
    {
        ArgumentNullException.ThrowIfNull(notifyGetOidcConnectionsColleague);
        ArgumentNullException.ThrowIfNull(paginationService);

        paginationService.PaginationFilter = new PaginationFilter(pageNumber, pageSize);
        var response = await notifyGetOidcConnectionsColleague.GetResponse();

        return Results.Ok(response);
    }

    /// <summary>
    /// Runs aggregation for configured OIDC connections in Ping Federate using <see cref="PingFederateService"/>
    /// </summary>
    /// <param name="pingFederateService"></param>
    /// <returns><see cref="IResult"/></returns>
    public static async Task<IResult> AggregateOidcConnectionsAsync(
        [FromServices] NotifyAggregateOidcConnectionsColleague notifyAggregateOidcConnectionsColleague,
        [FromServices] IOptionsMonitor<ApiOptions> apiOptions)
    {
        ArgumentNullException.ThrowIfNull(notifyAggregateOidcConnectionsColleague);
        ArgumentNullException.ThrowIfNull(apiOptions);

        var options = apiOptions.CurrentValue;
        var oidcConnectionsEndpoint = options.BaseEndpoint.AppendPathSegment(options.OidcConnectionsEndpoint);

        var resposne = await notifyAggregateOidcConnectionsColleague.Notify();

        return Results.Accepted(oidcConnectionsEndpoint, resposne);
    }
}