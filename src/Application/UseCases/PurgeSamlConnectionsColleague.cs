using Hangfire;
using Microsoft.Extensions.Logging;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PingFederate;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.ResourceState;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Exceptions;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Data;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.UseCases;

/// <summary>
/// Concrete implementation of <see cref="IColleague"/>
/// </summary>
public class PurgeSamlConnectionColleague : IColleague
{
    private readonly IMediator _mediator;
    private readonly IPingFederateService _pingFederateService;
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly IResourceStateService _resourceStateService;
    private readonly ILogger<PurgeSamlConnectionColleague> _logger;

    /// <summary>
    /// Creates an instance of <see cref="PurgeSamlConnectionColleague"/>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="pingFederateService"></param>
    /// <param name="backgroundJobClient"></param>
    /// <param name="resourceStateService"></param>
    /// <param name="logger"></param>
    public PurgeSamlConnectionColleague(
        IMediator mediator,
        IPingFederateService pingFederateService,
        IBackgroundJobClient backgroundJobClient,
        IResourceStateService resourceStateService,
        ILogger<PurgeSamlConnectionColleague> logger)
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
    /// Send message
    /// </summary>
    public async Task<object> Execute(string serviceKey, object? payloadData) => await _mediator.Notify(this, serviceKey, payloadData!);

    /// <summary>
    /// Receive notification
    /// </summary>
    public Task<object> Receive(string serviceKey, object? payloadData)
    {
        if (_resourceStateService.IsSamlAggregationRunning)
        {
            throw new ConflictException(ErrorNames.AggregationInProgress, "SAML aggregation is running. Please wait for it to complete."); 
        }

        if (_resourceStateService.IsSamlPurgeRunning)
        {
            throw new ConflictException(ErrorNames.PurgeInProgress, "SAML purge is running. Please wait for it to complete.");
        }

        _backgroundJobClient.Enqueue(() => _pingFederateService.PurgeSpConnectionsAsync());
        _logger.LogDebug("Purge SAML Connections Colleague Receives: {message}", serviceKey);

        return Task.FromResult<object>(new Response<IEnumerable<SpConnection>>()
        {
            Message = $"{nameof(ApplicationDbContext.PingFederateSamlConnections)} table purge started on {DateTime.Now.ToLocalTime()}"
        });
    }
}