using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

public class PingOneIdentityProvider
{
    /// <summary>
    /// The unique identifier for the identity provider.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// The name of the identity provider.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// The type of the identity provider.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// The description of the identity provider.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }
}