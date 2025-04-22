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
/// Endpoints for Service Now API.
/// </summary>
public static class ServiceNowEndpoints
{
    /// <summary>
    /// Maps the service now endpoints for the API.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapServiceNowEndpoints(this IEndpointRouteBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        var optionsMonitor = app.ServiceProvider.GetService<IOptionsMonitor<ApiOptions>>();
        var options = optionsMonitor?.CurrentValue;
        ArgumentNullException.ThrowIfNull(options);

        var serviceNowApplicationsAggregationEndpoint = options.BaseEndpoint.AppendPathSegment(options.ServiceNowApplicationsAggregationEndpoint);
        var serviceNowApplicationsEndpoint = options.BaseEndpoint.AppendPathSegment(options.ServiceNowApplicationsEndpoint);
        var serviceNowApplicationsPurgeEndpoint = options.BaseEndpoint.AppendPathSegment(options.ServiceNowApplicationsPurgeEndpoint);

        var serviceNowUsersAggregationEndpoint = options.BaseEndpoint.AppendPathSegment(options.ServiceNowUsersAggregationEndpoint);
        var serviceNowUsersEndpoint = options.BaseEndpoint.AppendPathSegment(options.ServiceNowUsersEndpoint);
        var serviceNowUsersPurgeEndpoint = options.BaseEndpoint.AppendPathSegment(options.ServiceNowUsersPurgeEndpoint);

        app.MapGet(serviceNowApplicationsEndpoint, GetServiceNowApplicationsAsync)
            .WithName("Get Service Now Applications")
            .WithOpenApi(options => {
                options.Description = "Use this to get a list of applications from Service Now.";
                options.Summary = "Service Now applications";
                options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Service Now Applications" } };

                return options;
            })
            .RequireAuthorization();
        
        app.MapPost(serviceNowApplicationsAggregationEndpoint, AggregateServiceNowApplicationsAsync)
            .WithName("Run Service Now Applications Aggregation")
            .WithOpenApi(options => {
                options.Description = "Use this to aggregate all currently configured applications in Service Now.";
                options.Summary = "Aggregate applications";
                options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Service Now Applications" } };

                return options;
            })
            .RequireAuthorization();

        app.MapPost(serviceNowApplicationsPurgeEndpoint, PurgeServiceNowApplicationsAsync)
            .WithName("Run Service Now Applications Purge")
            .WithOpenApi(options => {
                options.Description = "Use this to purge all currently configured applications in Service Now.";
                options.Summary = "Purge applications";
                options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Service Now Applications" } };

                return options;
            })
            .RequireAuthorization();


        app.MapGet(serviceNowUsersEndpoint, GetServiceNowUsersAsync)
            .WithName("Get Service Now Users")
            .WithOpenApi(options => {
                options.Description = "Use this to get a list of users from Service Now.";
                options.Summary = "Service Now users";
                options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Service Now Users" } };

                return options;
            })
            .RequireAuthorization();
        
        app.MapPost(serviceNowUsersAggregationEndpoint, AggregateServiceNowUsersAsync)
            .WithName("Run Service Now Users Aggregation")
            .WithOpenApi(options => {
                options.Description = "Use this to aggregate all currently configured Users in Service Now.";
                options.Summary = "Aggregate Users";
                options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Service Now Users" } };

                return options;
            })
            .RequireAuthorization();

        app.MapPost(serviceNowUsersPurgeEndpoint, PurgeServiceNowUsersAsync)
            .WithName("Run Service Now Users Purge")
            .WithOpenApi(options => {
                options.Description = "Use this to purge all currently configured Users in Service Now.";
                options.Summary = "Purge Users";
                options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Service Now Users" } };

                return options;
            })
            .RequireAuthorization();

        return app;
    }

    /// <summary>
    /// Returns all currently configured applications in Service Now.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static async Task<IResult> GetServiceNowApplicationsAsync(
        [FromServices] NotifyGetServiceNowApplicationsColleague notifyGetServiceNowApplicationsColleague,
        [FromServices] IPaginationService paginationService,
        [FromQuery] int pageNumber = 0,
        [FromQuery] int pageSize = 0,
        [FromQuery] string? applicationNameFilter = null)
    {
        ArgumentNullException.ThrowIfNull(notifyGetServiceNowApplicationsColleague);
        ArgumentNullException.ThrowIfNull(paginationService);

        paginationService.PaginationFilter = new PaginationFilter(pageNumber, pageSize);
        
        var response = await notifyGetServiceNowApplicationsColleague.GetResponse(new {
            ApplicationNameFilter = applicationNameFilter
        });

        return Results.Ok(response);
    }

    /// <summary>
    /// Aggregates all currently configured applications in Service Now.
    /// </summary>
    /// <param name="notifyAggregateServiceNowApplicationsColleague"></param>
    /// <param name="apiOptions"></param>
    public static async Task<IResult> AggregateServiceNowApplicationsAsync(
        [FromServices] NotifyAggregateServiceNowApplicationsColleague notifyAggregateServiceNowApplicationsColleague,
        [FromServices] IOptionsMonitor<ApiOptions> apiOptions)
    {
        ArgumentNullException.ThrowIfNull(notifyAggregateServiceNowApplicationsColleague);
        ArgumentNullException.ThrowIfNull(apiOptions);

        var options = apiOptions.CurrentValue;
        var serviceNowApplicationsEndpoint = options.BaseEndpoint.AppendPathSegment(options.ServiceNowApplicationsEndpoint);

        var response = await notifyAggregateServiceNowApplicationsColleague.Notify();
        return Results.Accepted(serviceNowApplicationsEndpoint, response);
    }

    /// <summary>
    /// Purges all currently configured applications in Service Now.
    /// </summary>
    public static async Task<IResult> PurgeServiceNowApplicationsAsync(
        [FromServices] IOptionsMonitor<ApiOptions> apiOptions,
        [FromServices] NotifyPurgeServiceNowApplicationsColleague notifyPurgeServiceNowApplicationsColleague)
    {
        ArgumentNullException.ThrowIfNull(apiOptions);
        ArgumentNullException.ThrowIfNull(notifyPurgeServiceNowApplicationsColleague);

        var options = apiOptions.CurrentValue;
        var oidcConnectionsEndpoint = options.BaseEndpoint.AppendPathSegment(options.ServiceNowApplicationsEndpoint);

        var response = await notifyPurgeServiceNowApplicationsColleague.Notify();

        return Results.Accepted(oidcConnectionsEndpoint, response);
    }

    /// <summary>
    /// Returns all currently configured Users in Service Now.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static async Task<IResult> GetServiceNowUsersAsync(
        [FromServices] NotifyGetServiceNowUsersColleague notifyGetServiceNowUsersColleague,
        [FromServices] IPaginationService paginationService,
        [FromQuery] int pageNumber = 0,
        [FromQuery] int pageSize = 0)
    {
        ArgumentNullException.ThrowIfNull(notifyGetServiceNowUsersColleague);
        ArgumentNullException.ThrowIfNull(paginationService);

        paginationService.PaginationFilter = new PaginationFilter(pageNumber, pageSize);
        var response = await notifyGetServiceNowUsersColleague.GetResponse();

        return Results.Ok(response);
    }

    /// <summary>
    /// Aggregates all currently configured Users in Service Now.
    /// </summary>
    /// <param name="notifyAggregateServiceNowUsersColleague"></param>
    /// <param name="apiOptions"></param>
    public static async Task<IResult> AggregateServiceNowUsersAsync(
        [FromServices] NotifyAggregateServiceNowUsersColleague notifyAggregateServiceNowUsersColleague,
        [FromServices] IOptionsMonitor<ApiOptions> apiOptions)
    {
        ArgumentNullException.ThrowIfNull(notifyAggregateServiceNowUsersColleague);
        ArgumentNullException.ThrowIfNull(apiOptions);

        var options = apiOptions.CurrentValue;
        var serviceNowUsersEndpoint = options.BaseEndpoint.AppendPathSegment(options.ServiceNowUsersEndpoint);

        var response = await notifyAggregateServiceNowUsersColleague.Notify();
        return Results.Accepted(serviceNowUsersEndpoint, response);
    }

    /// <summary>
    /// Purges all currently configured Users in Service Now.
    /// </summary>
    public static async Task<IResult> PurgeServiceNowUsersAsync(
        [FromServices] IOptionsMonitor<ApiOptions> apiOptions,
        [FromServices] NotifyPurgeServiceNowUsersColleague notifyPurgeServiceNowUsersColleague)
    {
        ArgumentNullException.ThrowIfNull(apiOptions);
        ArgumentNullException.ThrowIfNull(notifyPurgeServiceNowUsersColleague);

        var options = apiOptions.CurrentValue;
        var oidcConnectionsEndpoint = options.BaseEndpoint.AppendPathSegment(options.ServiceNowUsersEndpoint);

        var response = await notifyPurgeServiceNowUsersColleague.Notify();

        return Results.Accepted(oidcConnectionsEndpoint, response);
    }
}