using Microsoft.EntityFrameworkCore;
using Hangfire;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Extensions;

/// <summary>
/// Extension methods for <see cref="WebApplicationBuilder"/>
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Add health checks.
    /// </summary>
    /// <returns><see cref="WebApplicationBuilder"/></returns>
    public static WebApplicationBuilder AddHealthChecks(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        var hangFireConnectionString = builder.Configuration.GetConnectionString("HangfireConnection");
        ArgumentNullException.ThrowIfNull(hangFireConnectionString);
        var sqlConnectionString = builder.Configuration.GetConnectionString("sqlConnection");
        ArgumentNullException.ThrowIfNull(sqlConnectionString);

        builder.Services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>("SSO Connections Tables", HealthStatus.Unhealthy)
            .AddSqlServer(hangFireConnectionString, name: "Hangfire Database")
            .AddSqlServer(sqlConnectionString, name: "SSO Connections Database");

        return builder;
    }

    /// <summary>
    /// Add Hangfire support for background jobs processing.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns><see cref="WebApplicationBuilder"/></returns>
    public static WebApplicationBuilder AddHangfire(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        var connectionString = builder.Configuration.GetConnectionString("HangfireConnection");

        builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(connectionString));

        builder.Services.AddHangfireServer();

        return builder;
    }

    /// <summary>
    /// Add data persistence with SQL Server.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddDataPersistence(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        var connectionString = builder.Configuration.GetConnectionString("SqlConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(opt => 
            opt.UseSqlServer(connectionString));

        return builder;
    }
}