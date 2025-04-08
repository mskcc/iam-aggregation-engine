using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;

[Table("IDM_PingID_Attributes_List")]
public class AttributeContractFulfillment
{
    /// <summary>
    /// Represents the primary key for the DTO
    /// </summary>
    [Key]   
    public Guid PrimaryKey { get; set; }

    /// <summary>
    /// Connection Id for joining tables
    /// </summary>
    public string? ConnectionId { get; set; }

    /// <summary>
    /// Name of the claim
    /// </summary>
    public string? ClaimName { get; set; }

    /// <summary>
    /// value of the claim
    /// </summary>
    public string? ClaimValue { get; set; }

    /// <summary>
    /// Type of claim
    /// </summary>
    public string? ClaimType { get; set; }

    /// <summary>
    /// Type of connection (SAML, OIDC, etc)
    /// </summary>
    public string ConnectionType { get; set; } = string.Empty;
}
