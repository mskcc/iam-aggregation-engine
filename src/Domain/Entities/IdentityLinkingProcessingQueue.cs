using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;

[Table("Ping_IdentityLinking_Processing_Request_Queue")]
public class IdentityLinkingProcessingReqeustQueue
{
    /// <summary>
    /// Unique identifier for the entity.
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// The date that the identity linking processing request was put into the queue.
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Type of identity linking processing request put into the queue.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// The date that the identity linking processing request was last attempted.
    /// </summary>
    public DateTime? LastProcessingAttempt { get; set; }

    /// <summary>
    /// Environment of the identities being processed.
    /// </summary>
    public string? Environment { get; set; }

    /// <summary>
    /// Unique identifier for the pingone user in the Identity Linking Processing Queue.
    /// </summary>
    public string? PingOneUserId { get; set; }

    /// <summary>
    /// Unique identifier for the Entra user in the Identity Linking Processing Queue.
    /// </summary>
    public string? EntraObjectId { get; set; }

    /// <summary>
    /// Unique identifier for the pingone user in the Identity Linking Processing Queue.
    /// </summary>
    public string? SamAccountName { get; set; }

    /// <summary>
    /// Unique identifier for the MSK user in the Identity Linking Processing Queue.
    /// </summary>
    public string? EmployeeId { get; set; }

    /// <summary>
    /// The status of the identity linking processing request.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// The number of attempts of the record being processed.
    /// </summary>
    public int? Attempts { get; set; }

    /// <summary>
    /// The source of which the identity linking processing request came from.
    /// </summary>
    public string? Source { get; set; }

    /// <summary>
    /// Idicator of whether the identity linking processing request was processed or not.
    /// </summary>
    public bool IsProcessedSuccessfully { get; set; } = false;
}