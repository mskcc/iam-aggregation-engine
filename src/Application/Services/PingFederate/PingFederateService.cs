using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Data;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.ResourceState;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Extensions;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;
using Microsoft.Extensions.Options;


namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PingFederate;

/// <inheritdoc/>
public class PingFederateService : IPingFederateService
{
    private readonly ILogger<PingFederateService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ApplicationDbContext _pingFederateDbContext;
    private readonly IResourceStateService _resourceStateService;
    private readonly PingFederateOptions _pingFederateOptions;

    /// <summary>
    /// Creates an instance of <see cref="PingFederateService"/>
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="httpClientFactory"></param>
    /// <param name="pingFederateDbContext"></param>
    public PingFederateService(
        ILogger<PingFederateService> logger, 
        IHttpClientFactory httpClientFactory,
        ApplicationDbContext pingFederateDbContext,
        IResourceStateService resourceStateService,
        IOptionsMonitor<PingFederateOptions> pingFederateOptions)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(httpClientFactory);
        ArgumentNullException.ThrowIfNull(pingFederateDbContext);
        ArgumentNullException.ThrowIfNull(resourceStateService);
        ArgumentNullException.ThrowIfNull(pingFederateOptions);
        
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _pingFederateDbContext = pingFederateDbContext;
        _resourceStateService = resourceStateService;
        _pingFederateOptions = pingFederateOptions.CurrentValue;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<SpConnection>> GetSpConnectionsAsync(PaginationFilter paginationFilter)
    {
        ArgumentNullException.ThrowIfNull(paginationFilter);
        _logger.LogInformation("Getting SP connections");

        if (_resourceStateService.IsSamlAggregationRunning || _resourceStateService.IsSamlPurgeRunning)
        {
            _logger.LogWarning("SAML aggregation or purge is running. Please wait for it to complete.");
            return Enumerable.Empty<SpConnection>();
        }

        var pingFederateSpConnections = _pingFederateDbContext.Set<SpConnection>();
        var spConnections = await pingFederateSpConnections
            .IncludeAllSpConnectionTables()
            .AsNoTracking()
            .AsSplitQuery()
            .OrderBy(sp => sp.Id)
            .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
            .Take(paginationFilter.PageSize)
            .ToListAsync();

        ArgumentNullException.ThrowIfNull(spConnections);

        _logger.LogInformation("Returning SP connections");
        return spConnections;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<SpConnection>> GetSpConnectionsAsync()
    {
        _logger.LogInformation("Getting SP connections");

        if (_resourceStateService.IsSamlAggregationRunning || _resourceStateService.IsSamlPurgeRunning)
        {
            _logger.LogWarning("SAML aggregation or purge is running. Please wait for it to complete.");
            return Enumerable.Empty<SpConnection>();
        }

        var pingFederateSpConnections = _pingFederateDbContext.Set<SpConnection>();
        var spConnections = await pingFederateSpConnections
            .IncludeAllSpConnectionTables()
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync();

        ArgumentNullException.ThrowIfNull(spConnections);

        _logger.LogInformation("Returning SP connections");
        return spConnections;
    }

    /// <inheritdoc/>
    public int GetSpConnectionsCount() => _pingFederateDbContext.Set<SpConnection>().Count();
    
    /// <inheritdoc/>
    public async Task<IEnumerable<SpConnection>> AggregateSpConnectionsAsync()
    {
        _logger.LogInformation("Starting aggregation of SP connections");
        _resourceStateService.IsSamlAggregationRunning = true;
        _logger.LogDebug("Creating HTTP client");
        var pingFederateClient = _httpClientFactory.CreateClient(HttpClientNames.PingFederateClient);
        var spConnectionsJson = await pingFederateClient
            .GetFromJsonAsync<SpConnectionItemsJson>(_pingFederateOptions.SpConnectionsEndpoint);
        ArgumentNullException.ThrowIfNull(spConnectionsJson);

        DbSet<SpConnection>? spConnectionsSet;

        try
        {
            spConnectionsSet = _pingFederateDbContext.Set<SpConnection>();
            var certificateSet = _pingFederateDbContext.Set<SpConnectionCertificate>();
            var attributeContractFulfillmentSet = _pingFederateDbContext.Set<AttributeContractFulfillment>();

            var spConnectionsList = spConnectionsJson.PingFederateConnections?.ToList();
            ArgumentNullException.ThrowIfNull(spConnectionsList);

            foreach (var spConnectionItem in spConnectionsList)
            {
                var existingConnection = spConnectionsSet
                    .IncludeAllSpConnectionTables()
                    .AsNoTracking()
                    .AsSplitQuery()
                    .SingleOrDefault(sp => sp.Id == spConnectionItem.Id);

                if (existingConnection is not null)
                {
                    _logger.LogDebug("Aggregating connection details: {SpConnectionId} - {SpConnection}", spConnectionItem.Id, spConnectionItem.Name);
                    
                    existingConnection.UpdateFrom(spConnectionItem, attributeContractFulfillmentSet);
                    spConnectionsSet.Update(existingConnection);

                    continue;
                }
                
                _logger.LogDebug("Adding {SpConnectionId} - {SpConnection}", spConnectionItem.Id, spConnectionItem.Name);
                SpConnection newSpConnection = new();
                newSpConnection.UpdateFrom(spConnectionItem, attributeContractFulfillmentSet);
                await spConnectionsSet.AddAsync(newSpConnection);

                if (spConnectionItem.Credentials?.Certs is null)
                {
                    continue;
                }

                foreach (var certificate in spConnectionItem.Credentials?.Certs!)
                {
                    SpConnectionCertificate newCertificate = new()
                    {
                        Id = certificate.CertView?.Id,
                        EntityId = spConnectionItem.EntityId,
                        SerialNumber = certificate.CertView?.SerialNumber ?? string.Empty,
                        SubjectDN = certificate.CertView?.SubjectDN ?? string.Empty,
                        IssuerDN = certificate.CertView?.IssuerDN ?? string.Empty,
                        ValidFrom = certificate.CertView?.ValidFrom ?? DateTime.MinValue,
                        Expires = certificate.CertView?.Expires ?? DateTime.MinValue,
                        KeyAlgorithm = certificate.CertView?.KeyAlgorithm ?? string.Empty,
                        KeySize = certificate.CertView?.KeySize ?? 0,
                        SignatureAlgorithm = certificate.CertView?.SignatureAlgorithm ?? string.Empty,
                        ActiveVerificationCert = certificate.ActiveVerificationCert ?? false,
                        EncryptionCert = certificate.EncryptionCert ?? false,
                        FileData = certificate.X509File?.FileData ?? string.Empty,
                        PrimaryVerificationCert = certificate.PrimaryVerificationCert ?? false,
                        SecondaryVerificationCert = certificate.SecondaryVerificationCert ?? false,
                        SubjectAlternativeNames = certificate.CertView?.SubjectAlternativeNames?.ToList() ?? new List<string>(),
                        Version = certificate.CertView?.Version ?? 0,
                        X509FileId = certificate.X509File?.Id ?? string.Empty
                    };

                    await certificateSet.AddAsync(newCertificate);
                }
            }

            spConnectionsSet
                .IncludeAllSpConnectionTables()
                .AsNoTracking()
                .AsSplitQuery()
                .ToList()
                .SyncRemovedConnections(spConnectionsList);

            _logger.LogDebug("Saving changes to SP connections table");
            await _pingFederateDbContext.SaveChangesAsync();
        }
        finally
        {
            _resourceStateService.IsSamlAggregationRunning = false;
        }
        
        _resourceStateService.IsSamlAggregationRunning = false;
        _logger.LogInformation("Returning SP connections");
        return spConnectionsSet.ToList().AsReadOnly();
    }
    
    /// <inheritdoc/>
    public async Task PurgeSpConnectionsAsync()
    {
        _logger.LogInformation("Purging SP connections table");
        _resourceStateService.IsSamlPurgeRunning = true;

        var spConnectionSet = _pingFederateDbContext.Set<SpConnection>();
        foreach(var spConnection in spConnectionSet)
        {
            spConnectionSet.Remove(spConnection);
        }

        var certificateSet = _pingFederateDbContext.Set<SpConnectionCertificate>();
        foreach(var certificate in certificateSet)
        {
            certificateSet.Remove(certificate);
        }

        _logger.LogDebug("Saving changes to database");
        await _pingFederateDbContext.SaveChangesAsync();

        _resourceStateService.IsSamlPurgeRunning = false;
        _logger.LogInformation("SP connections table purged");
    }
    
    /// <inheritdoc/>
    public async Task<IEnumerable<OidcClient>> GetOidcConnectionsAsync(PaginationFilter paginationFilter)
    {
        _logger.LogInformation("Getting OIDC connections");

        if (_resourceStateService.IsOidcAggregationRunning || _resourceStateService.IsOidcPurgeRunning)
        {
            _logger.LogWarning("OIDC aggregation or purge is running. Please wait for it to complete.");
            return Enumerable.Empty<OidcClient>();
        }

        var pingFederateOidcConnections = _pingFederateDbContext.Set<OidcClient>();
        var oidcConnections = await pingFederateOidcConnections
            .IncludeAllOidcClientTables()
            .AsNoTracking()
            .AsSplitQuery()
            .OrderBy(oc => oc.ClientId)
            .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
            .Take(paginationFilter.PageSize)
            .ToListAsync();

        ArgumentNullException.ThrowIfNull(oidcConnections);

        _logger.LogInformation("Returning OIDC connections");
        return oidcConnections.AsReadOnly();
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<OidcClient>> GetOidcConnectionsAsync()
    {
        _logger.LogInformation("Getting OIDC connections");

        if (_resourceStateService.IsOidcAggregationRunning || _resourceStateService.IsOidcPurgeRunning)
        {
            _logger.LogWarning("OIDC aggregation or purge is running. Please wait for it to complete.");
            return Enumerable.Empty<OidcClient>();
        }

        var pingFederateOidcConnections = _pingFederateDbContext.Set<OidcClient>();
        var oidcConnections = await pingFederateOidcConnections
            .IncludeAllOidcClientTables()
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync();

        ArgumentNullException.ThrowIfNull(oidcConnections);

        _logger.LogInformation("Returning OIDC connections");
        return oidcConnections.AsReadOnly();
    }

    /// <inheritdoc/>
    public int GetOidcConnectionsCount() => _pingFederateDbContext.Set<OidcClient>().Count();
    
    /// <inheritdoc/>
    public async Task<IEnumerable<OidcClient>> AggregateOidcConnectionsAsync()
    {
        _logger.LogInformation("Starting aggregation of OIDC connections");
        _resourceStateService.IsOidcAggregationRunning = true;
        _logger.LogDebug("Creating HTTP client");
        
        var pingFederateOidcClient = _httpClientFactory.CreateClient(HttpClientNames.PingFederateClient);
        var oidcClientsJson = await pingFederateOidcClient
            .GetFromJsonAsync<OidcClientItemsJson>(_pingFederateOptions.OauthClientsEndpoint);
        ArgumentNullException.ThrowIfNull(oidcClientsJson);
        var oidcAttributeContractFulfillmentJson = await pingFederateOidcClient
            .GetFromJsonAsync<OidcAttributeContractFulfillmentItemsJson>(_pingFederateOptions.OidcPoliciesEndpoint);
        ArgumentNullException.ThrowIfNull(oidcAttributeContractFulfillmentJson);

        DbSet<OidcClient>? oidcClientSet;
        
        try
        {
            oidcClientSet = _pingFederateDbContext.Set<OidcClient>();
            var attributeContractFulfillmentSet = _pingFederateDbContext.Set<AttributeContractFulfillment>();

            var oidcClients = oidcClientsJson.PingFederateConnections?.ToList();
            ArgumentNullException.ThrowIfNull(oidcClients);

            foreach (var oidcClient in oidcClients)
            {
                var existingConnection = oidcClientSet
                    .IncludeAllOidcClientTables()
                    .AsNoTracking()
                    .AsSplitQuery()
                    .SingleOrDefault(oc => oc.ClientId == oidcClient.ClientId);

                if (existingConnection is not null)
                {
                    _logger.LogDebug("Aggregating connection details: {ClientId} - {ClientName}", oidcClient.ClientId, oidcClient.Name);

                    existingConnection.UpdateFrom(oidcClient);
                    existingConnection.UpdateFromPolicies(oidcAttributeContractFulfillmentJson, attributeContractFulfillmentSet);
                    oidcClientSet.Update(existingConnection);

                    continue;
                }

                _logger.LogDebug("Adding new connection: {ClientId} - {ClientName}", oidcClient.ClientId, oidcClient.Name);
                OidcClient newOidcClient = new();
                newOidcClient.UpdateFrom(oidcClient);
                newOidcClient.UpdateFromPolicies(oidcAttributeContractFulfillmentJson, attributeContractFulfillmentSet);
                await oidcClientSet.AddAsync(newOidcClient);
            }

            oidcClientSet
                .IncludeAllOidcClientTables()
                .AsNoTracking()
                .AsSplitQuery()
                .ToList()
                .SyncRemovedConnections(oidcClients);

            _logger.LogDebug("Saving changes to OIDC connections table");
            await _pingFederateDbContext.SaveChangesAsync();
        }
        finally
        {
            _resourceStateService.IsOidcAggregationRunning = false;
        }

        _resourceStateService.IsOidcAggregationRunning = false;
        _logger.LogInformation("Returning OIDC connections");
        return oidcClientSet.ToList().AsReadOnly();
    }

    /// <inheritdoc/>
    public async Task PurgeOidcConnectionsAsync()
    {
        _logger.LogInformation("Purging OIDC connections table");
        _resourceStateService.IsOidcPurgeRunning = true;
       
        var oidcClientSet = _pingFederateDbContext.Set<OidcClient>();
        foreach(var oidcClient in oidcClientSet)
        {
            oidcClientSet.Remove(oidcClient);
        }
        
        await _pingFederateDbContext.SaveChangesAsync();
        _resourceStateService.IsOidcPurgeRunning = false;
        _logger.LogInformation("OIDC connections table purged");
    }
}