using Hangfire;
using Microsoft.Extensions.Logging;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.IdentityLinkingService;
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
public class IdentityLinkingColleague : IColleague
{
    private readonly IMediator _mediator;
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly IIdentityLinkingService _identityLinkingService;
    private readonly ILogger<IdentityLinkingColleague> _logger;

    /// <summary>
    /// Creates an instance of <see cref="IdentityLinkingColleague"/>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="backgroundJobClient"></param>
    /// <param name="identityLinkingService"></param>
    /// <param name="logger"></param>
    public IdentityLinkingColleague(
        IMediator mediator,
        IBackgroundJobClient backgroundJobClient,
        IIdentityLinkingService identityLinkingService,
        ILogger<IdentityLinkingColleague> logger)
    {
        ArgumentNullException.ThrowIfNull(mediator);
        ArgumentNullException.ThrowIfNull(backgroundJobClient);
        ArgumentNullException.ThrowIfNull(identityLinkingService);
        ArgumentNullException.ThrowIfNull(logger);
        
        _mediator = mediator;
        _backgroundJobClient = backgroundJobClient;
        _identityLinkingService = identityLinkingService;
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
        ArgumentNullException.ThrowIfNullOrEmpty(serviceKey);
        ArgumentNullException.ThrowIfNull(payloadData);

        var payload = payloadData as IdentityLinkingNotification;
        var samAccountName = payload?.SamAccountName;
        ArgumentNullException.ThrowIfNull(samAccountName);

        if (payload?.NotificationType is nameof(PingFederateIdentityLinkingNotification))
        {
            _backgroundJobClient.Enqueue(() => _identityLinkingService.LinkIdentityFromPingFederate(samAccountName));
        }

        if (payload?.NotificationType is nameof(MicrosoftIdentityLinkingNotification))
        {
            _backgroundJobClient.Enqueue(() => _identityLinkingService.LinkIdentityFromEntraId(samAccountName));
        }

        if (payload?.NotificationType is nameof(LdapGatewayIdentityLinkingNotification))
        {
            _backgroundJobClient.Enqueue(() => _identityLinkingService.LinkIdentityFromLdapGateway(samAccountName));
        }

        if (payload?.NotificationType is nameof(BatchIdentityLinkingNotification))
        {
             _backgroundJobClient.Enqueue(() => _identityLinkingService.ProcessInBulk());
        }

        _logger.LogDebug("Identty Linking Colleague Receives: {message}", serviceKey);

        return Task.FromResult<object>(new Response<IdentityLinkingResponse>()
        {
            Message = $"Identity link creation for {samAccountName} started on {DateTime.Now.ToLocalTime()}"
        });
    }
}