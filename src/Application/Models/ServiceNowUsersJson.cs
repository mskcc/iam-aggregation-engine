using System;
using System.Text.Json.Serialization;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

/// <summary>
/// Model used for deserializing Service Now users endpoint responses.
/// </summary>
public class ServiceNowUsersJson
{
    /// <summary>
    /// Gets or sets the calendar integration status.
    /// </summary>
    [JsonPropertyName("calendar_integration")]
    public string? CalendarIntegration { get; set; }

    /// <summary>
    /// Gets or sets the country of the user.
    /// </summary>
    [JsonPropertyName("country")]
    public string? Country { get; set; }

    /// <summary>
    /// Gets or sets the last position update of the user.
    /// </summary>
    [JsonPropertyName("last_position_update")]
    public string? LastPositionUpdate { get; set; }

    /// <summary>
    /// Gets or sets the last login time of the user.
    /// </summary>
    [JsonPropertyName("last_login_time")]
    public string? LastLoginTime { get; set; }

    /// <summary>
    /// Gets or sets the attestation status of the user.
    /// </summary>
    [JsonPropertyName("u_attestation")]
    public string? UAttestation { get; set; }

    /// <summary>
    /// Gets or sets the report system ID of the user.
    /// </summary>
    [JsonPropertyName("u_rpt_sys_id")]
    public string? URptSysId { get; set; }

    /// <summary>
    /// Gets or sets the source of the user.
    /// </summary>
    [JsonPropertyName("source")]
    public string? Source { get; set; }

    /// <summary>
    /// Gets or sets the last system update time.
    /// </summary>
    [JsonPropertyName("sys_updated_on")]
    public string? SysUpdatedOn { get; set; }

    /// <summary>
    /// Gets or sets the building of the user.
    /// </summary>
    [JsonPropertyName("building")]
    public string? Building { get; set; }

    /// <summary>
    /// Gets or sets whether the user has web service access only.
    /// </summary>
    [JsonPropertyName("web_service_access_only")]
    public string? WebServiceAccessOnly { get; set; }

    /// <summary>
    /// Gets or sets the notification level for the user.
    /// </summary>
    [JsonPropertyName("notification")]
    public string? Notification { get; set; }

    /// <summary>
    /// Gets or sets the availability for round-robin scheduling.
    /// </summary>
    [JsonPropertyName("u_avlbl_round_robin")]
    public string? UAvlblRoundRobin { get; set; }

    /// <summary>
    /// Gets or sets whether multifactor authentication is enabled.
    /// </summary>
    [JsonPropertyName("enable_multifactor_authn")]
    public string? EnableMultifactorAuthn { get; set; }

    /// <summary>
    /// Gets or sets the name of the user who updated the system.
    /// </summary>
    [JsonPropertyName("sys_updated_by")]
    public string? SysUpdatedBy { get; set; }

    /// <summary>
    /// Gets or sets the SSO source for the user.
    /// </summary>
    [JsonPropertyName("sso_source")]
    public string? SsoSource { get; set; }

    /// <summary>
    /// Gets or sets the system creation timestamp.
    /// </summary>
    [JsonPropertyName("sys_created_on")]
    public string? SysCreatedOn { get; set; }

    /// <summary>
    /// Gets or sets the agent status.
    /// </summary>
    [JsonPropertyName("agent_status")]
    public string? AgentStatus { get; set; }

    /// <summary>
    /// Gets or sets the system domain information.
    /// </summary>
    [JsonPropertyName("sys_domain")]
    public SysDomain? SysDomain { get; set; }

    /// <summary>
    /// Gets or sets the state of the user.
    /// </summary>
    [JsonPropertyName("state")]
    public string? State { get; set; }

    /// <summary>
    /// Gets or sets whether the user is marked as VIP.
    /// </summary>
    [JsonPropertyName("vip")]
    public string? Vip { get; set; }

    /// <summary>
    /// Gets or sets the user who created the system entry.
    /// </summary>
    [JsonPropertyName("sys_created_by")]
    public string? SysCreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the longitude associated with the user.
    /// </summary>
    [JsonPropertyName("longitude")]
    public string? Longitude { get; set; }

    /// <summary>
    /// Gets or sets the zip code of the user.
    /// </summary>
    [JsonPropertyName("zip")]
    public string? Zip { get; set; }

    /// <summary>
    /// Gets or sets the home phone number of the user.
    /// </summary>
    [JsonPropertyName("home_phone")]
    public string? HomePhone { get; set; }

    /// <summary>
    /// Gets or sets the time format preference of the user.
    /// </summary>
    [JsonPropertyName("time_format")]
    public string? TimeFormat { get; set; }

    /// <summary>
    /// Gets or sets the last login timestamp.
    /// </summary>
    [JsonPropertyName("last_login")]
    public string? LastLogin { get; set; }

    /// <summary>
    /// Gets or sets whether geolocation is tracked for the user.
    /// </summary>
    [JsonPropertyName("geolocation_tracked")]
    public string? GeolocationTracked { get; set; }

    /// <summary>
    /// Gets or sets whether the user is active.
    /// </summary>
    [JsonPropertyName("active")]
    public string? Active { get; set; }

    /// <summary>
    /// Gets or sets the average daily FTE (Full-Time Equivalent) of the user.
    /// </summary>
    [JsonPropertyName("average_daily_fte")]
    public string? AverageDailyFte { get; set; }

    /// <summary>
    /// Gets or sets the last assigned task for the user.
    /// </summary>
    [JsonPropertyName("u_last_assigned_task")]
    public string? ULastAssignedTask { get; set; }

    /// <summary>
    /// Gets or sets the system domain path.
    /// </summary>
    [JsonPropertyName("sys_domain_path")]
    public string? SysDomainPath { get; set; }

    /// <summary>
    /// Gets or sets the transaction log of the user.
    /// </summary>
    [JsonPropertyName("transaction_log")]
    public string? TransactionLog { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the user.
    /// </summary>
    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    /// <summary>
    /// Gets or sets the full name of the user.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the employee number of the user.
    /// </summary>
    [JsonPropertyName("employee_number")]
    public string? EmployeeNumber { get; set; }

    /// <summary>
    /// Gets or sets whether the user is a teleworker.
    /// </summary>
    [JsonPropertyName("u_teleworker")]
    public string? UTeleworker { get; set; }

    /// <summary>
    /// Gets or sets the gender of the user.
    /// </summary>
    [JsonPropertyName("gender")]
    public string? Gender { get; set; }

    /// <summary>
    /// Gets or sets the city of the user.
    /// </summary>
    [JsonPropertyName("city")]
    public string? City { get; set; }

    /// <summary>
    /// Gets or sets the number of failed login attempts for the user.
    /// </summary>
    [JsonPropertyName("failed_attempts")]
    public string? FailedAttempts { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    [JsonPropertyName("user_name")]
    public string? UserName { get; set; }

    /// <summary>
    /// Gets or sets the latitude associated with the user.
    /// </summary>
    [JsonPropertyName("latitude")]
    public string? Latitude { get; set; }

    /// <summary>
    /// Gets or sets the title of the user.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the system class name for the user.
    /// </summary>
    [JsonPropertyName("sys_class_name")]
    public string? SysClassName { get; set; }

    /// <summary>
    /// Gets or sets the system ID for the user.
    /// </summary>
    [JsonPropertyName("sys_id")]
    public string? SysId { get; set; }

    /// <summary>
    /// Gets or sets the datetime of the last assigned task.
    /// </summary>
    [JsonPropertyName("u_last_assigned_task_datetime")]
    public string? ULastAssignedTaskDatetime { get; set; }

    /// <summary>
    /// Gets or sets the federated ID of the user.
    /// </summary>
    [JsonPropertyName("federated_id")]
    public string? FederatedId { get; set; }

    /// <summary>
    /// Gets or sets the internal integration user status.
    /// </summary>
    [JsonPropertyName("internal_integration_user")]
    public string? InternalIntegrationUser { get; set; }

    /// <summary>
    /// Gets or sets the mobile phone number of the user.
    /// </summary>
    [JsonPropertyName("mobile_phone")]
    public string? MobilePhone { get; set; }

    /// <summary>
    /// Gets or sets the street address of the user.
    /// </summary>
    [JsonPropertyName("street")]
    public string? Street { get; set; }

    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the introduction or description of the user.
    /// </summary>
    [JsonPropertyName("introduction")]
    public string? Introduction { get; set; }

    /// <summary>
    /// Gets or sets the preferred language of the user.
    /// </summary>
    [JsonPropertyName("preferred_language")]
    public string? PreferredLanguage { get; set; }

    /// <summary>
    /// Gets or sets the business criticality of the user.
    /// </summary>
    [JsonPropertyName("business_criticality")]
    public string? BusinessCriticality { get; set; }

    /// <summary>
    /// Gets or sets whether the user is a manager.
    /// </summary>
    [JsonPropertyName("u_is_manager")]
    public string? UIsManager { get; set; }

    /// <summary>
    /// Gets or sets the floor where the user is located.
    /// </summary>
    [JsonPropertyName("u_floor")]
    public string? UFloor { get; set; }

    /// <summary>
    /// Gets or sets the system modification count.
    /// </summary>
    [JsonPropertyName("sys_mod_count")]
    public string? SysModCount { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the photo URL of the user.
    /// </summary>
    [JsonPropertyName("photo")]
    public string? Photo { get; set; }

    /// <summary>
    /// Gets or sets the job family group of the user.
    /// </summary>
    [JsonPropertyName("u_job_family_group")]
    public string? UJobFamilyGroup { get; set; }

    /// <summary>
    /// Gets or sets the avatar URL for the user.
    /// </summary>
    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }

    /// <summary>
    /// Gets or sets the middle name of the user.
    /// </summary>
    [JsonPropertyName("middle_name")]
    public string? MiddleName { get; set; }

    /// <summary>
    /// Gets or sets system tags related to the user.
    /// </summary>
    [JsonPropertyName("sys_tags")]
    public string? SysTags { get; set; }

    /// <summary>
    /// Gets or sets the time zone of the user.
    /// </summary>
    [JsonPropertyName("time_zone")]
    public string? TimeZone { get; set; }

    /// <summary>
    /// Gets or sets the suffix of the user’s name.
    /// </summary>
    [JsonPropertyName("u_suffix")]
    public string? USuffix { get; set; }

    /// <summary>
    /// Gets or sets the attestation date of the user.
    /// </summary>
    [JsonPropertyName("u_attestation_date")]
    public string? UAttestationDate { get; set; }

    /// <summary>
    /// Gets or sets the job family of the user.
    /// </summary>
    [JsonPropertyName("u_job_family")]
    public string? UJobFamily { get; set; }

    /// <summary>
    /// Gets or sets the room assigned to the user.
    /// </summary>
    [JsonPropertyName("u_room")]
    public string? URoom { get; set; }

    /// <summary>
    /// Gets or sets whether the user is on schedule.
    /// </summary>
    [JsonPropertyName("on_schedule")]
    public string? OnSchedule { get; set; }

    /// <summary>
    /// Gets or sets the correlation ID of the user.
    /// </summary>
    [JsonPropertyName("correlation_id")]
    public string? CorrelationId { get; set; }

    /// <summary>
    /// Gets or sets the date format for the user.
    /// </summary>
    [JsonPropertyName("date_format")]
    public string? DateFormat { get; set; }
}

/// <summary>
/// Represents the system domain of the user.
/// </summary>
public class SysDomain
{
    /// <summary>
    /// Gets or sets the link for the system domain.
    /// </summary>
    [JsonPropertyName("link")]
    public string? Link { get; set; }

    /// <summary>
    /// Gets or sets the value of the system domain.
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; set; }
}

/// <summary>
/// Represents the cost center of the user.
/// </summary>
public class CostCenter
{
    /// <summary>
    /// Gets or sets the link for the cost center.
    /// </summary>
    [JsonPropertyName("link")]
    public string? Link { get; set; }

    /// <summary>
    /// Gets or sets the value of the cost center.
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; set; }
}

/// <summary>
/// Represents the department of the user.
/// </summary>
public class Department
{
    /// <summary>
    /// Gets or sets the link for the department.
    /// </summary>
    [JsonPropertyName("link")]
    public string? Link { get; set; }

    /// <summary>
    /// Gets or sets the value of the department.
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; set; }
}

/// <summary>
/// Represents the manager of the user.
/// </summary>
public class Manager
{
    /// <summary>
    /// Gets or sets the link for the manager.
    /// </summary>
    [JsonPropertyName("link")]
    public string? Link { get; set; }

    /// <summary>
    /// Gets or sets the value of the manager.
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; set; }
}

/// <summary>
/// Represents the location of the user.
/// </summary>
public class Location
{
    /// <summary>
    /// Gets or sets the link for the location.
    /// </summary>
    [JsonPropertyName("link")]
    public string? Link { get; set; }

    /// <summary>
    /// Gets or sets the value of the location.
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; set; }
}
