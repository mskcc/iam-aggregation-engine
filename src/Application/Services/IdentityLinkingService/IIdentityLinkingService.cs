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
}