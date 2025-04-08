using Hangfire;
using Microsoft.Extensions.Logging;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.ResourceState;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.ServiceNow;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Exceptions;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Data;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.UseCases;

public class PurgeServiceNowUsersColleague : IColleague
{
    private readonly IMediator _mediator;
    private readonly IServiceNowService _serviceNowService;
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly IResourceStateService _resourceStateService;
    private readonly ILogger<PurgeServiceNowUsersColleague> _logger;

    /// <summary>
    /// Creates an instance of <see cref="PurgeServiceNowUsersColleague"/>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="serviceNowService"></param>
    /// <param name="backgroundJobClient"></param>
    /// <param name="resourceStateService"></param>
    /// <param name="logger"></param>
    public PurgeServiceNowUsersColleague(
        IMediator mediator,
        IServiceNowService serviceNowService,
        IBackgroundJobClient backgroundJobClient,
        IResourceStateService resourceStateService,
        ILogger<PurgeServiceNowUsersColleague> logger
    )
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
    /// Execute task
    /// </summary>
    /// <param name="serviceKey"></param>
    /// <returns></returns>
    public async Task<object> Execute(string serviceKey, object? payloadData) => await _mediator.Notify(this, serviceKey, payloadData!);

    /// <summary>
    /// Receive notification
    /// </summary>
    /// <param name="serviceKey"></param>
    /// <returns></returns>
    public Task<object> Receive(string serviceKey, object? payloadData)
    {
        ArgumentNullException.ThrowIfNull(serviceKey);

        if (_resourceStateService.IsServiceNowUsersAggregationRunning)
        {
            throw new ConflictException(ErrorNames.AggregationInProgress, "Service Now Users aggregation is running. Please wait for it to complete."); 
        }

        if (_resourceStateService.IsServiceNowUsersPurgingRunning)
        {
            throw new ConflictException(ErrorNames.PurgeInProgress, "Service Now Users purge is running. Please wait for it to complete.");
        }

        _backgroundJobClient.Enqueue(() => _serviceNowService.PurgeServiceNowUsersAsync());
        _logger.LogDebug("Purge Service Now Users Colleague Receives: {message}", serviceKey);

        return Task.FromResult<object>(new Response<IEnumerable<ServiceNowUser>>()
        {
            Message = $"{nameof(ApplicationDbContext.ServiceNowUsers)} table purge started on {DateTime.Now.ToLocalTime()}"
        });
    }
}