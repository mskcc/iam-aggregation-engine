using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ServiceNowApplicationEnhacements04072025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApiAccountLifeCycleManagement",
                table: "APM_ServiceNow_Applications_Info",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthenticationProtocols",
                table: "APM_ServiceNow_Applications_Info",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsExternallyFacing",
                table: "APM_ServiceNow_Applications_Info",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LifeCycleStageStatus",
                table: "APM_ServiceNow_Applications_Info",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsesEnterpriseIdentitiesNetworkId",
                table: "APM_ServiceNow_Applications_Info",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiAccountLifeCycleManagement",
                table: "APM_ServiceNow_Applications_Info");

            migrationBuilder.DropColumn(
                name: "AuthenticationProtocols",
                table: "APM_ServiceNow_Applications_Info");

            migrationBuilder.DropColumn(
                name: "IsExternallyFacing",
                table: "APM_ServiceNow_Applications_Info");

            migrationBuilder.DropColumn(
                name: "LifeCycleStageStatus",
                table: "APM_ServiceNow_Applications_Info");

            migrationBuilder.DropColumn(
                name: "UsesEnterpriseIdentitiesNetworkId",
                table: "APM_ServiceNow_Applications_Info");
        }
    }
}
