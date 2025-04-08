namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

/// <summary>
/// Represents a paged response structure for data, including pagination metadata such as page numbers,
/// total records, and navigation links for paginated results.
/// </summary>
/// <typeparam name="T">The type of data returned in the response.</typeparam>
public class PagedResponse<T> : Response<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PagedResponse{T}"/> class with the specified data, page number, and page size.
    /// </summary>
    /// <param name="data">The data to be included in the response for the current page.</param>
    /// <param name="pageNumber">The current page number of the response.</param>
    /// <param name="pageSize">The number of items per page in the response.</param>
    public PagedResponse()
    {
        Message = string.Empty;
        Succeeded = true;
        Errors = null;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PagedResponse{T}"/> class with the specified data, page number, and page size.
    /// </summary>
    /// <param name="data">The data to be included in the response for the current page.</param>
    /// <param name="pageNumber">The current page number of the response.</param>
    /// <param name="pageSize">The number of items per page in the response.</param>
    public PagedResponse(T data, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Data = data;
        Message = string.Empty;
        Succeeded = true;
        Errors = null;
    }

    /// <summary>
    /// Gets or sets the URI of the first page in the paginated result set.
    /// </summary>
    public Uri? FirstPage { get; set; }

    /// <summary>
    /// Gets or sets the URI of the last page in the paginated result set.
    /// </summary>
    public Uri? LastPage { get; set; }

    /// <summary>
    /// Gets or sets the URI of the next page in the paginated result set, or <c>null</c> if there is no next page.
    /// </summary>
    public Uri? NextPage { get; set; }

    /// <summary>
    /// Gets or sets the URI of the previous page in the paginated result set, or <c>null</c> if there is no previous page.
    /// </summary>
    public Uri? PreviousPage { get; set; }

    /// <summary>
    /// Gets or sets the current page number in the paginated response.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Gets or sets the number of items per page in the paginated response.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the total number of pages available in the result set.
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Gets or sets the total number of records available across all pages.
    /// </summary>
    public int TotalRecords { get; set; }
}
