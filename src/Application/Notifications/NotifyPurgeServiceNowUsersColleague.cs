using Microsoft.Extensions.DependencyInjection;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;

public class NotifyPurgeServiceNowUsersColleague
{
    private readonly IColleague _purgeServiceNowUsersColleague;

    /// <summary>
    /// Creates an instance of <see cref="NotifyPurgeServiceNowUsersColleague"/>
    /// </summary>
    /// <param name="purgeServiceNowUsersColleague"></param>
    public NotifyPurgeServiceNowUsersColleague(
        [FromKeyedServices(ServiceKeys.PurgeServiceNowUsersColleague)] IColleague purgeServiceNowUsersColleague
    )
    {
        ArgumentNullException.ThrowIfNull(purgeServiceNowUsersColleague);
        _purgeServiceNowUsersColleague = purgeServiceNowUsersColleague;
    }

    /// <summary>
    /// Execute the use case
    /// </summary>
    /// <returns></returns>
    public async Task<object> Notify(object? payloadData = null) => 
        await _purgeServiceNowUsersColleague.Execute(ServiceKeys.PurgeServiceNowUsersColleague, payloadData);
}