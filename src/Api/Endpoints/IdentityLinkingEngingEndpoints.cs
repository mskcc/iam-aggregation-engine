using Flurl;
using HealthChecks.UI.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.IdentityLinkingService;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Api.Endpoints;

/// <summary>
/// Endpoints for Identity Linking Engine API.
/// </summary>
public static class IdentityLinkingEngineEndpoints
{
    /// <summary>
    /// Maps the Identity Linking Engine endpoints for the API.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapIdentityLinkingEngineEndpoints(this IEndpointRouteBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);
        
        var options = app.ServiceProvider.GetRequiredService<IOptionsMonitor<ApiOptions>>().CurrentValue;
        var testEndpoint = options.BaseEndpoint.AppendPathSegment("identitylinking/test/create");
        var linkPingFederateIdentityEndpoint = options.BaseEndpoint.AppendPathSegment("identitylinking/pingfederate/create");
        var linkMicrosoftIdentityEndpoint = options.BaseEndpoint.AppendPathSegment("identitylinking/microsoft/create");

        app.MapPost(linkPingFederateIdentityEndpoint, LinkPingFederateIdentity)
        .WithName("Create linked account for external ping federate identity provider")
        .WithOpenApi(options => {
            options.Description = "Use this to link a PingFederate identity in PingOne.";
            options.Summary = "Link a PingFederate identity in PingOne.";
            options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Ping Federate Identity Linking" } };

            return options;
        })
        .RequireAuthorization();

        app.MapPost(linkMicrosoftIdentityEndpoint, LinkMicrosoftIdentity)
        .WithName("Create linked account for external microsoft identity provider")
        .WithOpenApi(options => {
            options.Description = "Use this to link a microsoft identity in PingOne.";
            options.Summary = "Link a microsoft identity in PingOne.";
            options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Microsoft Identity Linking" } };

            return options;
        })
        .RequireAuthorization();

        return app;
    }

    /// <summary>
    /// Links the PingFederate identity in PingOne.
    /// </summary>
    /// <param name="identityLinkingService"></param>
    /// <param name="contextAccessor"></param>
    /// <param name="samAccountName"></param>
    /// <returns></returns>
    public static async Task<IResult> LinkPingFederateIdentity(
        [FromServices] NotifyIdentityLinkingColleague notifyIdentityLinkingColleague, 
        [FromBody] IdentityLinkingApiRequest identityLinkingApiRequest)
    {
        ArgumentNullException.ThrowIfNull(notifyIdentityLinkingColleague);
        ArgumentNullException.ThrowIfNull(identityLinkingApiRequest);

        if (string.IsNullOrEmpty(identityLinkingApiRequest.SamAccountName))
        {
            return Results.BadRequest("SamAccountName is required.");
        }

        var linkingObject = new PingFederateIdentityLinkingNotification
        {
            SamAccountName = identityLinkingApiRequest.SamAccountName,
            NotificationType = nameof(PingFederateIdentityLinkingNotification)
        };

        var result = await notifyIdentityLinkingColleague.Notify(linkingObject);
        
        return Results.Ok(result);
    }

    /// <summary>
    /// Links the Microsoft identity in PingOne.
    /// </summary>
    /// <param name="identityLinkingService"></param>
    /// <param name="contextAccessor"></param>
    /// <param name="samAccountName"></param>
    /// <returns></returns>
    public static async Task<IResult> LinkMicrosoftIdentity(
        [FromServices] NotifyIdentityLinkingColleague notifyIdentityLinkingColleague, 
        [FromBody] IdentityLinkingApiRequest identityLinkingApiRequest)
    {
        ArgumentNullException.ThrowIfNull(notifyIdentityLinkingColleague);
        ArgumentNullException.ThrowIfNull(identityLinkingApiRequest);

        if (string.IsNullOrEmpty(identityLinkingApiRequest.SamAccountName))
        {
            return Results.BadRequest("SamAccountName is required.");
        }

        var linkingObject = new MicrosoftIdentityLinkingNotification
        {
            SamAccountName = identityLinkingApiRequest.SamAccountName,
            NotificationType = nameof(MicrosoftIdentityLinkingNotification)
        };

        var result = await notifyIdentityLinkingColleague.Notify(linkingObject);
        
        return Results.Ok(result);
    }
}