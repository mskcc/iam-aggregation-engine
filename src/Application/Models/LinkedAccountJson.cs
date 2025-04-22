using System.Text.Json.Serialization;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

public class LinkedAccountJson
{
    /// <summary>
    /// The unique identifier for the linked account.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// The unique identifier for the user in the source system.
    /// </summary>
    [JsonPropertyName("externalId")]
    public string? ExternalId { get; set; }

    /// <summary>
    /// The unique identifier for the user in PingOne.
    /// </summary>
    [JsonPropertyName("identityProvider")]
    public IdentityProviderJson? IdentityProvider { get; set; }
}

public class IdentityProviderJson
{
    /// <summary>
    /// The unique identifier for the identity provider.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

}