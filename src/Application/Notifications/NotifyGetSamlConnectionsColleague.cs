using Microsoft.Extensions.DependencyInjection;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;

public class NotifyGetSamlConnectionsColleague
{
    private readonly IColleague _getSamlConnectionsColleague;
    /// <summary>
    /// Creates an instance of <see cref="NotifyGetSamlConnectionsColleague"/>
    /// </summary>
    public NotifyGetSamlConnectionsColleague(
        [FromKeyedServices(ServiceKeys.GetSamlConnectionsColleague)] IColleague getSamlConnectionsColleague)
    {
        ArgumentNullException.ThrowIfNull(getSamlConnectionsColleague);
        _getSamlConnectionsColleague = getSamlConnectionsColleague;
    }

    /// <summary>
    /// Send service key
    /// </summary>
    public async Task<object> GetResponse(object? payloadData = null) => 
        await _getSamlConnectionsColleague.Execute(ServiceKeys.GetSamlConnectionsColleague, payloadData);
}