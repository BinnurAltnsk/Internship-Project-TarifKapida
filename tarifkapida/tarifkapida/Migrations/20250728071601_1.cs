using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tarifkapida.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CATEGORY_RECIPE_RecipeId",
                table: "CATEGORY");

            migrationBuilder.DropIndex(
                name: "IX_CATEGORY_RecipeId",
                table: "CATEGORY");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CATEGORY");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "CATEGORY");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CATEGORY");

            migrationBuilder.AddColumn<int>(
                name: "ParentCategoryId",
                table: "CATEGORY",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentCategoryId",
                table: "CATEGORY");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CATEGORY",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "CATEGORY",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CATEGORY",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
    }
}
