using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tarifkapida.Migrations
{
    /// <inheritdoc />
    public partial class _7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {            
            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "CATEGORY",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORY_RecipeId",
                table: "CATEGORY",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CATEGORY_RECIPE_RecipeId",
                table: "CATEGORY",
                column: "RecipeId",
                principalTable: "RECIPE",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CATEGORY_RECIPE_RecipeId",
                table: "CATEGORY");

            migrationBuilder.DropIndex(
                name: "IX_CATEGORY_RecipeId",
                table: "CATEGORY");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "CATEGORY");

        }
    }
}
