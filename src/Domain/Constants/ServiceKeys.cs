namespace Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;

/// <summary>
/// Service keys for use with Mediator.
/// </summary>
/// <remarks>
/// This class is used to define keys for services that will be used by Mediator.
/// see: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-8.0#keyed-services
/// </remarks>
public static class ServiceKeys
{
    /// <summary>
    /// Get all keys to be used by Mediator
    /// </summary>
    /// <returns><see cref="IEnumerable"/> of type <see cref="string"/></returns>
    public static IEnumerable<string> GetKeys()
    {
        yield return PurgeOidcConnectionColleague;
        yield return AggregateOidcConnectionsColleague;
        yield return GetOidcConnectionsColleague;
        yield return PurgeSamlConnectionsColleague;
        yield return GetSamlConnectionsColleague;
        yield return AggregateSamlConnectionsColleague;
        yield return PurgeLegacyConnectionsColleague;
        yield return GetLegacyConnectionsColleague;
        yield return SearchGetLegacyConnectionsColleague;
        yield return AggregateLegacyConnectionsColleague;
        yield return AggregateServiceNowApplicationsColleague;
        yield return GetServiceNowApplicationsColleague;
        yield return PurgeServiceNowApplicationsColleague;
        yield return AggregateServiceNowUsersColleague;
        yield return GetServiceNowUsersColleague;
        yield return PurgeServiceNowUsersColleague;
        yield return IdentityLinkingColleague;
    }

    /// <summary>
    /// Key for PurgeOidcConnectionColleague
    /// </summary>
    public const string PurgeOidcConnectionColleague = "PurgeOidcConnectionColleague";

    /// <summary>
    /// Key for AggregateOidcConnectionColleague
    /// </summary>
    public const string AggregateOidcConnectionsColleague = "AggregateOidcConnectionsColleague";

    /// <summary>
    /// Key for GetOidcConnectionsColleague
    /// </summary>
    public const string GetOidcConnectionsColleague = "GetOidcConnectionsColleague";

    /// <summary>
    /// Key for PurgeSamlConnectionsColleague
    /// </summary>
    public const string PurgeSamlConnectionsColleague = "PurgeSamlConnectionsColleague";

    /// <summary>
    /// Key for GetSamlConnectionsColleague
    /// </summary>
    public const string GetSamlConnectionsColleague = "GetSamlConnectionsColleague";

    /// <summary>
    /// Key for AggregateSamlConnectionsColleague
    /// </summary>
    public const string AggregateSamlConnectionsColleague = "AggregateSamlConnectionsColleague";

    /// <summary>
    /// Key for PurgeLegacyConnectionsColleague
    /// </summary>
    public const string PurgeLegacyConnectionsColleague = "PurgeLegacyConnectionsColleague";

    /// <summary>
    /// Key for GetLegacyConnectionsColleague
    /// </summary>
    public const string GetLegacyConnectionsColleague = "GetLegacyConnectionsColleague";

    /// <summary>
    /// Key for SearchGetLegacyConnectionsColleague
    /// </summary>
    public const string SearchGetLegacyConnectionsColleague = "SearchGetLegacyConnectionsColleague";

    /// <summary>
    /// Key for AggregateLegacyConnectionsColleague
    /// </summary>
    public const string AggregateLegacyConnectionsColleague = "AggregateLegacyConnectionsColleague";

    /// <summary>
    /// Key for AggregateServiceNowApplicationsColleague
    /// </summary>
    public const string AggregateServiceNowApplicationsColleague = "AggregateServiceNowApplicationsColleague";

    /// <summary>
    /// Key for GetServiceNowApplicationsColleague
    /// </summary>
    public const string GetServiceNowApplicationsColleague = "GetServiceNowApplicationsColleague";

    /// <summary>
    /// Key for PurgeServiceNowApplicationsColleague
    /// </summary>
    public const string PurgeServiceNowApplicationsColleague = "PurgeServiceNowApplicationsColleague";

        /// <summary>
    /// Key for AggregateServiceNowUsersColleague
    /// </summary>
    public const string AggregateServiceNowUsersColleague = "AggregateServiceNowUsersColleague";

    /// <summary>
    /// Key for GetServiceNowUsersColleague
    /// </summary>
    public const string GetServiceNowUsersColleague = "GetServiceNowUsersColleague";

    /// <summary>
    /// Key for PurgeServiceNowUsersColleague
    /// </summary>
    public const string PurgeServiceNowUsersColleague = "PurgeServiceNowUsersColleague";

    /// <summary>
    /// Key for IdentityLinkingColleague
    /// </summary>
    public const string IdentityLinkingColleague = "IdentityLinkingColleague";
}
