using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

/// <summary>
/// Represents a connection received in the response from querying the spConnections endpoint.
/// </summary>
public class SpConnectionJson
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
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    
    /// <summary>
    /// Represents the type of connection.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("type")]
    public string? Type { get; set; }
    
    /// <summary>
    /// Represents the name of the connection.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    /// <summary>
    /// Represents the entity id of the connection.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("entityId")]
    public string? EntityId { get; set; }
    
    /// <summary>
    /// Represents if the connection is active.
    /// </summary>
    [JsonPropertyName("active")]
    public bool? Active { get; set; }

    /// <summary>
    /// Represents the contact information for the connection.
    /// </summary>
    [JsonPropertyName("contactInfo")]
    public ContactInfo? ContactInfo { get; set; }

    /// <summary>
    /// Represents the base url for this connection.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("baseUrl")]
    public string? BaseUrl { get; set; }

    /// <summary>
    /// Represents the logging mode for the connection.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("loggingMode")]
    public string? LoggingMode { get; set; }

    /// <summary>
    /// Represents the virtual entity IDs associated with this connection.
    /// </summary>
    [JsonPropertyName("virtualEntityIds")]
    public List<string>? VirtualEntityIds { get; set; }

    /// <summary>
    /// Represents the license connection group.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("licenseConnectionGroup")]
    public string? LicenseConnectionGroup { get; set; }

    /// <summary>
    /// Represents credentials for the connection (e.g., certificates).
    /// </summary>
    [JsonPropertyName("credentials")]
    public Credentials? Credentials { get; set; }

    /// <summary>
    /// Represents the last modified date/time of the connection.
    /// </summary>
    [JsonPropertyName("modificationDate")]
    public DateTime ModificationDate { get; set; }

    /// <summary>
    /// Represents the last creation date/time of the connection.
    /// </summary>
    [JsonPropertyName("creationDate")]
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// Represents the SSO configuration for the service provider.
    /// </summary>
    [JsonPropertyName("spBrowserSso")]
    public SpBrowserSso? SpBrowserSso { get; set; }
    
    /// <summary>
    /// Represents the name of the application.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("applicationName")]
    public string? ApplicationName { get; set; }
    
    /// <summary>
    /// Represents the targeted connection type.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("connectionTargetType")]
    public string? ConnectionTargetType { get; set; }
}

// Nested DTOs

public class MetadataReloadSettings
{
    /// <summary>
    /// Represents the primary key for the DTO
    /// </summary>
    [Key]   
    public Guid PrimaryKey { get; set; }

    /// <summary>
    /// Represents the metadata URL reference for this connection.
    /// </summary>
    [JsonPropertyName("metadataUrlRef")]
    public MetadataUrlRef? MetadataUrlRef { get; set; }

    /// <summary>
    /// Represents whether auto metadata update is enabled for this connection.
    /// </summary>
    [JsonPropertyName("enableAutoMetadataUpdate")]
    public bool? EnableAutoMetadataUpdate { get; set; }
}

public class MetadataUrlRef
{
    /// <summary>
    /// Represents the primary key for the DTO
    /// </summary>
    [Key]   
    public Guid PrimaryKey { get; set; }

    /// <summary>
    /// Represents the ID of the metadata URL.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Represents the location of the metadata URL.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("location")]
    public string? Location { get; set; }
}

public class Credentials
{
    /// <summary>
    /// Represents the primary key for the DTO
    /// </summary>
    [Key]   
    public Guid PrimaryKey { get; set; }

    /// <summary>
    /// Represents a List of certificates associated with this connection.
    /// </summary>
    [JsonPropertyName("certs")]
    public List<Certificate>? Certs { get; set; }

    /// <summary>
    /// Represents signing settings associated with the connection.
    /// </summary>
    [JsonPropertyName("signingSettings")]
    public SigningSettings? SigningSettings { get; set; }
}

public class Certificate
{
    /// <summary>
    /// Represents the primary key for the DTO
    /// </summary>
    [Key]   
    public Guid PrimaryKey { get; set; }

    /// <summary>
    /// Represents whether this is the primary verification certificate.
    /// </summary>
    [JsonPropertyName("primaryVerificationCert")]
    public bool? PrimaryVerificationCert { get; set; }

    /// <summary>
    /// Represents whether this is the secondary verification certificate.
    /// </summary>
    [JsonPropertyName("secondaryVerificationCert")]
    public bool? SecondaryVerificationCert { get; set; }

    /// <summary>
    /// Represents the certificate view for this certificate.
    /// </summary>
    [JsonPropertyName("certView")]
    public CertView? CertView { get; set; }

    /// <summary>
    /// Represents the X509 file for this certificate.
    /// </summary>
    [JsonPropertyName("x509File")]
    public X509File? X509File { get; set; }

    /// <summary>
    /// Represents whether this is the encryption certificate.
    /// </summary>
    [JsonPropertyName("encryptionCert")]
    public bool? EncryptionCert { get; set; }

    /// <summary>
    /// Represents whether this certificate is active.
    /// </summary>
    [JsonPropertyName("activeVerificationCert")]
    public bool? ActiveVerificationCert { get; set; }
}

public class X509File
{
    /// <summary>
    /// Represents the primary key for the DTO
    /// </summary>
    [Key]   
    public Guid PrimaryKey { get; set; }

    /// <summary>
    /// Represents the ID of the X509 file.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Represents the file data of the X509 file.
    /// </summary>
    [MaxLength(5000)]
    [JsonPropertyName("fileData")]
    public string? FileData { get; set; }
}

public class CertView
{
    /// <summary>
    /// Represents the primary key for the DTO
    /// </summary>
    [Key]   
    public Guid PrimaryKey { get; set; }

    /// <summary>
    /// Represents the certificate ID.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Represents the serial number of the certificate.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("serialNumber")]
    public string? SerialNumber { get; set; }

    /// <summary>
    /// Represents the subject DN of the certificate.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("subjectDN")]
    public string? SubjectDN { get; set; }

    /// <summary>
    /// Represents the subject alternative names for the certificate.
    /// </summary>
    [JsonPropertyName("subjectAlternativeNames")]
    public List<string>? SubjectAlternativeNames { get; set; }

    /// <summary>
    /// Represents the issuer DN of the certificate.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("issuerDN")]
    public string? IssuerDN { get; set; }

    /// <summary>
    /// Represents the certificate validity start date.
    /// </summary>
    [JsonPropertyName("validFrom")]
    public DateTime? ValidFrom { get; set; }

    /// <summary>
    /// Represents the certificate expiration date.
    /// </summary>
    [JsonPropertyName("expires")]
    public DateTime? Expires { get; set; }

    /// <summary>
    /// Represents the key algorithm of the certificate.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("keyAlgorithm")]
    public string? KeyAlgorithm { get; set; }

    /// <summary>
    /// Represents the key size of the certificate.
    /// </summary>
    [JsonPropertyName("keySize")]
    public int KeySize { get; set; }

    /// <summary>
    /// Represents the signature algorithm of the certificate.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("signatureAlgorithm")]
    public string? SignatureAlgorithm { get; set; }

    /// <summary>
    /// Represents the certificate version.
    /// </summary>
    [JsonPropertyName("version")]
    public int Version { get; set; }

    /// <summary>
    /// Represents the SHA1 fingerprint of the certificate.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("fingerprintSHA1")]
    public string? FingerprintSHA1 { get; set; }

    /// <summary>
    /// Represents the SHA256 fingerprint of the certificate.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("fingerprintSHA256")]
    public string? FingerprintSHA256 { get; set; }
}

public class SigningSettings
{
    /// <summary>
    /// Represents the primary key for the DTO
    /// </summary>
    [Key]   
    public Guid PrimaryKey { get; set; }

    /// <summary>
    /// Represents the signing key pair reference.
    /// </summary>
    [JsonPropertyName("signingKeyPairRef")]
    public SigningKeyPairRef? SigningKeyPairRef { get; set; }

    /// <summary>
    /// Represents whether to include the certificate in the signature.
    /// </summary>
    [JsonPropertyName("includeCertInSignature")]
    public bool? IncludeCertInSignature { get; set; }

    /// <summary>
    /// Represents whether to include the raw key in the signature.
    /// </summary>
    [JsonPropertyName("includeRawKeyInSignature")]
    public bool? IncludeRawKeyInSignature { get; set; }
}

public class SigningKeyPairRef
{
    /// <summary>
    /// Represents the primary key for the DTO
    /// </summary>
    [Key]   
    public Guid PrimaryKey { get; set; }

    /// <summary>
    /// Represents the key pair ID.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Represents the key pair location.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("location")]
    public string? Location { get; set; }
}

public class BackChannelAuth
{
    /// <summary>
    /// Represents the primary key for the DTO
    /// </summary>
    [Key]   
    public Guid PrimaryKey { get; set; }

    /// <summary>
    /// Represents the type of back-channel authentication (OUTBOUND/INBOUND).
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Represents whether digital signature is enabled for back-channel authentication.
    /// </summary>
    [JsonPropertyName("digitalSignature")]
    public bool? DigitalSignature { get; set; }

    /// <summary>
    /// Represents whether to validate partner certificates for outbound authentication.
    /// </summary>
    [JsonPropertyName("validatePartnerCert")]
    public bool? ValidatePartnerCert { get; set; }

    /// <summary>
    /// Represents whether SSL is required for inbound authentication.
    /// </summary>
    [JsonPropertyName("requireSsl")]
    public bool? RequireSsl { get; set; }
}

public class ConditionalCriteria
{
    /// <summary>
    /// Error result
    /// </summary>
    [JsonPropertyName("errorResult")]
    public string? ErrorResult { get; set; }

    /// <summary>
    /// Attribute name
    /// </summary>
    [JsonPropertyName("attributeName")]
    public string? AttributeName { get; set; }

    /// <summary>
    /// Condition
    /// </summary>
    [JsonPropertyName("condition")]
    public string? Condition { get; set; }

    /// <summary>
    /// <see cref="Cond"/>
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; set; }
}

/// <summary>
/// Represents the issuance criteria object.
/// </summary>
public class IssuanceCriteriaSamlObject
{
    /// <summary>
    /// Gets or sets the error result of the issuance criteria.
    /// </summary>
    [JsonPropertyName("errorResult")]
    public string? ErrorResult { get; set; }

    /// <summary>
    /// Gets or sets the expression.
    /// </summary>
    [JsonPropertyName("expression")]
    public string? Expression { get; set; }
}

public class IssuanceCriteria
{
    /// <summary>
    /// List of <see cref="ConditionalCriteria"/>
    /// </summary>
    [JsonPropertyName("conditionalCriteria")]
    public IEnumerable<ConditionalCriteria>? ConditionalCriterias { get; set; }

        /// <summary>
    /// Gets or sets the list of expression criteria.
    /// </summary>
    [JsonPropertyName("expressionCriteria")]
    public List<IssuanceCriteriaSamlObject>? ExpressionCriteria { get; set; }
}

public class AuthenticationPolicyContractAssertionMapping
{
    /// <summary>
    /// Issuance criteria
    /// </summary>
    [JsonPropertyName("issuanceCriteria")]
    public IssuanceCriteria? IssuanceCriteria { get; set; }

    [JsonPropertyName("attributeContractFulfillment")]
    public AttributeContractFulfillmentDetails? AttributeContractFulfillment { get; set; }
}

public class AttributeContractFulfillmentDetails
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement> AdditionalProperties { get; set; } = new Dictionary<string, JsonElement>();

    /// <summary>
    /// SAML Subject json
    /// </summary>
    [JsonPropertyName("SAML_SUBJECT")]
    public AttributeContractFulfillmentMapping? Subject { get; set;}
}

public class sloServiceEndpoints
{
    /// <summary>
    /// Represents the primary key for the DTO
    /// </summary>
    [Key]   
    public Guid PrimaryKey { get; set; }

    /// <summary>
    /// Binding property
    /// </summary>
    [JsonPropertyName("binding")]
    public string? Binding { get; set; }

    /// <summary>
    /// Slo service endpoint url
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Response url
    /// </summary>
    [JsonPropertyName("responseUrl")]
    public string? ResponseUrl { get; set; }
}

public class SpBrowserSso
{
    /// <summary>
    /// Represents the primary key for the DTO
    /// </summary>
    [Key]   
    public Guid PrimaryKey { get; set; }

    /// <summary>
    /// Represents the SSO protocol.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("protocol")]
    public string? Protocol { get; set; }

    /// <summary>
    /// Represents the enabled SSO profiles.
    /// </summary>
    [JsonPropertyName("enabledProfiles")]
    public List<string>? EnabledProfiles { get; set; }

    /// <summary>
    /// slo Service Endpoints
    /// </summary>
    [JsonPropertyName("sloServiceEndpoints")]
    public IEnumerable<sloServiceEndpoints>? sloServiceEndpoints { get; set; }

    /// <summary>
    /// Authentication Policy Contract Assertion Mappings
    /// </summary>
    [JsonPropertyName("authenticationPolicyContractAssertionMappings")]
    public IEnumerable<AuthenticationPolicyContractAssertionMapping>? AuthenticationPolicyContractAssertionMappings { get; set; }

    /// <summary>
    /// Represents the incoming SSO bindings.
    /// </summary>
    [JsonPropertyName("incomingBindings")]
    public List<string>? IncomingBindings { get; set; }

    /// <summary>
    /// Represents the artifact resolver locations.
    /// </summary>
    [JsonPropertyName("artifact")]
    public Artifact? Artifact { get; set; }

    /// <summary>
    /// Represents whether to always sign the artifact response.
    /// </summary>
    [JsonPropertyName("alwaysSignArtifactResponse")]
    public bool? AlwaysSignArtifactResponse { get; set; }

    /// <summary>
    /// Represents the SSO application endpoint URL.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("ssoApplicationEndpoint")]
    public string? SsoApplicationEndpoint { get; set; }

    /// <summary>
    /// Represents the List of SSO service endpoints.
    /// </summary>
    [JsonPropertyName("ssoServiceEndpoints")]
    public List<SsoServiceEndpoint>? SsoServiceEndpoints { get; set; }

    /// <summary>
    /// Represents whether assertions should be signed.
    /// </summary>
    [JsonPropertyName("signAssertions")]
    public bool? SignAssertions { get; set; }

    /// <summary>
    /// Represents whether the response should be signed as required.
    /// </summary>
    [JsonPropertyName("signResponseAsRequired")]
    public bool? SignResponseAsRequired { get; set; }

    /// <summary>
    /// Represents the SP SAML identity mapping.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("spSamlIdentityMapping")]
    public string? SpSamlIdentityMapping { get; set; }

    /// <summary>
    /// Represents whether authentication requests should be signed.
    /// </summary>
    [JsonPropertyName("requireSignedAuthnRequests")]
    public bool? RequireSignedAuthnRequests { get; set; }

    /// <summary>
    /// Represents the assertion lifetime settings.
    /// </summary>
    [JsonPropertyName("assertionLifetime")]
    public AssertionLifetime? AssertionLifetime { get; set; }

    /// <summary>
    /// Represents the encryption policy for assertions.
    /// </summary>
    [JsonPropertyName("encryptionPolicy")]
    public EncryptionPolicy? EncryptionPolicy { get; set; }

    /// <summary>
    /// Represents the attribute contract for SAML attributes.
    /// </summary>
    [JsonPropertyName("attributeContract")]
    public AttributeContract? AttributeContract { get; set; }

    /// <summary>
    /// Adapter mapping json.
    /// </summary>
    [JsonPropertyName("adapterMappings")]
    public IEnumerable<AdapterMappings>? AdapterMappings { get; set;}
}

public class AdapterMappings
{
    /// <summary>
    /// SAML subject attribute mapping.
    /// </summary>
    [JsonPropertyName("attributeContractFulfillment")]
    public SubjectAttributeContractFulfillmentMapping? SubjectAttributeContractFulfillmentMapping { get; set;}
}

public class Artifact
{
    /// <summary>
    /// Represents the primary key for the DTO
    /// </summary>
    [Key]   
    public Guid PrimaryKey { get; set; }

    /// <summary>
    /// Represents the resolver locations for artifact.
    /// </summary>
    [JsonPropertyName("resolverLocations")]
    public List<ResolverLocation>? ResolverLocations { get; set; }
}

public class ResolverLocation
{
    /// <summary>
    /// Represents the primary key for the DTO
    /// </summary>
    [Key]   
    public Guid PrimaryKey { get; set; }

    /// <summary>
    /// Represents the resolver location index.
    /// </summary>
    [JsonPropertyName("index")]
    public int Index { get; set; }

    /// <summary>
    /// Represents the resolver location URL.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }
}

public class SsoServiceEndpoint
{
    /// <summary>
    /// Represents the primary key for the DTO
    /// </summary>
    [Key]   
    public Guid PrimaryKey { get; set; }

    /// <summary>
    /// Represents the binding type for the SSO service.
    /// </summary>
    [JsonPropertyName("binding")]
    public string? Binding { get; set; }

    /// <summary>
    /// Represents the URL for the SSO service endpoint.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Represents whether this SSO service is the default.
    /// </summary>
    [JsonPropertyName("isDefault")]
    public bool? IsDefault { get; set; }

    /// <summary>
    /// Represents the index of the SSO service endpoint.
    /// </summary>
    [JsonPropertyName("index")]
    public int Index { get; set; }
}

public class AssertionLifetime
{
    /// <summary>
    /// Represents the primary key for the DTO
    /// </summary>
    [Key]   
    public Guid PrimaryKey { get; set; }

    /// <summary>
    /// Represents the minutes before the assertion expires.
    /// </summary>
    [JsonPropertyName("minutesBefore")]
    public int MinutesBefore { get; set; }

    /// <summary>
    /// Represents the minutes after the assertion expires.
    /// </summary>
    [JsonPropertyName("minutesAfter")]
    public int MinutesAfter { get; set; }
}

public class EncryptionPolicy
{
    /// <summary>
    /// Represents the primary key for the DTO
    /// </summary>
    [Key]   
    public Guid PrimaryKey { get; set; }

    /// <summary>
    /// Represents whether the assertion should be encrypted.
    /// </summary>
    [JsonPropertyName("encryptAssertion")]
    public bool? EncryptAssertion { get; set; }

    /// <summary>
    /// Represents whether the SLO subject name ID should be encrypted.
    /// </summary>
    [JsonPropertyName("encryptSloSubjectNameId")]
    public bool? EncryptSloSubjectNameId { get; set; }

    /// <summary>
    /// Represents whether the SLO subject name ID is encrypted.
    /// </summary>
    [JsonPropertyName("sloSubjectNameIDEncrypted")]
    public bool? SloSubjectNameIDEncrypted { get; set; }

    /// <summary>
    /// Represents the List of encrypted attributes.
    /// </summary>
    [JsonPropertyName("encryptedAttributes")]
    public List<string>? EncryptedAttributes { get; set; }
}

public class AttributeContract
{
    /// <summary>
    /// Represents the primary key for the DTO
    /// </summary>
    [Key]   
    public Guid PrimaryKey { get; set; }

    /// <summary>
    /// Represents the core SAML attributes.
    /// </summary>
    [JsonPropertyName("coreAttributes")]
    public List<Attribute>? CoreAttributes { get; set; }

    /// <summary>
    /// Represents the extended SAML attributes.
    /// </summary>
    [JsonPropertyName("extendedAttributes")]
    public List<ExtendedAttributes>? ExtendedAttributes { get; set; }
}

public class Attribute
{
    /// <summary>
    /// Represents the primary key for the DTO
    /// </summary>
    [Key]   
    public Guid PrimaryKey { get; set; }

    /// <summary>
    /// Represents the name of the attribute.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Represents the name format of the attribute.
    /// </summary>
    [JsonPropertyName("nameFormat")]
    public string? NameFormat { get; set; }
}

public class ExtendedAttributes
{
    /// <summary>
    /// Represents the primary key for the DTO
    /// </summary>
    [Key]   
    public Guid PrimaryKey { get; set; }

    /// <summary>
    /// Represents the name of the attribute.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Represents the name format of the attribute.
    /// </summary>
    [JsonPropertyName("nameFormat")]
    public string? NameFormat { get; set; }
}

public class ContactInfo
{
    /// <summary>
    /// Represents the primary key for the DTO
    /// </summary>
    [Key]   
    public Guid PrimaryKey { get; set; }

    /// <summary>
    /// Represents the company contact info.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("company")]
    public string? Company { get; set; }

    /// <summary>
    /// Represents the first name contact info.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("firstName")]
    public string? FirstName { get; set; }

    /// <summary>
    /// Represents the last name contact info.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("lastName")]
    public string? LastName { get; set; }

    /// <summary>
    /// Represents the email contact info.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Represents the phone contact info.
    /// </summary>
    [MaxLength(500)]
    [JsonPropertyName("phone")]
    public string? Phone { get; set; }
}

public class SubjectAttributeContractFulfillmentMapping
{
    /// <summary>
    /// SAML Subject json
    /// </summary>
    [JsonPropertyName("SAML_SUBJECT")]
    public AttributeContractFulfillmentMapping? Subject { get; set;}
}

public class AttributeContractFulfillmentMapping
{
    /// <summary>
    /// Value of the AtrributeContractFulfillmentDetails object
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; set; }

    /// <summary>
    /// Source of the AttributeContractFulfillmentDetails object
    /// </summary>
    [JsonPropertyName("source")]
    public Source? Source { get; set; }
}

public class Source
{
    /// <summary>
    /// Type of the source
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Id of the source
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}