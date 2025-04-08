using System.ComponentModel.DataAnnotations;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;

/// <summary>
/// Represents options for interfacing with Ping Federate Instance.
/// </summary>
public class PingFederateOptions
{
    public const string SectionKey = "PingFederate";
    
    /// <summary>
    /// Gets or Sets the base URL for the Ping Federate API.
    /// </summary>
    [Required]
    [Url]
    public string BaseUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the username for authenticating against the Ping Federate API.
    /// </summary>
    public string Username { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the password for authenticating against the Ping Federate API.
    /// </summary>
    public string Password { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the host header for the Ping Federate API.
    /// </summary>
    public string Host { get; set; } = string.Empty;
    
    /// <summary>
    /// Represents a boolean value that indicates whether the server's SSL certificate will be trusted.
    /// </summary>
    public bool TrustServerSslCertificate { get; set; }

    /// <summary>
    /// Endpoint for making HTTP requests for SP Connections against Ping Federate's API
    /// </summary>
    public string SpConnectionsEndpoint { get; set; } = string.Empty;

    /// <summary>
    /// Endpoint for making HTTP requests for OAuth clients against Ping Federate's API
    /// </summary>
    public string OauthClientsEndpoint { get; set; } = string.Empty;

    /// <summary>
    /// Endpoint for making HTTP requests for OIDC Policies against Ping Federate's API
    /// </summary>
    public string OidcPoliciesEndpoint { get; set; } = string.Empty;

    /// <summary>
    /// Environment of the configured Ping Federate instance.
    /// </summary>
    public string InstanceEnvironment { get; set; } = string.Empty;

    /// <summary>
    /// Default Issuance Criteria for the Ping Federate instance.
    /// </summary>
    /// <remarks>
    /// This is a value that is used when the SSO connection's Issuance Criteria is not set.
    /// </remarks>
    public string DefaultIssuanceCriteria { get; set; } = string.Empty;
}