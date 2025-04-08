using Hangfire;
using Microsoft.Extensions.Logging;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.ResourceState;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Exceptions;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.UseCases;

/// <summary>
/// Concrete implementation of <see cref="IColleague"/>
/// </summary>
public class AggregateLegacyConnectionsColleague : IColleague
{
    private readonly IMediator _mediator;
    private readonly ILegacyService _legacyService;
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly IResourceStateService _resourceStateService;
    private readonly ILogger<AggregateLegacyConnectionsColleague> _logger;

    /// <summary>
    /// Creates an instance of <see cref="AggregateLegacyConnectionsColleague"/>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="legacyService"></param>
    /// <param name="backgroundJobClient"></param>
    /// <param name="resourceStateService"></param>
    /// <param name="logger"></param>
    public AggregateLegacyConnectionsColleague(
        IMediator mediator,
        ILegacyService legacyService,
        IBackgroundJobClient backgroundJobClient,
        IResourceStateService resourceStateService,
        ILogger<AggregateLegacyConnectionsColleague> logger)
    {
        ArgumentNullException.ThrowIfNull(mediator);
        ArgumentNullException.ThrowIfNull(legacyService);
        ArgumentNullException.ThrowIfNull(backgroundJobClient);
        ArgumentNullException.ThrowIfNull(resourceStateService);
        ArgumentNullException.ThrowIfNull(logger);
        
        _mediator = mediator;
        _legacyService = legacyService;
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

        if (_resourceStateService.IsSamlAggregationRunning)
        {
            _logger.LogWarning("SAML Aggregation is running. Please wait for it to complete.");
            throw new ConflictException(ErrorNames.AggregationInProgress, "SAML aggregation is running. Please wait for it to complete.");
        }

        if (_resourceStateService.IsSamlPurgeRunning)
        {
            _logger.LogWarning("SAML Purge is running. Please wait for it to complete.");
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
        
        _backgroundJobClient.Enqueue(() => _legacyService.AggregateLegacyConnectionsAsync());
        _logger.LogDebug("Aggregate Legacy Connections Colleague Receives: {message}", serviceKey);

        return Task.FromResult<object>(new Response<IEnumerable<OidcClient>>
        {
            Message = $"Started Legacy connections aggregation background job on {DateTime.Now.ToLocalTime()}"
        });
    }
}