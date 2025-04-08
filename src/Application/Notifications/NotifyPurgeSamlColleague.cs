using Microsoft.Extensions.DependencyInjection;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;

/// <summary>
/// Use case to notify colleagues to purge SAML connections
/// </summary> <summary>
public class NotifyPurgeSamlColleague
{
    private readonly IColleague _purgeSamlConnectionColleague;

    /// <summary>
    /// Creates an instance of <see cref="NotifyPurgeSamlColleague"/>
    /// </summary>
    public NotifyPurgeSamlColleague(
        [FromKeyedServices(ServiceKeys.PurgeSamlConnectionsColleague)] IColleague purgeSamlConnectionColleague)
    {
        ArgumentNullException.ThrowIfNull(purgeSamlConnectionColleague);
        _purgeSamlConnectionColleague = purgeSamlConnectionColleague;
    }

    /// <summary>
    /// Execute the use case
    /// </summary>
    public async Task<object> Notify(object? payloadData = null) => 
        await _purgeSamlConnectionColleague.Execute(ServiceKeys.PurgeSamlConnectionsColleague, payloadData);
}