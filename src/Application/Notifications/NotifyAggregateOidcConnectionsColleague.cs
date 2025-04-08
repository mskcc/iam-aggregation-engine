using Microsoft.Extensions.DependencyInjection;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;

/// <summary>
/// Use case to notify colleagues to aggregate OIDC connections
/// </summary>
public class NotifyAggregateOidcConnectionsColleague
{
    private readonly IColleague _aggregateOidcConnectionsColleague;

    /// <summary>
    /// Creates an instance of <see cref="NotifyAggregateOidcConnectionsColleague"/>
    /// </summary>
    public NotifyAggregateOidcConnectionsColleague(
        [FromKeyedServices(ServiceKeys.AggregateOidcConnectionsColleague)] IColleague aggregateOidcConnectionsColleague)
    {
        ArgumentNullException.ThrowIfNull(aggregateOidcConnectionsColleague);
        _aggregateOidcConnectionsColleague = aggregateOidcConnectionsColleague;
    }

    /// <summary>
    /// Send service key
    /// </summary>
    public async Task<object> Notify(object? payloadData = null) => 
        await _aggregateOidcConnectionsColleague.Execute(ServiceKeys.AggregateOidcConnectionsColleague, payloadData);
}