using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;


namespace Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;

[Keyless]
public class AzureUsersSource
{
    /// <summary>
    /// Unique identifier for the user in the source system.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Unique identifier for the user in the source system.
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// The user's first name.
    /// </summary>
    public string? GivenName { get; set; }

    /// <summary>
    /// The user's job title.
    /// </summary>
    public string? JobTitle { get; set; }

    /// <summary>
    /// The user's mail address.
    /// </summary>
    public string? Mail { get; set; }

    /// <summary>
    /// The user's mobile phone number.
    /// </summary>
    public string? MobilePhone { get; set; }

    /// <summary>
    /// The user's office location.
    /// </summary>
    public string? OfficeLocation { get; set; }

    /// <summary>
    /// The user's preferred language.
    /// </summary>
    public string? PreferredLanguage { get; set; }

    /// <summary>
    /// The user's surname.
    /// </summary>
    public string? Surname { get; set; }

    /// <summary>
    /// The user's telephone number.
    /// </summary>
    public string? UserPrincipalName { get; set; }

    /// <summary>
    /// The user's account status.
    /// </summary>
    public string? AccountEnabled { get; set; }

    /// <summary>
    /// The user's account status.
    /// </summary>
    public string? EmployeeId { get; set; }

    /// <summary>
    /// The user's account status.
    /// </summary>
    public string? Department { get; set; }

    /// <summary>
    /// The user's account status.
    /// </summary>
    public DateTime? CreatedDateTime { get; set; }

    /// <summary>
    /// The user's account status.
    /// </summary>
    public string? OnPremisesDistinguishedName { get; set; }

    /// <summary>
    /// The user's account status.
    /// </summary>
    public string? OnPremisesDomainName { get; set; }

    /// <summary>
    /// The user's account status.
    /// </summary>
    public DateTime? OnPremisesLastSyncDateTime { get; set; }

    /// <summary>
    /// The user's account status.
    /// </summary>
    public string? OnPremisesSamAccountName { get; set; }

    /// <summary>
    /// The user's account status.
    /// </summary>
    public string? OnPremisesSecurityIdentifier { get; set; }

    /// <summary>
    /// The user's account status.
    /// </summary>
    public string? OnPremisesSyncEnabled { get; set; }

    /// <summary>
    /// The user's account status.
    /// </summary>
    public string? OnPremisesUserPrincipalName { get; set; }

    /// <summary>
    /// The user's account status.
    /// </summary>
    public string? UserType { get; set; }

    /// <summary>
    /// The user's account status.
    /// </summary>
    public string? UsageLocation { get; set; }
}
