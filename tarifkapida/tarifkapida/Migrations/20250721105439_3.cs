using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tarifkapida.Migrations
{
    /// <inheritdoc />
    public partial class _3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RECIPE_USER_UsersUserId",
                table: "RECIPE");

            migrationBuilder.DropIndex(
                name: "IX_RECIPE_UsersUserId",
                table: "RECIPE");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "USER");

            migrationBuilder.DropColumn(
                name: "UsersUserId",
                table: "RECIPE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecipeUpdatedAt",
                table: "RECIPE",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecipeCreatedAt",
                table: "RECIPE",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "ReviewId",
                table: "RECIPE",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_REVIEW_UserId",
                table: "REVIEW",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RECIPE_UserId",
                table: "RECIPE",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RECIPE_USER_UserId",
                table: "RECIPE",
                column: "UserId",
                principalTable: "USER",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_REVIEW_USER_UserId",
                table: "REVIEW",
                column: "UserId",
                principalTable: "USER",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RECIPE_USER_UserId",
                table: "RECIPE");

            migrationBuilder.DropForeignKey(
                name: "FK_REVIEW_USER_UserId",
                table: "REVIEW");

            migrationBuilder.DropIndex(
                name: "IX_REVIEW_UserId",
                table: "REVIEW");

            migrationBuilder.DropIndex(
                name: "IX_RECIPE_UserId",
                table: "RECIPE");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "RECIPE");

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "USER",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecipeUpdatedAt",
                table: "RECIPE",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecipeCreatedAt",
                table: "RECIPE",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsersUserId",
                table: "RECIPE",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RECIPE_UsersUserId",
                table: "RECIPE",
                column: "UsersUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RECIPE_USER_UsersUserId",
                table: "RECIPE",
                column: "UsersUserId",
                principalTable: "USER",
                principalColumn: "UserId");
        }
    }
}
