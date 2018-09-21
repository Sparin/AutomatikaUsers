using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutomatikaUsers.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Software",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Software", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdentityName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSoftware",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    SoftwareId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSoftware", x => new { x.UserId, x.SoftwareId });
                    table.ForeignKey(
                        name: "FK_UserSoftware_Software_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "Software",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSoftware_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Software",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Microsoft Office 2017" },
                    { 2, "Microsoft Visual Studio 2017" },
                    { 3, "CCleaner" },
                    { 4, "Discord" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "IdentityName", "LastName" },
                values: new object[,]
                {
                    { 1, "sparin285@gmail.com", "Yuriy", "Sparin", "Medveditskov" },
                    { 2, null, null, "MockUser2", null },
                    { 3, null, null, "MockUser3", null }
                });

            migrationBuilder.InsertData(
                table: "UserSoftware",
                columns: new[] { "UserId", "SoftwareId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 4 },
                    { 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Software_Name",
                table: "Software",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdentityName",
                table: "Users",
                column: "IdentityName",
                unique: true,
                filter: "[IdentityName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserSoftware_SoftwareId",
                table: "UserSoftware",
                column: "SoftwareId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSoftware");

            migrationBuilder.DropTable(
                name: "Software");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
