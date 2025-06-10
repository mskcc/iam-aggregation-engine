using Microsoft.Extensions.DependencyInjection;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;

/// <summary>
/// Use case to notify colleagues to start identity linking engine jobs.
/// </summary> <summary>
public class NotifyPingFederateInsightsColleague
{
    private readonly IColleague _pingFederateInsightsColleague;

    /// <summary>
    /// Creates an instance of <see cref="NotifyPingFederateInsightsColleague"/>
    /// </summary>
    public NotifyPingFederateInsightsColleague(
        [FromKeyedServices(ServiceKeys.PingFederateInsightsColleague)] IColleague pingFederateInsightsColleague)
    {
        ArgumentNullException.ThrowIfNull(pingFederateInsightsColleague);
        _pingFederateInsightsColleague = pingFederateInsightsColleague;
    }

    /// <summary>
    /// Execute the use case
    /// </summary>
    public async Task<object> Notify(object? payloadData = null) => 
        await _pingFederateInsightsColleague.Execute(ServiceKeys.PingFederateInsightsColleague, payloadData);
}
