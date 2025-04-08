using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;

/// <summary>
/// Represents a client configuration for legacy connections to be persisted in legacy table.
/// </summary>
[Table("IDM_PingID_Connections_List")]
public class LegacyConnection
{
    /// <summary>
    /// Represents the primary key for the DTO (Data Transfer Object).
    /// </summary>
    [Key]
    public Guid PrimaryKey { get; set; }

    /// <summary>
    /// Id represents the primary key of the origin.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// The ID of the configured SSO connection in Ping Federate.
    /// </summary>
    public string? PingConnectionID { get; set; }

    /// <summary>
    /// The name of the configured SSO connection in Ping Federate.
    /// </summary>
    /// <remarks>
    /// The ConnectionName represents ClientId when the configured SSO connection is an OIDC connection.
    /// </remarks>
    public string? ConnectionName { get; set; }

    /// <summary>
    /// The application name of the configurd SSO connection in Ping Federate.
    /// </summary>
    /// <remarks>
    /// The ApplicationName represents the Name when the configured SSO connection is an OIDC connection.
    /// </remarks>
    public string? ApplicationName { get; set; }

    /// <summary>
    /// The entity id of the configured SSO connection in ping federate.
    /// </summary>
    /// <remarks>
    /// The EntityID represents the ClientId when the configured SSO connection is an OIDC connection.
    /// </remarks>
    public string? EntityID { get; set; }

    /// <summary>
    /// The active status of the configured SSO connection in ping federate.
    /// </summary>
    /// <remarks>
    /// The IsActive column represetnts the enabled property when the configured SSO connection is an OIDC connection.
    /// </remarks>
    public bool IsActive { get; set;}

    /// <summary>
    /// Gets or sets the date and time when the client was last modified.
    /// This timestamp is typically set by the system whenever a change is made to the client configuration.
    /// </summary>
    public DateTime ModificationDate { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the client was created.
    /// This timestamp indicates when the client configuration was first created.
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// The ticket number associated with the configured SSO connection in ping federate.
    /// </summary>
    /// <remarks>
    /// TicketNumber represents 'COMPANY' when the configured SSO connection is a SAML connection.
    /// TicketNumber represents the first section of the parsed field 'Description' when the 
    /// configured SSO connection is an OIDC connection.
    /// </remarks>
    public string? TicketNumber { get; set;}

    /// <summary>
    /// The business owner associated with the configured SSO connection in ping federate.
    /// </summary>
    /// <remarks>
    /// BusinessOwner represents 'CONTACT NAME' when the configured SSO connection is a SAML connection.
    /// BusinessOwner represents the second section of the parsed field 'Description' when the 
    /// configured SSO connection is an OIDC connection.
    /// </remarks>
    public string? BusinessOwner { get; set; }

    /// <summary>
    /// The technical owner associated with the configured SSO connection in ping federate.
    /// </summary>
    /// <remarks>
    /// TechnicalOwner represents 'CONTACT EMAIL' when the configured SSO connection is a SAML connection.
    /// TechnicalOwner represents the third section of the parsed field 'Description' when the 
    /// configured SSO connection is an OIDC connection.
    /// </remarks>
    public string? TechnicalOwner { get; set; }

    /// <summary>
    /// The BaseUrl of the configured SSO connection. This represents the 'Base URL' field in a SAML SSSO connection.
    /// </summary>
    /// <remarks>
    /// When the configured SSO connection is OIDC this column in empty.
    /// </remarks>
    public string? BaseUrl { get; set; }

    /// <summary>
    /// Represents the ACS Endpoint of a configured SSO connection in ping federate.
    /// </summary>
    /// <remarks>
    /// ACSEndpoint represents ACSEndpoint when the configured SSO connection is a SAML connection.
    /// ACSEndpoint represents the RedirectURIs when the configured SSO connection is an OIDC connection.
    /// </remarks>
    public string? ACSEndpoint { get; set; }

    /// <summary>
    /// Conditional issuance Criteria of a configured SSO connection in ping federate.
    /// </summary>
    /// <remarks>
    /// This field is empty when the configured SSO connection is an OIDC connection.
    /// </remarks>
    public string? ConditionalIssuanceCriteria { get; set; }

    /// <summary>
    /// Expression issuance Criteria of a configured SSO connection in ping federate.
    /// </summary>
    /// <remarks>
    /// This field is empty when the configured SSO connection is an OIDC connection.
    /// </remarks>
    public string? ExpressionIssuanceCriteria { get; set; }

    /// <summary>
    /// Represents the environment in which we are retrieving ping federate data from.
    /// </summary>
    public string? Environment { get; set; }

    /// <summary>
    /// Represents the instance (Prod/Test/QA/Dev) in which the configured SSO connection is setup for. 
    /// </summary>
    public string? Instance { get; set; }

    /// <summary>
    /// Represents the APM ID from the APM feed so we can tie an 
    /// application to an SSO connection within MSKCC business logic operations.
    /// </summary>
    public string? APMNumber { get; set; }

    /// <summary>
    /// Represents the type of SSO connection configured in ping federate.
    /// </summary>
    /// <remarks>
    /// Options are 'SAML' or 'OIDC'.
    /// </remarks>
    public string? Type { get; set;}
}