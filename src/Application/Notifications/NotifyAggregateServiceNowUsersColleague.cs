using Microsoft.Extensions.DependencyInjection;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;

public class NotifyAggregateServiceNowUsersColleague
{
    private readonly IColleague _aggregateServiceNowUsersColleague;

    /// <summary>
    /// Creates an instance of <see cref="NotifyAggregateServiceNowUsersColleague"/>
    /// </summary>
    /// <param name="aggregateServiceNowUsersColleague"></param>
    public NotifyAggregateServiceNowUsersColleague(
        [FromKeyedServices(ServiceKeys.AggregateServiceNowUsersColleague)] IColleague aggregateServiceNowUsersColleague)
    {
        ArgumentNullException.ThrowIfNull(aggregateServiceNowUsersColleague);
        _aggregateServiceNowUsersColleague = aggregateServiceNowUsersColleague;
    }

    /// <summary>
    /// Send service key
    /// </summary>
    public async Task<object> Notify(object? payloadData = null) => 
        await _aggregateServiceNowUsersColleague.Execute(ServiceKeys.AggregateServiceNowUsersColleague, payloadData);
}