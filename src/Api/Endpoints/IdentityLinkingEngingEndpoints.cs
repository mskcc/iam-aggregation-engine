using Flurl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Exceptions;
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
        var linkPingFederateIdentityEndpoint = options.BaseEndpoint.AppendPathSegment("identitylinking/pingfederate/create");
        var linkMicrosoftEntraIdentityEndpoint = options.BaseEndpoint.AppendPathSegment("identitylinking/microsoft/create");
        var ldapGatewayIdentityEndpoint = options.BaseEndpoint.AppendPathSegment("identitylinking/ldap/create");
        
        var linkAllTargetsIdentityEndpoint = options.BaseEndpoint.AppendPathSegment("identitylinking/all/create");
        var recurringBatchJobStartAllIdentityEndpoint = options.BaseEndpoint.AppendPathSegment("identitylinking/recurringbatchjob/start");
        var recurringBatchJobStopAllIdentityEndpoint = options.BaseEndpoint.AppendPathSegment("identitylinking/recurringbatchjob/stop");

        var unlinkAllAccountsIdentityEndpoint = options.BaseEndpoint.AppendPathSegment("identitylinking/all/delete");
        var unlinkPingFederateIdentityEndpoint = options.BaseEndpoint.AppendPathSegment("identitylinking/pingfederate/delete");
        var unlinkMicrosoftEntraIdentityEndpoint = options.BaseEndpoint.AppendPathSegment("identitylinking/microsoft/delete");
        var unlinkLdapGatewayIdentityEndpoint = options.BaseEndpoint.AppendPathSegment("identitylinking/ldap/delete");

        app.MapPost(linkPingFederateIdentityEndpoint, LinkPingFederateIdentity)
        .WithName("Create linked account for external ping federate identity provider")
        .WithOpenApi(options => {
            options.Description = "Use this to link a PingFederate identity in PingOne.";
            options.Summary = "Link a PingFederate identity in PingOne.";
            options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Ping Federate Identity Linking" } };

            return options;
        })
        .RequireAuthorization();

        app.MapPost(linkMicrosoftEntraIdentityEndpoint, LinkEntraIdentity)
        .WithName("Create linked account for external microsoft identity provider")
        .WithOpenApi(options => {
            options.Description = "Use this to link a microsoft identity in PingOne.";
            options.Summary = "Link a microsoft identity in PingOne.";
            options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Microsoft Identity Linking" } };

            return options;
        })
        .RequireAuthorization();

        app.MapPost(ldapGatewayIdentityEndpoint, LinkLdapGatewayIdentity)
        .WithName("Create linked account for external LDAP gateway")
        .WithOpenApi(options => {
            options.Description = "Use this to link a LDAP gateway in PingOne.";
            options.Summary = "Link a LDAP gateway in PingOne.";
            options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "LDAP gateway Identity Linking" } };

            return options;
        })
        .RequireAuthorization();

        app.MapPost(linkAllTargetsIdentityEndpoint, LinkAllTargetsForIdentity)
        .WithName("Create linked account for external LDAP Gateway, Entra ID and Ping Federate in batches.")
        .WithOpenApi(options => {
            options.Description = "Use this to link identities to all three identity providers in PingOne.";
            options.Summary = "Link identities to all three identity providers in PingOne.";
            options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Link Batch Identities To All Identity Proivders" } };

            return options;
        })
        .RequireAuthorization();

        app.MapPost(recurringBatchJobStartAllIdentityEndpoint, StartProcessingLinkAllBatchIdentities)
        .WithName("Start Processing of the batch identity linking job.")
        .WithOpenApi(options => {
            options.Description = "Start the processing of the batch identity linking job.";
            options.Summary = "Start the processing of the batch identity linking job.";
            options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Start Bulk Identity Linking Processing" } };

            return options;
        })
        .RequireAuthorization();

        app.MapPost(recurringBatchJobStopAllIdentityEndpoint, StopProcessingLinkAllBatchIdentities)
        .WithName("Stop Processing of the batch identity linking job.")
        .WithOpenApi(options => {
            options.Description = "Stops the processing of the batch identity linking job.";
            options.Summary = "Stops the processing of the batch identity linking job.";
            options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Stop Bulk Identity Linking Processing" } };

            return options;
        })
        .RequireAuthorization();

        app.MapPost(unlinkAllAccountsIdentityEndpoint, UnlinkAllIdentityProviderAccounts)
        .WithName("Unlink all identity provider accounts for a given user in PingOne.")
        .WithOpenApi(options => {
            options.Description = "Use this to unlink all identity provider accounts for a given user in PingOne.";
            options.Summary = "Unlinks all identity provider accounts for a given user in PingOne by deleting the linked account.";
            options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Unlink All Identity Providers" } };

            return options;
        })
        .RequireAuthorization();

        app.MapPost(unlinkPingFederateIdentityEndpoint, UnlinkPingFederateIdentityProviderAccounts)
        .WithName("Unlink Ping Federate identity provider accounts for a given user in PingOne.")
        .WithOpenApi(options => {
            options.Description = "Use this to unlink Ping Federate identity provider accounts for a given user in PingOne.";
            options.Summary = "Unlinks Ping Federate identity provider accounts for a given user in PingOne by deleting the linked account.";
            options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Unlink Ping Federate Identity Providers" } };

            return options;
        })
        .RequireAuthorization();

        app.MapPost(unlinkMicrosoftEntraIdentityEndpoint, UnlinkMicrosoftEntraIdIdentityProviderAccounts)
        .WithName("Unlink Microsoft Entra identity provider accounts for a given user in PingOne.")
        .WithOpenApi(options => {
            options.Description = "Use this to unlink Microsoft Entra identity provider accounts for a given user in PingOne.";
            options.Summary = "Unlinks Microsoft Entra identity provider accounts for a given user in PingOne by deleting the linked account.";
            options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Unlink Microsoft Entra Identity Providers" } };

            return options;
        })
        .RequireAuthorization();

        app.MapPost(unlinkLdapGatewayIdentityEndpoint, UnlinkLdapGatewayIdentityProviderAccounts)
        .WithName("Unlink LDAP Gateway identity provider accounts for a given user in PingOne.")
        .WithOpenApi(options => {
            options.Description = "Use this to unlink LDAP Gateway identity provider accounts for a given user in PingOne.";
            options.Summary = "Unlinks LDAP Gateway identity provider accounts for a given user in PingOne by deleting the linked account.";
            options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Unlink LDAP Gateway Identity Providers" } };

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
            throw new ValidationException(ErrorNames.ValidationError, "samAccountName is required for this endpoint.");
        }

        var linkingObject = new PingFederateIdentityLinkingNotification
        {
            SamAccountName = identityLinkingApiRequest.SamAccountName,
            NotificationType = nameof(PingFederateIdentityLinkingNotification)
        };

        var result = await notifyIdentityLinkingColleague.Notify(linkingObject);
        
        return Results.Accepted(string.Empty, result);
    }

    /// <summary>
    /// Links the Microsoft Entra identity in PingOne.
    /// </summary>
    /// <param name="identityLinkingService"></param>
    /// <param name="contextAccessor"></param>
    /// <param name="samAccountName"></param>
    /// <returns></returns>
    public static async Task<IResult> LinkEntraIdentity(
        [FromServices] NotifyIdentityLinkingColleague notifyIdentityLinkingColleague, 
        [FromBody] IdentityLinkingApiRequest identityLinkingApiRequest)
    {
        ArgumentNullException.ThrowIfNull(notifyIdentityLinkingColleague);
        ArgumentNullException.ThrowIfNull(identityLinkingApiRequest);

        if (string.IsNullOrEmpty(identityLinkingApiRequest.SamAccountName))
        {
            throw new ValidationException(ErrorNames.ValidationError, "samAccountName is required for this endpoint.");
        }

        var linkingObject = new EntraIdentityLinkingNotification
        {
            SamAccountName = identityLinkingApiRequest.SamAccountName,
            NotificationType = nameof(EntraIdentityLinkingNotification)
        };

        var result = await notifyIdentityLinkingColleague.Notify(linkingObject);
        
        return Results.Accepted(string.Empty, result);
    }

    /// <summary>
    /// Links the LDAP gateway in PingOne.
    /// </summary>
    /// <param name="identityLinkingService"></param>
    /// <param name="contextAccessor"></param>
    /// <param name="samAccountName"></param>
    /// <returns></returns>
    public static async Task<IResult> LinkLdapGatewayIdentity(
        [FromServices] NotifyIdentityLinkingColleague notifyIdentityLinkingColleague, 
        [FromBody] IdentityLinkingApiRequest identityLinkingApiRequest)
    {
        ArgumentNullException.ThrowIfNull(notifyIdentityLinkingColleague);
        ArgumentNullException.ThrowIfNull(identityLinkingApiRequest);

        if (string.IsNullOrEmpty(identityLinkingApiRequest.SamAccountName))
        {
            throw new ValidationException(ErrorNames.ValidationError, "samAccountName is required for this endpoint.");
        }

        var linkingObject = new LdapGatewayIdentityLinkingNotification
        {
            SamAccountName = identityLinkingApiRequest.SamAccountName,
            NotificationType = nameof(LdapGatewayIdentityLinkingNotification)
        };

        var result = await notifyIdentityLinkingColleague.Notify(linkingObject);
        
        return Results.Accepted(string.Empty, result);
    }

    /// <summary>
    /// Links the LDAP gateway in PingOne.
    /// </summary>
    /// <param name="identityLinkingService"></param>
    /// <param name="contextAccessor"></param>
    /// <param name="samAccountName"></param>
    /// <returns></returns>
    public static async Task<IResult> LinkAllTargetsForIdentity(
        [FromServices] NotifyIdentityLinkingColleague notifyIdentityLinkingColleague, 
        [FromBody] IdentityLinkingApiRequest identityLinkingApiRequest)
    {
        ArgumentNullException.ThrowIfNull(notifyIdentityLinkingColleague);
        ArgumentNullException.ThrowIfNull(identityLinkingApiRequest);

        if (string.IsNullOrEmpty(identityLinkingApiRequest.SamAccountName))
        {
            throw new ValidationException(ErrorNames.ValidationError, "samAccountName is required for this endpoint.");
        }

        var linkingObject = new LinkAllTargetsForIdentityNotification
        {
            SamAccountName = identityLinkingApiRequest.SamAccountName,
            NotificationType = nameof(LinkAllTargetsForIdentityNotification)
        };

        var result = await notifyIdentityLinkingColleague.Notify(linkingObject);
        
        return Results.Accepted(string.Empty, result);
    }

    /// <summary>
    /// Starts the processing of the batch identity linking job.
    /// </summary>
    /// <param name="notifyIdentityLinkingColleague"></param>
    /// <returns></returns>
    public static async Task<IResult> StartProcessingLinkAllBatchIdentities(
        [FromServices] NotifyIdentityLinkingEngineJobColleague notifyIdentityLinkingEngineJobColleague)
    {
        ArgumentNullException.ThrowIfNull(notifyIdentityLinkingEngineJobColleague);

        var linkingObject = new StartIdentityLinkingBatchProcessingNotification
        {
            NotificationType = nameof(StartIdentityLinkingBatchProcessingNotification)
        };

        var result = await notifyIdentityLinkingEngineJobColleague.Notify(linkingObject);
        
        return Results.Accepted(string.Empty, result);
    }

    /// <summary>
    /// Stops the processing of the batch identity linking job.
    /// </summary>
    /// <param name="notifyIdentityLinkingColleague"></param>
    /// <returns></returns>
    public static async Task<IResult> StopProcessingLinkAllBatchIdentities(
        [FromServices] NotifyIdentityLinkingEngineJobColleague notifyIdentityLinkingEngineJobColleague)
    {
        ArgumentNullException.ThrowIfNull(notifyIdentityLinkingEngineJobColleague);

        var linkingObject = new StopIdentityLinkingBatchProcessingNotification
        {
            NotificationType = nameof(StopIdentityLinkingBatchProcessingNotification)
        };

        var result = await notifyIdentityLinkingEngineJobColleague.Notify(linkingObject);
        
        return Results.Accepted(string.Empty, result);
    }

    /// <summary>
    /// Unlinks all identity provider accounts for a given user in PingOne.
    /// </summary>
    /// <param name="notifyIdentityLinkingColleague"></param>
    /// <param name="identityLinkingApiRequest"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public static async Task<IResult> UnlinkAllIdentityProviderAccounts(
        [FromServices] NotifyIdentityLinkingColleague notifyIdentityLinkingColleague, 
        [FromBody] IdentityLinkingApiRequest identityLinkingApiRequest)
    {
        ArgumentNullException.ThrowIfNull(notifyIdentityLinkingColleague);
        ArgumentNullException.ThrowIfNull(identityLinkingApiRequest);

        if (string.IsNullOrEmpty(identityLinkingApiRequest.SamAccountName))
        {
            throw new ValidationException(ErrorNames.ValidationError, "samAccountName is required for this endpoint.");
        }

        var linkingObject = new UnlinkAccountIdentityLinkingNotification
        {
            SamAccountName = identityLinkingApiRequest.SamAccountName,
            NotificationType = nameof(UnlinkAccountIdentityLinkingNotification)
        };

        var result = await notifyIdentityLinkingColleague.Notify(linkingObject);
        
        return Results.Accepted(string.Empty, result);        
    }

    /// <summary>
    /// Unlinks PingFederate identity provider accounts for a given user in PingOne.
    /// </summary>
    /// <param name="notifyIdentityLinkingColleague"></param>
    /// <param name="identityLinkingApiRequest"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public static async Task<IResult> UnlinkPingFederateIdentityProviderAccounts(
        [FromServices] NotifyIdentityLinkingColleague notifyIdentityLinkingColleague, 
        [FromBody] IdentityLinkingApiRequest identityLinkingApiRequest)
    {
        ArgumentNullException.ThrowIfNull(notifyIdentityLinkingColleague);
        ArgumentNullException.ThrowIfNull(identityLinkingApiRequest);

        if (string.IsNullOrEmpty(identityLinkingApiRequest.SamAccountName))
        {
            throw new ValidationException(ErrorNames.ValidationError, "samAccountName is required for this endpoint.");
        }

        var linkingObject = new UnlinkPingFederateIdentityLinkingNotification
        {
            SamAccountName = identityLinkingApiRequest.SamAccountName,
            NotificationType = nameof(UnlinkPingFederateIdentityLinkingNotification)
        };

        var result = await notifyIdentityLinkingColleague.Notify(linkingObject);
        
        return Results.Accepted(string.Empty, result);    
    }

    /// <summary>
    /// Unlinks Microsoft Entra identity provider accounts for a given user in PingOne.
    /// </summary>
    /// <param name="notifyIdentityLinkingColleague"></param>
    /// <param name="identityLinkingApiRequest"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public static async Task<IResult> UnlinkMicrosoftEntraIdIdentityProviderAccounts(
        [FromServices] NotifyIdentityLinkingColleague notifyIdentityLinkingColleague, 
        [FromBody] IdentityLinkingApiRequest identityLinkingApiRequest)
    {
        ArgumentNullException.ThrowIfNull(notifyIdentityLinkingColleague);
        ArgumentNullException.ThrowIfNull(identityLinkingApiRequest);

        if (string.IsNullOrEmpty(identityLinkingApiRequest.SamAccountName))
        {
            throw new ValidationException(ErrorNames.ValidationError, "samAccountName is required for this endpoint.");
        }

        var linkingObject = new UnlinkEntraIdentityLinkingNotification
        {
            SamAccountName = identityLinkingApiRequest.SamAccountName,
            NotificationType = nameof(UnlinkEntraIdentityLinkingNotification)
        };

        var result = await notifyIdentityLinkingColleague.Notify(linkingObject);
        
        return Results.Accepted(string.Empty, result);    
    }

/// <summary>
    /// Unlinks LDAP Gateway identity provider accounts for a given user in PingOne.
    /// </summary>
    /// <param name="notifyIdentityLinkingColleague"></param>
    /// <param name="identityLinkingApiRequest"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public static async Task<IResult> UnlinkLdapGatewayIdentityProviderAccounts(
        [FromServices] NotifyIdentityLinkingColleague notifyIdentityLinkingColleague, 
        [FromBody] IdentityLinkingApiRequest identityLinkingApiRequest)
    {
        ArgumentNullException.ThrowIfNull(notifyIdentityLinkingColleague);
        ArgumentNullException.ThrowIfNull(identityLinkingApiRequest);

        if (string.IsNullOrEmpty(identityLinkingApiRequest.SamAccountName))
        {
            throw new ValidationException(ErrorNames.ValidationError, "samAccountName is required for this endpoint.");
        }

        var linkingObject = new UnlinkLdapGatewayIdentityLinkingNotification
        {
            SamAccountName = identityLinkingApiRequest.SamAccountName,
            NotificationType = nameof(UnlinkLdapGatewayIdentityLinkingNotification)
        };

        var result = await notifyIdentityLinkingColleague.Notify(linkingObject);
        
        return Results.Accepted(string.Empty, result);    
    }
}