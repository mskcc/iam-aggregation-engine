using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PagedResponse;

/// <summary>
/// Service to create a paged response.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IPagedResponseService<T> where T : class
{
    /// <summary>
    /// Get paged response.
    /// </summary>
    /// <param name="connections"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    PagedResponse<IEnumerable<T>> GetPagedResponse(IEnumerable<T> connectionsm, int totalRecords, PaginationFilter filter, string endpoint);

    /// <summary>
    /// Pagination filter.
    /// </summary>
    PaginationFilter? PaginationFilter { get; set; }
}
