using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Extensions;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PingFederate;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.ResourceState;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Data;

/// <summary>
/// Service for handling the MSKCC legacy SQL table for pingfederate SAML and OIDC connections combined in one table.
/// </summary>
public class LegacyService : ILegacyService
{
    private readonly ILogger<LegacyService> _logger;
    private readonly IPingFederateService _pingFederateService;
    private readonly ApplicationDbContext _legacyDbContext;
    private readonly IResourceStateService _resourceStateService;
    private readonly PingFederateOptions _pingFederateOptions;

    /// <summary>
    /// Creates an instance of <see cref="LegacyService"/>
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="pingFederateService"></param>
    /// <param name="legacyDbContext"></param>
    /// <param name="resourceStateService"></param>
    /// <param name="pingFederateOptions"></param>
    /// <param name="httpClientFactory"></param>
    /// <param name="pingFederateOptions"></param>
    public LegacyService(ILogger<LegacyService> logger,
        IPingFederateService pingFederateService,
        ApplicationDbContext legacyDbContext,
        IResourceStateService resourceStateService,
        IOptionsMonitor<PingFederateOptions> pingFederateOptions)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(pingFederateService);
        ArgumentNullException.ThrowIfNull(legacyDbContext);
        ArgumentNullException.ThrowIfNull(resourceStateService);
        ArgumentNullException.ThrowIfNull(pingFederateOptions);

        _logger = logger;
        _pingFederateService = pingFederateService;
        _legacyDbContext = legacyDbContext;
        _resourceStateService = resourceStateService;
        _pingFederateOptions = pingFederateOptions.CurrentValue;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<LegacyConnection>> AggregateLegacyConnectionsAsync()
    {
        _logger.LogInformation("Starting aggregation of legacy connections");
        _resourceStateService.IsLegacyAggregationRunning = true;
        _logger.LogDebug("Getting SP connections Ping Federate");
        var spConnectionsList = await _pingFederateService.GetSpConnectionsAsync();
        _logger.LogDebug("Getting OIDC connections directly from Ping Federate API");
        var oidcClientsList = await _pingFederateService.GetOidcConnectionsAsync();
        ArgumentNullException.ThrowIfNull(spConnectionsList);
        ArgumentNullException.ThrowIfNull(oidcClientsList);

         List<LegacyConnection> legacyConnectionsList = [];

        try
        {
            foreach(var connection in spConnectionsList)
            {
                _logger.LogDebug("Processing SP connection {ConnectionName}", connection.Name);
                var legacySpConnection = new LegacyConnection
                {
                    Id = connection.PrimaryKey.ToString(),
                    PingConnectionID = connection.Id,
                    ConnectionName = connection.Name,
                    ApplicationName = connection.ApplicationName,
                    EntityID = connection.EntityId,
                    IsActive = connection.Active,
                    CreationDate = connection.CreationDate,
                    ModificationDate = connection.ModificationDate,
                    TicketNumber = connection.ContactInfoCompany ?? string.Empty,
                    BusinessOwner = connection.ContactInfoFirstName ?? string.Empty,
                    TechnicalOwner = connection.ContactInfoEmail ?? string.Empty,
                    BaseUrl = connection.BaseUrl,
                    ACSEndpoint = connection.AcsUrl,
                    ConditionalIssuanceCriteria =  DetermineIssuanceCriteria(connection.ConditionalIssuanceCriteria!, _pingFederateOptions),
                    ExpressionIssuanceCriteria = connection.ExpressionIssuanceCriteria ?? string.Empty,
                    Environment = _pingFederateOptions.InstanceEnvironment,
                    Instance = string.Empty,
                    APMNumber = connection.ContactInfoNumber ?? string.Empty,
                    Type = "SAML"
                };

                _logger.LogInformation("Adding SP connection {ConnectionName} to legacy connections list", connection.Name);
                legacyConnectionsList.Add(legacySpConnection);
            }

            foreach(var client in oidcClientsList)
            {
                var descriptionParts = client.Description?.Split("|");
                var redirectUrlsList = client.RedirectUris ?? new List<string>();
                _logger.LogDebug("Processing OIDC client {ClientName}", client.Name);
                var legacyOidcClient = new LegacyConnection
                {
                    Id = client.PrimaryKey.ToString(),
                    PingConnectionID = client.OidcPolicyId,
                    ConnectionName = client.Name,
                    ApplicationName = client.Name,
                    EntityID = client.ClientId,
                    IsActive = client.Enabled,
                    CreationDate = client.CreationDate,
                    ModificationDate = client.ModificationDate,
                    TicketNumber = ParseTicketNumber(client.Description!),
                    BusinessOwner = ParseBusinessOwner(client.Description!),
                    TechnicalOwner = ParseTechnicalOwner(client.Description!),
                    ACSEndpoint = string.Join(",", redirectUrlsList) ?? string.Empty,
                    Environment = _pingFederateOptions.InstanceEnvironment,
                    Instance = string.Empty,
                    APMNumber = ParseApmNumber(client.Description!),
                    BaseUrl = string.Empty,
                    ConditionalIssuanceCriteria = DetermineIssuanceCriteria(client.ConditionalIssuanceCriteria!, _pingFederateOptions),
                    ExpressionIssuanceCriteria = client.ExpressionIssuanceCriteria ?? string.Empty,
                    Type = "OIDC"
                };

                _logger.LogInformation("Adding OIDC client {ClientName} to legacy connections list", client.Name);
                legacyConnectionsList.Add(legacyOidcClient);
            }

            var legacyConnectionDbSet = _legacyDbContext.Set<LegacyConnection>();

            foreach(var legacyConnection in legacyConnectionsList)
            {
                var existingConnection = legacyConnectionDbSet
                    .IncludeAllLegacyConnectionTables()
                    .AsTracking()
                    .AsSplitQuery()
                    .SingleOrDefault(c => c.Id == legacyConnection.Id);

                if (existingConnection is not null)
                {
                    _logger.LogDebug("Updating legacy connection {ConnectionName} in database", legacyConnection.ConnectionName);

                    var updatedConnection = existingConnection.MapFrom(legacyConnection);
                    legacyConnectionDbSet.Update(updatedConnection);
                    continue;
                }

                _logger.LogDebug("Adding legacy connection {ConnectionName} to database", legacyConnection.ConnectionName);
                legacyConnectionDbSet.Add(legacyConnection);
            }

            legacyConnectionDbSet
                    .IncludeAllLegacyConnectionTables()
                    .AsTracking()
                    .AsSplitQuery()
                    .ToList()
                    .SyncRemovedConnections(legacyConnectionsList);
            
            _logger.LogDebug("Saving changes to database");
            await _legacyDbContext.SaveChangesAsync();
        }
        finally
        {
            _resourceStateService.IsLegacyAggregationRunning = false;
        }

        _resourceStateService.IsLegacyAggregationRunning = false;
        _logger.LogInformation("Finished aggregation of legacy connections");
        return legacyConnectionsList;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<LegacyConnection>> GetLegacyConnectionsAsync(PaginationFilter paginationFilter)
    {        
        ArgumentNullException.ThrowIfNull(paginationFilter);
        _logger.LogInformation("Getting legacy connections");

        if (_resourceStateService.IsLegacyAggregationRunning || _resourceStateService.IsLegacyPurgeRunning)
        {
            _logger.LogWarning("Legacy aggregation or purge is running. Please wait for it to complete.");
            return Enumerable.Empty<LegacyConnection>();
        }

        var legacyConnectionsDbSet = _legacyDbContext.Set<LegacyConnection>();
        var legacyConnections = await legacyConnectionsDbSet
            .IncludeAllLegacyConnectionTables()
            .AsNoTracking()
            .AsSplitQuery()
            .OrderBy(sp => sp.PrimaryKey)
            .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
            .Take(paginationFilter.PageSize)
            .ToListAsync();

        ArgumentNullException.ThrowIfNull(legacyConnections);

        _logger.LogInformation("Returning legacy connections");
        return legacyConnections;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<LegacyConnection>> SearchLegacyConnectionsAsync(PaginationFilter paginationFilter, SearchCriteria searchCriteria)
    {        
        ArgumentNullException.ThrowIfNull(paginationFilter);
        ArgumentNullException.ThrowIfNull(searchCriteria);
        _logger.LogInformation("Getting legacy connections");

        if (_resourceStateService.IsLegacyAggregationRunning || _resourceStateService.IsLegacyPurgeRunning)
        {
            _logger.LogWarning("Legacy aggregation or purge is running. Please wait for it to complete.");
            return Enumerable.Empty<LegacyConnection>();
        }

        var legacyConnectionsDbSet = _legacyDbContext.Set<LegacyConnection>();
        var samlLegacyConnections = await legacyConnectionsDbSet
            .IncludeAllLegacyConnectionTables()
            .AsNoTracking()
            .AsSplitQuery()
            .Where(c => c.BaseUrl!.Contains(searchCriteria.Criteria) || c.EntityID!.Contains(searchCriteria.Criteria))
            .OrderBy(c => c.PrimaryKey)
            .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
            .Take(paginationFilter.PageSize)
            .ToListAsync();

        ArgumentNullException.ThrowIfNull(samlLegacyConnections);

        var legacyConnections = await legacyConnectionsDbSet
            .IncludeAllLegacyConnectionTables()
            .AsNoTracking()
            .AsSplitQuery()
            .Where(c => c.ACSEndpoint!.Contains(searchCriteria.Criteria))
            .OrderBy(c => c.PrimaryKey)
            .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
            .Take(paginationFilter.PageSize)
            .ToListAsync();

        ArgumentNullException.ThrowIfNull(legacyConnections);

        _logger.LogInformation("Returning legacy connections");
        legacyConnections.AddRange(samlLegacyConnections);
        return legacyConnections;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<LegacyConnection>> GetLegacyConnectionsAsync()
    {
        _logger.LogInformation("Getting legacy connections");

        if (_resourceStateService.IsLegacyAggregationRunning || _resourceStateService.IsLegacyPurgeRunning)
        {
            _logger.LogWarning("Legacy aggregation or purge is running. Please wait for it to complete.");
            return Enumerable.Empty<LegacyConnection>();
        }

        var legacyConnectionsDbSet = _legacyDbContext.Set<LegacyConnection>();
        var legacyConnections = await legacyConnectionsDbSet
            .IncludeAllLegacyConnectionTables()
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync();

        ArgumentNullException.ThrowIfNull(legacyConnections);

        _logger.LogInformation("Returning legacy connections");
        return legacyConnections;
    }

    /// <inheritdoc/>
    public int GetLegacyConnectionsCount() => _legacyDbContext.Set<LegacyConnection>().Count();

    /// <inheritdoc/>
    public int GetLegacyConnectionsCountWithCriteria(SearchCriteria searchCriteria)
    {
        var oidcCount = _legacyDbContext.Set<LegacyConnection>().Where(c => c.ACSEndpoint!.Contains(searchCriteria.Criteria)).Count();
        var samlCount = _legacyDbContext.Set<LegacyConnection>().Where(c => c.BaseUrl!.Contains(searchCriteria.Criteria)).Count();

        return oidcCount + samlCount;
    }

    /// <inheritdoc/>
    public async Task PurgeLegacyConnectionsAsync()
    {
        _logger.LogInformation("Purging legacy connections table");
        _resourceStateService.IsLegacyPurgeRunning = true;

        var legacyConnections = _legacyDbContext.Set<LegacyConnection>();
        foreach(var legacyConnection in legacyConnections)
        {
            legacyConnections.Remove(legacyConnection);
        }
        _logger.LogDebug("Saving changes to database");
        await _legacyDbContext.SaveChangesAsync();

        _resourceStateService.IsLegacyPurgeRunning = false;
        _logger.LogInformation("Legacy connections table purged");
    }

    private static string DetermineIssuanceCriteria(string issuanceCriteria, PingFederateOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        if (string.IsNullOrEmpty(issuanceCriteria))
        {
            return options.DefaultIssuanceCriteria;
        }

        return issuanceCriteria;
    }

    private static string ParseTicketNumber(string description) => parseOidcDescription(description, 0);

    private static string ParseBusinessOwner(string description) => parseOidcDescription(description, 1);

    private static string ParseTechnicalOwner(string description) => parseOidcDescription(description, 2);

    private static string ParseApmNumber(string description)
    {
        ArgumentNullException.ThrowIfNull(description);
        if (string.IsNullOrEmpty(description) || !description.Contains("|"))
        {
            return string.Empty;
        }
        try
        {
            return description.Split("|")[3];
        }
        catch
        {
            return string.Empty;
        }
    }

    private static string parseOidcDescription(string description, int index)
    {
        ArgumentNullException.ThrowIfNull(description);
        if (string.IsNullOrEmpty(description))
        {
            return string.Empty;
        }
        if(!description.Contains("|"))
        {
            return description;
        }
        try
        {
            return description.Split("|")[index];
        }
        catch
        {
            return description;
        }
    }
}