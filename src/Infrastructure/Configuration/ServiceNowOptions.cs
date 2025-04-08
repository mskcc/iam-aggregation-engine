using System.ComponentModel.DataAnnotations;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;

/// <summary>
/// Represents options for interfacing with Service Now Instance.
/// </summary>
public class ServiceNowOptions
{
    public const string SectionKey = "ServiceNow";
    
    /// <summary>
    /// Gets or Sets the base URL for the Service Now API.
    /// </summary>
    [Required]
    [Url]
    public string BaseUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the username for authenticating against the Service Now API.
    /// </summary>
    public string Username { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the password for authenticating against the Service Now API.
    /// </summary>
    public string Password { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the host header for the Service Now API.
    /// </summary>
    public string Host { get; set; } = string.Empty;
    
    /// <summary>
    /// Represents a boolean value that indicates whether the server's SSL certificate will be trusted.
    /// </summary>
    public bool TrustServerSslCertificate { get; set; }

    /// <summary>
    /// Environment of the configured Service Now instance.
    /// </summary>
    public string InstanceEnvironment { get; set; } = string.Empty;

    /// <summary>
    /// Endpoint for the Service Now applications.
    /// </summary>
    public string ApplicationsEndpoint { get; set; } = string.Empty;

    /// <summary>
    /// Endpoint for the Service Now users.
    /// </summary>
    public string UsersEndpoint { get; set; } = string.Empty;
}