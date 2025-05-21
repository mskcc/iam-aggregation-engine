using Microsoft.Extensions.Logging;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PingFederate;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;

public class PingFederateInsightService : IPingFederateInsightService
{
    private readonly ILogger<PingFederateInsightService> _logger;
    private readonly IPingFederateService _pingFederateService;
    private readonly ILegacyService _legacyService;

    /// <summary>
    /// Initializes a new instance of the <see cref="PingFederateInsightService"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="legacyService">The legacy service.</param>
    public PingFederateInsightService
    (
        ILogger<PingFederateInsightService> logger,
        IPingFederateService pingFederateService,
        ILegacyService legacyService
    )
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(pingFederateService);
        ArgumentNullException.ThrowIfNull(legacyService);

        _logger = logger;
        _pingFederateService = pingFederateService;
        _legacyService = legacyService;
    }

    /// <inheritdoc/>
    public Task<InsightResponse> GetApmNumberInsightsAsync(string applicationName)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<InsightResponse> GetApplicationNameInsightsAsync(string apmNumber)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public async Task<InsightResponse> GetConfigurationsWithMissingApmInsightsAsync()
    {
        var oidcConnections = await _pingFederateService.GetOidcConnectionsAsync();
        var oidcConnectionsWithMissingApmNumber = oidcConnections
            .Where(oidcConnection => string.IsNullOrEmpty(oidcConnection.Description) is false || oidcConnection.Description?.ToLower().Contains("apm") is false)
            .ToList();
        
        var spConnections = await _pingFederateService.GetSpConnectionsAsync();
        var spConnectionsWithMissingApmNumber = spConnections
            .Where(spConnection => string.IsNullOrEmpty(spConnection.ContactInfoNumber) is false || spConnection.ContactInfoNumber?.ToLower().Contains("apm") is false)
            .ToList();
        
        ArgumentNullException.ThrowIfNull(spConnectionsWithMissingApmNumber);
        
        return new InsightResponse
        {
            SpConnectionsWithMissingApmNumbers = spConnectionsWithMissingApmNumber,
            OidcConnectionsWithMissingApmNumbers = oidcConnectionsWithMissingApmNumber
        };
    }

    /// <inheritdoc/>
    public async Task<InsightResponse> GetConfigurationsWithMissingTechnicalOwnersInsightsAsync()
    {
        var oidcConnections = await _pingFederateService.GetOidcConnectionsAsync();
        var oidcConnectionsWithDescriptions = oidcConnections
            .Where(oidcConnection => string.IsNullOrEmpty(oidcConnection.Description) is false)
            .ToList();

        var oidcConnectionsWithMissingTechnicalOwners = new List<OidcClient>();
        foreach (var oidcConnection in oidcConnectionsWithDescriptions)
        {
            var description = oidcConnection.Description;

            if (string.IsNullOrEmpty(description))
            {
                continue;
            }

            if (description?.ToLower().Contains("@mskcc.org") is false)
            {
                oidcConnectionsWithMissingTechnicalOwners.Add(oidcConnection);
                continue;
            }

            // Business Procedure assumptions: The first part is SCTASK number, the second part is the business owner, 
            // the third part is the technical owner and last part is the APM number.
            // Since there is no guarantee that the description will always be in this format, 
            // we need to check if the technical owner is present with manual hardcoed parameters.
            var descriptionParts = GetDescriptionParts(description!);
            var technicalOwner = descriptionParts.Count > 2 ? descriptionParts[2] : string.Empty;

            if (string.IsNullOrEmpty(technicalOwner) || technicalOwner.ToLower().Contains("@mskcc.org") is false)
            {
                oidcConnectionsWithMissingTechnicalOwners.Add(oidcConnection);
                continue;
            }
        }

        var spConnections = await _pingFederateService.GetSpConnectionsAsync();
        var spConnectionsWithMissingOwners = spConnections
            .Where(spConnection => string.IsNullOrEmpty(spConnection.ContactInfoEmail) is false|| spConnection.ContactInfoEmail?.ToLower().Contains("@mskcc.org") is false)
            .ToList();
        
        ArgumentNullException.ThrowIfNull(spConnectionsWithMissingOwners);
        ArgumentNullException.ThrowIfNull(oidcConnectionsWithMissingTechnicalOwners);
        
        return new InsightResponse
        {
            SpConnectionsWithMissingTechnicalOwners = spConnectionsWithMissingOwners,
            OidcConnectionsWithMissingTechnicalOwners = oidcConnectionsWithMissingTechnicalOwners
        };
    }

    /// <inheritdoc/>
    public async Task<InsightResponse> GetConfigurationsWithMissingBusinessOwnersInsightsAsync()
    {
        // For OIDC programatically, what is considered a missing owner? No @ symbols? only 1 @ symbol? Issue is both technical owners and business owners meet criteria. 
        // Only solution is to rely on the sequential ordering of the delimited entries.
        
        var spConnections = await _pingFederateService.GetSpConnectionsAsync();
        var spConnectionsWithMissingOwners = spConnections
            .Where(spConnection => string.IsNullOrEmpty(spConnection.ContactInfoFirstName) is false || spConnection.ContactInfoFirstName?.ToLower().Contains("@mskcc.org") is false)
            .ToList();
        
        ArgumentNullException.ThrowIfNull(spConnectionsWithMissingOwners);
        
        return new InsightResponse
        {
            SpConnectionsWithMissingBusinessOwners = spConnectionsWithMissingOwners
        };
    }
    /// <inheritdoc/>
    public Task<InsightResponse> GetIncompleteConfigurationInsightsAsync()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Splits the description into parts based on the '|' character. 
    /// This is needed because in the OIDC configuration there is no actual proper place to store business related information in Ping Federate.
    /// Our IAM operations requires us to to store the business owner, technical owner and APM number in the description field.
    /// </summary>
    /// <param name="description"></param>
    /// <returns></returns>
    private List<string> GetDescriptionParts(string description)
    {
        var parts = description
            .Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(part => part.Trim())
            .ToList();

        return parts;
    }
}