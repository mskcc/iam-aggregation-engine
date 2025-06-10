using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

public interface IPingFederateInsightService
{
    /// <summary>
    /// Get all APM insights for a given application name.
    /// </summary>
    /// <param name="applicationName"></param>
    /// <returns></returns>
    Task<InsightResponse> GetApmNumberInsightsAsync(string applicationName);

    /// <summary>
    /// Get all application name insights for a given apm number.
    /// </summary>
    /// <param name="apmNumber"></param>
    /// <returns></returns>
    Task<InsightResponse> GetApplicationNameInsightsAsync(string apmNumber);

    /// <summary>
    /// Get insights for all incomplete SSO configurations configured in Ping Federate.
    /// </summary>
    /// <returns></returns>
    Task<InsightResponse> GetIncompleteConfigurationInsightsAsync();

    /// <summary>
    /// Get insights for all configurations with missing owners configured in Ping Federate.
    /// </summary>
    /// <returns></returns>
    Task<InsightResponse> GetConfigurationsWithMissingTechnicalOwnersInsightsAsync();

    /// <summary>
    /// Get insights for all configurations with missing owners configured in Ping Federate.
    /// </summary>
    /// <returns></returns>
    Task<InsightResponse> GetConfigurationsWithMissingBusinessOwnersInsightsAsync();

    /// <summary>
    /// Get insights for all configurations with missing APM insights configured in Ping Federate.
    /// </summary>
    /// <returns></returns>
    Task<InsightResponse> GetConfigurationsWithMissingApmInsightsAsync();
}