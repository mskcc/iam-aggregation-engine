using Microsoft.Extensions.DependencyInjection;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;

public class NotifyPurgeServiceNowApplicationsColleague
{
    private readonly IColleague _purgeServiceNowApplicationsColleague;

    /// <summary>
    /// Creates an instance of <see cref="NotifyPurgeServiceNowApplicationsColleague"/>
    /// </summary>
    /// <param name="purgeServiceNowApplicationsColleague"></param>
    public NotifyPurgeServiceNowApplicationsColleague(
        [FromKeyedServices(ServiceKeys.PurgeServiceNowApplicationsColleague)] IColleague purgeServiceNowApplicationsColleague
    )
    {
        ArgumentNullException.ThrowIfNull(purgeServiceNowApplicationsColleague);
        _purgeServiceNowApplicationsColleague = purgeServiceNowApplicationsColleague;
    }

    /// <summary>
    /// Execute the use case
    /// </summary>
    /// <returns></returns>
    public async Task<object> Notify(object? payloadData = null) => 
        await _purgeServiceNowApplicationsColleague.Execute(ServiceKeys.PurgeServiceNowApplicationsColleague, payloadData);
}