using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnnuaireEntrepriseAPI.Migrations
{
    /// <inheritdoc />
    public partial class Snapshot0410232 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Service_ServiceNavigationName",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Site_SiteNavigationTown",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ServiceNavigationName",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_SiteNavigationTown",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Site__FF37FFF7D8B84281",
                table: "Site");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Service__737584F71EBE5F88",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "ServiceNavigationName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SiteNavigationTown",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "ServiceNavigationId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SiteNavigationId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Site",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Service",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Site__FF37FFF7D8B84281",
                table: "Site",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Service__737584F71EBE5F88",
                table: "Service",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ServiceNavigationId",
                table: "Users",
                column: "ServiceNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SiteNavigationId",
                table: "Users",
                column: "SiteNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Service_ServiceNavigationId",
                table: "Users",
                column: "ServiceNavigationId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Site_SiteNavigationId",
                table: "Users",
                column: "SiteNavigationId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Service_ServiceNavigationId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Site_SiteNavigationId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ServiceNavigationId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_SiteNavigationId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Site__FF37FFF7D8B84281",
                table: "Site");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Service__737584F71EBE5F88",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "ServiceNavigationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SiteNavigationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Site");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Service");

            migrationBuilder.AddColumn<string>(
                name: "ServiceNavigationName",
                table: "Users",
                type: "nvarchar(70)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SiteNavigationTown",
                table: "Users",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Site__FF37FFF7D8B84281",
                table: "Site",
                column: "Town");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Service__737584F71EBE5F88",
                table: "Service",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ServiceNavigationName",
                table: "Users",
                column: "ServiceNavigationName");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SiteNavigationTown",
                table: "Users",
                column: "SiteNavigationTown");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Service_ServiceNavigationName",
                table: "Users",
                column: "ServiceNavigationName",
                principalTable: "Service",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Site_SiteNavigationTown",
                table: "Users",
                column: "SiteNavigationTown",
                principalTable: "Site",
                principalColumn: "Town",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
