using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tarifkapida.Migrations
{
    /// <inheritdoc />
    public partial class _5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FavoriteRecipeId",
                table: "USER",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FavoriteCount",
                table: "RECIPE",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavoriteRecipeId",
                table: "USER");

            migrationBuilder.DropColumn(
                name: "FavoriteCount",
                table: "RECIPE");
        }
    }
}
