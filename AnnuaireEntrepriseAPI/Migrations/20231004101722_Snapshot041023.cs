using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnnuaireEntrepriseAPI.Migrations
{
    /// <inheritdoc />
    public partial class Snapshot041023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Service__737584F71EBE5F88", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Site",
                columns: table => new
                {
                    Town = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Site__FF37FFF7D8B84281", x => x.Town);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    MobilePhone = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    Service = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Site = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ServiceNavigationName = table.Column<string>(type: "nvarchar(70)", nullable: false),
                    SiteNavigationTown = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__3214EC07918F8F15", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Service_ServiceNavigationName",
                        column: x => x.ServiceNavigationName,
                        principalTable: "Service",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Site_SiteNavigationTown",
                        column: x => x.SiteNavigationTown,
                        principalTable: "Site",
                        principalColumn: "Town",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ServiceNavigationName",
                table: "Users",
                column: "ServiceNavigationName");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SiteNavigationTown",
                table: "Users",
                column: "SiteNavigationTown");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Site");
        }
    }
}
