using Flurl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.Pagination;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Api.Endpoints;

/// <summary>
/// Endpoints for PingFederate legacy connections API.
/// This endpoint serves as a means to maintain currently existing dependencies on legacy schemas.
/// </summary>
public static class PingFederateLegacyConnectionsEndpoints
{ 
    /// <summary>
    /// Maps the ping federate legacy connections endpoint for the API.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapPingFederateLegacyConnectionsEndpoints(this IEndpointRouteBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        var optionsMonitor = app.ServiceProvider.GetService<IOptionsMonitor<ApiOptions>>();
        var options = optionsMonitor?.CurrentValue;

        ArgumentNullException.ThrowIfNull(options);

        var legacyConnectionsEndpoint = options.BaseEndpoint.AppendPathSegment(options.LegacyConnectionsEndpoint);
        var legacyConnectionsSearchEndpoint = options.BaseEndpoint.AppendPathSegment(options.LegacyConnectionsEndpoint).AppendPathSegment("search");
        var legacyConnectionsAggregationEndpoint = options.BaseEndpoint.AppendPathSegment(options.LegacyConnectionsAggregateEndpoint);
        var legacyConnectionsPurgeEndpoint = options.BaseEndpoint.AppendPathSegment(options.LegacyConnectionsPurgeEndpoint);

        app.MapGet(legacyConnectionsEndpoint, GetLegacyConnectionsAsync)
            .WithName("Get Legacy Connections")
            .WithOpenApi(options => {
                options.Description = "Use this to get a list of legacy objects that represent all aggregated OIDC/SAML connections from Ping Federate.";
                options.Summary ="Legacy connections";
                options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Legacy Connections" } };

                return options;
            })
            .RequireAuthorization();

        app.MapGet(legacyConnectionsSearchEndpoint, SearchLegacyConnectionsAsync)
            .WithName("Search Legacy Connections")
            .WithOpenApi(options => {
                options.Description = "Use this to search a list of legacy objects that represent all aggregated OIDC/SAML connections from Ping Federate.";
                options.Summary ="Search Legacy connections";
                options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Search Legacy Connections" } };

                return options;
            })
            .RequireAuthorization();

        app.MapPost(legacyConnectionsAggregationEndpoint, AggregateLegacyConnectionsAsync)
            .WithName("Run Legacy Connections Aggregation")
            .WithOpenApi(options => {
                options.Description = "Use this to aggregate all currently configured OIDC/SAML connections in Ping Federate.";
                options.Summary = "Aggregate OIDC/SAML connections";
                options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Legacy Connections" } };

                return options;
            })
            .RequireAuthorization();

        app.MapPost(legacyConnectionsPurgeEndpoint, PurgeLegacyConnectionsAsync)
            .WithName("Run Legacy Connections Purge")
            .WithOpenApi(options => {
                options.Description = "Use this to purge all OIDC/SAML connections in the configured databased.";
                options.Summary = "Purge OIDC/SAML connections";
                options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Legacy Connections" } };

                return options;
            })
            .RequireAuthorization();

        return app;
    }

    /// <summary>
    /// Purges data from table by notifying <see cref="PurgeLegacyConnectionsColleague"/> 
    /// </summary>
    /// <param name="apiOptions"></param>
    /// <param name="notifyPurgeLegacyColleague"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static async Task<IResult> PurgeLegacyConnectionsAsync(
        [FromServices] IOptionsMonitor<ApiOptions> apiOptions,
        [FromServices] NotifyPurgeLegacyColleague notifyPurgeLegacyColleague)
    {
        ArgumentNullException.ThrowIfNull(apiOptions);
        ArgumentNullException.ThrowIfNull(notifyPurgeLegacyColleague);

        var options = apiOptions.CurrentValue;
        var legacyConnectionsEndpoint = options.BaseEndpoint.AppendPathSegment(options.LegacyConnectionsEndpoint);

        var response = await notifyPurgeLegacyColleague.Notify();

        return Results.Accepted(legacyConnectionsEndpoint, response);
    }

    /// <summary>
    /// Gets all configured legacy connections.
    /// </summary>
    /// <param name="notifyGetLegacyConnectionsColleague"></param>
    /// <param name="paginationService"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns><see cref="Task"/> of type <see cref="IResult"/></returns>
    public static async Task<IResult> GetLegacyConnectionsAsync(
        [FromServices] NotifyGetLegacyConnectionsColleague notifyGetLegacyConnectionsColleague,
        [FromServices] IPaginationService paginationService,
        [FromQuery] int pageNumber = 0,
        [FromQuery] int pageSize = 0)
    {
        ArgumentNullException.ThrowIfNull(notifyGetLegacyConnectionsColleague);
        ArgumentNullException.ThrowIfNull(paginationService);

        paginationService.PaginationFilter = new PaginationFilter(pageNumber, pageSize);
        var response = await notifyGetLegacyConnectionsColleague.GetResponse();

        return Results.Ok(response);
    }

    /// <summary>
    /// Gets all configured legacy connections in PIng Federate using <see cref="PingFederateService"/>
    /// </summary>
    /// <param name="notifySearchLegacyConnectionsColleague"></param>
    /// <param name="paginationService"></param>
    /// <param name="criteria"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns><see cref="Task"/> of type <see cref="IResult"/></returns>
    public static async Task<IResult> SearchLegacyConnectionsAsync(
        [FromServices] NotifySearchLegacyConnectionsColleague notifySearchLegacyConnectionsColleague,
        [FromServices] IPaginationService paginationService,
        [FromQuery] string criteria,
        [FromQuery] int pageNumber = 0,
        [FromQuery] int pageSize = 0)
    {
        ArgumentNullException.ThrowIfNull(notifySearchLegacyConnectionsColleague);
        ArgumentNullException.ThrowIfNull(paginationService);
        ArgumentNullException.ThrowIfNull(paginationService);

        paginationService.PaginationFilter = new PaginationFilter(pageNumber, pageSize);
        var searchCriteria = new SearchCriteria
        {
            Criteria = criteria
        };
        var response = await notifySearchLegacyConnectionsColleague.GetResponse(searchCriteria);

        return Results.Ok(response);
    }

    /// <summary>
    /// Runs aggregation for configured legacy connections.
    /// </summary>
    /// <param name="notifyAggregateLegacyConnectionsColleague"></param>
    /// <param name="apiOptions"></param>
    /// <returns><see cref="IResult"/></returns>
    public static async Task<IResult> AggregateLegacyConnectionsAsync(
        [FromServices] NotifyAggregateLegacyConnectionsColleague notifyAggregateLegacyConnectionsColleague,
        [FromServices] IOptionsMonitor<ApiOptions> apiOptions)
    {
        ArgumentNullException.ThrowIfNull(notifyAggregateLegacyConnectionsColleague);
        ArgumentNullException.ThrowIfNull(apiOptions);

        var options = apiOptions.CurrentValue;
        var legacyConnectionsEndpoint = options.BaseEndpoint.AppendPathSegment(options.LegacyConnectionsEndpoint);

        var resposne = await notifyAggregateLegacyConnectionsColleague.Notify();

        return Results.Accepted(legacyConnectionsEndpoint, resposne);
    }
}