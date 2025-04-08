using Microsoft.Extensions.DependencyInjection;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;

/// <summary>
/// Use case to notify colleagues to purge Legacy connections
/// </summary> <summary>
public class NotifyPurgeLegacyColleague
{
    private readonly IColleague _purgeLegacyConnectionColleague;

    /// <summary>
    /// Creates an instance of <see cref="NotifyPurgeLegacyColleague"/>
    /// </summary>
    public NotifyPurgeLegacyColleague(
        [FromKeyedServices(ServiceKeys.PurgeOidcConnectionColleague)] IColleague purgeLegacyConnectionColleague)
    {
        ArgumentNullException.ThrowIfNull(purgeLegacyConnectionColleague);
        _purgeLegacyConnectionColleague = purgeLegacyConnectionColleague;
    }

    /// <summary>
    /// Execute the use case
    /// </summary>
    public async Task<object> Notify(object? payloadData = null) => 
        await _purgeLegacyConnectionColleague.Execute(ServiceKeys.PurgeLegacyConnectionsColleague, payloadData);
}