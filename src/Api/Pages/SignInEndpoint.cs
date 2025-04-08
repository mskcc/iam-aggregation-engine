using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.OpenApi.Models;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Pages;

/// <summary>
/// Endpoints for sign on.
/// </summary>
public static class SignInPage
{
    /// <summary>
    /// Maps the sign on endpoints for the application.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapSignInAndSignOutPages(this IEndpointRouteBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.MapGet("/", HandleAuthentication)
            .WithName("Handles authentication requests")
            .WithOpenApi(options => {
                options.Description = "Used to authenticate identity against OIDC configuration.";
                options.Summary = "Authentication with OIDC";
                options.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Authentication" } };

                return options;
            });

        return app;
    }

    /// <summary>
    /// Handles authentication requests.
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    private static async Task HandleAuthentication(HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext);
        
        var result = await httpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        if (result.Succeeded)
        {
            httpContext.Response.Redirect("/hangfire");
        }
        else
        {
            await httpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}