using System.Text.Json.Serialization;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

/// <summary>
/// Model used to deserialize Service Now application responses.
/// </summary>
public class ServiceNowApplicationsJson
{
    /// <summary>
    /// Represents the APM number of the Service Now application.
    /// This is typically a unique identifier for the application within Service Now.
    /// </summary>
    [JsonPropertyName("number")]
    public string? Number { get; set; }

    /// <summary>
    /// Represents the name of the Service Now application.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Represents the short description of the Service Now application.
    /// This typically gives a brief overview or summary of the application.
    /// </summary>
    [JsonPropertyName("short_description")]
    public string? ShortDescription { get; set; }

    /// <summary>
    /// Represents the application category as a linked value.
    /// This category helps in classifying the application within Service Now.
    /// </summary>
    [JsonPropertyName("application_category")]
    [JsonConverter(typeof(StringOrObjectConverter<LinkValue>))]
    public LinkValue? ApplicationCategory { get; set; }

    /// <summary>
    /// Represents the type of the application (e.g., 'Enterprise', 'Non-Enterprise', etc.).
    /// </summary>
    [JsonPropertyName("application_type")]
    public string? ApplicationType { get; set; }

    /// <summary>
    /// Represents the architecture type of the application (e.g., 'Client-Server', 'Web-Based', etc.).
    /// </summary>
    [JsonPropertyName("architecture_type")]
    public string? ArchitectureType { get; set; }

    /// <summary>
    /// Represents the installation type of the application (e.g., 'On-Premise', 'Cloud', etc.).
    /// </summary>
    [JsonPropertyName("install_type")]
    public string? InstallType { get; set; }

    /// <summary>
    /// Represents the user base for the application, describing the number or type of users who use the application.
    /// </summary>
    [JsonPropertyName("user_base")]
    public string? UserBase { get; set; }

    /// <summary>
    /// Represents the platform on which the application runs (e.g., 'Windows', 'Linux', etc.).
    /// </summary>
    [JsonPropertyName("platform")]
    public string? Platform { get; set; }

    /// <summary>
    /// Represents the contract end date for the Service Now application.
    /// This indicates when the current contract for the application expires.
    /// </summary>
    [JsonPropertyName("contract_end_date")]
    public string? ContractEndDate { get; set; }

    /// <summary>
    /// Represents the business criticality of the application (e.g., 'High', 'Medium', 'Low').
    /// This helps in prioritizing application support and operations.
    /// </summary>
    [JsonPropertyName("business_criticality")]
    public string? BusinessCriticality { get; set; }

    /// <summary>
    /// Represents the data classification of the application (e.g., 'Confidential', 'Public', etc.).
    /// </summary>
    [JsonPropertyName("data_classification")]
    public string? DataClassification { get; set; }

    /// <summary>
    /// Represents the IT application owner as a linked value.
    /// This refers to the individual or team responsible for managing the application.
    /// </summary>
    [JsonPropertyName("it_application_owner")]
    [JsonConverter(typeof(StringOrObjectConverter<LinkValue>))]
    public LinkValue? ItApplicationOwner { get; set; }

    /// <summary>
    /// Represents the entity that owns the application, typically a business unit or department.
    /// </summary>
    [JsonPropertyName("owned_by")]
    [JsonConverter(typeof(StringOrObjectConverter<LinkValue>))]
    public LinkValue? OwnedBy { get; set; }

    /// <summary>
    /// Represents the URL for accessing more information or managing the Service Now application.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Represents the emergency tier of the application (e.g., 'Tier 1', 'Tier 2').
    /// This indicates the level of urgency and priority for supporting the application.
    /// </summary>
    [JsonPropertyName("emergency_tier")]
    public string? EmergencyTier { get; set; }

    /// <summary>
    /// Represents the group responsible for managing the application as a linked value.
    /// This may be a technical team, vendor, or business unit.
    /// </summary>
    [JsonPropertyName("managed_by_group")]
    [JsonConverter(typeof(StringOrObjectConverter<LinkValue>))]
    public LinkValue? ManagedBy { get; set; }

    /// <summary>
    /// Represents the current installation status of the application (e.g., 'Installed', 'Not Installed').
    /// </summary>
    [JsonPropertyName("install_status")]
    public string? InstallStatus { get; set; }

    /// <summary>
    /// Represents the support group responsible for providing technical assistance for the application.
    /// </summary>
    [JsonPropertyName("support_group")]
    [JsonConverter(typeof(StringOrObjectConverter<LinkValue>))]
    public LinkValue? SupportGroup { get; set; }

    /// <summary>
    /// Represents the entity or team responsible for supporting the application, such as the vendor or internal IT team.
    /// </summary>
    [JsonPropertyName("supported_by")]
    [JsonConverter(typeof(StringOrObjectConverter<LinkValue>))]
    public LinkValue? SupportedBy { get; set; }

    /// <summary>
    /// Represents the vendor providing the application as a linked value.
    /// This typically includes vendor details and a reference link.
    /// </summary>
    [JsonPropertyName("vendor")]
    [JsonConverter(typeof(StringOrObjectConverter<LinkValue>))]
    public LinkValue? Vendor { get; set; }

    /// <summary>
    /// Represents the status of the application in its life cycle (e.g., 'InUse', 'Retired').
    /// </summary>
    [JsonPropertyName("life_cycle_stage_status")]
    [JsonConverter(typeof(StringOrObjectConverter<LinkValue>))]
    public LinkValue? LifeCycleStageStatus { get; set; }

    /// <summary>
    /// Represents the network ID associated with the application.
    /// This may refer to the network infrastructure or environment where the application is hosted.
    /// </summary>
    [JsonPropertyName("u_uses_enterprise_identities_network_id")]
    public string? UsesEnterpriseIdentitiesNetworkId { get; set; }

    /// <summary>
    /// Represents the authentication protocols used by the application.
    /// </summary>
    [JsonPropertyName("u_authentication_protocols")]
    public string? AuthenticationProtocols { get; set; }

    /// <summary>
    /// Represents the identity management protocols used by the application.
    /// </summary>
    [JsonPropertyName("u_api_acct_life_cycle_mgmt")]
    public string? ApiAccountLifeCycleManagement { get; set; }

    /// <summary>
    /// Represents the identity management protocols used by the application.
    /// </summary>
    [JsonPropertyName("u_is_externally_facing")]
    public string? IsExternallyFacing { get; set; }
}

/// <summary>
/// Represents a link value with a URL and associated value.
/// This is used for fields that contain a hyperlink along with an associated value.
/// </summary>
public class LinkValue
{
    /// <summary>
    /// Gets or sets the link (URL) associated with the value.
    /// </summary>
    [JsonPropertyName("link")]
    public string? Link { get; set; }

    /// <summary>
    /// Gets or sets the value associated with the link.
    /// This could be a name, description, or identifier related to the linked resource.
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; set; }
}