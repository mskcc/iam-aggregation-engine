using Flurl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PingFederate;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.UseCases;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.Pagination;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Api.Endpoints;

/// <summary>
/// Endpoints for PingFederate connections API.
/// </summary>
public static class PingFederateSamlConnectionsEndpoints
{
    /// <summary>
    /// Maps the PingFederate Connections endpoints for the API.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapPingFederateSamlConnectionsEndpoints(this IEndpointRouteBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        var optionsMonitor = app.ServiceProvider.GetService<IOptionsMonitor<ApiOptions>>();
        var options = optionsMonitor?.CurrentValue;

        ArgumentNullException.ThrowIfNull(options);

        var samlConnectionsEndpoint = options.BaseEndpoint.AppendPathSegment(options.SamlConnectionsEndpoint);
        var samlConnectionsAggregationEndpoint = options.BaseEndpoint.AppendPathSegment(options.SamlConnectionsAggregateEndpoint);
        var samlConnectionsPurgeEndpoint = options.BaseEndpoint.AppendPathSegment(options.SamlConnectionsPurgeEndpoint);

        app.MapGet(samlConnectionsEndpoint, GetSpConnections)
            .WithName("Get SAML Connections")
            .WithOpenApi(options => {
                options.Description = "Use this to get a list of SAML objects that represent all aggregated SAML connections from Ping Federate.";
                options.Summary ="SAML connections";
                options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "SAML Connections" } };

                return options;
            })
            .RequireAuthorization();

        app.MapPost(samlConnectionsAggregationEndpoint, RunSpConnectionsAggregation)
            .WithName("Run SAML Connections Aggregation")
            .WithOpenApi(options => {
                options.Description = "Use this to aggregate all currently configured SAML connections in Ping Federate.";
                options.Summary = "Aggregate SAML connections";
                options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "SAML Connections" } };

                return options;
            })
            .RequireAuthorization();

        app.MapPost(samlConnectionsPurgeEndpoint, PurgeSamlConnectionsAsync)
            .WithName("Run SAML Connections Purge")
            .WithOpenApi(options => {
                options.Description = "Use this to purge all SAML connections in configred database.";
                options.Summary = "Purge SAML connections";
                options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "SAML Connections" } };
                
                return options;
            })
            .RequireAuthorization();

        return app;
    }

    /// <summary>
    /// Purges data from table by notifying <see cref="PurgeSamlConnectionColleague"/>
    /// </summary>
    /// <param name="pingFederateDbContext"></param>
    /// <returns><see cref="IResult"/></returns>
    public static async Task<IResult> PurgeSamlConnectionsAsync(
        [FromServices] IOptionsMonitor<ApiOptions> apiOptions,
        [FromServices] NotifyPurgeSamlColleague notifyPurgeSamlColleague)
    {
        ArgumentNullException.ThrowIfNull(apiOptions);
        ArgumentNullException.ThrowIfNull(notifyPurgeSamlColleague);

        var options = apiOptions.CurrentValue;
        var spConnectionsEndpoint = options.BaseEndpoint.AppendPathSegment(options.SamlConnectionsEndpoint);

        var response = await notifyPurgeSamlColleague.Notify();

        return Results.Accepted(spConnectionsEndpoint, response);
    }

    
    /// <summary>
    /// Get SAML SP Connections by notifying <see cref="GetSamlConnectionsColleague"/>
    /// </summary>
    /// <param name="pingFederateService"></param>
    /// <returns><see cref="IResult"/></returns>
    public static async Task<IResult> GetSpConnections(
        [FromServices] NotifyGetSamlConnectionsColleague notifyGetSamlConnectionsColleague,
        [FromServices] IPaginationService paginationService,
        [FromQuery] int pageNumber = 0,
        [FromQuery] int pageSize = 0)
    {
        ArgumentNullException.ThrowIfNull(notifyGetSamlConnectionsColleague);
        ArgumentNullException.ThrowIfNull(paginationService);

        paginationService.PaginationFilter = new PaginationFilter(pageNumber, pageSize);
        var response = await notifyGetSamlConnectionsColleague.GetResponse();

        return Results.Ok(response);
    }

    /// <summary>
    /// Run SAML SP Connections aggregation using <see cref="PingFederateService"/>
    /// </summary>
    /// <param name="pingFederateService"></param>
    /// <returns><see cref="IResult"/></returns>
    public static async Task<IResult> RunSpConnectionsAggregation(
        [FromServices] NotifyAggregateSamlConnectionsColleague notifyAggregateSamlConnectionsColleague,
        [FromServices] IOptionsMonitor<ApiOptions> apiOptions)
    {
        ArgumentNullException.ThrowIfNull(notifyAggregateSamlConnectionsColleague);
        ArgumentNullException.ThrowIfNull(apiOptions);

        var options = apiOptions.CurrentValue;
        var spConnectionsEndpoint = options.BaseEndpoint.AppendPathSegment(options.SamlConnectionsEndpoint);

        var resposne = await notifyAggregateSamlConnectionsColleague.Notify();

        return Results.Accepted(spConnectionsEndpoint, resposne);

    }
}