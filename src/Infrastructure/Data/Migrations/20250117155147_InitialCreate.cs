using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "APM_ServiceNow_Applications_Info",
                columns: table => new
                {
                    PrimaryKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArchitectureType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstallType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserBase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Platform = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessCriticality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataClassification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItApplicationOwner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyTier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstallStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupportGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupportedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vendor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APM_ServiceNow_Applications_Info", x => x.PrimaryKey);
                });

            migrationBuilder.CreateTable(
                name: "IDM_PingID_Certificates_List",
                columns: table => new
                {
                    PrimaryKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PrimaryVerificationCert = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondaryVerificationCert = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EncryptionCert = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveVerificationCert = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    X509FileId = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FileData = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    Id = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SubjectDN = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SubjectAlternativeNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssuerDN = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KeyAlgorithm = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    KeySize = table.Column<int>(type: "int", nullable: false),
                    SignatureAlgorithm = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDM_PingID_Certificates_List", x => x.PrimaryKey);
                });

            migrationBuilder.CreateTable(
                name: "IDM_PingID_Connections_List",
                columns: table => new
                {
                    PrimaryKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PingConnectionID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConnectionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessOwner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TechnicalOwner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ACSEndpoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConditionalIssuanceCriteria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpressionIssuanceCriteria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Environment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    APMNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDM_PingID_Connections_List", x => x.PrimaryKey);
                });

            migrationBuilder.CreateTable(
                name: "IDM_PingID_OIDC_Information",
                columns: table => new
                {
                    PrimaryKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefreshRolling = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RefreshTokenRollingIntervalType = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PersistentGrantExpirationType = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PersistentGrantExpirationTime = table.Column<int>(type: "int", nullable: false),
                    PersistentGrantExpirationTimeUnit = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PersistentGrantIdleTimeoutType = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PersistentGrantIdleTimeout = table.Column<int>(type: "int", nullable: false),
                    PersistentGrantIdleTimeoutTimeUnit = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PersistentGrantReuseType = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AllowAuthenticationApiInit = table.Column<bool>(type: "bit", nullable: false),
                    BypassApprovalPage = table.Column<bool>(type: "bit", nullable: false),
                    RestrictScopes = table.Column<bool>(type: "bit", nullable: false),
                    RequirePushedAuthorizationRequests = table.Column<bool>(type: "bit", nullable: false),
                    RequireJwtSecuredAuthorizationResponseMode = table.Column<bool>(type: "bit", nullable: false),
                    RestrictToDefaultAccessTokenManager = table.Column<bool>(type: "bit", nullable: false),
                    ValidateUsingAllEligibleAtms = table.Column<bool>(type: "bit", nullable: false),
                    DeviceFlowSettingType = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RequireProofKeyForCodeExchange = table.Column<bool>(type: "bit", nullable: false),
                    RefreshTokenRollingGracePeriodType = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ClientSecretRetentionPeriodType = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ClientSecretChangedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequireDpop = table.Column<bool>(type: "bit", nullable: false),
                    RequireSignedRequests = table.Column<bool>(type: "bit", nullable: false),
                    RedirectUris = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrantTypes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestrictedScopes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExclusiveScopes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestrictedResponseTypes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorizationDetailTypes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OidcPolicyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConditionalIssuanceCriteria = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ExpressionIssuanceCriteria = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDM_PingID_OIDC_Information", x => x.PrimaryKey);
                });

            migrationBuilder.CreateTable(
                name: "IDM_PIngID_SAML_Information",
                columns: table => new
                {
                    PrimaryKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    BaseUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Protocol = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EnabledProfiles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdpInitiatedLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AcsUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SignAssertions = table.Column<bool>(type: "bit", nullable: false),
                    SignResponseAsRequired = table.Column<bool>(type: "bit", maxLength: 500, nullable: false),
                    RequireSignedAuthnRequests = table.Column<bool>(type: "bit", maxLength: 500, nullable: false),
                    NameIdPolicy = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ConditionalIssuanceCriteria = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ExpressionIssuanceCriteria = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    ApplicationName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ContactInfoCompany = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ContactInfoFirstName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ContactInfoLastName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ContactInfoEmail = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ContactInfoNumber = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDM_PIngID_SAML_Information", x => x.PrimaryKey);
                });

            migrationBuilder.CreateTable(
                name: "IDM_PingID_Attributes_List",
                columns: table => new
                {
                    PrimaryKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConnectionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConnectionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OidcClientPrimaryKey = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SpConnectionPrimaryKey = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDM_PingID_Attributes_List", x => x.PrimaryKey);
                    table.ForeignKey(
                        name: "FK_IDM_PingID_Attributes_List_IDM_PIngID_SAML_Information_SpConnectionPrimaryKey",
                        column: x => x.SpConnectionPrimaryKey,
                        principalTable: "IDM_PIngID_SAML_Information",
                        principalColumn: "PrimaryKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IDM_PingID_Attributes_List_IDM_PingID_OIDC_Information_OidcClientPrimaryKey",
                        column: x => x.OidcClientPrimaryKey,
                        principalTable: "IDM_PingID_OIDC_Information",
                        principalColumn: "PrimaryKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IDM_PingID_Attributes_List_OidcClientPrimaryKey",
                table: "IDM_PingID_Attributes_List",
                column: "OidcClientPrimaryKey");

            migrationBuilder.CreateIndex(
                name: "IX_IDM_PingID_Attributes_List_SpConnectionPrimaryKey",
                table: "IDM_PingID_Attributes_List",
                column: "SpConnectionPrimaryKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APM_ServiceNow_Applications_Info");

            migrationBuilder.DropTable(
                name: "IDM_PingID_Attributes_List");

            migrationBuilder.DropTable(
                name: "IDM_PingID_Certificates_List");

            migrationBuilder.DropTable(
                name: "IDM_PingID_Connections_List");

            migrationBuilder.DropTable(
                name: "IDM_PIngID_SAML_Information");

            migrationBuilder.DropTable(
                name: "IDM_PingID_OIDC_Information");
        }
    }
}
