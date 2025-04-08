using System.Text.Json.Serialization;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

/// <summary>
/// Represents the list of connection items in the Ping Federate spConnections endpoint response.
/// </summary>
public class SpConnectionItemsJson
{
    /// <summary>
    /// Represents the list of <see cref="SpConnectionJson"/>.
    /// </summary>
    [JsonPropertyName("items")]
    public IEnumerable<SpConnectionJson>? PingFederateConnections { get; set; }
}