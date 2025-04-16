using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.IdentityLinkingService;

public interface IIdentityLinkingService
{
    /// <summary>
    /// Links the PingFederate identity in PingOne.
    /// </summary>
    /// <param name="samAccountName"></param>
    /// <returns>Task of <see cref="IdentityLinkingResponse"/></returns>
    Task<IdentityLinkingResponse> LinkIdentityFromPingFederate(string samAccountName);

    /// <summary>
    /// Links the Entra ID identity in PingOne.
    /// </summary>
    /// <param name="samAccountName"></param>
    /// <returns>Task of <see cref="IdentityLinkingResponse"/></returns>
    Task<IdentityLinkingResponse> LinkIdentityFromEntraId(string samAccountName);

    /// <summary>
    /// Links the LDAP identity in PingOne.
    /// </summary>
    /// <param name="samAccountName"></param>
    /// <returns>Task of <see cref="IdentityLinkingResponse"/></returns>
    Task<IdentityLinkingResponse> LinkIdentityFromLdapGateway(string samAccountName);

    /// <summary>
    /// Bulk processing for linking identities in pingone.
    /// </summary>
    /// <remarks>
    /// This method has dependencies for all GUIDs in the database.
    /// </remarks>
    /// <returns></returns>
    Task<IdentityLinkingResponse> ProcessInBulk();

    /// <summary>
    /// Bulk processing for linking identities in pingone.
    /// This method gets PingOne identites directly from PingOne user API for processing and bypasses the database queue.
    /// </summary>
    /// <remarks>
    /// This method has no dependency on SQL processing queue.
    /// </remarks>
    /// <returns></returns>
    Task<IdentityLinkingResponse> JustInTimeProcessInBulk();
}