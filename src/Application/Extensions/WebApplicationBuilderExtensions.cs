using Microsoft.Extensions.Options;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Http;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PingFederate;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.UriHelper;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.ResourceState;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PagedResponse;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Exceptions;
using Microsoft.IdentityModel.Tokens;
using Flurl;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.UseCases;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.Pagination;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;
using Serilog;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.Configuration;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.ServiceNow;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.IdentityLinkingService;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Extensions;

/// <summary>
/// Extension methods for <see cref="WebApplicationBuilder"/>
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Add Mediator and Colleagues.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddMediator(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        // Register Mediator
        builder.Services.AddSingleton<IMediator, Mediator>();

        // Register Colleagues
        builder.Services.AddKeyedTransient<IColleague, PurgeOidcConnectionColleague>(ServiceKeys.PurgeOidcConnectionColleague);
        builder.Services.AddKeyedTransient<IColleague, AggregateOidcConnectionsColleague>(ServiceKeys.AggregateOidcConnectionsColleague);
        builder.Services.AddKeyedTransient<IColleague, GetOidcConnectionsColleague>(ServiceKeys.GetOidcConnectionsColleague);

        builder.Services.AddKeyedTransient<IColleague, PurgeSamlConnectionColleague>(ServiceKeys.PurgeSamlConnectionsColleague);
        builder.Services.AddKeyedTransient<IColleague, AggregateSamlConnectionsColleague>(ServiceKeys.AggregateSamlConnectionsColleague);
        builder.Services.AddKeyedTransient<IColleague, GetSamlConnectionsColleague>(ServiceKeys.GetSamlConnectionsColleague);

        builder.Services.AddKeyedTransient<IColleague, PurgeLegacyConnectionColleague>(ServiceKeys.PurgeLegacyConnectionsColleague);
        builder.Services.AddKeyedTransient<IColleague, AggregateLegacyConnectionsColleague>(ServiceKeys.AggregateLegacyConnectionsColleague);
        builder.Services.AddKeyedTransient<IColleague, GetLegacyConnectionsColleague>(ServiceKeys.GetLegacyConnectionsColleague);
        builder.Services.AddKeyedTransient<IColleague, SearchLegacyConnectionsColleague>(ServiceKeys.SearchGetLegacyConnectionsColleague);

        builder.Services.AddKeyedTransient<IColleague, AggregateServiceNowApplicationsColleague>(ServiceKeys.AggregateServiceNowApplicationsColleague);
        builder.Services.AddKeyedTransient<IColleague, GetServiceNowApplicationsColleague>(ServiceKeys.GetServiceNowApplicationsColleague);
        builder.Services.AddKeyedTransient<IColleague, PurgeServiceNowApplicationsColleague>(ServiceKeys.PurgeServiceNowApplicationsColleague);

        builder.Services.AddKeyedTransient<IColleague, AggregateServiceNowUsersColleague>(ServiceKeys.AggregateServiceNowUsersColleague);
        builder.Services.AddKeyedTransient<IColleague, GetServiceNowUsersColleague>(ServiceKeys.GetServiceNowUsersColleague);
        builder.Services.AddKeyedTransient<IColleague, PurgeServiceNowUsersColleague>(ServiceKeys.PurgeServiceNowUsersColleague);

        builder.Services.AddKeyedTransient<IColleague, IdentityLinkingColleague>(ServiceKeys.IdentityLinkingColleague);

        // Register Notifications
        builder.Services.AddTransient<NotifyPurgeOidcColleague>();
        builder.Services.AddTransient<NotifyAggregateOidcConnectionsColleague>();
        builder.Services.AddTransient<NotifyGetOidcConnectionsColleague>();
        
        builder.Services.AddTransient<NotifyPurgeSamlColleague>();
        builder.Services.AddTransient<NotifyAggregateSamlConnectionsColleague>();
        builder.Services.AddTransient<NotifyGetSamlConnectionsColleague>();

        builder.Services.AddTransient<NotifyPurgeLegacyColleague>();
        builder.Services.AddTransient<NotifyGetLegacyConnectionsColleague>();
        builder.Services.AddTransient<NotifySearchLegacyConnectionsColleague>();
        builder.Services.AddTransient<NotifyAggregateLegacyConnectionsColleague>();

        builder.Services.AddTransient<NotifyAggregateServiceNowApplicationsColleague>();
        builder.Services.AddTransient<NotifyGetServiceNowApplicationsColleague>();
        builder.Services.AddTransient<NotifyPurgeServiceNowApplicationsColleague>();

        builder.Services.AddTransient<NotifyAggregateServiceNowUsersColleague>();
        builder.Services.AddTransient<NotifyGetServiceNowUsersColleague>();
        builder.Services.AddTransient<NotifyPurgeServiceNowUsersColleague>();

        builder.Services.AddTransient<NotifyIdentityLinkingColleague>();

        return builder;
    }

    /// <summary>
    /// Add API problems.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddApiProblems(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Instance = 
                    $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

                context.ProblemDetails.Extensions.TryAdd("requestId", 
                    context.HttpContext.TraceIdentifier);

                var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                context.ProblemDetails.Extensions.TryAdd("traceId", 
                    activity?.Id);
            };
        });

        builder.Services.AddExceptionHandler<ProblemExceptionHandler>();

        return builder;
    }

    /// <summary>
    /// Add Ping Federate HTTP clients and authn/authz HTTP client handlers.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns><see cref="WebApplicationBuilder"/></returns>
    public static WebApplicationBuilder AddPingFederateHttpClients(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddOptionsWithValidateOnStart<PingFederateOptions>()
            .BindConfiguration(PingFederateOptions.SectionKey);

        builder.Services.AddTransient<PingFederateHttpHandler>()
            .AddHttpClient(HttpClientNames.PingFederateClient, (services, client) =>
            {
                var pingFederateOptionsMonitor = services.GetRequiredService<IOptionsMonitor<PingFederateOptions>>();
                var pingFederateOptions = pingFederateOptionsMonitor.CurrentValue;

                client.BaseAddress = new Uri(pingFederateOptions.BaseUrl);
            })
            .ConfigurePrimaryHttpMessageHandler((services) =>
            {
                var pingFederateOptionsMonitor = services.GetRequiredService<IOptionsMonitor<PingFederateOptions>>();
                var pingFederateOptions = pingFederateOptionsMonitor.CurrentValue;
                var handler = new HttpClientHandler();

                if (pingFederateOptions.TrustServerSslCertificate is true)
                {
                    handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                }

                return handler;
            })
            .AddHttpMessageHandler<PingFederateHttpHandler>();

        return builder;
    }

    /// <summary>
    /// Add Service Now HTTP clients and authn/authz HTTP client handlers.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns><see cref="WebApplicationBuilder"/></returns>
    public static WebApplicationBuilder AddServiceNowHttpClients(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddOptionsWithValidateOnStart<ServiceNowOptions>()
            .BindConfiguration(ServiceNowOptions.SectionKey);

        builder.Services.AddTransient<ServiceNowHttpHandler>()
            .AddHttpClient(HttpClientNames.ServiceNowClient, (services, client) =>
            {
                var serviceNowOptionsMonitor = services.GetRequiredService<IOptionsMonitor<ServiceNowOptions>>();
                var serviceNowOptions = serviceNowOptionsMonitor.CurrentValue;

                client.BaseAddress = new Uri(serviceNowOptions.BaseUrl);
            })
            .ConfigurePrimaryHttpMessageHandler(services => {
                var serviceNowOptionsMonitor = services.GetRequiredService<IOptionsMonitor<ServiceNowOptions>>();
                var serviceNowOptions = serviceNowOptionsMonitor.CurrentValue;
                var handler = new HttpClientHandler();

                if (serviceNowOptions.TrustServerSslCertificate is true)
                {
                    handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                }

                return handler;
            })
            .AddHttpMessageHandler<ServiceNowHttpHandler>();

        return builder;
    }

    /// <summary>
    /// Add access control to the API.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns><see cref="WebApplicationBuilder"/></returns>
    public static WebApplicationBuilder AddAccessControl(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(options =>
            {
                var authority = builder.Configuration["Authentication:Schemes:OpenIdConnect:Authority"];
                var clientId = builder.Configuration["Authentication:Schemes:OpenIdConnect:ClientId"];
                var clientSecret = builder.Configuration["Authentication:Schemes:OpenIdConnect:ClientSecret"];
                var scope = builder.Configuration["Authentication:Schemes:OpenIdConnect:Scope"];
                var responseType = builder.Configuration["Authentication:Schemes:OpenIdConnect:ResponseType"];
                var redirectUri = builder.Configuration["Authentication:Schemes:OpenIdConnect:RedirectUri"];

                ArgumentNullException.ThrowIfNull(authority);
                ArgumentNullException.ThrowIfNull(clientId);
                ArgumentNullException.ThrowIfNull(clientSecret);
                ArgumentNullException.ThrowIfNull(scope);
                ArgumentNullException.ThrowIfNull(responseType);

                options.Authority = authority;
                options.ClientId = clientId;
                options.ClientSecret = clientSecret;
                options.ResponseType = responseType;
                options.Scope.Add(scope);
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.CallbackPath = redirectUri;
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                options.Events = new OpenIdConnectEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        context.Response.Redirect("/scalar/v1");
                        context.HandleResponse();
                        return Task.CompletedTask;
                    }
                };
                options.NonceCookie.SecurePolicy = CookieSecurePolicy.Always;
            })
            .AddJwtBearer(options =>
            {
                var issuer = builder.Configuration["Authentication:Schemes:Bearer:Issuer"];
                var audience = builder.Configuration["Authentication:Schemes:Bearer:Audience"];
                var authority = builder.Configuration["Authentication:Schemes:Bearer:Authority"];

                ArgumentNullException.ThrowIfNull(issuer);
                ArgumentNullException.ThrowIfNull(audience);
                ArgumentNullException.ThrowIfNull(authority);

                options.Authority = authority;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
                    {
                        var client = new HttpClient();
                        var keySetUrl = authority.AppendPathSegments("pf", "JWKS");
                        var response = client.GetAsync(keySetUrl).Result;
                        var json = response.Content.ReadAsStringAsync().Result;
                        var keys = new JsonWebKeySet(json);
                        return keys.GetSigningKeys();
                    }
                };
            });

            builder.Services.AddAuthorization(o => {
                // Add your authorization policies here
            });

        return builder;
    }

    /// <summary>
    /// Maps the login and logout endpoints.
    /// </summary>
    /// <param name="endpointRouteBuilder">The endpoint route builder.</param>
    /// <returns>The endpoint convention builder.</returns>
    /// <remarks>
    /// This method maps the login and logout endpoints.
    /// </remarks>
    public static IEndpointConventionBuilder MapLoginAndLogout(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        ArgumentNullException.ThrowIfNull(endpointRouteBuilder);

        var group = endpointRouteBuilder.MapGroup("");

        // See usager: https://github.com/dotnet/aspnetcore/issues/56302#issuecomment-2179009327
        group.MapGet("/login", (string? returnUrl) =>
                TypedResults.Challenge(GetAuthProperties(returnUrl)))
            .AllowAnonymous();

        // https://auth0.com/docs/authenticate/login/logout/log-users-out-of-auth0
        group.MapPost("/logout",
            ([FromForm] string? returnUrl,
                    [FromServices] IOptionsSnapshot<AuthenticationOptions> authenticationOptions) =>
                TypedResults.SignOut(GetAuthProperties(returnUrl),
                    [CookieAuthenticationDefaults.AuthenticationScheme, authenticationOptions.Value.DefaultAuthenticateScheme!]));
        
        return group;
    }

    /// <summary>
    /// Adds API configuraiton.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns><see cref="WebApplicationBuilder"/></returns>
    public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddOptionsWithValidateOnStart<ApiOptions>()
            .BindConfiguration(ApiOptions.SectionKey);
        builder.Services.AddOptionsWithValidateOnStart<PingOneOptions>()
            .BindConfiguration(PingOneOptions.SectionKey);
        
        return builder;
    }

    /// <summary>
    /// Add OpenApi documentation.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddOpenApiDocumentation(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddOpenApi("v1", options => { options.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); });

        return builder;
    }

    /// <summary>
    /// Documentation for adding Bearer security scheme found here: https://stackoverflow.com/questions/79265776/how-to-add-jwt-token-support-globally-in-scalar-for-a-net-9-application
    /// </summary>
    /// <param name="authenticationSchemeProvider"></param>
    internal sealed class BearerSecuritySchemeTransformer(Microsoft.AspNetCore.Authentication.IAuthenticationSchemeProvider authenticationSchemeProvider) : IOpenApiDocumentTransformer
    {
        public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
        {
            var authenticationSchemes = await authenticationSchemeProvider.GetAllSchemesAsync();
            if (authenticationSchemes.Any(authScheme => authScheme.Name == "Bearer"))
            {
                var requirements = new Dictionary<string, OpenApiSecurityScheme>
                {
                    ["Bearer"] = new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer", 
                        In = ParameterLocation.Header,
                        BearerFormat = "Json Web Token"
                    }
                };
                document.Components ??= new OpenApiComponents();
                document.Components.SecuritySchemes = requirements;

                foreach (var operation in document.Paths.Values.SelectMany(path => path.Operations))
                {
                    operation.Value.Security.Add(new OpenApiSecurityRequirement
                    {
                        [new OpenApiSecurityScheme { Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme } }] = Array.Empty<string>()
                    });
                }
            }
        }
    }

    /// <summary>
    /// Add services.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns><see cref="WebApplicationBuilder"/></returns>
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        // Thread safe singleton services
        builder.Services.AddSingleton<IResourceStateService, ResourceStateService>();
        builder.Services.AddSingleton<IPaginationService, PaginationService>();

        // Scoped services
        builder.Services.AddScoped<IPagedResponseService<SpConnection>, PagedResponseService<SpConnection>>();
        builder.Services.AddScoped<IPagedResponseService<OidcClient>, PagedResponseService<OidcClient>>();
        builder.Services.AddScoped<IPagedResponseService<LegacyConnection>, PagedResponseService<LegacyConnection>>();
        builder.Services.AddScoped<IPagedResponseService<ServiceNowApplication>, PagedResponseService<ServiceNowApplication>>();
        builder.Services.AddScoped<IPagedResponseService<ServiceNowUser>, PagedResponseService<ServiceNowUser>>();
        builder.Services.AddScoped<IPingFederateService, PingFederateService>();
        builder.Services.AddScoped<ILegacyService, LegacyService>();
        builder.Services.AddScoped<IServiceNowService, ServiceNowService>();
        builder.Services.AddScoped<IIdentityLinkingService, IdentityLinkingService>();
        builder.Services
            .AddHttpContextAccessor()
            .AddScoped<IUriHelperService, UriHelperService>(services => {
                var accessor = services.GetRequiredService<IHttpContextAccessor>();
                var request = accessor?.HttpContext?.Request;
                var uri = string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent());
                return new UriHelperService(uri);
            });

        // PingIdentity services
        builder.AddPingIdentityServices();
            
        return builder;
    }

    /// <summary>
    /// Add Ping Identity services to the web application.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns><see cref="WebApplicationBuilder"/></returns>
    public static WebApplicationBuilder AddPingIdentityServices(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddPingIdentityHttpClients(builder.Configuration);
        builder.Services.AddPingIdentityServices(builder.Configuration);

        return builder;
    }

    private static AuthenticationProperties GetAuthProperties(string? returnUrl)
    {
        const string pathBase = "/";

        if (string.IsNullOrEmpty(returnUrl))
        {
            returnUrl = pathBase;
        }
        else if (!Uri.IsWellFormedUriString(returnUrl, UriKind.Relative))
        {
            returnUrl = new Uri(returnUrl, UriKind.Absolute).PathAndQuery;
        }
        else if (returnUrl[0] != '/')
        {
            returnUrl = $"{pathBase}{returnUrl}";
        }

        return new AuthenticationProperties { RedirectUri = returnUrl };
    }
}