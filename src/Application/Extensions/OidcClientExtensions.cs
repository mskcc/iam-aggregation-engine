using Microsoft.EntityFrameworkCore;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Extensions;

/// <summary>
/// Extension methods for <see cref="OidcClient"/>
/// </summary>
public static class OidcClientExtensions
{
    /// <summary>
    /// Updates the <see cref="OidcClient"/> from the <see cref="OidcClientJson"/>.
    /// by copying the properties from the <see cref="OidcClientJson"/> to the <see cref="OidcClient"/>.
    /// </summary>
    /// <param name="oidcClient">The <see cref="OidcClient"/> to update.</param>
    public static OidcClient UpdateFrom(this OidcClient oidcClient, OidcClientJson oidcClientJson)
    {
        ArgumentNullException.ThrowIfNull(oidcClientJson);

        oidcClient.ClientId = oidcClientJson.ClientId;
        oidcClient.Enabled = oidcClientJson.Enabled;
        oidcClient.Name = oidcClientJson.Name;
        oidcClient.Description = oidcClientJson.Description;
        oidcClient.ModificationDate = oidcClientJson.ModificationDate;
        oidcClient.CreationDate = oidcClientJson.CreationDate;
        oidcClient.RefreshRolling = oidcClientJson.RefreshRolling;
        oidcClient.RefreshTokenRollingIntervalType = oidcClientJson.RefreshTokenRollingIntervalType;
        oidcClient.PersistentGrantExpirationType = oidcClientJson.PersistentGrantExpirationType;
        oidcClient.PersistentGrantExpirationTime = oidcClientJson.PersistentGrantExpirationTime;
        oidcClient.PersistentGrantExpirationTimeUnit = oidcClientJson.PersistentGrantExpirationTimeUnit;
        oidcClient.PersistentGrantIdleTimeoutType = oidcClientJson.PersistentGrantIdleTimeoutType;
        oidcClient.PersistentGrantIdleTimeout = oidcClientJson.PersistentGrantIdleTimeout;
        oidcClient.PersistentGrantIdleTimeoutTimeUnit = oidcClientJson.PersistentGrantIdleTimeoutTimeUnit;
        oidcClient.PersistentGrantReuseType = oidcClientJson.PersistentGrantReuseType;
        oidcClient.AllowAuthenticationApiInit = oidcClientJson.AllowAuthenticationApiInit;
        oidcClient.BypassApprovalPage = oidcClientJson.BypassApprovalPage;
        oidcClient.RestrictScopes = oidcClientJson.RestrictScopes;
        oidcClient.RequirePushedAuthorizationRequests = oidcClientJson.RequirePushedAuthorizationRequests;
        oidcClient.RequireJwtSecuredAuthorizationResponseMode = oidcClientJson.RequireJwtSecuredAuthorizationResponseMode;
        oidcClient.RestrictToDefaultAccessTokenManager = oidcClientJson.RestrictToDefaultAccessTokenManager;
        oidcClient.ValidateUsingAllEligibleAtms = oidcClientJson.ValidateUsingAllEligibleAtms;
        oidcClient.DeviceFlowSettingType = oidcClientJson.DeviceFlowSettingType;
        oidcClient.RequireProofKeyForCodeExchange = oidcClientJson.RequireProofKeyForCodeExchange;
        oidcClient.RefreshTokenRollingGracePeriodType = oidcClientJson.RefreshTokenRollingGracePeriodType;
        oidcClient.ClientSecretRetentionPeriodType = oidcClientJson.ClientSecretRetentionPeriodType;
        oidcClient.ClientSecretChangedTime = oidcClientJson.ClientSecretChangedTime;
        oidcClient.RequireDpop = oidcClientJson.RequireDpop;
        oidcClient.RequireSignedRequests = oidcClientJson.RequireSignedRequests;
        oidcClient.RedirectUris = oidcClientJson.RedirectUris;
        oidcClient.GrantTypes = oidcClientJson.GrantTypes;
        oidcClient.RestrictedScopes = oidcClientJson.RestrictedScopes;
        oidcClient.ExclusiveScopes = oidcClientJson.ExclusiveScopes;
        oidcClient.RestrictedResponseTypes = oidcClientJson.RestrictedResponseTypes;
        oidcClient.AuthorizationDetailTypes = oidcClientJson.AuthorizationDetailTypes;
        oidcClient.OidcPolicyId = oidcClientJson.OidcPolicy?.OidcPolicyGroup?.Id;

        return oidcClient;
    }

    /// <summary>
    /// Updates the <see cref="OidcClient"/> <see cref="AttributeContractFulfillment"/>.
    /// Then updates the <see cref="OidcClient"/> <see cref="OidcIssuanceCriteria"/>.
    /// </summary>
    /// <param name="oidcClient">The <see cref="OidcClient"/> to update.</param>
    /// <param name="oidcAttributeContractFulfillmentJson">The <see cref="OidcAttributeContractFulfillmentItemsJson"/> to update.</param>
    public static OidcClient UpdateFromPolicies(this OidcClient oidcClient, 
        OidcAttributeContractFulfillmentItemsJson oidcAttributeContractFulfillmentJson, DbSet<AttributeContractFulfillment> dbSet) => 
            oidcClient
                .UpdateAttributeContractFulfillments(oidcAttributeContractFulfillmentJson, dbSet)
                .UpdateIssuanceCriteria(oidcAttributeContractFulfillmentJson);

    /// <summary>
    /// Syncs up the database with which connections were removed from the configured Ping Federate instance
    /// </summary>
    /// <param name="oidcClientSetList"></param>
    /// <param name="oidcClientJsons"></param>
    /// <returns></returns>
    public static List<OidcClient>? SyncRemovedConnections(this List<OidcClient>? oidcClientSetList, List<OidcClientJson>? oidcClientJsons)
    {
        ArgumentNullException.ThrowIfNull(oidcClientSetList);
        ArgumentNullException.ThrowIfNull(oidcClientJsons);

        oidcClientSetList.RemoveAll(connectionSet => 
            !oidcClientJsons.Any(c => c.ClientId == connectionSet.ClientId));

        return oidcClientSetList;
    }

    private static OidcClient UpdateAttributeContractFulfillments(this OidcClient oidcClient, 
        OidcAttributeContractFulfillmentItemsJson oidcAttributeContractFulfillmentJson,
        DbSet<AttributeContractFulfillment> dbSet)
    {
        ArgumentNullException.ThrowIfNull(oidcAttributeContractFulfillmentJson);

        var oidcAttributeContractFulfillmentItems = oidcAttributeContractFulfillmentJson.Items?.ToList();
        ArgumentNullException.ThrowIfNull(oidcAttributeContractFulfillmentItems);

        var existingItems = dbSet
            .AsNoTracking()
            .AsSplitQuery()
            .Where(i => i.ConnectionId == oidcClient.OidcPolicyId).ToList();
        if (existingItems.Count > 0)
        {
            dbSet.RemoveRange(existingItems);
        }

        var oidcAtrributeContractFulfillments = new List<AttributeContractFulfillment>();
        foreach (var item in oidcAttributeContractFulfillmentItems)
        {
            if (item.Id != oidcClient.OidcPolicyId)
            {
                continue;
            }

            var keys = item.AttributeContract?.ExtendedAttributes?.Select(ac => ac.Name).ToList();
            if (keys is null)
            {
                continue;
            }
            keys.Add("sub");

            var values = item.AttributeMapping?.AttributeContractFulfillment?.ToDictionary<string, OidcAttributeContractFulfillment>();
            if (values is null)
            {
                continue;
            }

            foreach (var key in keys)
            {
                if (string.IsNullOrEmpty(key))
                {
                    continue;
                }

                var attributeContractFulfillment = new AttributeContractFulfillment
                {
                    ConnectionId = item.Id,
                    ClaimName = key,
                    ClaimValue = values[key].Value,
                    ClaimType = values[key].Source?.Type,
                    ConnectionType = "OIDC",
                };

                oidcAtrributeContractFulfillments.Add(attributeContractFulfillment);
            }
        }

        oidcClient.AttributeContractFulfillment = oidcAtrributeContractFulfillments;
        return oidcClient;        
    }

    private static OidcClient UpdateIssuanceCriteria(this OidcClient oidcClient, 
        OidcAttributeContractFulfillmentItemsJson oidcAttributeContractFulfillmentJson)
    {
        ArgumentNullException.ThrowIfNull(oidcAttributeContractFulfillmentJson);

        var oidcAttributeContractFulfillmentItems = oidcAttributeContractFulfillmentJson.Items?.ToList();

        if (oidcAttributeContractFulfillmentItems is null)
        {
            return oidcClient;
        }

        foreach(var item in oidcAttributeContractFulfillmentItems)
        {
            if (item.Id != oidcClient.OidcPolicyId)
            {
                continue;
            }

            var conditionalIssuanceCriteria = item.AttributeMapping?.IssuanceCriteria?.ConditionalCriteria?.SingleOrDefault()?.Expression;
            var expressionIssuanceCriteria = item.AttributeMapping?.IssuanceCriteria?.ExpressionCriteria?.SingleOrDefault()?.Expression;

            if (conditionalIssuanceCriteria is not null)
            {
                oidcClient.ConditionalIssuanceCriteria = conditionalIssuanceCriteria ?? string.Empty;
            }

            if (expressionIssuanceCriteria is not null)
            {
                oidcClient.ExpressionIssuanceCriteria = expressionIssuanceCriteria ?? string.Empty;
            }
        }

        return oidcClient;
    }
}