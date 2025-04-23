namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.ResourceState;

/// <summary>
/// Service for managing the state of resources.
/// </summary>
/// <remarks>
public interface IResourceStateService
{
    /// <summary>
    /// Gets or sets a value indicating whether the OIDC aggregation process is currently running.
    /// </summary>
    /// <remarks>
    /// This property is thread-safe.
    /// </remarks>
    public bool IsOidcAggregationRunning { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the OIDC purge process is currently running.
    /// </summary>
    /// <remarks>
    /// This property is thread-safe.
    /// </remarks>
    public bool IsOidcPurgeRunning { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the SAML aggregation process is currently running.
    /// </summary>
    /// <remarks>
    /// This property is thread-safe.
    /// </remarks>
    public bool IsSamlAggregationRunning { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the SAML purge process is currently running.
    /// </summary>
    /// <remarks>
    /// This property is thread-safe.
    /// </remarks>
    public bool IsSamlPurgeRunning { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the legacy aggregation process is currently running.
    /// </summary>
    /// <remarks>
    /// This property is thread-safe.
    /// </remarks>
    public bool IsLegacyAggregationRunning { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the legacy purge process is currently running.
    /// </summary>
    /// <remarks>
    /// This property is thread-safe.
    /// </remarks>
    public bool IsLegacyPurgeRunning { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the ServiceNow applications aggregation process is currently running.
    /// </summary>
    /// <remarks>
    /// This property is thread-safe.
    /// </remarks>
    public bool IsServiceNowApplicationsAggregationRunning { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the ServiceNow applications purge process is currently running.
    /// </summary>
    /// <remarks>
    /// This property is thread-safe.
    /// </remarks>
    public bool IsServiceNowApplicationsPurgingRunning { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the ServiceNow users aggregation process is currently running.
    /// </summary>
    /// <remarks>
    /// This property is thread-safe.
    /// </remarks>
    public bool IsServiceNowUsersAggregationRunning { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the ServiceNow users purge process is currently running.
    /// </summary>
    /// <remarks>
    /// This property is thread-safe.
    /// </remarks>
    public bool IsServiceNowUsersPurgingRunning { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the Identity Engine job is currently running.
    /// </summary>
    public bool IsIdentityEngineJobRunning { get; set; }
}
