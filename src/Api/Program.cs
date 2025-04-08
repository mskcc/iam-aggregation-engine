using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Extensions;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Extensions;
using Mskcc.Tools.Idp.ConnectionsAggregator.Api.Endpoints;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Pages;

var builder = WebApplication.CreateBuilder(args);

// Add logging
builder.AddSerilogLogging();

// Add configurations
builder.AddConfiguration();

// Add OpenAPI documentation
builder.AddOpenApiDocumentation();

// Add Health Checks
builder.AddHealthChecks();

// Add Hangfire
builder.AddHangfire();

// Add Http Clients
builder.AddPingFederateHttpClients();
builder.AddServiceNowHttpClients();

// Add Mediator and Colleagues
builder.AddMediator();

// Add Services
builder.AddServices();

// Add Data Persistence
builder.AddDataPersistence();

// Add Access Control
builder.AddAccessControl();

// Add problems in API responses
builder.AddApiProblems();

var app = builder.Build();

// Use exception handler
app.UseExceptionHandler();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseScalarOpenApiDocumentation();
}

// Use HTTP->HTTPS redirection
app.UseHttpsRedirection();

// Use Access Control
app.UseAccessControl();

// Use Hangfire dashboard
app.UseHangfire();

// Map Ping Federate connections endpoints for the API.
app.MapPingFederateSamlConnectionsEndpoints();
app.MapPingFederateOidcConnectionsEndpoints();
app.MapPingFederateLegacyConnectionsEndpoints();
app.MapServiceNowEndpoints();
app.MapSignInAndSignOutPages();

// Map Health Checks endpoint
app.MapHealthChecksEndpoints();

// Map Authentication endpoints
app.MapGroup("/authentication")
    .MapLoginAndLogout();

// Run database migrations.
app.RunDatabaseMigrations();

app.Run();