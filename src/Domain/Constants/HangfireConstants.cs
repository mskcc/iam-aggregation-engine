namespace Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;

/// <summary>
/// Constants for Hangfire.
/// </summary>
public static class HangfireConstants
{
    /// <summary>
    /// The recurring job id for OIDC connections.
    /// </summary>
    public const string OidcRecurringAggregationJobId = "Mskcc.Hangfire.Recurring.Aggregation.Oidc";

    /// <summary>
    /// The recurring job id for SAML connections.
    /// </summary>
    public const string SamlRecurringAggregationJobId = "Mskcc.Hangfire.Recurring.Aggregation.Saml";

    /// <summary>
    /// The recurring job id for legacy connections.
    /// </summary>
    public const string LegacyRecurringAggregationJobId = "Mskcc.Hangfire.Recurring.Aggregation.Legacy";

    /// <summary>
    /// The recurring job id for service now applications.
    /// </summary>
    public const string ServiceNowApplicationsRecurringAggregationJobId = "Mskcc.Hangfire.Recurring.ServiceNow.Aggregation.Applications";

    /// <summary>
    /// The recurring job id for service now user.
    /// </summary>
    public const string ServiceNowUsersRecurringAggregationJobId = "Mskcc.Hangfire.Recurring.ServiceNow.Aggregation.Users";

    /// <summary>
    /// The recurring job id for OIDC connections.
    /// </summary>
    public const string OidcRecurringPurgeJobId = "Mskcc.Hangfire.Recurring.Purge.Oidc";

    /// <summary>
    /// The recurring job id for SAML connections.
    /// </summary>
    public const string SamlRecurringPurgeJobId = "Mskcc.Hangfire.Recurring.Purge.Saml";

    /// <summary>
    /// The recurring job id for legacy connections.
    /// </summary>
    public const string LegacyRecurringPurgeJobId = "Mskcc.Hangfire.Recurring.Purge.Legacy";

    /// <summary>
    /// The recurring job id for service now applications.
    /// </summary>
    public const string ServiceNowApplicationsRecurringPurgeJobId = "Mskcc.Hangfire.Recurring.ServiceNow.Purge.Applications";

    /// <summary>
    /// The recurring job id for service now user.
    /// </summary>
    public const string ServiceNowUsersRecurringPurgeJobId = "Mskcc.Hangfire.Recurring.ServiceNow.Purge.Users";

    /// <summary>
    /// The recurring job id for starting the identity linking processing.
    /// </summary>
    public const string StartIdentityLinkingProcessingRecurringJobId = "Mskcc.Hangfire.Recurring.IdentityLinking.Start.BatchProcessing";
}