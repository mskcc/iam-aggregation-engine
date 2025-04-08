using System.Text.Json.Serialization;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

/// <summary>
/// Represents the OIDC Attribute Contract Fulfillment JSON structure.
/// </summary>
public class OidcAttributeContractFulfillmentJson
{
    /// <summary>
    /// Gets or sets the ID of the contract.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the contract.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the ID token lifetime.
    /// </summary>
    [JsonPropertyName("idTokenLifetime")]
    public int IdTokenLifetime { get; set; }

    /// <summary>
    /// Gets or sets the attribute contract.
    /// </summary>
    [JsonPropertyName("attributeContract")]
    public OidcAttributeContract? AttributeContract { get; set; }

    /// <summary>
    /// Gets or sets the attribute mapping.
    /// </summary>
    [JsonPropertyName("attributeMapping")]
    public OidcAttributeMapping? AttributeMapping { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to include Sri in ID token.
    /// </summary>
    [JsonPropertyName("includeSriInIdToken")]
    public bool IncludeSriInIdToken { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to include user info in ID token.
    /// </summary>
    [JsonPropertyName("includeUserInfoInIdToken")]
    public bool IncludeUserInfoInIdToken { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to include SHash in ID token.
    /// </summary>
    [JsonPropertyName("includeSHashInIdToken")]
    public bool IncludeSHashInIdToken { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to include X5t in ID token.
    /// </summary>
    [JsonPropertyName("includeX5tInIdToken")]
    public bool IncludeX5tInIdToken { get; set; }

    /// <summary>
    /// Gets or sets the ID token type header value.
    /// </summary>
    [JsonPropertyName("idTokenTypHeaderValue")]
    public string? IdTokenTypHeaderValue { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to return ID token on refresh grant.
    /// </summary>
    [JsonPropertyName("returnIdTokenOnRefreshGrant")]
    public bool ReturnIdTokenOnRefreshGrant { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to reissue ID token in hybrid flow.
    /// </summary>
    [JsonPropertyName("reissueIdTokenInHybridFlow")]
    public bool ReissueIdTokenInHybridFlow { get; set; }

    /// <summary>
    /// Gets or sets the reference to the access token manager.
    /// </summary>
    [JsonPropertyName("accessTokenManagerRef")]
    public AccessTokenManagerRef? AccessTokenManagerRef { get; set; }

    /// <summary>
    /// Gets or sets the scope attribute mappings.
    /// </summary>
    [JsonPropertyName("scopeAttributeMappings")]
    public ScopeAttributeMappings? ScopeAttributeMappings { get; set; }
}

/// <summary>
/// Represents the OIDC Attribute Contract structure.
/// </summary>
public class OidcAttributeContract
{
    /// <summary>
    /// Gets or sets the core attributes.
    /// </summary>
    [JsonPropertyName("coreAttributes")]
    public List<CoreAttribute>? CoreAttributes { get; set; }

    /// <summary>
    /// Gets or sets the extended attributes.
    /// </summary>
    [JsonPropertyName("extendedAttributes")]
    public List<ExtendedAttribute>? ExtendedAttributes { get; set; }
}

/// <summary>
/// Represents a core attribute.
/// </summary>
public class CoreAttribute
{
    /// <summary>
    /// Gets or sets the name of the core attribute.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the attribute is multi-valued.
    /// </summary>
    [JsonPropertyName("multiValued")]
    public bool MultiValued { get; set; }
}

/// <summary>
/// Represents an extended attribute.
/// </summary>
public class ExtendedAttribute
{
    /// <summary>
    /// Gets or sets the name of the extended attribute.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the attribute is multi-valued.
    /// </summary>
    [JsonPropertyName("multiValued")]
    public bool MultiValued { get; set; }
}

/// <summary>
/// Represents the OIDC Attribute Mapping structure.
/// </summary>
public class OidcAttributeMapping
{
    /// <summary>
    /// Gets or sets the attribute sources.
    /// </summary>
    [JsonPropertyName("attributeSources")]
    public List<AttributeSource>? AttributeSources { get; set; }

    /// <summary>
    /// Gets or sets the attribute contract fulfillment.
    /// </summary>
    [JsonPropertyName("attributeContractFulfillment")]
    public Dictionary<string, OidcAttributeContractFulfillment>? AttributeContractFulfillment { get; set; }

    /// <summary>
    /// Gets or sets the issuance criteria.
    /// </summary>
    [JsonPropertyName("issuanceCriteria")]
    public OidcIssuanceCriteria? IssuanceCriteria { get; set; }
}

/// <summary>
/// Represents an attribute source.
/// </summary>
public class AttributeSource
{
    /// <summary>
    /// Gets or sets the type of the attribute source.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the reference to the data store.
    /// </summary>
    [JsonPropertyName("dataStoreRef")]
    public DataStoreRef? DataStoreRef { get; set; }

    /// <summary>
    /// Gets or sets the ID of the attribute source.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the description of the attribute source.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the base DN for the search.
    /// </summary>
    [JsonPropertyName("baseDn")]
    public string? BaseDn { get; set; }

    /// <summary>
    /// Gets or sets the search scope for the attribute source.
    /// </summary>
    [JsonPropertyName("searchScope")]
    public string? SearchScope { get; set; }

    /// <summary>
    /// Gets or sets the search filter for the attribute source.
    /// </summary>
    [JsonPropertyName("searchFilter")]
    public string? SearchFilter { get; set; }

    /// <summary>
    /// Gets or sets the binary attribute settings.
    /// </summary>
    [JsonPropertyName("binaryAttributeSettings")]
    public BinaryAttributeSettings? BinaryAttributeSettings { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the source is a member of a nested group.
    /// </summary>
    [JsonPropertyName("memberOfNestedGroup")]
    public bool MemberOfNestedGroup { get; set; }
}

/// <summary>
/// Represents the data store reference.
/// </summary>
public class DataStoreRef
{
    /// <summary>
    /// Gets or sets the ID of the data store.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the location of the data store.
    /// </summary>
    [JsonPropertyName("location")]
    public string? Location { get; set; }
}

/// <summary>
/// Represents the binary attribute settings.
/// </summary>
public class BinaryAttributeSettings
{
    /// <summary>
    /// Gets or sets the object GUID.
    /// </summary>
    [JsonPropertyName("objectGUID")]
    public BinaryAttribute? ObjectGUID { get; set; }

    /// <summary>
    /// Gets or sets the object SID.
    /// </summary>
    [JsonPropertyName("objectSid")]
    public BinaryAttribute? ObjectSID { get; set; }
}

/// <summary>
/// Represents a binary attribute.
/// </summary>
public class BinaryAttribute
{
    /// <summary>
    /// Gets or sets the binary encoding for the attribute.
    /// </summary>
    [JsonPropertyName("binaryEncoding")]
    public string? BinaryEncoding { get; set; }
}

/// <summary>
/// Represents the attribute contract fulfillment.
/// </summary>
public class OidcAttributeContractFulfillment
{
    /// <summary>
    /// Gets or sets the source of the attribute contract.
    /// </summary>
    [JsonPropertyName("source")]
    public AttributeSourceInfo? Source { get; set; }

    /// <summary>
    /// Gets or sets the value of the attribute.
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; set; }
}

/// <summary>
/// Represents the attribute source information.
/// </summary>
public class AttributeSourceInfo
{
    /// <summary>
    /// Gets or sets the type of the attribute source.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the ID of the attribute source.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}

/// <summary>
/// Represents the OIDC issuance criteria.
/// </summary>
public class OidcIssuanceCriteria
{
    /// <summary>
    /// Gets or sets the list of conditional criteria.
    /// </summary>
    [JsonPropertyName("conditionalCriteria")]
    public List<IssuanceCriteriaOidcObject>? ConditionalCriteria { get; set; }

    /// <summary>
    /// Gets or sets the list of expression criteria.
    /// </summary>
    [JsonPropertyName("expressionCriteria")]
    public List<IssuanceCriteriaOidcObject>? ExpressionCriteria { get; set; }
}

/// <summary>
/// Represents the issuance criteria object.
/// </summary>
public class IssuanceCriteriaOidcObject
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

/// <summary>
/// Represents the reference to the access token manager.
/// </summary>
public class AccessTokenManagerRef
{
    /// <summary>
    /// Gets or sets the ID of the access token manager.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the location of the access token manager.
    /// </summary>
    [JsonPropertyName("location")]
    public string? Location { get; set; }
}

/// <summary>
/// Represents the scope attribute mappings.
/// </summary>
public class ScopeAttributeMappings
{
    /// <summary>
    /// Gets or sets the profile scope values.
    /// </summary>
    [JsonPropertyName("profile")]
    public ScopeValues? Profile { get; set; }

    /// <summary>
    /// Gets or sets the userinfo scope values.
    /// </summary>
    [JsonPropertyName("userinfo")]
    public ScopeValues? Userinfo { get; set; }
}

/// <summary>
/// Represents the scope values.
/// </summary>
public class ScopeValues
{
    /// <summary>
    /// Gets or sets the list of scope values.
    /// </summary>
    [JsonPropertyName("values")]
    public List<string>? Values { get; set; }
}