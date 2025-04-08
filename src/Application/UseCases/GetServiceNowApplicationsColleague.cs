using Flurl;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PagedResponse;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.Pagination;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.ResourceState;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.ServiceNow;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Exceptions;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.UseCases;

public class GetServiceNowApplicationsColleague : IColleague
{
    private readonly IMediator _mediator;
    private readonly IServiceNowService _serviceNowService;
    private readonly IResourceStateService _resourceStateService;
    private readonly ApiOptions _apiOptions;
    private readonly IPagedResponseService<ServiceNowApplication> _pagedResponseService;
    private readonly IPaginationService _paginationService;
    private readonly ILogger<GetServiceNowApplicationsColleague> _logger;

    /// <summary>
    /// Creates an instance of <see cref="GetServiceNowApplicationsColleague"/>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="serviceNowService"></param>
    /// <param name="resourceStateService"></param>
    /// <param name="apiOptions"></param>
    /// <param name="pagedResponseService"></param>
    /// <param name="paginationService"></param>
    /// <param name="logger"></param>
    public GetServiceNowApplicationsColleague(
        IMediator mediator,
        IServiceNowService serviceNowService,
        IResourceStateService resourceStateService,
        IOptionsMonitor<ApiOptions> apiOptions,
        IPagedResponseService<ServiceNowApplication> pagedResponseService,
        IPaginationService paginationService,
        ILogger<GetServiceNowApplicationsColleague> logger
    )
    {
        ArgumentNullException.ThrowIfNull(mediator);
        ArgumentNullException.ThrowIfNull(serviceNowService);
        ArgumentNullException.ThrowIfNull(resourceStateService);
        ArgumentNullException.ThrowIfNull(apiOptions);
        ArgumentNullException.ThrowIfNull(pagedResponseService);
        ArgumentNullException.ThrowIfNull(paginationService);
        ArgumentNullException.ThrowIfNull(logger);

        _mediator = mediator;
        _serviceNowService = serviceNowService;
        _resourceStateService = resourceStateService;
        _apiOptions = apiOptions.CurrentValue;
        _pagedResponseService = pagedResponseService;
        _paginationService = paginationService;
        _logger = logger;
    }

    /// <summary>
    /// Send service key
    /// </summary>
    /// <param name="serviceKey"></param>
    /// <returns></returns>
    public async Task<object> Execute(string serviceKey, object? payloadData) => await _mediator.Notify(this, serviceKey, payloadData!);

    /// <summary>
    /// Receive notification
    /// </summary>
    /// <param name="serviceKey"></param>
    /// <returns></returns>
    /// <exception cref="ConflictException"></exception>
    /// <exception cref="ValidationException"></exception>
    public async Task<object> Receive(string serviceKey, object? payloadData)
    {
        var serviceNowApplicationsEndpoint = _apiOptions.BaseEndpoint.AppendPathSegment(_apiOptions.ServiceNowApplicationsEndpoint);

        if (_resourceStateService.IsServiceNowApplicationsAggregationRunning)
        {
             throw new ConflictException(ErrorNames.AggregationInProgress, "Service Now applications aggregation is running. Please wait for it to complete.");
        }

        if (_resourceStateService.IsServiceNowApplicationsPurgingRunning)
        {
             throw new ConflictException(ErrorNames.PurgeInProgress, "Service Now applications purge is running. Please wait for it to complete.");
        }

        if (_apiOptions.UsePagination is false)
        {
            return new Response<IEnumerable<ServiceNowApplication>>(await _serviceNowService.GetServiceNowApplicationsAsync());
        }

        var filter = _paginationService.PaginationFilter ?? 
            throw new ValidationException(ErrorNames.ValidationError, "Please provide page filtering options.");

        if (filter.PageNumber < 1 || filter.PageSize < 1)
        {
            throw new ValidationException(ErrorNames.ValidationError, "Please provide valid page number and page size.");
        }

        var serviceNowApplications = await _serviceNowService.GetServiceNowApplicationsAsync(filter);
        var totalRecords = _serviceNowService.GetServiceNowApplicationsCount();

        var pagedResponse = _pagedResponseService
            .GetPagedResponse(serviceNowApplications, totalRecords, filter, serviceNowApplicationsEndpoint);

        _logger.LogDebug("Get Service Now Applications Colleague Receives: {message}", serviceKey);

        return pagedResponse;
    }
}