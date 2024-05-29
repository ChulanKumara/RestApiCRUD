using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmissionBreakdownApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmissionCategory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmissionCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmissionSubCategory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmissionSubCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmissionBreakdownRow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryId = table.Column<string>(type: "TEXT", nullable: false),
                    SubCategoryId = table.Column<string>(type: "TEXT", nullable: true),
                    TonsOfCO2 = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmissionBreakdownRow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmissionBreakdownRow_EmissionCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "EmissionCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmissionBreakdownRow_EmissionSubCategory_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "EmissionSubCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmissionBreakdownRow_CategoryId",
                table: "EmissionBreakdownRow",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EmissionBreakdownRow_SubCategoryId",
                table: "EmissionBreakdownRow",
                column: "SubCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmissionBreakdownRow");

            migrationBuilder.DropTable(
                name: "EmissionCategory");

            migrationBuilder.DropTable(
                name: "EmissionSubCategory");
        }
    }
}
