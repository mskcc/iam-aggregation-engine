using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Data;

/// <summary>
/// Db context for Ping Federate instance connection.
/// </summary>
public class ApplicationDbContext : DbContext
{
    private readonly ILogger<ApplicationDbContext> _logger;

    /// <summary>
    /// Creates an instance of <see cref="ApplicationDbContext"/>
    /// </summary>
    /// <param name="options"></param>
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ILogger<ApplicationDbContext> logger) : base(options) 
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(logger);

        _logger = logger;
    }
    
    /// <summary>
    /// Gets or sets all of the <see cref="SpConnectionCertificate"/> entities in the context.
    /// </summary>
    public DbSet<SpConnectionCertificate> PingFederateSamlCertificates => Set<SpConnectionCertificate>();

    /// <summary>
    /// Gets or sets all of the <see cref="SpConnection"/> entities in the context.
    /// </summary>
    public DbSet<SpConnection> PingFederateSamlConnections => Set<SpConnection>();

    /// <summary>
    /// Gets or sets all of the <see cref="OidcClient"/> entities in the context.
    /// </summary>
    public DbSet<OidcClient> PingFederateOidcConnections => Set<OidcClient>();

    /// <summary>
    /// Gets or sets all of the <see cref="LegacyConnection"/> entities in the context.
    /// </summary>
    public DbSet<LegacyConnection> LegacyConnections => Set<LegacyConnection>();

    /// <summary>
    /// Gets or sets all of the <see cref="ServiceNowApplications"/> entities in the context.
    /// </summary>
    public DbSet<ServiceNowApplication> ServiceNowApplications => Set<ServiceNowApplication>();

    /// <summary>
    /// Gets or sets all of the <see cref="ServiceNowUser"/> entities in the context.
    /// </summary>
    public DbSet<ServiceNowUser> ServiceNowUsers => Set<ServiceNowUser>();

    /// <summary>
    /// Gets or sets all of the <see cref="ServiceNowGroup"/> entities in the context.
    /// </summary>
    public DbSet<AzureUsersSource> AzureUsersSources => Set<AzureUsersSource>();

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        // Certificate Table
        modelBuilder.Entity<SpConnectionCertificate>()
            .Property(p => p.ValidFrom)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        modelBuilder.Entity<SpConnectionCertificate>()
            .Property(p => p.Expires)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        modelBuilder.Entity<SpConnectionCertificate>()
            .Property(p => p.PrimaryVerificationCert)
            .HasConversion(
                 v => v ? "true" : "false",
                v => v == "true");
        modelBuilder.Entity<SpConnectionCertificate>()
            .Property(p => p.SecondaryVerificationCert)
            .HasConversion(
                 v => v ? "true" : "false",
                v => v == "true");
        modelBuilder.Entity<SpConnectionCertificate>()
            .Property(p => p.EncryptionCert)
            .HasConversion(
                 v => v ? "true" : "false",
                v => v == "true");
        modelBuilder.Entity<SpConnectionCertificate>()
            .Property(p => p.ActiveVerificationCert)
            .HasConversion(
                 v => v ? "true" : "false",
                v => v == "true");

        // Legacy Table
        modelBuilder.Entity<LegacyConnection>()
            .Property(p => p.ModificationDate)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        modelBuilder.Entity<LegacyConnection>()
            .Property(p => p.CreationDate)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        modelBuilder.Entity<LegacyConnection>()
            .Property(p => p.IsActive)
            .HasConversion(
                 v => v ? "true" : "false",
                v => v == "true");

        // OIDC Table
        modelBuilder.Entity<OidcClient>()
            .Property(p => p.ModificationDate)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        modelBuilder.Entity<OidcClient>()
            .Property(p => p.CreationDate)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        
        // SAML Table
        modelBuilder.Entity<SpConnection>()
            .Property(p => p.ModificationDate)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)); 
        modelBuilder.Entity<SpConnection>()
            .Property(p => p.CreationDate)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        // Map SAML Table Navigation
        modelBuilder.Entity<SpConnection>()
            .HasMany(s => s.AttributeContractFulfillment)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OidcClient>()
            .HasMany(s => s.AttributeContractFulfillment)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        // Readonly Identity Engine Tables
        modelBuilder.Entity<AzureUsersSource>(entity =>
        {
            entity.HasNoKey();
        });
        
        base.OnModelCreating(modelBuilder);
    }
}