using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website.Migrations
{
    /// <inheritdoc />
    public partial class Discord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "discord-categories",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ReadRoles = table.Column<string>(type: "TEXT", nullable: false),
                    WriteRoles = table.Column<string>(type: "TEXT", nullable: false),
                    AdminRoles = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discord-categories", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "discord-roles",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Colour = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discord-roles", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "discord-channels",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CategoryName = table.Column<string>(type: "TEXT", nullable: false),
                    ReadRoles = table.Column<string>(type: "TEXT", nullable: false),
                    WriteRoles = table.Column<string>(type: "TEXT", nullable: false),
                    AdminRoles = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discord-channels", x => new { x.Name, x.CategoryName });
                    table.ForeignKey(
                        name: "FK_discord-channels_discord-categories_CategoryName",
                        column: x => x.CategoryName,
                        principalTable: "discord-categories",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_discord-channels_CategoryName",
                table: "discord-channels",
                column: "CategoryName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "discord-channels");

            migrationBuilder.DropTable(
                name: "discord-roles");

            migrationBuilder.DropTable(
                name: "discord-categories");
        }
    }
}
