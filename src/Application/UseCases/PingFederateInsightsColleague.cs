using Microsoft.Extensions.Logging;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.ResourceState;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Exceptions;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.UseCases;

public class PingFederateInsightsColleague : IColleague
{
    private readonly IMediator _mediator;
    private readonly IResourceStateService _resourceStateService;
    private readonly IPingFederateInsightService _pingFederateInsightService;
    private readonly ILogger<GetLegacyConnectionsColleague> _logger;

    /// <summary>
    /// Creates an instance of <see cref="PingFederateInsightsColleague"/>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="resourceStateService"></param>
    /// <param name="pingFederateInsightService"></param>
    /// <param name="logger"></param>
    public PingFederateInsightsColleague(
        IMediator mediator,
        IResourceStateService resourceStateService,
        IPingFederateInsightService pingFederateInsightService,
        ILogger<GetLegacyConnectionsColleague> logger)
    {
        ArgumentNullException.ThrowIfNull(mediator);
        ArgumentNullException.ThrowIfNull(resourceStateService);
        ArgumentNullException.ThrowIfNull(pingFederateInsightService);
        ArgumentNullException.ThrowIfNull(logger);
        
        _mediator = mediator;
        _resourceStateService = resourceStateService;
        _pingFederateInsightService = pingFederateInsightService;
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
        ArgumentNullException.ThrowIfNullOrEmpty(serviceKey);
        ArgumentNullException.ThrowIfNull(payloadData);

        var payload = payloadData as InsightsNotification;
        var notificationType = payload?.NotificationType!;

        if (_resourceStateService.IsLegacyAggregationRunning)
        {
            _logger.LogWarning("Legacy Aggregation is running. Please wait for it to complete.");
            throw new ConflictException(ErrorNames.AggregationInProgress, "Legacy aggregation is running. Please wait for it to complete.");
        }

        if (_resourceStateService.IsLegacyPurgeRunning)
        {
            _logger.LogWarning("Legacy Purge is running. Please wait for it to complete.");
            throw new ConflictException(ErrorNames.PurgeInProgress, "Legacy purge is running. Please wait for it to complete.");
        }

        InsightResponse? insightResponse = null;
        if (notificationType is InsightsPayload.MissingTechnicalOwners)
        {
            insightResponse = await _pingFederateInsightService.GetConfigurationsWithMissingTechnicalOwnersInsightsAsync();
        }
        if (notificationType is InsightsPayload.MissinBusinessOwners)
        {
            insightResponse = await _pingFederateInsightService.GetConfigurationsWithMissingBusinessOwnersInsightsAsync();
        }

        _logger.LogDebug("Get Ping Federate Insights received service key: {message}", serviceKey);

        return Task.FromResult<object>(new Response<InsightResponse>()
        {
            Message = $"Application insights processed on {DateTime.Now.ToLocalTime()}",
            Data = insightResponse
        });
    }
}