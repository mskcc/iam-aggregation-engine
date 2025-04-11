using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.IdentityLinkingService;

public interface IIdentityLinkingService
{
    /// <summary>
    /// Links the PingFederate identity in PingOne.
    /// </summary>
    /// <returns>Task of <see cref="IdentityLinkingResponse"/></returns>
    Task<IdentityLinkingResponse> LinkIdentityFromPingFederate();

    /// <summary>
    /// Links the Entra ID identity in PingOne.
    /// </summary>
    /// <returns>Task of <see cref="IdentityLinkingResponse"/></returns>
    Task<IdentityLinkingResponse> LinkIdentityFromEntraId();

    /// <summary>
    /// Links the LDAP identity in PingOne.
    /// </summary>
    /// <returns>Task of <see cref="IdentityLinkingResponse"/></returns>
    Task<IdentityLinkingResponse> LinkIdentityFromLdapGateway();
}