using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

public class InsightResponse
{
    /// <summary>
    /// Represents a list of SAML SSO configurations that do not have a technical owner properly configured in Ping Federate.
    /// </summary>
    public IEnumerable<SpConnection>? SpConnectionsWithMissingTechnicalOwners { get; set; }

    /// <summary>
    /// Represents a list of Oidc SSO configurations that do not have a technical owner properly configured in Ping Federate.
    /// </summary>
    public IEnumerable<OidcClient>? OidcConnectionsWithMissingTechnicalOwners { get; set; }

    /// <summary>
    /// Represents a list of SAML SSO configurations that do not have a business owner properly configured in Ping Federate.
    /// </summary>
    public IEnumerable<SpConnection>? SpConnectionsWithMissingBusinessOwners { get; set; }

    /// <summary>
    /// Represents a list of OIDC SSO configurations that do not have a business owner properly configured in Ping Federate.
    /// </summary>
    public IEnumerable<OidcClient>? OidcConnectionsWithMissingBusinessOwners { get; set; }

    /// <summary>
    /// Represents a list of SAML SSO configurations that do not have a APM number properly configured in Ping Federate.
    /// </summary>
    public IEnumerable<SpConnection>? SpConnectionsWithMissingApmNumbers { get; set; }

    /// <summary>
    /// Represents a list of OIDC SSO configurations that do not have a APM number properly configured in Ping Federate.
    /// </summary>
    public IEnumerable<OidcClient>? OidcConnectionsWithMissingApmNumbers { get; set; }
}