using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PingFederate;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Extensions;

/// <summary>
/// Extension methods for <see cref="SpConnection"/>
/// </summary>
public static class SpConnectionExtensions
{
    /// <summary>
    /// Updates the <see cref="SpConnection"/> from the <see cref="SpConnectionJson"/>.
    /// by copying the properties from the <see cref="SpConnectionJson"/> to the <see cref="SpConnection"/>.
    /// </summary>
    /// <param name="spConnection">The <see cref="SpConnection"/> to update.</param>
    public static SpConnection UpdateFrom(this SpConnection spConnection, SpConnectionJson json, DbSet<AttributeContractFulfillment> dbSet)
    {
        ArgumentNullException.ThrowIfNull(spConnection);
        ArgumentNullException.ThrowIfNull(json);

        spConnection.Id = json.Id;
        spConnection.Type = json.Type;
        spConnection.Name = json.Name;
        spConnection.ContactInfoCompany = json.ContactInfo?.Company;
        spConnection.ContactInfoFirstName = json.ContactInfo?.FirstName;
        spConnection.ContactInfoLastName = json.ContactInfo?.LastName;
        spConnection.ContactInfoEmail = json.ContactInfo?.Email;
        spConnection.EntityId = json.EntityId;
        spConnection.Active = json.Active ?? false;
        spConnection.BaseUrl = json.BaseUrl;
        spConnection.ModificationDate = json.ModificationDate;
        spConnection.CreationDate = json.CreationDate;
        spConnection.Protocol = json.SpBrowserSso?.Protocol;
        spConnection.EnabledProfiles = json.SpBrowserSso?.EnabledProfiles;
        spConnection.IdpInitiatedLink = json.SpBrowserSso?.SsoApplicationEndpoint;
        spConnection.SignAssertions = json.SpBrowserSso?.SignAssertions ?? false;
        spConnection.SignResponseAsRequired = json.SpBrowserSso?.SignResponseAsRequired ?? false;
        spConnection.RequireSignedAuthnRequests = json.SpBrowserSso?.RequireSignedAuthnRequests ?? false;
        spConnection.NameIdPolicy = json.SpBrowserSso?.AttributeContract?.CoreAttributes?.SingleOrDefault()?.NameFormat;
        
        var conditionalCriteriaList = json.SpBrowserSso?
                                            .AuthenticationPolicyContractAssertionMappings?.SingleOrDefault()?
                                            .IssuanceCriteria?.ConditionalCriterias?.ToList();
        if (conditionalCriteriaList is null)
        {
            spConnection.ConditionalIssuanceCriteria = null!;
        }
        else if (conditionalCriteriaList.Count > 1)
        {
            spConnection.ConditionalIssuanceCriteria = json.SpBrowserSso?
                                            .AuthenticationPolicyContractAssertionMappings?.SingleOrDefault()?
                                            .IssuanceCriteria?.ConditionalCriterias?.FirstOrDefault()?
                                            .Value;
        }
        else
        {
            spConnection.ConditionalIssuanceCriteria = json.SpBrowserSso?
                                            .AuthenticationPolicyContractAssertionMappings?.SingleOrDefault()?
                                            .IssuanceCriteria?.ConditionalCriterias?.SingleOrDefault()?
                                            .Value;
        }

        var expressionCriteriaList = json.SpBrowserSso?
                                            .AuthenticationPolicyContractAssertionMappings?.SingleOrDefault()?
                                            .IssuanceCriteria?.ExpressionCriteria?.ToList();
        if (expressionCriteriaList is null)
        {
            spConnection.ExpressionIssuanceCriteria = null!;
        }        
        else if (expressionCriteriaList.Count > 1)
        {
            spConnection.ExpressionIssuanceCriteria = json.SpBrowserSso?
                                                .AuthenticationPolicyContractAssertionMappings?.SingleOrDefault()?
                                                .IssuanceCriteria?.ExpressionCriteria?.FirstOrDefault()?
                                                .Expression;
        }
        else
        {
            spConnection.ExpressionIssuanceCriteria = json.SpBrowserSso?
                                                .AuthenticationPolicyContractAssertionMappings?.SingleOrDefault()?
                                                .IssuanceCriteria?.ExpressionCriteria?.SingleOrDefault()?
                                                .Expression;
        }

        spConnection.ApplicationName = json.ApplicationName;
        spConnection.ContactInfoNumber = json.ContactInfo?.Phone;
        if (json.SpBrowserSso?.SsoServiceEndpoints is not null)
        {
            var acsUrlsList = new List<string>();
            foreach(var serviceEndpoint in json.SpBrowserSso?.SsoServiceEndpoints!)
            {
                var acsUrl = serviceEndpoint.Url;
                if (acsUrl is null)
                {
                    continue;
                }
                acsUrlsList.Add(acsUrl);
            };
            spConnection.AcsUrl = string.Join(",", acsUrlsList);
        }

        var existingItems = dbSet
            .AsNoTracking()
            .AsSplitQuery()
            .Where(i => i.ConnectionId == spConnection.Id).ToList();
        if (existingItems.Count > 0)
        {
            dbSet.RemoveRange(existingItems);
        }

        var attributeContractList = new List<AttributeContractFulfillment>();
        foreach(var attribute in json.SpBrowserSso?.AuthenticationPolicyContractAssertionMappings!)
        {
            var subject = attribute.AttributeContractFulfillment?.Subject;
            if (subject is null)
            {
                continue;
            }

            var subjectValue = subject?.Value;
            var subjectType = subject?.Source?.Type;
            var newAttributeContractFulfillment = new AttributeContractFulfillment
            {
                ConnectionId = json.Id,
                ClaimName = "SAML_SUBJECT",
                ConnectionType = "SAML",
                ClaimType = subjectType,
                ClaimValue = subjectValue
            };
            attributeContractList.Add(newAttributeContractFulfillment);
        }

        foreach (var attributeContract in json.SpBrowserSso?.AttributeContract?.ExtendedAttributes!)
        {
            if (attributeContract is null)
            {
                continue;
            }

            if (attributeContract.Name == "SAML_SUBJECT")
            {
                continue;
            }


            var newAttributeContractFulfillment = new AttributeContractFulfillment
            {
                ConnectionId = json.Id,
                ClaimName = attributeContract?.Name,
                ConnectionType = "SAML"
            };
            
            foreach(var mapping in json.SpBrowserSso?.AuthenticationPolicyContractAssertionMappings!)
            {
                if (mapping is null)
                {
                    continue;
                }

                var jsonAttribute = mapping.AttributeContractFulfillment?.AdditionalProperties[attributeContract?.Name!].ToString();
                var attribute = JsonSerializer.Deserialize<AttributeContractFulfillmentMapping>(jsonAttribute!);
                newAttributeContractFulfillment.ClaimValue = attribute?.Value;
                newAttributeContractFulfillment.ClaimType = attribute?.Source?.Type;
            }

            attributeContractList.Add(newAttributeContractFulfillment);
        };
        spConnection.AttributeContractFulfillment = attributeContractList;

        return spConnection;
    }

    /// <summary>
    /// Syncs up the database with which connections were removed from the configured Ping Federate instance
    /// </summary>
    /// <param name="spConnectionSetList"></param>
    /// <param name="spConnectionJsons"></param>
    /// <returns></returns>
    public static List<SpConnection>? SyncRemovedConnections(this List<SpConnection>? spConnectionSetList, List<SpConnectionJson>? spConnectionJsons)
    {
        ArgumentNullException.ThrowIfNull(spConnectionSetList);
        ArgumentNullException.ThrowIfNull(spConnectionJsons);

        spConnectionSetList.RemoveAll(connectionSet => 
            !spConnectionJsons.Any(c => c.Id == connectionSet.Id));

        return spConnectionSetList;
    }
}