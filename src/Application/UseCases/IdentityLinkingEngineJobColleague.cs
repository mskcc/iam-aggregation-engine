using Hangfire;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.IdentityLinkingService;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.ResourceState;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.UseCases;

/// <summary>
/// Concrete implementation of <see cref="IColleague"/>
/// </summary>
public class IdentityLinkingEngineJobColleague : IColleague
{
    private readonly IMediator _mediator;
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly IIdentityLinkingService _identityLinkingService;
    private readonly ILogger<IdentityLinkingColleague> _logger;
    private readonly ApiOptions _apiOptions;
    private readonly IRecurringJobManager _recurringJobManager;
    private readonly IResourceStateService _resourceStateService;
    private readonly NotifyIdentityLinkingColleague _notifyIdentityLinkingColleague;

    /// <summary>
    /// Creates an instance of <see cref="IdentityLinkingColleague"/>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="backgroundJobClient"></param>
    /// <param name="identityLinkingService"></param>
    /// <param name="logger"></param>
    /// <param name="apiOptions"></param>
    /// <param name="recurringJobManager"></param>
    public IdentityLinkingEngineJobColleague(
        IMediator mediator,
        IBackgroundJobClient backgroundJobClient,
        IIdentityLinkingService identityLinkingService,
        ILogger<IdentityLinkingColleague> logger,
        IOptionsMonitor<ApiOptions> apiOptions,
        IRecurringJobManager recurringJobManager,
        IResourceStateService resourceStateService,
        NotifyIdentityLinkingColleague notifyIdentityLinkingColleague)
    {
        ArgumentNullException.ThrowIfNull(mediator);
        ArgumentNullException.ThrowIfNull(backgroundJobClient);
        ArgumentNullException.ThrowIfNull(identityLinkingService);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(apiOptions);
        ArgumentNullException.ThrowIfNull(recurringJobManager);
        ArgumentNullException.ThrowIfNull(resourceStateService);
        
        _mediator = mediator;
        _backgroundJobClient = backgroundJobClient;
        _identityLinkingService = identityLinkingService;
        _logger = logger;
        _apiOptions = apiOptions.CurrentValue;
        _recurringJobManager = recurringJobManager;
        _resourceStateService = resourceStateService;
        _notifyIdentityLinkingColleague = notifyIdentityLinkingColleague;
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
        ArgumentNullException.ThrowIfNullOrEmpty(serviceKey);
        ArgumentNullException.ThrowIfNull(payloadData);

        var payload = payloadData as IdentityLinkingNotification;
        var samAccountName = payload?.SamAccountName!;

        if (payload?.NotificationType is nameof(StartIdentityLinkingBatchProcessingNotification))
        {
            _logger.LogInformation("Identity Linking Colleague Receives: {Notification}", nameof(StartIdentityLinkingBatchProcessingNotification));
            _recurringJobManager.AddOrUpdate(
                HangfireConstants.StartIdentityLinkingProcessingRecurringJobId,
                () => _notifyIdentityLinkingColleague.Notify(payloadData),
                _apiOptions.BulkProcessingBatchSchedule);
        }

        if (payload?.NotificationType is nameof(StopIdentityLinkingBatchProcessingNotification))
        {
            _logger.LogInformation("Identity Linking Colleague Receives: {Notification}", nameof(StopIdentityLinkingBatchProcessingNotification));
            _recurringJobManager.RemoveIfExists(HangfireConstants.StartIdentityLinkingProcessingRecurringJobId);
        }

        return Task.FromResult<object>(new Response<IdentityLinkingResponse>()
        {
            Message = $"Identity Linking Engine job scheduled on {DateTime.Now.ToLocalTime()}"
        });
    }
}