using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityLinkingQueueAndArchiveTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ping_IdentityLinking_Processing_Request_Archive",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastProcessingAttempt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Environment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PingOneUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntraObjectId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SamAccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attempts = table.Column<int>(type: "int", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsProcessedSuccessfully = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ping_IdentityLinking_Processing_Request_Archive", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ping_IdentityLinking_Processing_Request_Queue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastProcessingAttempt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Environment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PingOneUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntraObjectId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SamAccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attempts = table.Column<int>(type: "int", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsProcessedSuccessfully = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ping_IdentityLinking_Processing_Request_Queue", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ping_IdentityLinking_Processing_Request_Archive");

            migrationBuilder.DropTable(
                name: "Ping_IdentityLinking_Processing_Request_Queue");
        }
    }
}
