using Microsoft.Extensions.DependencyInjection;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;

/// <summary>
/// Use case to notify colleagues to purge OIDC connections
/// </summary> <summary>
public class NotifyIdentityLinkingColleague
{
    private readonly IColleague _identityLinkingColleague;

    /// <summary>
    /// Creates an instance of <see cref="NotifyIdentityLinkingColleague"/>
    /// </summary>
    public NotifyIdentityLinkingColleague(
        [FromKeyedServices(ServiceKeys.IdentityLinkingColleague)] IColleague identityLinkingColleague)
    {
        ArgumentNullException.ThrowIfNull(identityLinkingColleague);
        _identityLinkingColleague = identityLinkingColleague;
    }

    /// <summary>
    /// Execute the use case
    /// </summary>
    public async Task<object> Notify(object? payloadData = null) => 
        await _identityLinkingColleague.Execute(ServiceKeys.IdentityLinkingColleague, payloadData);
}
