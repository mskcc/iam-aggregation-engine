using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;

/// <summary>
/// Interface for the legacy service.
/// </summary>
public interface ILegacyService
{
    /// <summary>
    /// Searches the Legacy connection from the configured Ping Federate SQL table instance.
    /// </summary>
    /// <param name="paginationFilter"></param>
    /// <param name="criteria"></param>
    /// <returns></returns>
    Task<IEnumerable<LegacyConnection>> SearchLegacyConnectionsAsync(PaginationFilter paginationFilter, SearchCriteria searchCriteria);

    /// <summary>
    /// Gets the Legacy connections from the configured Ping Federate SQL table instance.
    /// </summary>
    public Task<IEnumerable<LegacyConnection>> GetLegacyConnectionsAsync(PaginationFilter paginationFilter);

    /// <summary>
    /// Gets the Legacy connections from the configured Ping Federate SQL table instance.
    /// </summary>
    public Task<IEnumerable<LegacyConnection>> GetLegacyConnectionsAsync();

    /// <summary>
    /// Gets the count of Legacy connections from the configured Ping Federate SQL table instance.
    /// </summary>
    /// <returns></returns>
    public int GetLegacyConnectionsCount();

    /// <summary>
    /// Gets the count of Legacy connections from the configured Ping Federate SQL table instance. This method a search criteria.
    /// </summary>
    /// <param name="searchCriteria"></param>
    /// <returns></returns>
    public int GetLegacyConnectionsCountWithCriteria(SearchCriteria searchCriteria);

    /// <summary>
    /// Aggregates Legacy connections from the configured Ping Federate SQL table instance.
    /// </summary>
    public Task<IEnumerable<LegacyConnection>> AggregateLegacyConnectionsAsync();

    /// <summary>
    /// Purges Legacy connections from database.
    /// </summary>
    /// <returns><see cref="Void"/></returns>
    public Task PurgeLegacyConnectionsAsync();
}