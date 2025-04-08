using Microsoft.Extensions.DependencyInjection;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;

/// <summary>
/// Use case to notify colleagues to get Legacy connections
/// </summary>
public class NotifyGetLegacyConnectionsColleague
{
    private readonly IColleague _getLegacyConnectionsColleague;
    /// <summary>
    /// Creates an instance of <see cref="NotifyGetLegacyConnectionsColleague"/>
    /// </summary>
    public NotifyGetLegacyConnectionsColleague(
        [FromKeyedServices(ServiceKeys.GetLegacyConnectionsColleague)] IColleague getLegacyConnectionsColleague)
    {
        ArgumentNullException.ThrowIfNull(getLegacyConnectionsColleague);
        _getLegacyConnectionsColleague = getLegacyConnectionsColleague;
    }

    /// <summary>
    /// Send service key
    /// </summary>
    public async Task<object> GetResponse(object? payloadData = null) => 
        await _getLegacyConnectionsColleague.Execute(ServiceKeys.GetLegacyConnectionsColleague, payloadData);
}