using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tarifkapida.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USERPROFILE_USER_UserId",
                table: "USERPROFILE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USERPROFILE",
                table: "USERPROFILE");

            migrationBuilder.DropIndex(
                name: "IX_USERPROFILE_UserId",
                table: "USERPROFILE");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "USERPROFILE");

            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "USERPROFILE");

            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "USERPROFILE");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "USERPROFILE");

            migrationBuilder.RenameTable(
                name: "USERPROFILE",
                newName: "UserProfiles");

            migrationBuilder.AddColumn<int>(
                name: "UserProfileUserId",
                table: "USER",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProfileImageUrl",
                table: "UserProfiles",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "UserProfiles",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "UserProfiles",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "UserProfiles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UserProfiles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "UserProfiles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "UserProfiles",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "UserProfiles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "UserProfiles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "UserProfiles",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfiles",
                table: "UserProfiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_UserProfileUserId",
                table: "USER",
                column: "UserProfileUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_USER_UserProfiles_UserProfileUserId",
                table: "USER",
                column: "UserProfileUserId",
                principalTable: "UserProfiles",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_USER_UserId",
                table: "UserProfiles",
                column: "UserId",
                principalTable: "USER",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USER_UserProfiles_UserProfileUserId",
                table: "USER");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_USER_UserId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_USER_UserProfileUserId",
                table: "USER");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfiles",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "UserProfileUserId",
                table: "USER");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "UserProfiles");

            migrationBuilder.RenameTable(
                name: "UserProfiles",
                newName: "USERPROFILE");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileImageUrl",
                table: "USERPROFILE",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "USERPROFILE",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "USERPROFILE",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "USERPROFILE",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "USERPROFILE",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "USERPROFILE",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_USERPROFILE",
                table: "USERPROFILE",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_USERPROFILE_UserId",
                table: "USERPROFILE",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_USERPROFILE_USER_UserId",
                table: "USERPROFILE",
                column: "UserId",
                principalTable: "USER",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
