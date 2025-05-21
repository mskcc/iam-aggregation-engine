using Flurl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Api.Endpoints;

/// <summary>
/// Endpoints for PingFederate insights API.
/// </summary>
public static class PingFederateInsightsEndpoints
{
    /// <summary>
    /// Maps the PingFederate Connections endpoints for the API.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapPingFederateInsightsEndpoints(this IEndpointRouteBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        var options = app.ServiceProvider.GetRequiredService<IOptionsMonitor<ApiOptions>>().CurrentValue;
        var pingFederateMissingTechnicalOwnerInsightsEndpoint = options.BaseEndpoint.AppendPathSegment("pingfederate/insights");

        app.MapGet(pingFederateMissingTechnicalOwnerInsightsEndpoint, GetPingFederateMissingTechnicalOwnerInsights)
            .WithName("Get Ping Federate Insights")
            .WithOpenApi(options =>
            {
                options.Description = "Use this to get ping federate application insights regarding connections with missing technical owners";
                options.Summary = "Ping Federate Insights";
                options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Ping Federate Insights" } };

                return options;
            })
            .RequireAuthorization();

        return app;
    }

    /// <summary>
    /// Gets the ping federate missing technical owner insights.``
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static async Task<IResult> GetPingFederateMissingTechnicalOwnerInsights(
        [FromServices] NotifyPingFederateInsightsColleague notifyPingFederateInsightsColleague
    )
    {
        ArgumentNullException.ThrowIfNull(notifyPingFederateInsightsColleague);

        var insightsObject = new InsightsNotification
        {
            NotificationType = nameof(InsightsPayload.MissingTechnicalOwners)
        };

        var result = await notifyPingFederateInsightsColleague.Notify(insightsObject);

        return Results.Ok(result);
    }
}