using System.Text.Json.Serialization;

/// <summary>
/// Represents an authentication response from PingOne.
/// </summary>
public class PingOneAuthentication
{
    /// <summary>
    /// Represents the access token from an authentication response from PingOne.
    /// </summary>
    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }

    /// <summary>
    /// Represents the token type from an authentication response from PingOne.
    /// </summary>
    [JsonPropertyName("token_type")]
    public string? TokenType { get; set; }

    /// <summary>
    /// Represents the expiration time from an authentication response from PingOne.
    /// </summary>
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; } = 0;
}