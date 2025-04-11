using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PingOne;

public interface IPingOneService
{
    /// <summary>
    /// Creates a linked account for PingFederate in PingOne.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<PingOneResponse> CreateLinkedAccountForPingFederate(string userId);

    /// <summary>
    /// Creates a linked account for Microsoft Entra ID in PingOne.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<PingOneResponse> CreateLinkedAccountForMicrosoftEntra(string userId);

    /// <summary>
    /// Links a specified PingOne identity to the configured LDAP Gateway.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<PingOneResponse> CreateLinkedAccountForLdapGateway(string userId);
}