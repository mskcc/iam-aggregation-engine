# Identity Linking Engine 

This document outlines how to configure the application to start/stop processing identities from the Identity Engine Request Queue and how to configure the applications service through `appsettings.json` with monitoring through hangfire dashboard.

----

This application has multiple different operating procedures. In this document we will go over the operating procedure for Identity Linking planned and spec'd out by this [Lucid Flowchart](https://lucid.app/lucidchart/f3200eae-463e-4991-9c53-2377ed2478e9/edit?docId=f3200eae-463e-4991-9c53-2377ed2478e9&shared=true&page=0_0&invitationId=inv_6f02fae2-02b5-460e-98b2-5199740189ef#) for more information on the other available operating procedures that this service supports like 'Just In Time Linking' checkout [Just in Time Linking](https://github.com/mskcc/iam-aggregation-engine/blob/main/docs/JustInTimeIdentityLinking.md)

## Configuration in `appsettings.json`

Below is an example of the relevant section of the configuration:

```json
{
  "Api": {
    "AzureUsersSourceTableName": "AzureUsers_Source_Stage", // This is the table used to link identites to associated Microsoft Entra Object IDs.
    "BulkProcessingBatchSize": 25, // This tells the service how many identites to process during each recurring job cycle.
    "BulkProcessingBatchSchedule": "* * * * *" // This tells the service the frequency in which to process each job cycle. 
  },
  "PingOne": {
    "ApiBaseUrl": "https://api.pingone.com/v1", // This is the base url for API requests to PingOne.
    "ClientId": "...", // The Client ID of the worker application setup in PingOne Applications.
    "ClientSecret": "...", // The Client Secret of the worker application setup in PingOne Applications.
    "EnvironmentId": "...", // The environment ID in which to make API HTTP requests against.
    "Authority": "https://auth.pingone.com/", // The authority to validate tokens against. 
    "TrustServerSslCertificate": true, // Indicates to the service to trust SSL Certificate exceptions during development. 
    "PingFederateIdentityProviderId": "...", // This is a hardcoded value for linking and creating Ping Federate Identities.
    "MicrosoftEntraIdentityProviderId": "...", // This is a hardcoded value for linking and creating Entra ID identities. 
    "LdapGatewayId": "...", // This is a hardcoded value for linking and creating LDAP Gateways.
    "LdapGatewayUserTypeId": "..." // This is a hardcoded value for linking and creating LDAP Gateways.
  },
  "ConnectionStrings": {
    "SqlConnection": "Server=ExampleServer;Database=ExampleDatabase;User Id=deadbeef;Password=SecretPassword;TrustServerCertificate=True;",
    "HangfireConnection": "Server=ExampleServer;Database=ExampleDatabase;User Id=deadbeef;Password=SecretPassword;TrustServerCertificate=True;"
  },
}
```

## SQL Dependencies
Each environment instance manages it's own tables, entities, and migrations. EF Core is leveraged to handle everything from database migration to entity and modeling creation and unit testing. This is true for all dependent tables except for the Azure Users Source Table. 

----

### Azure Users Source Table 

The source for which the application identitfies microsoft netra object ids. This table in the 'appsettings.json' configuration is called: ```AzureUsersSourceTableName```.  The service uses this to lookup the entra object ID based on some basic information like samaccountname. This table is not managed by the code base and any requests to do databse migrations, environment propogation, purging, and aggregation cannot be managed through this application.  Wherever this Identity Linking Engine service runs, it needs the databse it's configured to use to be provisioned with this table or else an exception will be thrown and the operating procedure for this workflow will fail.  The table needs to be provisioned with aggregated data for the same environment that this identity linking engine service is configured for. 

----

### Identity Linking Engine Tables

The Identity Linking Engine service leverages EF Core to manage it's own databse tables. This includes 3 tables: 
- Ping_IdentityLinking_Processing_Request_Log
- Ping_IdentityLinking_Processing_Request_Queue
- Ping_IdentityLinking_Processing_Request_Archive

----

#### Ping_IdentityLinking_Processing_Request_Log Table Schema Breakdown
This Identity Linking Engine service is configured to log to both a rolling log file and a SQL log table. 
The SQL table isn't logged too as verbosly as the rolling log file since there are concerns around security and resources.

| Column Name           | Data Type         | Description |
|------------------------|------------------|-------------|
| **Id**                 | `UNIQUEIDENTIFIER` | Primary key; uniquely identifies each log entry. |
| **Message**            | `VARCHAR`        | The rendered log message, including values injected into the template. |
| **MessageTemplate**    | `VARCHAR`        | The original message template before any variables were rendered. |
| **Level**              | `VARCHAR`         | Severity level of the log (e.g., Info, Warning, Error, Critical). |
| **TimeStamp**          | `DATETIME`        | The date and time when the log entry was created. |
| **Exception**          | `VARCHAR`        | Exception message or stack trace, if the log is related to an error. |
| **Properties**         | `VARCHAR`        | Additional structured data captured with the log, typically in JSON format. |
| **LogEvent**           | `VARCHAR`        | Serialized log event object, useful for detailed inspection or rehydration. |
| **RequestId**          | `VARCHAR`         | Identifier for tracking a specific request or transaction across logs. |
| **PingOneUserId**      | `VARCHAR`         | ID of the user in the PingOne identity system associated with the log event, if available. |
| **Environment**        | `VARCHAR`         | The environment where the log was generated (e.g., Development, Staging, Production). |
| **Status**             | `VARCHAR`         | Optional status flag (e.g., Processed, Pending, Ignored). |
| **Detail**             | `VARCHAR`        | Additional descriptive information or context for the log. |
| **Error**              | `VARCHAR`        | Specific error message, if applicable and separate from the full exception stack. |

----

#### Ping_IdentityLinking_Processing_Request_Queue 
This table is used by the identity linking engine service as a processing queue to handle requests for processing identities in bulk/batch jobs. 
This table is read by the service when the Bulk/Batch Processing Job API Endpoint is invoked.


| Column Name              | Data Type        | Description |
|--------------------------|------------------|-------------|
| **Id**                   | `UNIQUEIDENTIFIER`| Primary key; uniquely identifies each record. |
| **CreatedAt**            | `DATETIME`       | Timestamp indicating when the record was created. |
| **Type**                 | `VARCHAR`        | Describes the type/category of the record (e.g., sync, update). |
| **LastProcessingAttempt** | `DATETIME`      | Timestamp of the most recent processing attempt. |
| **Environment**          | `VARCHAR`        | Specifies the environment the record belongs to (e.g., Dev, QA, Prod). |
| **PingOneUserId**        | `VARCHAR`        | User ID from the PingOne identity provider. |
| **EntraObjectId**        | `VARCHAR`        | Object ID from Microsoft Entra ID (formerly Azure AD). |
| **SamAccountName**       | `VARCHAR`        | The user's SAM (Security Account Manager) account name. |
| **EmployeeId**           | `VARCHAR`        | Internal employee ID, typically from HR systems. |
| **Status**               | `VARCHAR`        | Current processing status (e.g., Pending, Success, Failed). |
| **Attempts**             | `INT`            | Number of times the record has been attempted for processing. |
| **Source**               | `VARCHAR`        | Origin or system that generated the record. |
| **IsProcessedSuccessfully** | `BOOLEAN`     | Indicates whether the record has been processed successfully. |

----

#### Ping_IdentityLinking_Processing_Request_Archive
This table is used by the identity linking engine service as an archive to persist successfully handled requests after processing identities in bulk/batch jobs. 
This table is written to by the service when the Bulk/Batch Processing Job API Endpoint is invoked and successfully processes an identity from the queue.


| Column Name              | Data Type        | Description |
|--------------------------|------------------|-------------|
| **Id**                   | `UNIQUEIDENTIFIER`| Primary key; uniquely identifies each record. |
| **CreatedAt**            | `DATETIME`       | Timestamp indicating when the record was created. |
| **Type**                 | `VARCHAR`        | Describes the type/category of the record (e.g., sync, update). |
| **LastProcessingAttempt** | `DATETIME`      | Timestamp of the most recent processing attempt. |
| **Environment**          | `VARCHAR`        | Specifies the environment the record belongs to (e.g., Dev, QA, Prod). |
| **PingOneUserId**        | `VARCHAR`        | User ID from the PingOne identity provider. |
| **EntraObjectId**        | `VARCHAR`        | Object ID from Microsoft Entra ID (formerly Azure AD). |
| **SamAccountName**       | `VARCHAR`        | The user's SAM (Security Account Manager) account name. |
| **EmployeeId**           | `VARCHAR`        | Internal employee ID, typically from HR systems. |
| **Status**               | `VARCHAR`        | Current processing status (e.g., Pending, Success, Failed). |
| **Attempts**             | `INT`            | Number of times the record has been attempted for processing. |
| **Source**               | `VARCHAR`        | Origin or system that generated the record. |
| **IsProcessedSuccessfully** | `BOOLEAN`     | Indicates whether the record has been processed successfully. |

----

## API Endpoints for Identity Linking
This section will go over the available API endpoints used to link identities and which API endpoints are used for this identity linking workflow: [Lucid Flowchart](https://lucid.app/lucidchart/f3200eae-463e-4991-9c53-2377ed2478e9/edit?docId=f3200eae-463e-4991-9c53-2377ed2478e9&shared=true&page=0_0&invitationId=inv_6f02fae2-02b5-460e-98b2-5199740189ef#). 

### Identity Linking API Overview & Setup
OpenAPI is used within the code base to document the API endpoints as they are being developed.  Scalar is a Microsoft supported service that displays the OpenAPI documentation in a human readable and interactive interface in a web UI (Scalar has since replaced the deprecated Swagger workflow). It can be found by appending /scalar/v1 (ex: https://server:7250/scalar/v1).  Here you will be able to make requests, and see code examples of how to use the API endpoints in other code bases. It also has an option to download the API documentation as a Postman collection. This section of the readme will be a walkthrough of how to use the API endpoints with postman. 

1. navigate to /scalar/v1 and click "Download OpenAPI Document". 
2. Open postman and import the collection (it will be in the format of a json document)
3. Configure Postman to use OAuth 2.0 Client Credentials flow in the root of the collection. You will need the Client ID and Secret of the worker application that this service is leveraging. 
4. All subsequent API Requests will be configured to use "Parent" Auth so that it only needs to be configured/changed in 1 place. 
5. Test receiving a token by going to Authorization tab in the root of the collection and clicking "Get New Access Token".

You are now ready to start using the service.

---- 

### Identity Linking API Operating Procedure
The Identity Linking Engine provides two endpoints that will be used to start and stop the batch processing of linking identities to all 3 targets. 

API Endpoints: 
|           Action            |                    Endpoint                      |                Description                        |
|-----------------------------|--------------------------------------------------|---------------------------------------------------|
| **Start Identity Linking**  | `/api/v1/identitylinking/recurringbatchjob/start`| Primary key; uniquely identifies each record.     |
| **Stop Identity Linking**   | `/api/v1/identitylinking/recurringbatchjob/stop`  | Primary key; uniquely identifies each record.    |

----

#### /api/v1/identitylinking/recurringbatchjob/start Endpoint 
When the start is invoked, the service will run through the queue and process X identities at a time at a frequency of Y. Where X is the appsetting for BulkProcessingBatchSize and Y is the appsetting for BulkProcessingBatchSchedule. In the example configuration in this document, the service is configured to process 25 identities every minute. In the background this will configure a hangfire job that can be monitored via the hangfire dashbaord (/hangfire).  This will show you all successful, currently processing, and failed jobs with error messages and all. 

#### /api/v1/identitylinking/recurringbatchjob/stop Endpoint
When the stop endpoint is invoked, the service will remove the configured hangfire job that was seutp in the 'start' endpoint. This will cancel all future jobs for processing and can be triggered at anytime of this process. Jobs can also be managed (canceled/deleted/stopped) through the hangfire dashbaord at (/hangfire).

### Other Identity Linking API Endpoints
These are some helpful identity linking endpoints that can be leveraged for triage situations or situations where a fallback script needs to handle ad-hoc one off linking creations. These endpoints will also cover unlinking/deleting identities for both ad-hoc one off operations and fully processed (as in unlinking all 3 targets at once or one at a time).


API Endpoints:

|         Action           |                Endpoint                      |                Description                        |
|--------------------------|----------------------------------------------|---------------------------------------------------|
| **Link Ping Federate**   | `/api/v1/identitylinking/pingfederate/create`| Primary key; uniquely identifies each record.     |
| **Link Entra ID**        | `/api/v1/identitylinking/microsoft/create`   | Timestamp indicating when the record was created. |
| **Link LDAP Gateway**    | `/api/v1/identitylinking/ldap/create`        | Describes the type/category of the record (e.g., sync, update). |
| **Link All Targets**     | `/api/v1/identitylinking/all/create`         | Timestamp of the most recent processing attempt. |
| **Unlink All Targets**   | `/api/v1/identitylinking/all/delete`         | Specifies the environment the record belongs to (e.g., Dev, QA, Prod). |
| **Unlink Ping Federate** | `/api/v1/identitylinking/pingfederate/delete`| User ID from the PingOne identity provider. |
| **Unlink Entra ID**      | `/api/v1/identitylinking/microsoft/delete`   | Object ID from Microsoft Entra ID (formerly Azure AD). |
| **Unlink LDAP Gateway**  | `/api/v1/identitylinking/ldap/delete`        | The user's SAM (Security Account Manager) account name. |

----