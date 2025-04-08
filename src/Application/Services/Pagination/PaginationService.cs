using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.Pagination;

/// <summary>
/// Service for pagination.
/// </summary>
/// <remarks>
/// This service is thread safe.
/// </remarks>
public class PaginationService : IPaginationService
{
    private readonly ApiOptions _apiOptions;
    private readonly ILogger<PaginationService> _logger;
    private readonly ReaderWriterLockSlim _lock;
    private PaginationFilter? _paginationFilter;

    /// <summary>
    /// Creates an instance of <see cref="PaginationService"/>.
    /// </summary>
    public PaginationService(
        IOptionsMonitor<ApiOptions> apiOptions,
        ILogger<PaginationService> logger)
    {
        ArgumentNullException.ThrowIfNull(apiOptions);
        ArgumentNullException.ThrowIfNull(logger);

        _apiOptions = apiOptions.CurrentValue;
        _logger = logger;
        _lock = new ReaderWriterLockSlim();
    }

    /// <inheritdoc/>
    public PaginationFilter? PaginationFilter
    {
        get
        {
            _logger.LogDebug("Entering read lock for PaginationFilter");
            _lock.EnterReadLock();
            try
            {
                var isPageSizeGreaterThanMaxPageSize = _paginationFilter?.PageSize > _apiOptions.MaxPageSize;
                _paginationFilter!.PageSize = isPageSizeGreaterThanMaxPageSize ? _apiOptions.MaxPageSize : _paginationFilter.PageSize;
                _paginationFilter!.MaxPageSize = _apiOptions.MaxPageSize;

                return _paginationFilter;
            }
            finally
            {
                _lock.ExitReadLock();
                _logger.LogDebug("Exiting read lock for PaginationFilter");
            }
        }
        set
        {
            _logger.LogDebug("Entering read lock for PaginationFilter");
            _lock.EnterReadLock();
            try
            {
                _paginationFilter = value;
            }
            finally
            {
                _logger.LogDebug("Exiting read lock for PaginationFilter");
                _lock.ExitReadLock();
            }
        }
    }
}

