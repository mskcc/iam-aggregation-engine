using Flurl;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PagedResponse;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.Pagination;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.ResourceState;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Exceptions;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.UseCases;

public class GetLegacyConnectionsColleague : IColleague
{
    private readonly IMediator _mediator;
    private readonly ILegacyService _legacyService;
    private readonly IResourceStateService _resourceStateService;
    private readonly IOptionsMonitor<ApiOptions> _apiOptions;
    private readonly IPagedResponseService<LegacyConnection> _pagedResponseService;
    private readonly IPaginationService _paginationService;
    private readonly ILogger<GetLegacyConnectionsColleague> _logger;

    /// <summary>
    /// Creates an instance of <see cref="GetLegacyConnectionsColleague"/>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="legacyService"></param>
    /// <param name="resourceStateService"></param>
    /// <param name="apiOptions"></param>
    /// <param name="pagedResponseService"></param>
    /// <param name="paginationService"></param>
    /// <param name="logger"></param>
    public GetLegacyConnectionsColleague(
        IMediator mediator,
        ILegacyService legacyService,
        IResourceStateService resourceStateService,
        IOptionsMonitor<ApiOptions> apiOptions,
        IPagedResponseService<LegacyConnection> pagedResponseService,
        IPaginationService paginationService,
        ILogger<GetLegacyConnectionsColleague> logger)
    {
        ArgumentNullException.ThrowIfNull(mediator);
        ArgumentNullException.ThrowIfNull(legacyService);
        ArgumentNullException.ThrowIfNull(resourceStateService);
        ArgumentNullException.ThrowIfNull(apiOptions);
        ArgumentNullException.ThrowIfNull(logger);
        
        _mediator = mediator;
        _legacyService = legacyService;
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
        var legacyConnectionsEndpoint = options.BaseEndpoint.AppendPathSegment(options.LegacyConnectionsEndpoint);

        if (_resourceStateService.IsOidcAggregationRunning)
        {
            throw new ConflictException(ErrorNames.AggregationInProgress, "OIDC aggregation is running. Please wait for it to complete.");
        }

        if (_resourceStateService.IsOidcPurgeRunning)
        {
            throw new ConflictException(ErrorNames.PurgeInProgress, "OIDC purge is running. Please wait for it to complete.");
        }

        if (_resourceStateService.IsSamlAggregationRunning)
        {
            throw new ConflictException(ErrorNames.AggregationInProgress, "SAML aggregation is running. Please wait for it to complete.");
        }

        if (_resourceStateService.IsSamlPurgeRunning)
        {
            throw new ConflictException(ErrorNames.PurgeInProgress, "SAML purge is running. Please wait for it to complete.");
        }

        if (_resourceStateService.IsLegacyAggregationRunning)
        {
            _logger.LogWarning("Legacy Aggregation is running. Please wait for it to complete.");
            throw new ConflictException(ErrorNames.AggregationInProgress, "Legacy aggregation is running. Please wait for it to complete.");
        }

        if(_resourceStateService.IsLegacyPurgeRunning)
        {
            _logger.LogWarning("Legacy Purge is running. Please wait for it to complete.");
            throw new ConflictException(ErrorNames.PurgeInProgress, "Legacy purge is running. Please wait for it to complete.");
        }

        if (_apiOptions.CurrentValue.UsePagination is false)
        {
            return new Response<IEnumerable<LegacyConnection>>(await _legacyService.GetLegacyConnectionsAsync());
        }

        var filter = _paginationService.PaginationFilter ?? 
            throw new ValidationException(ErrorNames.ValidationError, "Please provide page filtering options.");

        if (filter.PageNumber < 1 || filter.PageSize < 1)
        {
            throw new ValidationException(ErrorNames.ValidationError, "Please provide valid page number and page size.");
        }

        var legacyConnections = await _legacyService.GetLegacyConnectionsAsync(filter);
        var totalRecords = _legacyService.GetLegacyConnectionsCount();

        var pagedResponse = _pagedResponseService
            .GetPagedResponse(legacyConnections, totalRecords, filter, legacyConnectionsEndpoint);

        _logger.LogDebug("Get legacy Connections Colleague Receives: {message}", serviceKey);

        return pagedResponse;
    }
}