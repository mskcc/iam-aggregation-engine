using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

/// <summary>
/// Represents the pagination filter parameters for a paginated query, including the page number and page size.
/// </summary>
public class PaginationFilter
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PaginationFilter"/> class with default values.
    /// The default <see cref="PageNumber"/> is 1, and the default <see cref="PageSize"/> is 10.
    /// </summary>
    public PaginationFilter()
    {
        PageNumber = 1;
        PageSize = 10;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginationFilter"/> class with specified page number and page size.
    /// The <see cref="PageNumber"/> is set to the specified value, or defaults to 1 if the provided value is less than 1.
    /// The <see cref="PageSize"/> is set to the specified value, or defaults to 10 if the provided value is greater than 10.
    /// </summary>
    /// <param name="pageNumber">The page number for the paginated query. If less than 1, defaults to 1.</param>
    /// <param name="pageSize">The number of items per page for the paginated query. If greater than 10, defaults to 10.</param>
    public PaginationFilter(int pageNumber, int pageSize, int maxPageSize = 500)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize > maxPageSize ? maxPageSize : pageSize;
        MaxPageSize = maxPageSize;
    }
    
    /// <summary>
    /// Gets or sets the current page number for the paginated query.
    /// Defaults to 1 if not specified.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Gets or sets the number of items per page for the paginated query.
    /// Defaults to 10 if not specified. A maximum of 10 items per page is allowed.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of items per page for the paginated query.
    /// Defaults to 500 if not specified.
    /// </summary>
    public int MaxPageSize { get; set; }
}
