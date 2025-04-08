using Hangfire;
using Microsoft.Extensions.Logging;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.ResourceState;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.ServiceNow;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Exceptions;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.UseCases;

public class AggregateServiceNowApplicationsColleague : IColleague
{
    private readonly IMediator _mediator;
    private readonly IServiceNowService _serviceNowService;
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly IResourceStateService _resourceStateService;
    private readonly ILogger<AggregateServiceNowApplicationsColleague> _logger;

    /// <summary>
    /// Creates an instance of <see cref="AggregateServiceNowApplicationsColleague"/>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="backgroundJobClient"></param>
    /// <param name="serviceNowService"></param>
    /// <param name="resourceStateService"></param>
    /// <param name="logger"></param>
    public AggregateServiceNowApplicationsColleague(
        IMediator mediator,
        IServiceNowService serviceNowService,
        IBackgroundJobClient backgroundJobClient,
        IResourceStateService resourceStateService,
        ILogger<AggregateServiceNowApplicationsColleague> logger)
    {
        ArgumentNullException.ThrowIfNull(mediator);
        ArgumentNullException.ThrowIfNull(serviceNowService);
        ArgumentNullException.ThrowIfNull(backgroundJobClient);
        ArgumentNullException.ThrowIfNull(resourceStateService);
        ArgumentNullException.ThrowIfNull(logger);

        _mediator = mediator;
        _serviceNowService = serviceNowService;
        _backgroundJobClient = backgroundJobClient;
        _resourceStateService = resourceStateService;
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
    public Task<object> Receive(string serviceKey, object? payloadData)
    {
        if (_resourceStateService.IsServiceNowApplicationsAggregationRunning)
        {
            _logger.LogInformation("ServiceNow aggregation is already running. Skipping this run.");
            throw new ConflictException(ErrorNames.AggregationInProgress, "Service Now applications aggregation is running. Please wait for it to complete.");
        }

        if (_resourceStateService.IsServiceNowApplicationsPurgingRunning)
        {
            _logger.LogInformation("ServiceNow purging is already running. Skipping this run.");
            throw new ConflictException(ErrorNames.PurgeInProgress, "Service Now applications purging is running. Please wait for it to complete.");
        }

        _backgroundJobClient.Enqueue(() => _serviceNowService.AggregateServiceNowApplicationsAsync());
        _logger.LogDebug("Enqueued job to aggregate ServiceNow applications");

        return Task.FromResult<object>(new Response<IEnumerable<ServiceNowApplication>>
        {
            Message = $"Started Service Now applications aggregation background job on {DateTime.Now.ToLocalTime()}"
        });
    }
}