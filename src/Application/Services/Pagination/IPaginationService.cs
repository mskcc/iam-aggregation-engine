using System;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.Pagination;

/// <summary>
/// Service for pagination.
/// </summary>
public interface IPaginationService
{
    /// <summary>
    /// Pagination filter for api results.
    /// </summary>
    PaginationFilter? PaginationFilter { get; set; }
}
