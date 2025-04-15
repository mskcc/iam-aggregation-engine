using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PingOne;

public interface IPingOneService
{
    /// <summary>
    /// Creates a linked account for PingFederate in PingOne.
    /// </summary>
    /// <param name="pingOneUserId"></param>
    /// <param name="samAccountName"></param>
    /// <returns></returns>
    Task<PingOneResponse> CreateLinkedAccountForPingFederate(string pingOneUserId, string samAccountName);

    /// <summary>
    /// Creates a linked account for Microsoft Entra ID in PingOne.
    /// </summary>
    /// <param name="pingOneUserId"></param>
    /// <param name="microsoftObjectId"></param>
    /// <returns></returns>
    Task<PingOneResponse> CreateLinkedAccountForMicrosoftEntra(string pingOneUserId, string microsoftObjectId);

    /// <summary>
    /// Links a specified PingOne identity to the configured LDAP Gateway.
    /// </summary>
    /// <param name="pingOneUserId"></param>
    /// <returns></returns>
    Task<PingOneResponse> CreateLinkedAccountForLdapGateway(string pingOneUserId, string samAccountName);

    /// <summary>
    /// Fetches the PingOne user ID associated with a given SAM account name.
    /// </summary>
    /// <param name="samAccountName"></param>
    /// <returns></returns>
    Task<string> GetPingOneUserIdFromSamAccountName(string samAccountName);

    /// <summary>
    /// Fetches the external identity providers linked to a specified user in PingOne.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<PingOneResponse> GetExternalIdps();
}