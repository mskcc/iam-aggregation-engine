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
}