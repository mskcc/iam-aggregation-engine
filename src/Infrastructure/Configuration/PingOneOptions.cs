namespace Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;

public class PingOneOptions
{
    /// <summary>
    /// Represents the section key for the PingOne configuration.
    /// </summary>
    public const string SectionKey = "PingOne";

    /// <summary>
    /// Gets or sets the client ID.
    /// </summary>
    public string? ClientId { get; set; }

    /// <summary>
    /// Gets or sets the client secret.
    /// </summary>
    public string? ClientSecret { get; set; }

    /// <summary>
    /// Gets or sets the authority.
    /// </summary>
    public string? Authority { get; set; }

    /// <summary>
    /// Gets or sets the API base URL.
    /// </summary>
    public string? ApiBaseUrl { get; set; }

    /// <summary>
    /// Gets or sets the environment ID.
    /// </summary>
    public string? EnvironmentId { get; set; }

    /// <summary>
    /// Gets or sets the trust server SSL certificate.
    /// </summary>
    public bool TrustServerSslCertificate { get; set; }

    /// <summary>
    /// Gets or sets the PingFederate identity provider ID.
    /// </summary>
    public string? PingFederateIdentityProviderId { get; set; }

    /// <summary>
    /// Gets or sets the Microsoft Entra identity provider ID.
    /// </summary>
    public string? MicrosoftEntraIdentityProviderId { get; set; }

    /// <summary>
    /// Gets or sets the LDAP Gateway ID.
    /// </summary>
    public string? LdapGatewayId { get; set; }

    /// <summary>
    /// Gets or sets the LDAP Gateway user type ID.
    /// </summary>
    public string? LdapGatewayUserTypeId { get; set; }
}