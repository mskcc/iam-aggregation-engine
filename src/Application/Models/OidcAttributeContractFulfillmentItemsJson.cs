using System.Text.Json.Serialization;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

/// <summary>
/// Represents a collection of <see cref="OidcAttributeContractFulfillmentJson"/> items.
/// </summary>
public class OidcAttributeContractFulfillmentItemsJson
{
    /// <summary>
    /// Gets or sets the collection of <see cref="OidcAttributeContractFulfillmentJson"/> items.
    /// </summary>
    [JsonPropertyName("items")]
    public List<OidcAttributeContractFulfillmentJson>? Items { get; set; }
}