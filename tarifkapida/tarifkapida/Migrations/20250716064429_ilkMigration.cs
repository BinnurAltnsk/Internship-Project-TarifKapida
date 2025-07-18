using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tarifkapida.Migrations
{
    /// <inheritdoc />
    public partial class ilkMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "RECIPE",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipeIngredients = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipeInstructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipeImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    RecipeCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecipeUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RECIPE", x => x.RecipeId);
                    table.ForeignKey(
                        name: "FK_RECIPE_USER_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "USER",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "REVIEW",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ReviewText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    ReviewCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReviewUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REVIEW", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_REVIEW_RECIPE_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "RECIPE",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RECIPE_UsersUserId",
                table: "RECIPE",
                column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_REVIEW_RecipeId",
                table: "REVIEW",
                column: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "REVIEW");

            migrationBuilder.DropTable(
                name: "RECIPE");

            migrationBuilder.DropTable(
                name: "USER");
        }
    }
}
