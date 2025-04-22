using System.Text.Json.Serialization;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

public class IdentityLinkingApiRequest
{
    /// <summary>
    /// The SAM account name of the user to be linked.
    /// </summary>
    [JsonPropertyName("samAccountName")]
    public string? SamAccountName { get; set; }
}