using Hangfire;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Notifications;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Extensions;

/// <summary>
/// Extension methods for <see cref="WebApplication"/>
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Uses access control for the API.
    /// </summary>
    /// <param name="app"></param>
    /// <returns><see cref="WebApplication"/></returns>
    public static WebApplication UseAccessControl(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }

    /// <summary>
    /// Uses configured Hangfire instance and maps hangfire instance endpoints for dashboard.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication UseHangfire(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        var dashboardOptions = new DashboardOptions
        {
            IsReadOnlyFunc = context => true,
            Authorization = new[] { new AllowAllAuthorizationFilter() }
        };

        app.UseHangfireDashboard("/hangfire", dashboardOptions);
        app.MapHangfireDashboard();

        using var scope = app.Services.CreateScope();
        var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
        var options = scope.ServiceProvider.GetRequiredService<IOptionsMonitor<ApiOptions>>().CurrentValue;
            
        ArgumentNullException.ThrowIfNull(recurringJobManager);
        ArgumentNullException.ThrowIfNull(options);

        // Setup recurring aggregations
        var notifyAggregateOidcConnectionsColleague = scope.ServiceProvider.GetRequiredService<NotifyAggregateOidcConnectionsColleague>();
        var notifyAggregateSamlConnectionsColleague = scope.ServiceProvider.GetRequiredService<NotifyAggregateSamlConnectionsColleague>();
        var notifyAggregateLegacyConnectionsColleague = scope.ServiceProvider.GetRequiredService<NotifyAggregateLegacyConnectionsColleague>();
        var notifyAggregateServiceNowApplicationsColleague = scope.ServiceProvider.GetRequiredService<NotifyAggregateServiceNowApplicationsColleague>();
        var notifyAggregateServiceNowUsersColleague = scope.ServiceProvider.GetRequiredService<NotifyAggregateServiceNowUsersColleague>();
        ArgumentNullException.ThrowIfNull(notifyAggregateOidcConnectionsColleague);
        ArgumentNullException.ThrowIfNull(notifyAggregateSamlConnectionsColleague);
        ArgumentNullException.ThrowIfNull(notifyAggregateLegacyConnectionsColleague);
        ArgumentNullException.ThrowIfNull(notifyAggregateServiceNowApplicationsColleague);
        ArgumentNullException.ThrowIfNull(notifyAggregateServiceNowUsersColleague);

        recurringJobManager.AddOrUpdate(
            HangfireConstants.OidcRecurringAggregationJobId, 
            () => notifyAggregateOidcConnectionsColleague.Notify(null),
            options.OidcAggregationSchedule);

        recurringJobManager.AddOrUpdate(
            HangfireConstants.SamlRecurringAggregationJobId, 
            () => notifyAggregateSamlConnectionsColleague.Notify(null),
            options.SamlAggregationSchedule);

        recurringJobManager.AddOrUpdate(
            HangfireConstants.LegacyRecurringAggregationJobId, 
            () => notifyAggregateLegacyConnectionsColleague.Notify(null),
            options.LegacyAggregationSchedule);

        recurringJobManager.AddOrUpdate(
            HangfireConstants.ServiceNowApplicationsRecurringAggregationJobId,
            () => notifyAggregateServiceNowApplicationsColleague.Notify(null),
            options.ServiceNowApplicationsAggregationSchedule);

        recurringJobManager.AddOrUpdate(
            HangfireConstants.ServiceNowUsersRecurringAggregationJobId,
            () => notifyAggregateServiceNowUsersColleague.Notify(null),
            options.ServiceNowUsersAggregationSchedule);

        return app;
    }

    private class AllowAllAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            var result = httpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme).Result;

            if (result.Succeeded is false)
            {
                httpContext.Response.Redirect("/");
                httpContext.Response.CompleteAsync();
                return false;
            }

            return true;
        }
    }
}