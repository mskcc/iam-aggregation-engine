namespace Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;

/// <summary>
/// Error names for the application.
/// </summary>
public class ErrorNames
{
    /// <summary>
    /// The aggregation in progress error name for the Ping Federate client.
    /// </summary>
    public const string AggregationInProgress = "Mskcc.Tools.PingFederate.Connections.AggregationInProgress";

    /// <summary>
    /// The purge in progress error name for the Ping Federate client.
    /// </summary>
    public const string PurgeInProgress = "Mskcc.Tools.PingFederate.Connections.PurgeInProgress";

    /// <summary>
    /// The validation error name for the Ping Federate client.
    /// </summary>
    public const string ValidationError = "Mskcc.Tools.PingFederate.Connections.ValidationError";
}