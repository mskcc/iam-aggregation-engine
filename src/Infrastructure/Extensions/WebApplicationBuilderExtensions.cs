using Microsoft.EntityFrameworkCore;
using Hangfire;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;
using System.Collections.ObjectModel;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Extensions;

/// <summary>
/// Extension methods for <see cref="WebApplicationBuilder"/>
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Adds logging use serilog.
    /// Documentation can be found here: https://github.com/serilog/serilog-extensions-logging
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddSerilogLogging(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        var apiOptions = builder.Configuration.GetSection(ApiOptions.SectionKey).Get<ApiOptions>();
        ArgumentNullException.ThrowIfNull(apiOptions);

        var connectionString = builder.Configuration.GetConnectionString("sqlConnection");
        var tableName = "Ping_IdentityLinking_Processing_Request_Log";

        var sinkOptions = new MSSqlServerSinkOptions
        {
            TableName = tableName,
            AutoCreateSqlTable = true
        };

        var columnOptions = new ColumnOptions();
        columnOptions.Store.Add(StandardColumn.LogEvent);
        columnOptions.LogEvent.DataLength = -1;
        columnOptions.AdditionalColumns = new Collection<SqlColumn>
        {
            new SqlColumn("RequestId", System.Data.SqlDbType.NVarChar, dataLength: 100),
            new SqlColumn("PingOneUserId", System.Data.SqlDbType.NVarChar, dataLength: 100),
            new SqlColumn("Environment", System.Data.SqlDbType.NVarChar, dataLength: 100),
            new SqlColumn("Status", System.Data.SqlDbType.NVarChar, dataLength: 100),
            new SqlColumn("Detail", System.Data.SqlDbType.NVarChar, dataLength: 500),
            new SqlColumn("Error", System.Data.SqlDbType.NVarChar, dataLength: 500)
        };

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("logs/log-.txt", 
                rollingInterval: RollingInterval.Day, 
                outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}",
                retainedFileCountLimit: apiOptions.LoggingRetentionDays)
            .WriteTo.Logger(lc => lc
                .Filter.ByIncludingOnly(logEvent =>
                    logEvent.Properties.ContainsKey(LoggerContexts.SqlLogger))
                .WriteTo.MSSqlServer(
                    connectionString: connectionString,
                    sinkOptions: sinkOptions,
                    columnOptions: columnOptions)
            )
            .CreateLogger();

        builder.Services.AddLogging(loggingBuilder => {
            loggingBuilder.AddSerilog(dispose: true);
        });

        return builder;
    }

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