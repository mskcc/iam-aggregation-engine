using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;

[Table("IDM_PingID_Certificates_List")]
public class SpConnectionCertificate
{
    /// <summary>
    /// Represents the primary key for the DTO
    /// </summary>
    [Key]   
    public Guid PrimaryKey { get; set; }

    /// <summary>
    /// Represents the PingFederate Entity ID.
    /// </summary>
    [MaxLength(500)]
    public string? EntityId { get; set; }

    /// <summary>
    /// Represents whether this is the primary verification certificate.
    /// </summary>
    public bool PrimaryVerificationCert { get; set; }

    /// <summary>
    /// Represents whether this is the secondary verification certificate.
    /// </summary>
    public bool SecondaryVerificationCert { get; set; }

    /// <summary>
    /// Represents whether this is the encryption certificate.
    /// </summary>
    public bool EncryptionCert { get; set; }

    /// <summary>
    /// Represents whether this certificate is active.
    /// </summary>
    public bool ActiveVerificationCert { get; set; }

    /// <summary>
    /// Represents the ID of the X509 file.
    /// </summary>
    [MaxLength(500)]
    public string? X509FileId { get; set; }

    /// <summary>
    /// Represents the file data of the X509 file.
    /// </summary>
    [MaxLength(5000)]
    public string? FileData { get; set; }

    /// <summary>
    /// Represents the certificate ID.
    /// </summary>
    [MaxLength(500)]
    public string? Id { get; set; }

    /// <summary>
    /// Represents the serial number of the certificate.
    /// </summary>
    [MaxLength(500)]
    public string? SerialNumber { get; set; }

    /// <summary>
    /// Represents the subject DN of the certificate.
    /// </summary>
    [MaxLength(500)]
    public string? SubjectDN { get; set; }

    /// <summary>
    /// Represents the subject alternative names for the certificate.
    /// </summary>
    public List<string>? SubjectAlternativeNames { get; set; }

    /// <summary>
    /// Represents the issuer DN of the certificate.
    /// </summary>
    [MaxLength(500)]
    public string? IssuerDN { get; set; }

    /// <summary>
    /// Represents the certificate validity start date.
    /// </summary>
    public DateTime ValidFrom { get; set; }

    /// <summary>
    /// Represents the certificate expiration date.
    /// </summary>
    public DateTime Expires { get; set; }

    /// <summary>
    /// Represents the key algorithm of the certificate.
    /// </summary>
    [MaxLength(500)]
    public string? KeyAlgorithm { get; set; }

    /// <summary>
    /// Represents the key size of the certificate.
    /// </summary>
    public int KeySize { get; set; }

    /// <summary>
    /// Represents the signature algorithm of the certificate.
    /// </summary>
    [MaxLength(500)]
    public string? SignatureAlgorithm { get; set; }

    /// <summary>
    /// Represents the certificate version.
    /// </summary>
    public int Version { get; set; }
}