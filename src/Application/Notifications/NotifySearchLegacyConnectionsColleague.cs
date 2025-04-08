using Microsoft.Extensions.DependencyInjection;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;

/// <summary>
/// Use case to notify colleagues to search Legacy connections
/// </summary>
public class NotifySearchLegacyConnectionsColleague
{
    private readonly IColleague _searchLegacyConnectionsColleague;
    /// <summary>
    /// Creates an instance of <see cref="NotifySearchGetLegacyConnectionsColleague"/>
    /// </summary>
    public NotifySearchLegacyConnectionsColleague(
        [FromKeyedServices(ServiceKeys.SearchGetLegacyConnectionsColleague)] IColleague searchLegacyConnectionsColleague)
    {
        ArgumentNullException.ThrowIfNull(searchLegacyConnectionsColleague);
        _searchLegacyConnectionsColleague = searchLegacyConnectionsColleague;
    }

    /// <summary>
    /// Send service key
    /// </summary>
    /// <remarks>
    /// Payload data is intended to be of type <see cref="SearchCriteria"/> for this method.
    /// </remarks>
    public async Task<object> GetResponse(object? payloadData = null) => 
        await _searchLegacyConnectionsColleague.Execute(ServiceKeys.SearchGetLegacyConnectionsColleague, payloadData);
}