using Hangfire;
using Microsoft.Extensions.Logging;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PingFederate;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.ResourceState;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Exceptions;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.UseCases;

/// <summary>
/// Concrete implementation of <see cref="IColleague"/>
/// </summary>
public class AggregateOidcConnectionsColleague : IColleague
{
    private readonly IMediator _mediator;
    private readonly IPingFederateService _pingFederateService;
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly IResourceStateService _resourceStateService;
    private readonly ILogger<AggregateOidcConnectionsColleague> _logger;

    /// <summary>
    /// Creates an instance of <see cref="AggregateOidcConnectionsColleague"/>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="pingFederateService"></param>
    /// <param name="backgroundJobClient"></param>
    /// <param name="resourceStateService"></param>
    /// <param name="logger"></param>
    public AggregateOidcConnectionsColleague(
        IMediator mediator,
        IPingFederateService pingFederateService,
        IBackgroundJobClient backgroundJobClient,
        IResourceStateService resourceStateService,
        ILogger<AggregateOidcConnectionsColleague> logger)
    {
        ArgumentNullException.ThrowIfNull(mediator);
        ArgumentNullException.ThrowIfNull(pingFederateService);
        ArgumentNullException.ThrowIfNull(backgroundJobClient);
        ArgumentNullException.ThrowIfNull(resourceStateService);
        ArgumentNullException.ThrowIfNull(logger);
        
        _mediator = mediator;
        _pingFederateService = pingFederateService;
        _backgroundJobClient = backgroundJobClient;
        _resourceStateService = resourceStateService;
        _logger = logger;
    }

    /// <summary>
    /// Send service key
    /// </summary>
    public async Task<object> Execute(string serviceKey, object? payloadData) => await _mediator.Notify(this, serviceKey, payloadData!);

    /// <summary>
    /// Receive notification
    /// </summary>
    public Task<object> Receive(string serviceKey, object? payloadData)
    {
        if (_resourceStateService.IsOidcAggregationRunning)
        {
            _logger.LogWarning("OIDC Aggregation is running. Please wait for it to complete.");
            throw new ConflictException(ErrorNames.AggregationInProgress, "OIDC aggregation is running. Please wait for it to complete.");
        }

        if (_resourceStateService.IsOidcPurgeRunning)
        {
            _logger.LogWarning("OIDC Purge is running. Please wait for it to complete.");
            throw new ConflictException(ErrorNames.PurgeInProgress, "OIDC purge is running. Please wait for it to complete.");
        }
        
        _backgroundJobClient.Enqueue(() => _pingFederateService.AggregateOidcConnectionsAsync());
        _logger.LogDebug("Aggregate OIDC Connections Colleague Receives: {message}", serviceKey);

        return Task.FromResult<object>(new Response<IEnumerable<OidcClient>>
        {
            Message = $"Started OIDC clients aggregation background job on {DateTime.Now.ToLocalTime()}"
        });
    }
}