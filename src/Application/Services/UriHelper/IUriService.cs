using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.UriHelper;

/// <summary>
/// Service to help with URIs
/// </summary>
public interface IUriHelperService
{
    /// <summary>
    /// Gets the page uri.
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="route"></param>
    /// <returns><see cref="Uri"/></returns>
    public Uri GetPageUri(PaginationFilter filter, string route);
}