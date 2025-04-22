# Ping Federate Aggregator API 🚀

A powerful **Web API** that aggregates **Ping Federate** SAML and OIDC connection data into **SQL databases**. This API uses **Hangfire** for background job processing and **Scalar** for development mode API documentation.

---

## 🛠 Features

- **SAML & OIDC Connection Aggregation**: Collects connection data from **Ping Federate** and stores it into SQL databases.
- **Hangfire Integration**: Schedules and manages recurring and fire-and-forget background jobs with the **Hangfire Dashboard**.
- **Scalar**: Provides an interactive API documentation for easy development and testing in **development mode**.
- **Identity Linking Engine**: Service in the form of API endpionts for migrating/linking identities within PingOne. See 
documentation here: [Identity Linking Documentation](https://github.com/mskcc/iam-aggregation-engine/blob/main/docs/IdentityLinking.md)
---

## ⚙️ Getting Started Locally

To get this project up and running on your local machine, follow the steps below:

### 1️⃣ Clone the Repository

Start by cloning the repository to your local machine.

```bash
git clone https://dev.azure.com/{your-organization}/{your-project}/_git/{your-repository-name}
cd {your-repository-name}
```

### 2️⃣ Install Dependencies

Make sure you have **.NET 9.0 SDK** installed. You can check by running:

```bash
dotnet --version
```

Then, restore the project dependencies:

```bash
dotnet restore
```

### 3️⃣ Configure Environment Variables

Make sure to configure the necessary environment variables for your local development. You might need to provide:

- **Ping Federate API credentials** (client ID, secret, etc.)
- **SQL database connection string**

You can add these to a `.env` file or set them directly in your development environment.

### 4️⃣ Run build

To build the Web API locally, simply execute the following command:

```bash
dotnet build
```

### 5️⃣ Run tests

To test the Web API locally, simply execute the following command:

```bash
dotnet test
```

### 6️⃣ Run the API Locally

To run the Web API locally, simply execute the following command:

```bash
dotnet run --launch-profile https
```

The API will start and will be available at:

- **Scalar UI** (for development): `https://localhost:7250/Scalar/v1`
- **API**: `https://localhost:7250/api/v1`

### 7️⃣ Access Hangfire Dashboard

Once the application is running, you can also access the **Hangfire Dashboard** for job monitoring and management at:

- **Hangfire Dashbaord**: `https://localhost:7250/hangfire`

---

## Creating Database Migrations
Example of creating an InitialCreate migration. Run this the ```/src/Api``` directory.
connection string must be configured in the appsettings.json or appsettings.development.json file that your environment is pointing too. 
If you see an error similar to this: ```Format of the initialization string does not conform to specification starting at index 0```
This is an indication that there is a configuration issue during ef miggrations. First place to check is appsettings.
```bash
dotnet ef migrations add InitialCreate --output-dir ../Infrastructure/Data/Migrations --project ../Infrastructure/Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.csproj
```

## 🌟 API Endpoints
All API endpoints require authorization.

### 🛠 Health Checks
Endpoint for processing health status of the Ping Federate SSO connections database as well as the configured Hangfire database.

- **GET** `/api/v1/health`: Retrieve **Health** status for configured databases.

### 🛠 Ping Federate SAML Connections
Endpoints for processing SAML SSO connections configured in Ping Federate. 

- **GET** `/api/v1/pingfederate/saml/connections`: Retrieve all **SAML** connections.
- **POST** `/api/v1/pingfederate/saml/connections/aggregate`: Aggregate all **SAML** connections.
- **POST** `/api/v1/pingfederate/saml/connections/purge`: Purge all **SAML** connections from database.

### 🛠 Ping Federate OIDC Connections
Endpoints for processing SAML OIDC connections configured in Ping Federate. 

- **GET** `/api/v1/pingfederate/oidc/connections`: Retrieve all **OIDC** connections.
- **POST** `/api/v1/pingfederate/oidc/connections/aggregate`: Aggregate all **OIDC** connections.
- **POST** `/api/v1/pingfederate/oidc/connections/purge`: Purge all **OIDC** connections from database.

### 🛠Legacy Connections
Endpoints for processing MSKCC business specific legacy connections. These are aggregations of both OIDC and SAML SSO connection into one table.

- **GET** `/api/v1/mskcc/legacy/connections`: Retrieve all **Legacy SSO** connections.
- **GET** `/api/v1/mskcc/legacy/connections/search`: Search through all **Legacy SSO** connections with a specfied search criteria. Matches on ACS Endpoint column.
- **POST** `/api/v1/mskcc/legacy/connections/aggregate`: Aggregate all **Legacy SSO** connections.
- **POST** `/api/v1/mskcc/legacy/connections/purge`: Purge all **Legacy SSO** connections from database.

### 🛠Service Now
Endpoints for processing data that is returned from the Service Now endpoints.

- **GET** `/api/v1/servicenow/applications/`: Retrieve all **Service Now** applications.
- **POST** `/api/v1/servicenow/applications/aggregate`: Aggregate all **Service Now** applications.
- **POST** `/api/v1/servicenow/applications/purge`: Purge all **Service Now** applications from database.

### 🔑 How to Create a Client Credentials Flow in Postman to Get a JWT Access Token
To use the these endpoints, you need to obtain a JWT access token using the client credentials flow. Here’s how you can do it in Postman:

1. **Open Postman** and create a new request.

2. **Set the Request Type** to `POST`.

3. **Enter the Token Endpoint URL**:
   - URL: `https://your-pingfederate-server.com/as/token.oauth2`

4. **Set the Headers**:
   - Key: `Content-Type`
   - Value: `application/x-www-form-urlencoded`

5. **Set the Body**:
   - Select `x-www-form-urlencoded` as the body type.
   - Add the following key-value pairs:
     - `grant_type`: `client_credentials`
     - `client_id`: `your-client-id`
     - `client_secret`: `your-client-secret`
     - `scope`: `your-scope` (if required)

6. **Send the Request**:
   - Click on the `Send` button to send the request.

7. **Retrieve the Access Token**:
   - In the response, you will receive a JSON object containing the access token. Copy the value of the `access_token` field.

8. **Use the Access Token in Subsequent Requests**:
   - For subsequent requests to the Ping Federate endpoints, add the following header:
     - Key: `Authorization`
     - Value: `Bearer your-access-token`

### ⚡ Pagination Support

Both the **SAML** and **OIDC** connections endpoints support pagination to handle large datasets efficiently. You can control the number of records returned by specifying the **`pageNumber`** and **`pageSize`** as query parameters in your GET requests.

#### Pagination Parameters:
- **`pageNumber`** (optional): The page number for pagination. Default is `0` to indicate no pagination options requested.
- **`pageSize`** (optional): The number of records per page. Default is `0` to indicate no pagination options requested.

### Example Request:

```http
GET /api/v1/pingfederate/oidc/connections?pageNumber=2&pageSize=5
```

This request would retrieve the **OIDC connections** on **page 2**, with **5 connections per page**.

### How Pagination Works:

In the API implementation, pagination is handled by the `GetOidcConnections` method, which processes the `pageNumber` and `pageSize` query parameters. Here's a breakdown of how it works:

1. **Default Values**: If either `pageNumber` or `pageSize` is not provided, the API returns all connections without pagination.
   
2. **Pagination Logic**: When both `pageNumber` and `pageSize` are specified:
   - The API skips records based on the formula:  
     `Skip((pageNumber - 1) * pageSize)`  
     This skips the number of records before the current page.
   - The `Take(pageSize)` method limits the results to the specified page size.

3. **Paged Response**: If pagination is enabled in the app configuration, a **paged response** is returned. This response includes:
   - The data for the current page.
   - The current page number and page size.
   - Total records available (if applicable).

Example response (with pagination enabled):

```json
{
  "data": [ /* Array of OIDC connection objects */ ],
  "pageNumber": 2,
  "pageSize": 5,
  "totalRecords": 50
}
```

If pagination is not needed, the response will simply return all the records as usual.

---

---

### 💼 Background Jobs

### 🛠 Hangfire Dashboard

Hangfire is used for managing and scheduling background jobs in this application. The Hangfire Dashboard provides a user-friendly interface to monitor, manage, and schedule background jobs.

#### Accessing the Hangfire Dashboard

1. **Navigate to the Hangfire Dashboard**:
   - Open your web browser and go to `https://localhost:7250/hangfire` (replace `localhost:7250` with your application's base URL).

2. **Dashboard Overview**:
   - The dashboard provides an overview of all background jobs, including their status, execution history, and performance metrics.

#### Using the Hangfire Dashboard

1. **Monitoring Jobs**:
   - The dashboard displays a list of all background jobs, categorized by their status (e.g., Succeeded, Failed, Processing, Scheduled).
   - Click on a job category to view detailed information about the jobs in that category.

2. **Manually Triggering Jobs**:
   - You can manually trigger a job by navigating to the job details and clicking the "Enqueue" button.

3. **Scheduling Jobs**:
   - To schedule a new job, use the "Recurring Jobs" section of the dashboard.
   - Click on "Add recurring job" and fill in the necessary details, such as the job name, method to execute, and the CRON expression for scheduling.

4. **Managing Jobs**:
   - You can delete, retry, or requeue jobs from the dashboard.
   - Use the "Jobs" section to manage individual jobs and their execution history.

#### Example: Scheduling a Recurring Job

1. **Navigate to Recurring Jobs**:
   - In the Hangfire Dashboard, go to the "Recurring Jobs" section.

2. **Add a New Recurring Job**:
   - Click on "Add recurring job".
   - Enter the job name, method to execute (e.g., `MyNamespace.MyService.MyMethod`), and the CRON expression for scheduling (e.g., `0 * * * *` for every hour).

3. **Save the Job**:
   - Click "Add" to save the recurring job.

By following these steps, you can effectively use the Hangfire Dashboard to monitor, manage, and schedule background jobs in your application.

---

## 📝 Configuration Overview

Here’s a brief look at the configuration from `appsettings.json`:

```json
{
  "Api": {
    "BaseUrl": "https://{BaseUrl}",
    "BaseEndpoint": "/api/v1",
    "LegacyConnectionsEndpoint": "mskcc/legacy/connections",
    "LegacyConnectionsPurgeEndpoint": "mskcc/legacy/connections/purge",
    "LegacyConnectionsAggregateEndpoint": "mskcc/legacy/connections/aggregate",
    "OidcConnectionsEndpoint": "/pingfederate/oidc/connections",
    "OidcConnectionsPurgeEndpoint": "/pingfederate/oidc/connections/purge",
    "OidcConnectionsAggregateEndpoint": "/pingfederate/oidc/connections/aggregate",
    "SamlConnectionsEndpoint": "/pingfederate/saml/connections",
    "SamlConnectionsAggregateEndpoint": "/pingfederate/saml/connections/aggregate",
    "SamlConnectionsPurgeEndpoint": "/pingfederate/saml/connections/purge",
    "ServiceNowApplicationsAggregationEndpoint": "/servicenow/applications/aggregate",
    "ServiceNowApplicationsPurgeEndpoint": "/servicenow/applications/purge",
    "ServiceNowApplicationsEndpoint": "/servicenow/applications",
    "UsePagination": true,
    "MaxPageSize": 1000,
    "SamlAggregationSchedule": "15 * * * *",
    "OidcAggregationSchedule": "15 * * * *",
    "LegacyAggregationSchedule": "30 * * * *",
    "ServiceNowApplicationsAggregationSchedule": "10 * * * *",
    "LoggingRetentionDays": 7
  },
  "Authentication": {
    "DefaultScheme":  "LocalAuthIssuer",
    "Schemes": {
      "Bearer": {
        "Authority": "https://ssofeddev.mskcc.org",
        "Issuer": "https://ssofeddev.mskcc.org",
        "Audience": "api://ssofeddev.mskcc.aggregator"
      }
    },
    "OpenIdConnect": {
      "Authority": "https://{ssofeddev}.mskcc.org/",
      "ClientId": "{clientId}",
      "ClientSecret": "{clientSecret}",
      "Scope": "openid",
      "ResponseType": "code",
      "RedirectUri": "/callback/"
    }
  },
  "ConnectionStrings": {
    "SqlConnection": "{SqlConnectionString}",
    "HangfireConnection": "{HangfireConnectionString}"
  },
  "PingFederate": {
    "TrustServerSslCertificate": true,
    "BaseUrl": "{PingFederateBaseUrl}",
    "Username": "{Username}",
    "Password": "{Password}",
    "Host": "{Host}",
    "SpConnectionsEndpoint": "/pf-admin-api/v1/idp/spConnections",
    "OauthClientsEndpoint": "/pf-admin-api/v1/oauth/clients",
    "InstanceEnvironment": "Test",
    "DefaultIssuanceCriteria": "CN=Example,OU=example,OU=Resources,DC=MSKCC,DC=ROOT,DC=MSKCC,DC=ORG"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Hangfire": "Information"
    }
  }
}
```

- **Add Configuration**: Loads necessary configurations for API and services.
- **Scalar Integration**: Adds Open API with Scalar for API documentation.
- **Hangfire**: Sets up and configures Hangfire for background jobs and job monitoring.
- **PingFederate Services**: Integrates with Ping Federate to retrieve SAML and OIDC connection data.
- **Data Persistence**: Configures database connections and migrations.

---

## 📈 Monitoring & Dashboards

### 🚀 Hangfire Dashboard

The Hangfire dashboard provides a real-time overview of your background jobs, including scheduled, recurring, and completed jobs. You can access it at:

- **URL**: `/hangfire`

Here is the updated version of the notes with an explanation about Swagger's deprecation in .NET 9 due to a lack of community support:

---

### 📊 Scalar API Documentation UI

Scalar UI API documentation is available in **development mode** to interact with the API endpoints. It provides an interactive interface where you can test all API routes directly.

Scalar UI offers similar functionality to Swagger, allowing you to document and test your API endpoints interactively without relying on Swagger’s tooling. It integrates seamlessly with the OpenAPI specification and provides a modern, feature-rich API documentation experience.

- **URL**: `/scalar/v1`


#### 🚨 Important Notes about Swagger in .NET 9:

- **Swagger Deprecation in .NET 9**: 
  In .NET 9, **Swagger** has been officially deprecated due to a lack of community support and limited updates. This change primarily affects the use of Swagger UI for generating and interacting with OpenAPI documentation. 

---

This should provide a clear understanding of why Swagger is no longer the recommended tool in .NET 9 and how Scalar is a viable alternative for API documentation and testing.

## 🛠️ Built With

- **ASP.NET Core 6**: Modern web API framework
- **Hangfire**: Background job processing library
- **Scalar UI**: Interactive API documentation
- **Ping Federate**: SAML and OIDC integration
- **Entity Framework Core**: ORM for SQL database interaction

---

## 🤝 Development Documentation

- **Architecture** Architectural decision record [docs](https://dev.azure.com/MSKDevOps/IAM/_git/MskccToolsPingFederateConnections?path=/docs/ADR.md)
- **Configuring Hangfire Jobs** Hangfire documentation [docs](https://dev.azure.com/MSKDevOps/IAM/_git/MskccToolsPingFederateConnections?path=/docs/Hangfire.md)

---

## 🚀 Publish

Publishing the application to an IIS server requires the following to be installed on teh hosting server: 

- **Dotnet Runtime** ASP.NET 9.0 [isntallers](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- **Dotnet Hosting Bundle** ASP.NET 9.0 Hosting bundle for IIS Server [installers](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-9.0.0-windows-hosting-bundle-installer)

Once installed on the IIS server deployments can be made: 

1. Create a site in IIS and choose HTTPS and port 7250 for example. 
2. Select path to an empty folder that VS publish profile will later reference. 
3. Complete site creation.
4. Open in Visual Studio 
5. Right click solution and select 'Publish'.  If a publish profile is not created than create one following this documentation [Microsoft IIS Deployment](https://learn.microsoft.com/en-us/aspnet/core/tutorials/publish-to-iis?view=aspnetcore-9.0&tabs=net-cli)
6. Execute the publish from Visual Studio.
7. Visual Studio should take you to browse the newly published site. 

### 🧑‍💻 Contact

- **Project Maintainer**: [Robert Luisi](https://www.linkedin.com/in/robert-luisi)
- **Email**: luisir@mskcc.org

---

**Ping Federate Aggregator API**! 🎉🚀