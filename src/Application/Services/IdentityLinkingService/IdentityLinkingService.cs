using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PingOne;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.ResourceState;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Data;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Extensions;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.ExternalData;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.IdentityLinkingService;

public class IdentityLinkingService : IIdentityLinkingService
{
    private readonly ILogger<IdentityLinkingService> _logger;
    private readonly IPingOneService _pingOneService;
    private readonly DbSet<AzureUsersSource> _azureUsersSource;
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly ApiOptions _apiOptions;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IResourceStateService _resourceStateService;
    private readonly ExternalDbContext _externalDbContext;

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
        IHostEnvironment hostEnvironment,
        IResourceStateService resourceStateService,
        ExternalDbContext externalDbContext
    )
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(pingOneService);
        ArgumentNullException.ThrowIfNull(applicationDbContext);
        ArgumentNullException.ThrowIfNull(apiOptionsMonitor);
        ArgumentNullException.ThrowIfNull(hostEnvironment);
        ArgumentNullException.ThrowIfNull(resourceStateService);
        ArgumentNullException.ThrowIfNull(externalDbContext);

        _logger = logger;
        _pingOneService = pingOneService;
        _applicationDbContext = applicationDbContext;
        _apiOptions = apiOptionsMonitor.Get(ApiOptions.SectionKey);
        _hostEnvironment = hostEnvironment;
        _resourceStateService = resourceStateService;
        _externalDbContext = externalDbContext;

        //_azureUsersSource = applicationDbContext.Set<AzureUsersSource>();
        _azureUsersSource = externalDbContext.AzureUsersSource;
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
    public async Task<IdentityLinkingResponse> UnlinkAllIdentityProviderAccounts(string samAccountName)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(samAccountName);

        var pingOneUserId = await _pingOneService.GetPingOneUserIdFromSamAccountName(samAccountName);
        var pingOneLinkedAccountsResponse = await _pingOneService.GetLinkedIdentityProviderAccounts(pingOneUserId);
        var linkedAccounts = pingOneLinkedAccountsResponse.Embedded?.LinkedAccounts?.ToList();

        if (linkedAccounts?.Count is 0)
        {
            _logger.LogWarningToSql(
                logMessage: $"Unlinking Warning - No linked accounts for samAccountName: {samAccountName}",
                args: null,
                requestId: string.Empty,
                pingOneUserId: string.Empty,
                environment: _hostEnvironment.EnvironmentName,
                status: "Warning",
                detail: string.Empty);
        }

        foreach(var linkedAccount in linkedAccounts!)
        {
            if (string.IsNullOrEmpty(linkedAccount.Id))
            {
                continue;
            }

            var pingOneAccountUnlinkingSuccess = await _pingOneService.UnlinkIdentityProivder(pingOneUserId, linkedAccount.Id);

            if (pingOneAccountUnlinkingSuccess is false)
            {
                _logger.LogErrorToSql(
                    logMessage: $"Unlinking Error - PingOneAccountUnlinkingResponse is null for samAccountName: {samAccountName}",
                    args: null,
                    requestId: string.Empty,
                    pingOneUserId: string.Empty,
                    environment: _hostEnvironment.EnvironmentName,
                    status: "Error",
                    detail: "PingOneAccountUnlinkingResponse is null");
            }
        }

        var unlinkGatewayResponse = await _pingOneService.UnlinkLdapGatewayIdentity(pingOneUserId, samAccountName);
        if (unlinkGatewayResponse is null)
        {
            _logger.LogErrorToSql(
                logMessage: $"Unlinking Error - unlinkGatewayResponse is null for samAccountName: {samAccountName}",
                args: null,
                requestId: string.Empty,
                pingOneUserId: string.Empty,
                environment: _hostEnvironment.EnvironmentName,
                status: "Error",
                detail: "UnlinkGatewayResponse is null");
        }

        _logger.LogInformationToSql(
            logMessage: $"Unlinking - Successfully unlinking Identity Provider accounts for samAccountName: {samAccountName}",
            args: null,
            requestId: string.Empty,
            pingOneUserId: string.Empty,
            environment: _hostEnvironment.EnvironmentName,
            status: "Success",
            detail: string.Empty);

        return new IdentityLinkingResponse
        {
            IsUnlinkningSuccessful = true
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
    public async Task<IdentityLinkingResponse> ProcessIdentityLinkingRequestQueue()
    {
        return await ProcessInBulk(_apiOptions.MaxNumberOfIdentityLinkingRetries);
    }

    /// <inheritdoc/>
    public async Task<IdentityLinkingResponse> RetryProcessIdentityLinkingRequestQueue(int maxNumberOfAttempts)
    {
        return await ProcessInBulk(maxNumberOfAttempts);
    }

    /// <inheritdoc/>
    public async Task<IdentityLinkingResponse> ProcessInBulk(int maxNumberOfAttempts = 5)
    {
        _resourceStateService.IsIdentityEngineJobRunning = true;
        
        try
        {
            var identityProcessingRequestQueueDbSet = _applicationDbContext.Set<IdentityLinkingProcessingReqeustQueue>();
            var identityLinkingProcessingReqeustArchiveDbSet = _applicationDbContext.Set<IdentityLinkingProcessingReqeustArchive>();

            var requestedIdentitiesForProcessing = identityProcessingRequestQueueDbSet
                .Where(x => x.Status != "Completed" && x.Attempts < maxNumberOfAttempts)
                .AsSplitQuery()
                .OrderBy(r => r.Id)
                .Take(_apiOptions.BulkProcessingBatchSize)
                .ToList();

            foreach (var requestedIdentity in requestedIdentitiesForProcessing)
            {
                var pingOneUserId = requestedIdentity.PingOneUserId;
                var samAccountName = requestedIdentity.SamAccountName;
                var entraObjectId = requestedIdentity.EntraObjectId;

                var identityLinkingProcessingReqeustArchive = new IdentityLinkingProcessingReqeustArchive
                {
                    SamAccountName = requestedIdentity.SamAccountName,
                    PingOneUserId = requestedIdentity.PingOneUserId,
                    EntraObjectId = requestedIdentity.EntraObjectId,
                    Status = requestedIdentity.Status = "Failed",
                    IsProcessedSuccessfully = requestedIdentity.IsProcessedSuccessfully = false,
                    Attempts = ++requestedIdentity.Attempts,
                    LastProcessingAttempt = requestedIdentity.LastProcessingAttempt = DateTime.UtcNow,
                    CreatedAt = requestedIdentity.CreatedAt,
                    EmployeeId = requestedIdentity.EmployeeId,
                    Environment = _hostEnvironment.EnvironmentName,
                    Source = requestedIdentity.Source,
                    Type = requestedIdentity.Type,
                    RequestId = requestedIdentity.Id.ToString(),
                };

                if (requestedIdentity.Attempts >= maxNumberOfAttempts)
                {
                    await identityLinkingProcessingReqeustArchiveDbSet.AddAsync(identityLinkingProcessingReqeustArchive);
                    identityProcessingRequestQueueDbSet.Remove(requestedIdentity);
                    await _applicationDbContext.SaveChangesAsync();

                    continue;
                }

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

                var ldapGatewayLinkingResponse = await _pingOneService.CreateLinkedAccountForLdapGateway(pingOneUserId, samAccountName);
                if (ldapGatewayLinkingResponse is null)
                {
                    _logger.LogErrorToSql(
                        logMessage: $"Gateway: {requestedIdentity.Id} ldapGatewayLinkingResponse is null for samAccountName: {samAccountName}",
                        args: null,
                        requestId: requestedIdentity.Id.ToString(),
                        pingOneUserId: pingOneUserId,
                        environment: _hostEnvironment.EnvironmentName,
                        status: "Error",
                        detail: "LdapGatewayLinkingResponse is null");

                    if (ldapGatewayLinkingResponse?.ContainsError is false)
                    {
                        continue;
                    }

                    _logger.LogErrorToSql(
                        logMessage: $"Gateway: request {requestedIdentity.Id} ldapGatewayLinkingResponse is null for samAccountName: {samAccountName}",
                        args: null,
                        requestId: requestedIdentity.Id.ToString(),
                        pingOneUserId: pingOneUserId,
                        environment: _hostEnvironment.EnvironmentName,
                        status: "Error",
                        detail: $"Status Code {ldapGatewayLinkingResponse?.ErrorStatusCode}");

                    continue;
                }

                var pingFederateAccountLinkingResponse = await _pingOneService.CreateLinkedAccountForPingFederate(pingOneUserId, samAccountName);
                if (pingFederateAccountLinkingResponse is null)
                {
                    _logger.LogErrorToSql(
                        logMessage: $"PingFederate: request {requestedIdentity.Id} PingOneAccountLinkingResponse is null for samAccountName: {samAccountName}",
                        args: null,
                        requestId: requestedIdentity.Id.ToString(),
                        pingOneUserId: pingOneUserId,
                        environment: _hostEnvironment.EnvironmentName,
                        status: "Error",
                        detail: "PingOneAccountLinkingResponse is null");
                }
                if (pingFederateAccountLinkingResponse?.ContainsError is true)
                {
                    _logger.LogErrorToSql(
                        logMessage: $"PingFederate: request {requestedIdentity.Id} PingOneAccountLinkingResponse is null for samAccountName: {samAccountName}",
                        args: null,
                        requestId: requestedIdentity.Id.ToString(),
                        pingOneUserId: pingOneUserId,
                        environment: _hostEnvironment.EnvironmentName,
                        status: "Error",
                        detail: $"Status Code {pingFederateAccountLinkingResponse.ErrorStatusCode}");
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
                        detail: "Entra");
                }
                if (entraAccountLinkingResponse?.ContainsError is true)
                {
                    _logger.LogErrorToSql(
                        logMessage: $"Entra: request {requestedIdentity.Id} entraAccountLinkingResponse is null for samAccountName: {samAccountName}",
                        args: null,
                        requestId: requestedIdentity.Id.ToString(),
                        pingOneUserId: pingOneUserId,
                        environment: _hostEnvironment.EnvironmentName,
                        status: "Error",
                        detail: $"Status Code {entraAccountLinkingResponse.ErrorStatusCode}");
                }

                _logger.LogInformationToSql(
                    logMessage: $"RequestId: {requestedIdentity.Id} Successfully linked PingFederate, Entra, and LDAP Gateway accounts for samAccountName: {samAccountName}",
                    args: null,
                    requestId: requestedIdentity.Id.ToString(),
                    pingOneUserId: pingOneUserId,
                    environment: _hostEnvironment.EnvironmentName,
                    status: "Success",
                    detail: string.Empty);

                identityLinkingProcessingReqeustArchive.Status = "Completed";
                identityLinkingProcessingReqeustArchive.IsProcessedSuccessfully = true;

                await identityLinkingProcessingReqeustArchiveDbSet.AddAsync(identityLinkingProcessingReqeustArchive);
                identityProcessingRequestQueueDbSet.Remove(requestedIdentity);

                _logger.LogInformationToSql(
                    logMessage: $"Bulk processing - Successfully processed {requestedIdentity.SamAccountName}.",
                    args: null,
                    requestId: requestedIdentity.Id.ToString(),
                    pingOneUserId: requestedIdentity.PingOneUserId,
                    environment: _hostEnvironment.EnvironmentName,
                    status: "Success",
                    detail: string.Empty);

            }

            await _applicationDbContext.SaveChangesAsync();

            _logger.LogInformationToSql(
                logMessage: $"Bulk processing - Successfully processed {requestedIdentitiesForProcessing.Count} identities.",
                args: null,
                requestId: string.Empty,
                pingOneUserId: string.Empty,
                environment: _hostEnvironment.EnvironmentName,
                status: "Success",
                detail: string.Empty);
        }
        finally
        {
            _resourceStateService.IsIdentityEngineJobRunning = false;
        }

        return new IdentityLinkingResponse
        {
            PingOneResponse = null,
            IsBulkProcessingJob = true,
            IsBulkJustInTimeProcessingJob = false,
            IsBulkProcessingSuccessful = true
        };
    }

    /// <inheritdoc/>
    public async Task<IdentityLinkingResponse> UnlinkIdentityFromPingFederate(string samAccountName)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(samAccountName);

        var pingOneUserId = await _pingOneService.GetPingOneUserIdFromSamAccountName(samAccountName);
        var pingOneLinkedAccountsResponse = await _pingOneService.GetLinkedIdentityProviderAccounts(pingOneUserId);
        var linkedAccount = pingOneLinkedAccountsResponse.Embedded?.LinkedAccounts?.Where(la => la.ExternalId == samAccountName).SingleOrDefault();
        ArgumentNullException.ThrowIfNull(linkedAccount?.Id);
        
        var pingOneAccountUnlinkingSuccess = await _pingOneService.UnlinkIdentityProivder(pingOneUserId, linkedAccount.Id);

        if (pingOneAccountUnlinkingSuccess is false)
        {
            _logger.LogErrorToSql(
                logMessage: $"Unlinking Ping Federate Error - PingOneAccountUnlinkingResponse is null for samAccountName: {samAccountName}",
                args: null,
                requestId: string.Empty,
                pingOneUserId: string.Empty,
                environment: _hostEnvironment.EnvironmentName,
                status: "Error",
                detail: "PingOneAccountUnlinkingResponse is null");

            return new IdentityLinkingResponse
            {
                IsUnlinkningSuccessful = false
            };
        }

        _logger.LogInformationToSql(
            logMessage: $"Unlinking Ping Federate - Successfully unlinking Identity Provider accounts for samAccountName: {samAccountName}",
            args: null,
            requestId: string.Empty,
            pingOneUserId: string.Empty,
            environment: _hostEnvironment.EnvironmentName,
            status: "Success",
            detail: string.Empty);

        return new IdentityLinkingResponse
        {
            IsUnlinkningSuccessful = true
        };
    }

    /// <inheritdoc/>
    public async Task<IdentityLinkingResponse> UnlinkIdentityFromEntraId(string samAccountName)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(samAccountName);

        var pingOneUserId = await _pingOneService.GetPingOneUserIdFromSamAccountName(samAccountName);
        var pingOneLinkedAccountsResponse = await _pingOneService.GetLinkedIdentityProviderAccounts(pingOneUserId);
        var linkedAccount = pingOneLinkedAccountsResponse.Embedded?.LinkedAccounts?.Where(la => la.ExternalId != samAccountName).SingleOrDefault();
        ArgumentNullException.ThrowIfNull(linkedAccount?.Id);
        
        var pingOneAccountUnlinkingSuccess = await _pingOneService.UnlinkIdentityProivder(pingOneUserId, linkedAccount.Id);

        if (pingOneAccountUnlinkingSuccess is false)
        {
            _logger.LogErrorToSql(
                logMessage: $"Unlinking Ping Federate Error - PingOneAccountUnlinkingResponse is null for samAccountName: {samAccountName}",
                args: null,
                requestId: string.Empty,
                pingOneUserId: string.Empty,
                environment: _hostEnvironment.EnvironmentName,
                status: "Error",
                detail: "PingOneAccountUnlinkingResponse is null");

            return new IdentityLinkingResponse
            {
                IsUnlinkningSuccessful = false
            };
        }

        _logger.LogInformationToSql(
            logMessage: $"Unlinking Ping Federate - Successfully unlinking Identity Provider accounts for samAccountName: {samAccountName}",
            args: null,
            requestId: string.Empty,
            pingOneUserId: string.Empty,
            environment: _hostEnvironment.EnvironmentName,
            status: "Success",
            detail: string.Empty);

        return new IdentityLinkingResponse
        {
            IsUnlinkningSuccessful = true
        };
    }

    /// <inheritdoc/>
    public async Task<IdentityLinkingResponse> UnlinkIdentityFromLdapGateway(string samAccountName)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(samAccountName);

        var pingOneUserId = await _pingOneService.GetPingOneUserIdFromSamAccountName(samAccountName);
        ArgumentNullException.ThrowIfNull(pingOneUserId);

        var unlinkGatewayResponse = await _pingOneService.UnlinkLdapGatewayIdentity(pingOneUserId, samAccountName);
        if (unlinkGatewayResponse is null)
        {
            _logger.LogErrorToSql(
                logMessage: $"Unlinking LDAP Gateway Error - unlinkGatewayResponse is null for samAccountName: {samAccountName}",
                args: null,
                requestId: string.Empty,
                pingOneUserId: string.Empty,
                environment: _hostEnvironment.EnvironmentName,
                status: "Error",
                detail: "UnlinkGatewayResponse is null");
            
            return new IdentityLinkingResponse
            {
                IsUnlinkningSuccessful = false
            };
        }

        _logger.LogInformationToSql(
            logMessage: $"Unlinking LDAP Gateway - Successfully unlinking Identity Provider accounts for samAccountName: {samAccountName}",
            args: null,
            requestId: string.Empty,
            pingOneUserId: string.Empty,
            environment: _hostEnvironment.EnvironmentName,
            status: "Success",
            detail: string.Empty);
        
        return new IdentityLinkingResponse
        {
            IsUnlinkningSuccessful = true
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