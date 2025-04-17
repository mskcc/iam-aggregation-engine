namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

/// <summary>
/// Represents a response from PingOne's identity linking.
/// </summary>
public class IdentityLinkingResponse
{
    /// <summary>
    /// The PingOne response associated with account linking.
    /// </summary>
    public PingOneResponse? PingOneResponse { get; set; }

    /// <summary>
    /// Indicates whether the request is a just in time bulk operation.
    /// </summary>
    public bool IsBulkJustInTimeProcessingJob { get; set; } = false;

    /// <summary>
    /// Indicates whether the request is a bulk operation.
    /// </summary>
    public bool IsBulkProcessingJob { get; set; } = false;

    /// <summary>
    /// Indicates whether the bulk processing was successful.
    /// </summary>
    public bool IsBulkProcessingSuccessful { get; set; } = false;
}