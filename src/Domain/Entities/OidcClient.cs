using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;

/// <summary>
/// Represents a client configuration for OpenID Connect (OIDC).
/// </summary>
[Table("IDM_PingID_OIDC_Information")]
public class OidcClient
{
    /// <summary>
    /// Represents the primary key for the DTO (Data Transfer Object).
    /// </summary>
    [Key]
    public Guid PrimaryKey { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the client. 
    /// This ID is typically a string used to identify the OIDC client in a system.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("clientId")]
    public string? ClientId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the client is enabled or disabled.
    /// If set to true, the client is enabled, otherwise it is disabled.
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets the name of the client. This could be a human-readable identifier for the client.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the client. This provides additional information about the client.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /*/// <summary>
    /// Gets or sets the URL of the client's logo.
    /// This URL typically links to an image representing the client (e.g., company logo).
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("logoUrl")]
    public string? LogoUrl { get; set; }*/

    /// <summary>
    /// Gets or sets the date and time when the client was last modified.
    /// This timestamp is typically set by the system whenever a change is made to the client configuration.
    /// </summary>
    [JsonPropertyName("modificationDate")]
    public DateTime ModificationDate { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the client was created.
    /// This timestamp indicates when the client configuration was first created.
    /// </summary>
    [JsonPropertyName("creationDate")]
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// Gets or sets the refresh rolling setting. 
    /// This setting determines whether the refresh token is rolled over when used or not.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("refreshRolling")]
    public string? RefreshRolling { get; set; }

    /// <summary>
    /// Gets or sets the type of the refresh token rolling interval.
    /// This setting controls the frequency or conditions under which refresh tokens can be refreshed.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("refreshTokenRollingIntervalType")]
    public string? RefreshTokenRollingIntervalType { get; set; }

    /// <summary>
    /// Gets or sets the persistent grant expiration type.
    /// This setting controls how persistent grants are expired after a certain period.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("persistentGrantExpirationType")]
    public string? PersistentGrantExpirationType { get; set; }

    /// <summary>
    /// Gets or sets the expiration time for persistent grants (in minutes).
    /// Persistent grants represent tokens or authorization codes that have been issued and their expiry time.
    /// </summary>
    [JsonPropertyName("persistentGrantExpirationTime")]
    public int PersistentGrantExpirationTime { get; set; }

    /// <summary>
    /// Gets or sets the unit of time for the persistent grant expiration. 
    /// This could be in minutes, hours, or days depending on the configuration.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("persistentGrantExpirationTimeUnit")]
    public string? PersistentGrantExpirationTimeUnit { get; set; }

    /// <summary>
    /// Gets or sets the idle timeout type for the persistent grant.
    /// This could control how long a grant remains valid without activity.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("persistentGrantIdleTimeoutType")]
    public string? PersistentGrantIdleTimeoutType { get; set; }

    /// <summary>
    /// Gets or sets the idle timeout for the persistent grant (in minutes).
    /// This represents the time a client can be idle before the persistent grant is invalidated.
    /// </summary>
    [JsonPropertyName("persistentGrantIdleTimeout")]
    public int PersistentGrantIdleTimeout { get; set; }

    /// <summary>
    /// Gets or sets the unit of time for the persistent grant idle timeout.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("persistentGrantIdleTimeoutTimeUnit")]
    public string? PersistentGrantIdleTimeoutTimeUnit { get; set; }

    /// <summary>
    /// Gets or sets the reuse type for the persistent grant.
    /// This determines if and when a persistent grant can be reused by a client.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("persistentGrantReuseType")]
    public string? PersistentGrantReuseType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether authentication API initialization is allowed.
    /// This controls whether the client is allowed to initialize authentication API requests.
    /// </summary>
    [JsonPropertyName("allowAuthenticationApiInit")]
    public bool AllowAuthenticationApiInit { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to bypass the approval page.
    /// If true, the approval page will be skipped during the authentication process.
    /// </summary>
    [JsonPropertyName("bypassApprovalPage")]
    public bool BypassApprovalPage { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether scopes are restricted for this client.
    /// When true, the client can only use a specific subset of available scopes.
    /// </summary>
    [JsonPropertyName("restrictScopes")]
    public bool RestrictScopes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether pushed authorization requests are required.
    /// If true, the client must use the pushed authorization mechanism.
    /// </summary>
    [JsonPropertyName("requirePushedAuthorizationRequests")]
    public bool RequirePushedAuthorizationRequests { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether JWT secured authorization response mode is required.
    /// If true, the client must use a JWT-secured response for authorization requests.
    /// </summary>
    [JsonPropertyName("requireJwtSecuredAuthorizationResponseMode")]
    public bool RequireJwtSecuredAuthorizationResponseMode { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to restrict to the default access token manager.
    /// If true, the client will be restricted to using the default access token manager.
    /// </summary>
    [JsonPropertyName("restrictToDefaultAccessTokenManager")]
    public bool RestrictToDefaultAccessTokenManager { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to validate using all eligible access token managers (ATMs).
    /// If true, the client will perform validation using all eligible ATMs.
    /// </summary>
    [JsonPropertyName("validateUsingAllEligibleAtms")]
    public bool ValidateUsingAllEligibleAtms { get; set; }

    /// <summary>
    /// Gets or sets the device flow setting type.
    /// Defines the device flow mechanism and settings for the client.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("deviceFlowSettingType")]
    public string? DeviceFlowSettingType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether Proof Key for Code Exchange (PKCE) is required.
    /// When true, the client must include a PKCE during the authorization flow.
    /// </summary>
    [JsonPropertyName("requireProofKeyForCodeExchange")]
    public bool RequireProofKeyForCodeExchange { get; set; }

    /// <summary>
    /// Gets or sets the refresh token rolling grace period type.
    /// Defines the grace period for refresh token rolling.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("refreshTokenRollingGracePeriodType")]
    public string? RefreshTokenRollingGracePeriodType { get; set; }

    /// <summary>
    /// Gets or sets the client secret retention period type.
    /// This controls how long the client secret is retained.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("clientSecretRetentionPeriodType")]
    public string? ClientSecretRetentionPeriodType { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the client secret was last changed.
    /// This timestamp indicates when the client last updated or changed its secret.
    /// </summary>
    [JsonPropertyName("clientSecretChangedTime")]
    public DateTime ClientSecretChangedTime { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether DPoP (Demonstration of Proof of Possession) is required.
    /// If true, the client must use DPoP during authentication and authorization.
    /// </summary>
    [JsonPropertyName("requireDpop")]
    public bool RequireDpop { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether signed requests are required.
    /// If true, the client must sign its requests during the authentication and authorization process.
    /// </summary>
    [JsonPropertyName("requireSignedRequests")]
    public bool RequireSignedRequests { get; set; }

    // New fields added for extended DTO

    /// <summary>
    /// Gets or sets a collection of redirect URIs that the client is allowed to use for redirection during authentication.
    /// </summary>
    [JsonPropertyName("redirectUris")]
    public List<string>? RedirectUris { get; set; }

    /// <summary>
    /// Gets or sets a collection of grant types that the client supports (e.g., authorization code, implicit, client credentials).
    /// </summary>
    [JsonPropertyName("grantTypes")]
    public List<string>? GrantTypes { get; set; }

    /// <summary>
    /// Gets or sets a collection of restricted scopes that the client is allowed to request.
    /// This limits the available scopes to a predefined set.
    /// </summary>
    [JsonPropertyName("restrictedScopes")]
    public List<string>? RestrictedScopes { get; set; }

    /// <summary>
    /// Gets or sets a collection of exclusive scopes that the client can exclusively request.
    /// These scopes are prioritized over other scopes.
    /// </summary>
    [JsonPropertyName("exclusiveScopes")]
    public List<string>? ExclusiveScopes { get; set; }

    /// <summary>
    /// Gets or sets a collection of restricted response types (e.g., code, token) that the client is allowed to receive.
    /// </summary>
    [JsonPropertyName("restrictedResponseTypes")]
    public List<string>? RestrictedResponseTypes { get; set; }

    /// <summary>
    /// Gets or sets a collection of authorization detail types. This defines the specific details of the authorization process for the client.
    /// </summary>
    [JsonPropertyName("authorizationDetailTypes")]
    public List<string>? AuthorizationDetailTypes { get; set; }

    /// <summary>
    /// Gets or sets a collection of allowed CORS origins for the client.
    /// </summary>
    [JsonPropertyName("allowedCorsOrigins")]
    public string? OidcPolicyId { get; set; }

    /// <summary>
    /// Attribute contract fulfillment table
    /// </summary>
    public List<AttributeContractFulfillment>? AttributeContractFulfillment { get; set; }

    /// <summary>
    /// Issuance criteria
    /// </summary>
    [MaxLength(500)]
    public string? ConditionalIssuanceCriteria { get; set; }

    /// <summary>
    /// Issuance criteria
    /// </summary>
    [MaxLength(5000)]
    public string? ExpressionIssuanceCriteria { get; set; }
}