using Microsoft.Extensions.DependencyInjection;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;

public class NotifyGetOidcConnectionsColleague
{
    private readonly IColleague _getOidcConnectionsColleague;
    /// <summary>
    /// Creates an instance of <see cref="NotifyGetOidcConnectionsColleague"/>
    /// </summary>
    public NotifyGetOidcConnectionsColleague(
        [FromKeyedServices(ServiceKeys.GetOidcConnectionsColleague)] IColleague getOidcConnectionsColleague)
    {
        ArgumentNullException.ThrowIfNull(getOidcConnectionsColleague);
        _getOidcConnectionsColleague = getOidcConnectionsColleague;
    }

    /// <summary>
    /// Send service key
    /// </summary>
    public async Task<object> GetResponse(object? payloadData = null) => 
        await _getOidcConnectionsColleague.Execute(ServiceKeys.GetOidcConnectionsColleague, payloadData);
}
