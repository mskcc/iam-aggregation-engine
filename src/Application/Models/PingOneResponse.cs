using System.Text.Json.Serialization;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

/// <summary>
/// Represents the response from PingOne containing links and embedded information.
/// </summary>
public class PingOneResponse
{
    /// <summary>
    /// Gets or sets the links related to the PingOne response.
    /// </summary>
    [JsonPropertyName("_links")]
    public Links? Links { get; set; }

    /// <summary>
    /// Gets or sets the embedded data, including users and devices.
    /// </summary>
    [JsonPropertyName("_embedded")]
    public Embedded? Embedded { get; set; }

    /// <summary>
    /// Gets or sets the mfaEnabled property from the response.
    /// </summary>
    [JsonPropertyName("mfaEnabled")]
    public bool MfaEnabled { get; set; }

    /// <summary>
    /// Gets or sets the enbabled property from the response.
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool UserEnabled { get; set; }
}

/// <summary>
/// Represents a self link with an href property.
/// </summary>
public class SelfLink
{
    /// <summary>
    /// Gets or sets the URL of the self link.
    /// </summary>
    [JsonPropertyName("href")]
    public string?Href { get; set; }
}

/// <summary>
/// Represents a collection of links to resources.
/// </summary>
public class Links
{
    /// <summary>
    /// Gets or sets the link to the resource itself.
    /// </summary>
    [JsonPropertyName("self")]
    public SelfLink? Self { get; set; }

    /// <summary>
    /// Gets or sets the link to the password resource.
    /// </summary>
    [JsonPropertyName("password")]
    public SelfLink? Password { get; set; }

    /// <summary>
    /// Gets or sets the link to set the password.
    /// </summary>
    [JsonPropertyName("password.set")]
    public SelfLink? PasswordSet { get; set; }

    /// <summary>
    /// Gets or sets the link to send verification code to the account.
    /// </summary>
    [JsonPropertyName("account.sendVerificationCode")]
    public SelfLink? AccountSendVerificationCode { get; set; }

    /// <summary>
    /// Gets or sets the link to linked accounts.
    /// </summary>
    [JsonPropertyName("linkedAccounts")]
    public SelfLink? LinkedAccounts { get; set; }

    /// <summary>
    /// Gets or sets the link to check the password.
    /// </summary>
    [JsonPropertyName("password.check")]
    public SelfLink? PasswordCheck { get; set; }

    /// <summary>
    /// Gets or sets the link to reset the password.
    /// </summary>
    [JsonPropertyName("password.reset")]
    public SelfLink? PasswordReset { get; set; }

    /// <summary>
    /// Gets or sets the link to recover the password.
    /// </summary>
    [JsonPropertyName("password.recover")]
    public SelfLink? PasswordRecover { get; set; }
}

/// <summary>
/// Represents the status of the password.
/// </summary>
public class Password
{
    /// <summary>
    /// Gets or sets the status of the password.
    /// </summary>
    [JsonPropertyName("status")]
    public string?Status { get; set; }
}

/// <summary>
/// Represents the embedded data of the response, including users and devices.
/// </summary>
public class Embedded
{
    /// <summary>
    /// Gets or sets the list of PingOne users.
    /// </summary>
    [JsonPropertyName("users")]
    public List<PingOneUser>? Users { get; set; }

    /// <summary>
    /// Gets or sets the list of devices associated with the user.
    /// </summary>
    [JsonPropertyName("devices")]
    public List<Device>? Devices { get; set; }

    /// <summary>
    /// Gets or sets the list of identity providers associated with PingOne.
    /// </summary>
    [JsonPropertyName("identityProviders")]
    public List<PingOneIdentityProvider>? IdentityProviders { get; set; }

    /// <summary>
    /// Gets or sets the list of linked accounts associated with the user.
    /// </summary>
    [JsonPropertyName("linkedAccounts")]
    public List<LinkedAccountJson>? LinkedAccounts { get; set; }
}

/// <summary>
/// Represents a PingOne user.
/// </summary>
public class PingOneUser
{
    /// <summary>
    /// Gets or sets the links related to the PingOne user.
    /// </summary>
    [JsonPropertyName("_links")]
    public Links? Links { get; set; }

    /// <summary>
    /// Gets or sets the embedded data for the user.
    /// </summary>
    [JsonPropertyName("_embedded")]
    public Embedded? Embedded { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    [JsonPropertyName("id")]
    public string?Id { get; set; }

    /// <summary>
    /// Gets or sets the environment information for the user.
    /// </summary>
    [JsonPropertyName("environment")]
    public Environment? Environment { get; set; }

    /// <summary>
    /// Gets or sets the account information for the user.
    /// </summary>
    [JsonPropertyName("account")]
    public Account? Account { get; set; }

    /// <summary>
    /// Gets or sets the creation date of the user.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the user's email.
    /// </summary>
    [JsonPropertyName("email")]
    public string?Email { get; set; }

    /// <summary>
    /// Gets or sets whether the user's email has been verified.
    /// </summary>
    [JsonPropertyName("emailVerified")]
    public bool EmailVerified { get; set; }

    /// <summary>
    /// Gets or sets whether the user is enabled.
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets the identity provider of the user.
    /// </summary>
    [JsonPropertyName("identityProvider")]
    public IdentityProvider? IdentityProvider { get; set; }

    /// <summary>
    /// Gets or sets the lifecycle status of the user.
    /// </summary>
    [JsonPropertyName("lifecycle")]
    public Lifecycle? Lifecycle { get; set; }

    /// <summary>
    /// Gets or sets whether Multi-Factor Authentication (MFA) is enabled for the user.
    /// </summary>
    [JsonPropertyName("mfaEnabled")]
    public bool MfaEnabled { get; set; }

    /// <summary>
    /// Gets or sets the name details of the user.
    /// </summary>
    [JsonPropertyName("name")]
    public Name? Name { get; set; }

    /// <summary>
    /// Gets or sets the population associated with the user.
    /// </summary>
    [JsonPropertyName("population")]
    public Population? Population { get; set; }

    /// <summary>
    /// Gets or sets the last update date of the user.
    /// </summary>
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    [JsonPropertyName("username")]
    public string?Username { get; set; }

    /// <summary>
    /// Gets or sets the verification status of the user.
    /// </summary>
    [JsonPropertyName("verifyStatus")]
    public string?VerifyStatus { get; set; }
}

/// <summary>
/// Represents environment details for the user.
/// </summary>
public class Environment
{
    /// <summary>
    /// Gets or sets the environment ID.
    /// </summary>
    [JsonPropertyName("id")]
    public string?Id { get; set; }
}

/// <summary>
/// Represents account details for the user.
/// </summary>
public class Account
{
    /// <summary>
    /// Gets or sets whether the user can authenticate.
    /// </summary>
    [JsonPropertyName("canAuthenticate")]
    public bool CanAuthenticate { get; set; }

    /// <summary>
    /// Gets or sets the status of the account.
    /// </summary>
    [JsonPropertyName("status")]
    public string?Status { get; set; }
}

/// <summary>
/// Represents identity provider details for the user.
/// </summary>
public class IdentityProvider
{
    /// <summary>
    /// Gets or sets the type of identity provider.
    /// </summary>
    [JsonPropertyName("type")]
    public string?Type { get; set; }
}

/// <summary>
/// Represents the lifecycle status of the user.
/// </summary>
public class Lifecycle
{
    /// <summary>
    /// Gets or sets the status of the user's lifecycle.
    /// </summary>
    [JsonPropertyName("status")]
    public string?Status { get; set; }
}

/// <summary>
/// Represents the name of the user.
/// </summary>
public class Name
{
    /// <summary>
    /// Gets or sets the given name of the user.
    /// </summary>
    [JsonPropertyName("given")]
    public string?Given { get; set; }

    /// <summary>
    /// Gets or sets the family name of the user.
    /// </summary>
    [JsonPropertyName("family")]
    public string?Family { get; set; }
}

/// <summary>
/// Represents the population associated with the user.
/// </summary>
public class Population
{
    /// <summary>
    /// Gets or sets the population ID.
    /// </summary>
    [JsonPropertyName("id")]
    public string?Id { get; set; }
}

/// <summary>
/// Represents the root object that includes links, embedded data, and count/size information.
/// </summary>
public class Root
{
    /// <summary>
    /// Gets or sets the links related to the root response.
    /// </summary>
    [JsonPropertyName("_links")]
    public Links? Links { get; set; }

    /// <summary>
    /// Gets or sets the embedded data, including users and devices.
    /// </summary>
    [JsonPropertyName("_embedded")]
    public Embedded? Embedded { get; set; }

    /// <summary>
    /// Gets or sets the count of items in the response.
    /// </summary>
    [JsonPropertyName("count")]
    public int Count { get; set; }

    /// <summary>
    /// Gets or sets the size of the response data.
    /// </summary>
    [JsonPropertyName("size")]
    public int Size { get; set; }
}

/// <summary>
/// Represents a hyperlink.
/// </summary>
public class Href
{
    /// <summary>
    /// Gets or sets the href link.
    /// </summary>
    [JsonPropertyName("href")]
    public string?HrefValue { get; set; }
}