using System.Text.Json.Serialization;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

/// <summary>
/// Represents device details.
/// </summary>
public class Device
{
    /// <summary>
    /// Gets or sets the links related to the device.
    /// </summary>
    [JsonPropertyName("_links")]
    public DeviceLinks? Links { get; set; }

    /// <summary>
    /// Gets or sets the device ID.
    /// </summary>
    [JsonPropertyName("id")]
    public string?Id { get; set; }

    /// <summary>
    /// Gets or sets the environment associated with the device.
    /// </summary>
    [JsonPropertyName("environment")]
    public Environment? Environment { get; set; }

    /// <summary>
    /// Gets or sets the user associated with the device.
    /// </summary>
    [JsonPropertyName("user")]
    public DeviceUser? User { get; set; }

    /// <summary>
    /// Gets or sets the device type (e.g., mobile, desktop).
    /// </summary>
    [JsonPropertyName("type")]
    public string?Type { get; set; }

    /// <summary>
    /// Gets or sets the nickname for the device.
    /// </summary>
    [JsonPropertyName("nickname")]
    public string?Nickname { get; set; }

    /// <summary>
    /// Gets or sets the status of the device.
    /// </summary>
    [JsonPropertyName("status")]
    public string?Status { get; set; }

    /// <summary>
    /// Gets or sets the creation date of the device.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last update date of the device.
    /// </summary>
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the operating system details for the device.
    /// </summary>
    [JsonPropertyName("os")]
    public Os? Os { get; set; }

    /// <summary>
    /// Gets or sets the model details for the device.
    /// </summary>
    [JsonPropertyName("model")]
    public Model? Model { get; set; }

    /// <summary>
    /// Gets or sets the application details for the device.
    /// </summary>
    [JsonPropertyName("application")]
    public Application? Application { get; set; }

    /// <summary>
    /// Gets or sets whether push notifications are enabled on the device.
    /// </summary>
    [JsonPropertyName("pushEnabled")]
    public bool PushEnabled { get; set; }

    /// <summary>
    /// Gets or sets the SDK version.
    /// </summary>
    [JsonPropertyName("sdkVersion")]
    public string?SdkVersion { get; set; }

    /// <summary>
    /// Gets or sets whether the device is rooted.
    /// </summary>
    [JsonPropertyName("rooted")]
    public bool Rooted { get; set; }

    /// <summary>
    /// Gets or sets whether the device lock is enabled.
    /// </summary>
    [JsonPropertyName("lockEnabled")]
    public bool LockEnabled { get; set; }
}

/// <summary>
/// Represents the links for a device.
/// </summary>
public class DeviceLinks
{
    /// <summary>
    /// Gets or sets the self link for the device.
    /// </summary>
    [JsonPropertyName("self")]
    public Href? Self { get; set; }

    /// <summary>
    /// Gets or sets the environment link for the device.
    /// </summary>
    [JsonPropertyName("environment")]
    public Href? Environment { get; set; }

    /// <summary>
    /// Gets or sets the user link for the device.
    /// </summary>
    [JsonPropertyName("user")]
    public Href? User { get; set; }
}

/// <summary>
/// Represents the user associated with the device.
/// </summary>
public class DeviceUser
{
    /// <summary>
    /// Gets or sets the user ID associated with the device.
    /// </summary>
    [JsonPropertyName("id")]
    public string?Id { get; set; }
}

/// <summary>
/// Represents the operating system details of a device.
/// </summary>
public class Os
{
    /// <summary>
    /// Gets or sets the version of the operating system.
    /// </summary>
    [JsonPropertyName("version")]
    public string?Version { get; set; }

    /// <summary>
    /// Gets or sets the type of the operating system (e.g., Android, iOS).
    /// </summary>
    [JsonPropertyName("type")]
    public string?Type { get; set; }
}

/// <summary>
/// Represents the model details of the device.
/// </summary>
public class Model
{
    /// <summary>
    /// Gets or sets the name of the device model.
    /// </summary>
    [JsonPropertyName("name")]
    public string?Name { get; set; }
}

/// <summary>
/// Represents the application details for the device.
/// </summary>
public class Application
{
    /// <summary>
    /// Gets or sets the version of the application.
    /// </summary>
    [JsonPropertyName("version")]
    public string?Version { get; set; }

    /// <summary>
    /// Gets or sets whether the application is in the push sandbox environment.
    /// </summary>
    [JsonPropertyName("pushSandbox")]
    public bool PushSandbox { get; set; }
}