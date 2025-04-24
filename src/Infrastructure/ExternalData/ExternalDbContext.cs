using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.ExternalData;

/// <summary>
/// Db context for Ping Federate instance connection.
/// </summary>
public class ExternalDbContext : DbContext
{
    private readonly ILogger<ExternalDbContext> _logger;
    private readonly ApiOptions _apiOptions;

    /// <summary>
    /// Creates an instance of <see cref="ApplicationDbContext"/>
    /// </summary>
    /// <param name="options"></param>
    public ExternalDbContext(
        DbContextOptions<ExternalDbContext> options,
        IOptionsMonitor<ApiOptions> apiOptions,
        ILogger<ExternalDbContext> logger) : base(options) 
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(apiOptions);

        _logger = logger;
        _apiOptions = apiOptions.CurrentValue;
    }
    
    /// <summary>
    /// Gets or sets all of the <see cref="AzureUsersSource"/> entities in the context.
    /// </summary>
    public DbSet<AzureUsersSource> AzureUsersSource => Set<AzureUsersSource>();

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        // Identity Linking Table Dependencies
        modelBuilder.Entity<AzureUsersSource>(entity =>
        {
            entity.HasNoKey();
            entity.ToTable(_apiOptions.AzureUsersSourceTableName);
        });
        
        base.OnModelCreating(modelBuilder);
    }
}