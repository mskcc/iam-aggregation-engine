using Hangfire;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.IdentityLinkingService;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PingFederate;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.ResourceState;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Exceptions;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Data;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.UseCases;

/// <summary>
/// Concrete implementation of <see cref="IColleague"/>
/// </summary>
public class IdentityLinkingColleague : IColleague
{
    private readonly IMediator _mediator;
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly IIdentityLinkingService _identityLinkingService;
    private readonly ILogger<IdentityLinkingColleague> _logger;
    private readonly ApiOptions _apiOptions;
    private readonly IRecurringJobManager _recurringJobManager;
    private readonly IResourceStateService _resourceStateService;

    /// <summary>
    /// Creates an instance of <see cref="IdentityLinkingColleague"/>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="backgroundJobClient"></param>
    /// <param name="identityLinkingService"></param>
    /// <param name="logger"></param>
    /// <param name="apiOptions"></param>
    /// <param name="recurringJobManager"></param>
    public IdentityLinkingColleague(
        IMediator mediator,
        IBackgroundJobClient backgroundJobClient,
        IIdentityLinkingService identityLinkingService,
        ILogger<IdentityLinkingColleague> logger,
        IOptionsMonitor<ApiOptions> apiOptions,
        IRecurringJobManager recurringJobManager,
        IResourceStateService resourceStateService)
    {
        ArgumentNullException.ThrowIfNull(mediator);
        ArgumentNullException.ThrowIfNull(backgroundJobClient);
        ArgumentNullException.ThrowIfNull(identityLinkingService);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(apiOptions);
        ArgumentNullException.ThrowIfNull(recurringJobManager);
        
        _mediator = mediator;
        _backgroundJobClient = backgroundJobClient;
        _identityLinkingService = identityLinkingService;
        _logger = logger;
        _apiOptions = apiOptions.CurrentValue;
        _recurringJobManager = recurringJobManager;
        _resourceStateService = resourceStateService;
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

        if (payload?.NotificationType is nameof(PingFederateIdentityLinkingNotification))
        {
            _logger.LogInformation("Identity Linking Colleague Receives: {Notification}", nameof(PingFederateIdentityLinkingNotification));
            _backgroundJobClient.Enqueue(() => _identityLinkingService.LinkIdentityFromPingFederate(samAccountName));
        }

        if (payload?.NotificationType is nameof(EntraIdentityLinkingNotification))
        {
            _logger.LogInformation("Identity Linking Colleague Receives: {Notification}", nameof(EntraIdentityLinkingNotification));
            _backgroundJobClient.Enqueue(() => _identityLinkingService.LinkIdentityFromEntraId(samAccountName));
        }

        if (payload?.NotificationType is nameof(LdapGatewayIdentityLinkingNotification))
        {
            _logger.LogInformation("Identity Linking Colleague Receives: {Notification}", nameof(LdapGatewayIdentityLinkingNotification));
            _backgroundJobClient.Enqueue(() => _identityLinkingService.LinkIdentityFromLdapGateway(samAccountName));
        }

        if (payload?.NotificationType is nameof(LinkAllTargetsForIdentityNotification))
        {
            _logger.LogInformation("Identity Linking Colleague Receives: {Notification}", nameof(LinkAllTargetsForIdentityNotification));
            _backgroundJobClient.Enqueue(() => _identityLinkingService.LinkIdentityFromPingFederate(samAccountName));
            _backgroundJobClient.Enqueue(() => _identityLinkingService.LinkIdentityFromEntraId(samAccountName));
            _backgroundJobClient.Enqueue(() => _identityLinkingService.LinkIdentityFromLdapGateway(samAccountName));
        }

        if (payload?.NotificationType is nameof(UnlinkAccountIdentityLinkingNotification))
        {
            _logger.LogInformation("Identity Linking Colleague Receives: {Notification}", nameof(UnlinkAccountIdentityLinkingNotification));
            _backgroundJobClient.Enqueue(() => _identityLinkingService.UnlinkAllIdentityProviderAccounts(samAccountName));
        }

        if (payload?.NotificationType is nameof(UnlinkPingFederateIdentityLinkingNotification))
        {
            _logger.LogInformation("Identity Linking Colleague Receives: {Notification}", nameof(UnlinkPingFederateIdentityLinkingNotification));
            _backgroundJobClient.Enqueue(() => _identityLinkingService.UnlinkIdentityFromPingFederate(samAccountName));
        }

        if (payload?.NotificationType is nameof(UnlinkEntraIdentityLinkingNotification))
        {
            _logger.LogInformation("Identity Linking Colleague Receives: {Notification}", nameof(UnlinkEntraIdentityLinkingNotification));
            _backgroundJobClient.Enqueue(() => _identityLinkingService.UnlinkIdentityFromEntraId(samAccountName));
        }

        if (payload?.NotificationType is nameof(UnlinkLdapGatewayIdentityLinkingNotification))
        {
            _logger.LogInformation("Identity Linking Colleague Receives: {Notification}", nameof(UnlinkLdapGatewayIdentityLinkingNotification));
            _backgroundJobClient.Enqueue(() => _identityLinkingService.UnlinkIdentityFromLdapGateway(samAccountName));
        }

        if (payload?.NotificationType is nameof(StartIdentityLinkingBatchProcessingNotification) && 
            _resourceStateService.IsIdentityEngineJobRunning is false)
        {
            _logger.LogInformation("Identity Linking Colleague Receives: {Notification}", nameof(StartIdentityLinkingBatchProcessingNotification));
            _backgroundJobClient.Enqueue(() => _identityLinkingService.ProcessIdentityLinkingRequestQueue());
        }

        _logger.LogDebug("Identty Linking Colleague Receives: {message}", serviceKey);

        return Task.FromResult<object>(new Response<IdentityLinkingResponse>()
        {
            Message = $"Identity Linking Engine job scheduled on {DateTime.Now.ToLocalTime()}"
        });
    }
}