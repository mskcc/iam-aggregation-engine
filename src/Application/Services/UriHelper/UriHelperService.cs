using Microsoft.AspNetCore.WebUtilities;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.UriHelper;

/// <inheritdoc />
public class UriHelperService : IUriHelperService
{
    private readonly string _baseUri;
    
    /// <summary>
    /// Creates an instance of <see cref="UriHelperService"/>
    /// </summary>
    /// <param name="baseUri"></param>
    public UriHelperService(string baseUri)
    {
        _baseUri = baseUri;
    }

    /// <inheritdoc />
    public Uri GetPageUri(PaginationFilter filter, string route)
    {
        var _enpointUri = new Uri(string.Concat(_baseUri, route));
        var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
        modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
        return new Uri(modifiedUri);
    }
}