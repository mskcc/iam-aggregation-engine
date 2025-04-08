using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SupportServiceNowUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "APM_ServiceNow_Users_Info",
                columns: table => new
                {
                    PrimaryKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SysId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APM_ServiceNow_Users_Info", x => x.PrimaryKey);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APM_ServiceNow_Users_Info");
        }
    }
}
