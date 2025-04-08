using Microsoft.Extensions.DependencyInjection;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;

public class NotifyGetServiceNowApplicationsColleague
{
    private readonly IColleague _aggregateServiceNowApplicationsColleague;

    /// <summary>
    /// Creates an instance of <see cref="NotifyGetServiceNowApplicationsColleague"/>
    /// </summary>
    public NotifyGetServiceNowApplicationsColleague(
        [FromKeyedServices(ServiceKeys.GetServiceNowApplicationsColleague)] IColleague aggregateServiceNowApplicationsColleague)
    {
        ArgumentNullException.ThrowIfNull(aggregateServiceNowApplicationsColleague);
        _aggregateServiceNowApplicationsColleague = aggregateServiceNowApplicationsColleague;
    }

    /// <summary>
    /// Send service key
    /// </summary>
    public async Task<object> GetResponse(object? payloadData = null) => 
        await _aggregateServiceNowApplicationsColleague.Execute(ServiceKeys.GetServiceNowApplicationsColleague, payloadData);
}