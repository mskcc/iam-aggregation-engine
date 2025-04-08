using System.Text.Json.Serialization;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

/// <summary>
/// Represents the list of connection items in the Ping Federate spConnections endpoint response.
/// </summary>
public class OidcClientItemsJson
{
    /// <summary>
    /// Represents the list of <see cref="SpConnection"/>.
    /// </summary>
    [JsonPropertyName("items")]
    public IEnumerable<OidcClientJson>? PingFederateConnections { get; set; }
}