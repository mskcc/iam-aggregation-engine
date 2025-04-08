using Microsoft.Extensions.DependencyInjection;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;

/// <summary>
/// Use case to notify colleagues to purge OIDC connections
/// </summary> <summary>
public class NotifyPurgeOidcColleague
{
    private readonly IColleague _purgeOidcConnectionColleague;

    /// <summary>
    /// Creates an instance of <see cref="NotifyPurgeOidcColleague"/>
    /// </summary>
    public NotifyPurgeOidcColleague(
        [FromKeyedServices(ServiceKeys.PurgeOidcConnectionColleague)] IColleague purgeOidcConnectionColleague)
    {
        ArgumentNullException.ThrowIfNull(purgeOidcConnectionColleague);
        _purgeOidcConnectionColleague = purgeOidcConnectionColleague;
    }

    /// <summary>
    /// Execute the use case
    /// </summary>
    public async Task<object> Notify(object? payloadData = null) => 
        await _purgeOidcConnectionColleague.Execute(ServiceKeys.PurgeOidcConnectionColleague, payloadData);
}
