using Microsoft.EntityFrameworkCore;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Extensions;

/// <summary>
/// Provides extension methods for IQueryable of SpConnection.
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Includes all related tables for the SpConnection query.
    /// </summary>
    /// <param name="query">The query to include related tables.</param>
    /// <returns>The query with included related tables.</returns>
    public static IQueryable<SpConnection> IncludeAllSpConnectionTables(this IQueryable<SpConnection> query) => query.Include(sp => sp.AttributeContractFulfillment);

    /// <summary>
    /// Includes all related tables for the OIDC Client query.
    /// </summary>
    /// <param name="query">The query to include related tables.</param>
    /// <returns>The query with included related tables.</returns>
    public static IQueryable<OidcClient> IncludeAllOidcClientTables(this IQueryable<OidcClient> query) => query;

    /// <summary>
    /// Includes all related tables for the Legacy Connection query.
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public static IQueryable<LegacyConnection> IncludeAllLegacyConnectionTables(this IQueryable<LegacyConnection> query) => query;
}
