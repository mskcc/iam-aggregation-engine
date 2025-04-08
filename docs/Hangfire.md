# Cron Configuration for Recurring Hangfire Jobs

This document outlines how to configure recurring Hangfire jobs for aggregating Single Sign-On (SSO) connection data, including configuration via `appsettings.json` and through the Hangfire Dashboard.

## Configuration in `appsettings.json`

The aggregation schedules for different connection types (Legacy, OIDC, and SAML) are configured via cron expressions in the `appsettings.json` file. Below is an example of the relevant section of the configuration:

```json
{
  "Api": {
    "SamlAggregationSchedule": "0 * * * *",           // Every hour
    "OidcAggregationSchedule": "0 * * * *",           // Every hour
    "LegacyAggregationSchedule": "5 * * * *"          // Every hour, 5 minutes past
  }
}
```

### Explanation of Key Configuration Values:
- **Aggregation Schedules**: The cron expressions that define the frequency for aggregating each connection type.

## Cron Expressions

- `SamlAggregationSchedule: "0 * * * *"` — This means the aggregation will occur every hour, on the hour.
- `OidcAggregationSchedule: "0 * * * *"` — This also triggers aggregation every hour, on the hour.
- `LegacyAggregationSchedule: "5 * * * *"` — This triggers aggregation 5 minutes past every hour.

## Configuring Cron Jobs in Hangfire

### 1. Configure Cron Jobs Programmatically

To configure recurring Hangfire jobs for aggregating SSO connection data based on the cron expressions in `appsettings.json`, you can use Hangfire's built-in `RecurringJob` feature in your `Startup.cs` or `Program.cs` file.

Here’s an example of how to configure the recurring jobs:

```csharp
using Hangfire;
using Microsoft.Extensions.Configuration;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Load settings from appsettings.json
        var apiSettings = Configuration.GetSection("Api");

        // Add Hangfire services and configure the storage
        services.AddHangfire(config => config.UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection")));

        // Schedule recurring jobs based on the cron expressions
        RecurringJob.AddOrUpdate(
            "aggregate-saml-connections", 
            () => AggregateSamlConnections(), 
            apiSettings["SamlAggregationSchedule"]
        );

        RecurringJob.AddOrUpdate(
            "aggregate-oidc-connections", 
            () => AggregateOidcConnections(), 
            apiSettings["OidcAggregationSchedule"]
        );

        RecurringJob.AddOrUpdate(
            "aggregate-legacy-connections", 
            () => AggregateLegacyConnections(), 
            apiSettings["LegacyAggregationSchedule"]
        );
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Enable Hangfire dashboard
        app.UseHangfireDashboard();
        app.UseHangfireServer();
    }
}
```

In this example, we use Hangfire’s `RecurringJob.AddOrUpdate` method to configure recurring jobs for each connection type. The cron expressions are loaded from `appsettings.json`, ensuring that the scheduling can be easily adjusted without needing to modify the code.

### 2. Define the Aggregation Methods

Make sure to implement the methods responsible for the aggregation of SSO connections. These methods can be invoked by Hangfire's recurring jobs:

```csharp
public void AggregateSamlConnections()
{
    // Logic to aggregate SAML connections
}

public void AggregateOidcConnections()
{
    // Logic to aggregate OIDC connections
}

public void AggregateLegacyConnections()
{
    // Logic to aggregate Legacy connections
}
```

### 3. Start the Hangfire Server and Dashboard

To begin processing background jobs, you need to start the Hangfire server and configure the dashboard:

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // Enable Hangfire dashboard for monitoring jobs
    app.UseHangfireDashboard("/hangfire");

    // Start the Hangfire server
    app.UseHangfireServer();
}
```

Once the application is running, you can navigate to `/hangfire` to monitor the status of the recurring jobs.

---

## Configuring Cron Jobs via the Hangfire Dashboard

### 1. Access the Hangfire Dashboard

After starting your application and navigating to the `/hangfire` URL, you will have access to the Hangfire Dashboard. The dashboard provides a user interface to monitor, manage, and configure recurring jobs.

### 2. Add a New Recurring Job

1. In the Hangfire Dashboard, go to the **Recurring Jobs** tab.
2. Click on **Add Recurring Job** to create a new recurring job.
3. Enter the following information:
   - **Job Name**: A unique name for the job (e.g., "Aggregate SAML Connections").
   - **Cron Expression**: The cron expression that defines how often the job will run (e.g., `0 * * * *`).
   - **Method**: The method to be invoked for this job (e.g., `AggregateSamlConnections`).

4. Click **Save** to add the job.

### 3. Edit or Delete a Job

You can also edit or delete existing recurring jobs from the Hangfire Dashboard. This allows you to adjust the cron expressions or stop a job from running.

---