using System.Text.Json;
using Flurl;
using HealthChecks.UI.Client;
using HealthChecks.UI.Core;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Api.Endpoints;

/// <summary>
/// Endpoints for PingFederate connections API.
/// </summary>
public static class HealthChecksEndpoints
{
    /// <summary>
    /// Maps the PingFederate Connections endpoints for the API.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapHealthChecksEndpoints(this IEndpointRouteBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);
        
        var options = app.ServiceProvider.GetRequiredService<IOptionsMonitor<ApiOptions>>().CurrentValue;
        var healthEndpoint = options.BaseEndpoint.AppendPathSegment("health");

        app.MapGet(healthEndpoint, async (HealthCheckService healthCheck, 
        IHttpContextAccessor contextAccessor) =>
        {
            var report = await healthCheck.CheckHealthAsync();
            var uiReport = UIHealthReport.CreateFrom(report);
            return Results.Ok(uiReport);
        })
        .WithName("Health Checks")
        .WithOpenApi(options => {
            options.Description = "Use this to get health checks status' for configured databsed that this API uses.";
            options.Summary = "Get health checks status for configured databases.";
            options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Health Checks" } };

            return options;
        })
        .RequireAuthorization();

        return app;
    }
}