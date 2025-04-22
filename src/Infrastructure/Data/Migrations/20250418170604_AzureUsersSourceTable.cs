using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AzureUsersSourceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "...",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GivenName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobilePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreferredLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPrincipalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountEnabled = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OnPremisesDistinguishedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnPremisesDomainName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnPremisesLastSyncDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OnPremisesSamAccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnPremisesSecurityIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnPremisesSyncEnabled = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnPremisesUserPrincipalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsageLocation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "...");
        }
    }
}
