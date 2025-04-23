using Microsoft.Extensions.DependencyInjection;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;

/// <summary>
/// Use case to notify colleagues to start identity linking engine jobs.
/// </summary> <summary>
public class NotifyIdentityLinkingEngineJobColleague
{
    private readonly IColleague _identityLinkingEngineJobColleague;

    /// <summary>
    /// Creates an instance of <see cref="NotifyIdentityLinkingEngineJobColleague"/>
    /// </summary>
    public NotifyIdentityLinkingEngineJobColleague(
        [FromKeyedServices(ServiceKeys.IdentityLinkingEngineJobColleague)] IColleague identityLinkingEngineJobColleague)
    {
        ArgumentNullException.ThrowIfNull(identityLinkingEngineJobColleague);
        _identityLinkingEngineJobColleague = identityLinkingEngineJobColleague;
    }

    /// <summary>
    /// Execute the use case
    /// </summary>
    public async Task<object> Notify(object? payloadData = null) => 
        await _identityLinkingEngineJobColleague.Execute(ServiceKeys.IdentityLinkingEngineJobColleague, payloadData);
}
