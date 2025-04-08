using Microsoft.Extensions.Logging;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.UriHelper;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PagedResponse;

/// <inheritdoc/>
public class PagedResponseService<T> : IPagedResponseService<T> where T : class
{
    private readonly ILogger<PagedResponseService<T>> _logger;
    private readonly IUriHelperService _uriHelperService;

    /// <summary>
    /// Creates an instance of <see cref="PagedResponseService{T}"/>.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="uriHelperService"></param>
    public PagedResponseService(ILogger<PagedResponseService<T>> logger, IUriHelperService uriHelperService)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(uriHelperService);

        _logger = logger;
        _uriHelperService = uriHelperService;
    }

    /// <inheritdoc/>
    public PaginationFilter? PaginationFilter { get; set; }

    /// <inheritdoc/>
    public PagedResponse<IEnumerable<T>> GetPagedResponse(IEnumerable<T> connections, int totalRecords, PaginationFilter filter, string endpoint)
    {
        ArgumentNullException.ThrowIfNull(connections);
        ArgumentNullException.ThrowIfNull(filter);
        ArgumentNullException.ThrowIfNullOrEmpty(endpoint);
        
        var totalPages = (double)totalRecords / (double)filter.PageSize;
        var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
        var nextPage = filter.PageNumber >= 1 && filter.PageNumber < roundedTotalPages
            ? _uriHelperService.GetPageUri(new PaginationFilter(filter.PageNumber + 1, filter.PageSize), endpoint)
            : null;
        var previousPage = filter.PageNumber - 1 >= 1 && filter.PageNumber <= roundedTotalPages
            ? _uriHelperService.GetPageUri(new PaginationFilter(filter.PageNumber - 1, filter.PageSize), endpoint)
            : null;
        var firstPage = _uriHelperService.GetPageUri(new PaginationFilter(1, filter.PageSize), endpoint);
        var lastPage = _uriHelperService.GetPageUri(new PaginationFilter(roundedTotalPages, filter.PageSize), endpoint);

        return new PagedResponse<IEnumerable<T>>(connections, filter.PageNumber, filter.PageSize)
        {
            TotalPages = roundedTotalPages,
            TotalRecords = totalRecords,
            FirstPage = firstPage,
            NextPage = nextPage,
            PreviousPage = previousPage,
            LastPage = lastPage
        };
    }
}
