namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

public class IdentityLinkingNotification
{
    /// <summary>
    /// The PingFederate user ID associated with the SAM account name.
    /// </summary>
    public string? SamAccountName { get; set; }

    /// <summary>
    /// Name of the notification i.e "PingFederateLinkingNotification".
    /// </summary>
    public string? NotificationType { get; set; }
}