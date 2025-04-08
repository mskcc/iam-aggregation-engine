using Microsoft.Extensions.DependencyInjection;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;

public class NotifyGetServiceNowUsersColleague
{
    private readonly IColleague _aggregateServiceNowUsersColleague;

    /// <summary>
    /// Creates an instance of <see cref="NotifyGetServiceNowUsersColleague"/>
    /// </summary>
    public NotifyGetServiceNowUsersColleague(
        [FromKeyedServices(ServiceKeys.GetServiceNowUsersColleague)] IColleague aggregateServiceNowUsersColleague)
    {
        ArgumentNullException.ThrowIfNull(aggregateServiceNowUsersColleague);
        _aggregateServiceNowUsersColleague = aggregateServiceNowUsersColleague;
    }

    /// <summary>
    /// Send service key
    /// </summary>
    public async Task<object> GetResponse(object? payloadData = null) => 
        await _aggregateServiceNowUsersColleague.Execute(ServiceKeys.GetServiceNowUsersColleague, payloadData);
}