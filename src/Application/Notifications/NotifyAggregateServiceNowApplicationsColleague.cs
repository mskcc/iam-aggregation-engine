using Microsoft.Extensions.DependencyInjection;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;

public class NotifyAggregateServiceNowApplicationsColleague
{
    private readonly IColleague _aggregateServiceNowApplicationsColleague;

    /// <summary>
    /// Creates an instance of <see cref="NotifyAggregateServiceNowApplicationsColleague"/>
    /// </summary>
    /// <param name="aggregateServiceNowApplicationsColleague"></param>
    public NotifyAggregateServiceNowApplicationsColleague(
        [FromKeyedServices(ServiceKeys.AggregateServiceNowApplicationsColleague)] IColleague aggregateServiceNowApplicationsColleague)
    {
        ArgumentNullException.ThrowIfNull(aggregateServiceNowApplicationsColleague);
        _aggregateServiceNowApplicationsColleague = aggregateServiceNowApplicationsColleague;
    }

    /// <summary>
    /// Send service key
    /// </summary>
    public async Task<object> Notify(object? payloadData = null) => 
        await _aggregateServiceNowApplicationsColleague.Execute(ServiceKeys.AggregateServiceNowApplicationsColleague, payloadData);
}