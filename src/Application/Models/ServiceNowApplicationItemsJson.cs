using System.Text.Json.Serialization;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

/// <summary>
/// Represents the JSON object for ServiceNow applications.
/// </summary>
public class ServiceNowApplicationItemsJson
{
    /// <summary>
    /// Gets or sets the result.
    /// </summary>
    [JsonPropertyName("result")]
    public List<ServiceNowApplicationsJson>? Result { get; set; }
}