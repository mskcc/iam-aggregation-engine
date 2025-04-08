using Flurl;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PagedResponse;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.Pagination;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PingFederate;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.ResourceState;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Exceptions;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.UseCases;

public class GetOidcConnectionsColleague : IColleague
{
    private readonly IMediator _mediator;
    private readonly IPingFederateService _pingFederateService;
    private readonly IResourceStateService _resourceStateService;
    private readonly IOptionsMonitor<ApiOptions> _apiOptions;
    private readonly IPagedResponseService<OidcClient> _pagedResponseService;
    private readonly IPaginationService _paginationService;
    private readonly ILogger<GetOidcConnectionsColleague> _logger;

    /// <summary>
    /// Creates an instance of <see cref="GetOidcConnectionsColleague"/>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="pingFederateService"></param>
    /// <param name="resourceStateService"></param>
    /// <param name="apiOptions"></param>
    /// <param name="pagedResponseService"></param>
    /// <param name="paginationService"></param>
    /// <param name="logger"></param>
    public GetOidcConnectionsColleague(
        IMediator mediator,
        IPingFederateService pingFederateService,
        IResourceStateService resourceStateService,
        IOptionsMonitor<ApiOptions> apiOptions,
        IPagedResponseService<OidcClient> pagedResponseService,
        IPaginationService paginationService,
        ILogger<GetOidcConnectionsColleague> logger)
    {
        ArgumentNullException.ThrowIfNull(mediator);
        ArgumentNullException.ThrowIfNull(pingFederateService);
        ArgumentNullException.ThrowIfNull(resourceStateService);
        ArgumentNullException.ThrowIfNull(apiOptions);
        ArgumentNullException.ThrowIfNull(logger);
        
        _mediator = mediator;
        _pingFederateService = pingFederateService;
        _resourceStateService = resourceStateService;
        _apiOptions = apiOptions;
        _pagedResponseService = pagedResponseService;
        _paginationService = paginationService;
        _logger = logger;
    }

    /// <summary>
    /// Send service key
    /// </summary>
    public async Task<object> Execute(string serviceKey, object? payloadData) => await _mediator.Notify(this, serviceKey, payloadData!);

    /// <summary>
    /// Receive notification
    /// </summary>
    public async Task<object> Receive(string serviceKey, object? payloadData)
    {
        var options = _apiOptions.CurrentValue;
        var oidcConnectionsEndpoint = options.BaseEndpoint.AppendPathSegment(options.OidcConnectionsEndpoint);

        if (_resourceStateService.IsOidcAggregationRunning)
        {
            throw new ConflictException(ErrorNames.AggregationInProgress, "OIDC aggregation is running. Please wait for it to complete.");
        }

        if (_resourceStateService.IsOidcPurgeRunning)
        {
            throw new ConflictException(ErrorNames.PurgeInProgress, "OIDC purge is running. Please wait for it to complete.");
        }

        if (_apiOptions.CurrentValue.UsePagination is false)
        {
            return new Response<IEnumerable<OidcClient>>(await _pingFederateService.GetOidcConnectionsAsync());
        }

        var filter = _paginationService.PaginationFilter ?? 
            throw new ValidationException(ErrorNames.ValidationError, "Please provide page filtering options.");

        if (filter.PageNumber < 1 || filter.PageSize < 1)
        {
            throw new ValidationException(ErrorNames.ValidationError, "Please provide valid page number and page size.");
        }
            
        var oidcClients = await _pingFederateService.GetOidcConnectionsAsync(filter);
        var totalRecords = _pingFederateService.GetOidcConnectionsCount();

        var pagedResponse = _pagedResponseService
            .GetPagedResponse(oidcClients, totalRecords, filter, oidcConnectionsEndpoint);
        
        _logger.LogDebug("Get OIDC Connections Colleague Receives: {message}", serviceKey);

        return pagedResponse;
    }
}