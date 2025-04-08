using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;

/// <summary>
/// Represents a connection received in the response from querying the spConnections endpoint.
/// </summary>
[Table("IDM_PIngID_SAML_Information")]
public class SpConnection
{
    /// <summary>
    /// Represents the primary key for the DTO
    /// </summary>
    [Key]   
    public Guid PrimaryKey { get; set; }
    
    /// <summary>
    /// Represents the PingFederate SP connection id.
    /// </summary>
    [MaxLength(500)]
    public string? Id { get; set; }
    
    /// <summary>
    /// Represents the type of connection.
    /// </summary>
    [MaxLength(500)]
    public string? Type { get; set; }
    
    /// <summary>
    /// Represents the name of the connection.
    /// </summary>
    [MaxLength(500)]
    public string? Name { get; set; }
    
    /// <summary>
    /// Represents the entity id of the connection.
    /// </summary>
    [MaxLength(500)]
    public string? EntityId { get; set; }
    
    /// <summary>
    /// Represents if the connection is active.
    /// </summary>
    public bool Active { get; set; }

    /// <summary>
    /// Represents the base url for this connection.
    /// </summary>
    [MaxLength(500)]
    public string? BaseUrl { get; set; }

    /// <summary>
    /// Represents the last modified date/time of the connection.
    /// </summary>
    public DateTime ModificationDate { get; set; }

    /// <summary>
    /// Represents the last creation date/time of the connection.
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// Protocol sp browser sso connection
    /// </summary>
    /// <remarks>
    /// spBrowserSso.Protocol
    /// </remarks>
    [MaxLength(500)]
    public string? Protocol { get; set; }

    /// <summary>
    /// Enabled sso profiles
    /// </summary>
    /// <remarks>
    /// spBrowserSso.enabledProfiles
    /// </remarks>
    public IEnumerable<string>? EnabledProfiles { get; set; }

    /// <summary>
    /// Idp initiated link for sso
    /// </summary>
    /// <remarks>
    /// spBrowserSso.ssoApplicationEndpoint
    /// </remarks>
    [MaxLength(500)]
    public string? IdpInitiatedLink { get; set; }

    /// <summary>
    /// ACS Url
    /// </summary>
    /// <remarks>
    /// spBrowserSso.sloServiceEndpoints.url
    /// </remarks>
    [MaxLength(500)]
    public string? AcsUrl { get; set; }

    /// <summary>
    /// Sign assertions
    /// </summary>
    /// <remarks>
    /// spBrowserSso.signAssertions
    /// </remarks>
    public bool SignAssertions { get; set; }

    /// <summary>
    /// Sign response as required
    /// </summary>
    /// <remarks>
    /// spBrowserSso.signResponseAsRequired
    /// </remarks>
    [MaxLength(500)]
    public bool SignResponseAsRequired { get; set; }

    /// <summary>
    /// Require signed authentication requests
    /// </summary>
    /// <remarks>
    /// spBrowserSso.requireSignedAuthnRequests
    /// </remarks>
    [MaxLength(500)]
    public bool RequireSignedAuthnRequests { get; set; }

    /// <summary>
    /// Name id policy
    /// </summary>
    /// <remarks>
    /// spBrowserSso.attributeContract.coreAttributes.nameFormat
    /// </remarks>
    [MaxLength(500)]
    public string? NameIdPolicy { get; set; }

    /// <summary>
    /// Issuance criteria
    /// </summary>
    /// <remarks>
    /// spBrowserSso.authenticationPolicyContractAssertionMappings.issuanceCriteria.conditionalCriteria.value
    /// </remarks>
    [MaxLength(500)]
    public string? ConditionalIssuanceCriteria { get; set; }

    /// <summary>
    /// Issuance criteria
    /// </summary>
    /// <remarks>
    /// spBrowserSso.authenticationPolicyContractAssertionMappings.issuanceCriteria.conditionalCriteria.value
    /// </remarks>
    [MaxLength(5000)]
    public string? ExpressionIssuanceCriteria { get; set; }

    /// <summary>
    /// Represents the name of the application
    /// </summary>
    [MaxLength(500)]
    public string? ApplicationName { get; set; }

    /// <summary>
    /// Attribute contract fulfillment table
    /// </summary>
    public List<AttributeContractFulfillment>? AttributeContractFulfillment { get; set; }

    /// <summary>
    /// Company name from contact info within Ping Federate response
    /// </summary>
    /// <remarks>
    /// This represents the ticket number of the connection.
    /// </remarks>
    [MaxLength(500)]
    public string? ContactInfoCompany { get; set; }

    /// <summary>
    /// First name from contact info within Ping Federate response
    /// </summary>'
    /// <remarks>
    /// This represents the business owner of the connection.
    /// </remarks>
    [MaxLength(500)]
    public string? ContactInfoFirstName { get; set;}

    /// <summary>
    /// Last name from contact info within Ping Federate response
    /// </summary>
    [MaxLength(500)]
    public string? ContactInfoLastName { get; set; }

    /// <summary>
    /// Email from contact info within Ping Federate response
    /// </summary>
    /// <remarks>
    /// This represents the technical owner of the connection.
    /// </remarks>
    [MaxLength(500)]
    public string? ContactInfoEmail { get; set; }

    /// <summary>
    /// Phone number from contact info within Ping Federate response
    /// </summary>
    /// <remarks>
    /// This represents the APM number of the connection.
    /// </remarks>
    [MaxLength(500)]
    public string? ContactInfoNumber { get; set; }
}