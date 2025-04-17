using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PingOne;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Data;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Extensions;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.IdentityLinkingService;

public class IdentityLinkingService : IIdentityLinkingService
{
    private readonly ILogger<IdentityLinkingService> _logger;
    private readonly IPingOneService _pingOneService;
    private readonly DbSet<AzureUsersSource> _azureUsersSource;
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly ApiOptions _apiOptions;
    private readonly IHostEnvironment _hostEnvironment;

    /// <summary>
    /// Creates an instance of <see cref="IdentityLinkingService"/>
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="pingOneService"></param>
    /// <param name="applicationDbContext"></param>
    /// <param name="apiOptionsMonitor"></param>
    /// <param name="hostEnvironment"></param>
    public IdentityLinkingService(
        ILogger<IdentityLinkingService> logger,
        IPingOneService pingOneService,
        ApplicationDbContext applicationDbContext,
        IOptionsMonitor<ApiOptions> apiOptionsMonitor,
        IHostEnvironment hostEnvironment
    )
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(pingOneService);
        ArgumentNullException.ThrowIfNull(applicationDbContext);
        ArgumentNullException.ThrowIfNull(apiOptionsMonitor);
        ArgumentNullException.ThrowIfNull(hostEnvironment);

        _logger = logger;
        _pingOneService = pingOneService;
        _applicationDbContext = applicationDbContext;
        _apiOptions = apiOptionsMonitor.Get(ApiOptions.SectionKey);
        _hostEnvironment = hostEnvironment;

        _azureUsersSource = applicationDbContext.Set<AzureUsersSource>(_apiOptions.AzureUsersSourceTableName!);
    }

    /// <inheritdoc/>
    public async Task<IdentityLinkingResponse> LinkIdentityFromEntraId(string samAccountName)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(samAccountName);
        
        var objectId = await GetMicrosoftObjectId(samAccountName);
        var pingOneUserId = await _pingOneService.GetPingOneUserIdFromSamAccountName(samAccountName);
        var pingOneAccountLinkingResponse = await _pingOneService.CreateLinkedAccountForMicrosoftEntra(pingOneUserId, objectId);
        
        return new IdentityLinkingResponse
        {
            PingOneResponse = pingOneAccountLinkingResponse
        };
    }

    /// <inheritdoc/>
    public async Task<IdentityLinkingResponse> LinkIdentityFromLdapGateway(string samAccountName)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(samAccountName);

        var pingOneUserId = await _pingOneService.GetPingOneUserIdFromSamAccountName(samAccountName);
        var pingOneAccountLinkingResponse = await _pingOneService.CreateLinkedAccountForLdapGateway(pingOneUserId, samAccountName);
        
        return new IdentityLinkingResponse
        {
            PingOneResponse = pingOneAccountLinkingResponse
        };    
    }

    /// <inheritdoc/>
    public async Task<IdentityLinkingResponse> LinkIdentityFromPingFederate(string samAccountName)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(samAccountName);
        
        var pingOneUserId = await _pingOneService.GetPingOneUserIdFromSamAccountName(samAccountName);
        var pingOneAccountLinkingResponse = await _pingOneService.CreateLinkedAccountForPingFederate(pingOneUserId, samAccountName);
        
        return new IdentityLinkingResponse
        {
            PingOneResponse = pingOneAccountLinkingResponse
        };
    }

    /// <inheritdoc/>
    public async Task<IdentityLinkingResponse> JustInTimeProcessInBulk()
    {
        // TODO: Make this available via API. 
        var pingOneUsersResponse = await _pingOneService.GetPingOneIdentitiesForProcessing();
        var identitiesForProcessing = pingOneUsersResponse.Embedded?.Users;
        ArgumentNullException.ThrowIfNull(identitiesForProcessing);

        foreach(var identity in identitiesForProcessing)
        {
            if (string.IsNullOrEmpty(identity.Username))
            {
                _logger.LogError("Just In Time Identity Linking - PingOneUserId is null or empty for pingOneUserId: {PingOneUserId}", identity.Id);
                continue;
            }

            var pingFederateIdentityLinkingResponse = await LinkIdentityFromPingFederate(identity.Username);
            if (pingFederateIdentityLinkingResponse is null)
            {
                _logger.LogError("Just In Time Identity Linking - PingOneAccountLinkingResponse is null for samAccountName: {SamAccountName}", identity.Username);
                continue;
            }

            var entraIdentityLinkingResponse = await LinkIdentityFromEntraId(identity.Username);
            if (entraIdentityLinkingResponse is null)
            {
                _logger.LogError("Just In Time Identity Linking - EntraAccountLinkingResponse is null for samAccountName: {SamAccountName}", identity.Username);
                continue;
            }

            var ldapGatewayIdentityLinkingResponse = await LinkIdentityFromLdapGateway(identity.Username);
            if (ldapGatewayIdentityLinkingResponse is null)
            {
                _logger.LogError("Just In Time Identity Linking - LdapGatewayAccountLinkingResponse is null for samAccountName: {SamAccountName}", identity.Username);
                continue;
            }
        }

        _logger.LogInformation("Just In Time Identity Linking - Successfully linked PingFederate, Entra, and LDAP Gateway accounts.");
        
        return new IdentityLinkingResponse
        {
            PingOneResponse = null,
            IsBulkProcessingJob = false,
            IsBulkJustInTimeProcessingJob = true,
            IsBulkProcessingSuccessful = true
        };
    }

    /// <inheritdoc/>
    public async Task<IdentityLinkingResponse> ProcessInBulk()
    {
        // TODO: Make this a recurring job on a schedule.
        var identityProcessingRequestQueueDbSet = _applicationDbContext.Set<IdentityLinkingProcessingReqeustQueue>();
        var identityLinkingProcessingReqeustArchiveDbSet = _applicationDbContext.Set<IdentityLinkingProcessingReqeustArchive>();

        var requestedIdentitiesForProcessing = identityProcessingRequestQueueDbSet
            .Where(x => x.Status != "Completed")
            .AsSplitQuery()
            .OrderBy(r => r.Id)
            .Take(_apiOptions.BulkProcessingBatchSize)
            .ToList();

        foreach (var requestedIdentity in requestedIdentitiesForProcessing)
        {
            var pingOneUserId = requestedIdentity.PingOneUserId;
            var samAccountName = requestedIdentity.SamAccountName;
            var entraObjectId = requestedIdentity.EntraObjectId;

            if (string.IsNullOrEmpty(pingOneUserId))
            {
                _logger.LogErrorToSql(
                    logMessage: $"RequestId: {requestedIdentity.Id} pingOneUserId is null.",
                    args: null,
                    requestId: requestedIdentity.Id.ToString(),
                    pingOneUserId: string.Empty,
                    environment: _hostEnvironment.EnvironmentName,
                    status: "Error",
                    detail: "PingOneUserId is null");

                continue;
            }

            if (string.IsNullOrEmpty(samAccountName))
            {
                _logger.LogErrorToSql(
                    logMessage: $"RequestId: {requestedIdentity.Id} samAccountName is null.",
                    args: null,
                    requestId: requestedIdentity.Id.ToString(),
                    pingOneUserId: pingOneUserId,
                    environment: _hostEnvironment.EnvironmentName,
                    status: "Error",
                    detail: "SamAccountName is null");

                continue;
            }

            if (string.IsNullOrEmpty(entraObjectId))
            {
                _logger.LogErrorToSql(
                    logMessage: $"RequestId: {requestedIdentity.Id} entraObjectId is null.",
                    args: null,
                    requestId: requestedIdentity.Id.ToString(),
                    pingOneUserId: pingOneUserId,
                    environment: _hostEnvironment.EnvironmentName,
                    status: "Error",
                    detail: "EntraObjectId is null");

                continue;
            }

            var pingFederateAccountLinkingResponse = await _pingOneService.CreateLinkedAccountForPingFederate(pingOneUserId, samAccountName);
            if (pingFederateAccountLinkingResponse is null)
            {
                _logger.LogErrorToSql(
                    logMessage: $"RequestId: {requestedIdentity.Id} PingOneAccountLinkingResponse is null for samAccountName: {samAccountName}",
                    args: null,
                    requestId: requestedIdentity.Id.ToString(),
                    pingOneUserId: pingOneUserId,
                    environment: _hostEnvironment.EnvironmentName,
                    status: "Error",
                    detail: "PingOneAccountLinkingResponse is null");

                continue;
            }

            var entraAccountLinkingResponse = await _pingOneService.CreateLinkedAccountForMicrosoftEntra(pingOneUserId, entraObjectId);
            if (entraAccountLinkingResponse is null)
            {
                _logger.LogErrorToSql(
                    logMessage: $"RequestId: {requestedIdentity.Id} entraAccountLinkingResponse is null for samAccountName: {samAccountName}",
                    args: null,
                    requestId: requestedIdentity.Id.ToString(),
                    pingOneUserId: pingOneUserId,
                    environment: _hostEnvironment.EnvironmentName,
                    status: "Error",
                    detail: "EntraAccountLinkingResponse is null");

                continue;
            }

            var ldapGatewayLinkingResponse = await _pingOneService.CreateLinkedAccountForLdapGateway(pingOneUserId, samAccountName);
            if (ldapGatewayLinkingResponse is null)
            {
                _logger.LogErrorToSql(
                    logMessage: $"RequestId: {requestedIdentity.Id} ldapGatewayLinkingResponse is null for samAccountName: {samAccountName}",
                    args: null,
                    requestId: requestedIdentity.Id.ToString(),
                    pingOneUserId: pingOneUserId,
                    environment: _hostEnvironment.EnvironmentName,
                    status: "Error",
                    detail: "LdapGatewayLinkingResponse is null");

                continue;
            }

            _logger.LogErrorToSql(
                logMessage: $"RequestId: {requestedIdentity.Id} Successfully linked PingFederate, Entra, and LDAP Gateway accounts for samAccountName: {samAccountName}",
                args: null,
                requestId: requestedIdentity.Id.ToString(),
                pingOneUserId: pingOneUserId,
                environment: _hostEnvironment.EnvironmentName,
                status: "Success",
                detail: string.Empty);

            requestedIdentity.Status = "Completed";
            requestedIdentity.Attempts = requestedIdentity.Attempts + 1;
            requestedIdentity.LastProcessingAttempt = DateTime.UtcNow;

            var identityLinkingProcessingReqeustArchive = new IdentityLinkingProcessingReqeustArchive
            {
                SamAccountName = requestedIdentity.SamAccountName,
                PingOneUserId = requestedIdentity.PingOneUserId,
                EntraObjectId = requestedIdentity.EntraObjectId,
                Status = requestedIdentity.Status,
                Attempts = requestedIdentity.Attempts,
                LastProcessingAttempt = requestedIdentity.LastProcessingAttempt
            };
            await identityLinkingProcessingReqeustArchiveDbSet.AddAsync(identityLinkingProcessingReqeustArchive);
        }

        await _applicationDbContext.SaveChangesAsync();
        
        return new IdentityLinkingResponse
        {
            PingOneResponse = null,
            IsBulkProcessingJob = true,
            IsBulkJustInTimeProcessingJob = false,
            IsBulkProcessingSuccessful = true
        };
    }

    private async Task<string> GetMicrosoftObjectId(string samAccountName)
    {
        var azureUser = await _azureUsersSource
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.OnPremisesSamAccountName == samAccountName);
        ArgumentNullException.ThrowIfNull(azureUser?.Id);

        return azureUser.Id;
    }
}