using Microsoft.Extensions.DependencyInjection;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;

/// <summary>
/// Use case to notify colleagues to aggregate SAML connections
/// </summary>
public class NotifyAggregateSamlConnectionsColleague
{
    private readonly IColleague _aggregateSamlConnectionsColleague;

    /// <summary>
    /// Creates an instance of <see cref="NotifyAggregateSamlConnectionsColleague"/>
    /// </summary>
    public NotifyAggregateSamlConnectionsColleague(
        [FromKeyedServices(ServiceKeys.AggregateSamlConnectionsColleague)] IColleague aggregateSamlConnectionsColleague)
    {
        ArgumentNullException.ThrowIfNull(aggregateSamlConnectionsColleague);
        _aggregateSamlConnectionsColleague = aggregateSamlConnectionsColleague;
    }

    /// <summary>
    /// Send service key
    /// </summary>
    public async Task<object> Notify(object? payloadData = null) => 
        await _aggregateSamlConnectionsColleague.Execute(ServiceKeys.AggregateSamlConnectionsColleague, payloadData);
}