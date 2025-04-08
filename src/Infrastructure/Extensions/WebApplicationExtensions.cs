using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Data;
using Scalar.AspNetCore;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Extensions;

/// <summary>
/// Extension methods for <see cref="WebApplication"/>
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Runs migrations against connected SQL server instance.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication RunDatabaseMigrations(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        try
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            return app;
        }
        catch (Exception e)
        {
            var logger = app.Services.GetService<ILogger<WebApplication>>();
            logger?.LogError("{ExceptionMessage}", e.Message);

            return app;
        }
    }

    /// <summary>
    /// Adds scalar documentation
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication UseScalarOpenApiDocumentation(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);
        
        // See ms supported api documentation technologies: 
        // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/openapi/using-openapi-documents?view=aspnetcore-9.0#use-scalar-for-interactive-api-documentation
        // See Scalar documentation: https://github.com/scalar/scalar/blob/main/packages/scalar.aspnetcore/README.md
        app.MapOpenApi();
        app.MapScalarApiReference();
        return app;
    }
}