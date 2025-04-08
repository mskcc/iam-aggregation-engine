using Microsoft.Extensions.DependencyInjection;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;

/// <summary>
/// Use case to notify colleagues to aggregate Legacy connections
/// </summary>
public class NotifyAggregateLegacyConnectionsColleague
{
    private readonly IColleague _aggregateLegacyConnectionsColleague;

    /// <summary>
    /// Creates an instance of <see cref="NotifyAggregateLegacyConnectionsColleague"/>
    /// </summary>
    public NotifyAggregateLegacyConnectionsColleague(
        [FromKeyedServices(ServiceKeys.AggregateLegacyConnectionsColleague)] IColleague aggregateLegacyConnectionsColleague)
    {
        ArgumentNullException.ThrowIfNull(aggregateLegacyConnectionsColleague);
        _aggregateLegacyConnectionsColleague = aggregateLegacyConnectionsColleague;
    }

    /// <summary>
    /// Send service key
    /// </summary>
    public async Task<object> Notify(object? payloadData = null) => 
        await _aggregateLegacyConnectionsColleague.Execute(ServiceKeys.AggregateLegacyConnectionsColleague, payloadData);
}