using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;

/// <summary>
/// Entity for Service Now application
/// </summary>
[Table("APM_ServiceNow_Users_Info")]
public class ServiceNowUser
{
    /// <summary>
    /// Represents the primary key for the DTO (Data Transfer Object).
    /// </summary>
    [Key]
    public Guid PrimaryKey { get; set; }

    /// <summary>
    /// Represents the name of the Service Now user.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Represents the employee id of the service now user.
    /// </summary>
    public string? EmployeeId { get; set; }

    /// <summary>
    /// Represents the email of the service now user.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Represents the sys id fof the service now user.
    /// </summary>
    public string? SysId { get; set; }
}