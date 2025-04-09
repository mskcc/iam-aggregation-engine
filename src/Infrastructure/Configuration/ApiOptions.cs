namespace Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;

/// <summary>
/// Configuration options for the API.
/// </summary>
public class ApiOptions
{
    public const string SectionKey = "Api";

    /// <summary>
    /// Options for the default page size.
    /// </summary>
    public int MaxPageSize { get; set; }

    /// <summary>
    /// Options for using pagination in responses.
    /// </summary>
    public bool UsePagination { get; set; }

    /// <summary>
    /// Options for configured base url of the API.
    /// </summary>
    public string BaseUrl { get; set; } = string.Empty;

    /// <summary>
    /// Options for configured base endpoint of the API.
    /// </summary>
    public string BaseEndpoint { get; set; } = string.Empty;

    /// <summary>
    /// Options for configured legacy connections endpoint.
    /// </summary>
    public string LegacyConnectionsEndpoint { get; set; } = string.Empty;

    /// <summary>
    /// Options for configured legacy connections aggregation endpoint.
    /// </summary>
    public string LegacyConnectionsAggregateEndpoint { get; set; } = string.Empty;

    /// <summary>
    /// Options for configured legacy connections purge endpoint.
    /// </summary>
    public string LegacyConnectionsPurgeEndpoint { get; set; } = string.Empty;

    /// <summary>
    /// Options for configured oidc connections endpoint.
    /// </summary>
    public string OidcConnectionsEndpoint { get; set; } = string.Empty;

    /// <summary>
    /// Options for configured oidc connections aggregation endpoint.
    /// </summary>
    public string OidcConnectionsAggregateEndpoint { get; set; } = string.Empty;

    /// <summary>
    /// Options for configured oidc connections purge endpoint.
    /// </summary>
    public string OidcConnectionsPurgeEndpoint { get; set; } = string.Empty;

    /// <summary>
    /// Options for configured saml connections endpoint.
    /// </summary>
    public string SamlConnectionsEndpoint { get; set; } = string.Empty;

    /// <summary>
    /// Options for configured saml connections aggregation endpoint.
    /// </summary>
    public string SamlConnectionsAggregateEndpoint { get; set; } = string.Empty;

    /// <summary>
    /// Options for configured saml connections purge endpoint.
    /// </summary>
    public string SamlConnectionsPurgeEndpoint { get; set; } = string.Empty;

    /// <summary>
    /// Options for recurring Hangfire scheudle.
    /// </summary>
    public string SamlAggregationSchedule { get; set; } = string.Empty;

    /// <summary>
    /// Options for recurring Hangfire scheudle.
    /// </summary>
    public string OidcAggregationSchedule { get; set; } = string.Empty;

    /// <summary>
    /// Options for recurring Hangfire schedule.
    /// </summary>
    public string LegacyAggregationSchedule { get; set; } = string.Empty;

    /// <summary>
    /// Options for amount of days to retain log files
    /// </summary>
    /// <remarks>
    /// Default is 31 days. This matches the serilog default parameter.
    /// </remarks>
    public int LoggingRetentionDays { get; set; } = 31;

    /// <summary>
    /// Options for the ServiceNow applications endpoint.
    /// </summary>
    public string ServiceNowApplicationsAggregationEndpoint { get; set; } = string.Empty;

    /// <summary>
    /// Options for the ServiceNow applications purge endpoint.
    /// </summary>
    public string ServiceNowApplicationsPurgeEndpoint { get; set; } = string.Empty;

    /// <summary>
    /// Options for the ServiceNow applications endpoint.
    /// </summary>
    public string ServiceNowApplicationsEndpoint { get; set; } = string.Empty;

        /// <summary>
    /// Options for the ServiceNow users endpoint.
    /// </summary>
    public string ServiceNowUsersAggregationEndpoint { get; set; } = string.Empty;

    /// <summary>
    /// Options for the ServiceNow users purge endpoint.
    /// </summary>
    public string ServiceNowUsersPurgeEndpoint { get; set; } = string.Empty;

    /// <summary>
    /// Options for the ServiceNow users endpoint.
    /// </summary>
    public string ServiceNowUsersEndpoint { get; set; } = string.Empty;

    /// <summary>
    /// Options for recurring Hangfire schedule.
    /// </summary>
    public string ServiceNowApplicationsAggregationSchedule { get; set; } = string.Empty;

    /// <summary>
    /// Options for recurring Hangfire schedule.
    /// </summary>
    public string ServiceNowUsersAggregationSchedule { get; set; } = string.Empty;

    /// <summary>
    /// Options for recurring purges.
    /// </summary>
    public string PurgeSchedule { get; set; } = string.Empty;

    /// <summary>
    /// Options for the Hangfire dashboard path.
    /// </summary>
    public string HangfireDashboardPath { get; set;} = string.Empty;
}