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
            migrationBuilder.AddColumn<int>(
                name: "LinkedSocialAccountId",
                table: "UserProfiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NotificationSettingsId",
                table: "UserProfiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LinkedSocialAccount",
                columns: table => new
                {
                    LinkedSocialAccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserProfileUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkedSocialAccount", x => x.LinkedSocialAccountId);
                    table.ForeignKey(
                        name: "FK_LinkedSocialAccount_UserProfiles_UserProfileUserId",
                        column: x => x.UserProfileUserId,
                        principalTable: "UserProfiles",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "NotificationSettings",
                columns: table => new
                {
                    NotificationSettingsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailNotifications = table.Column<bool>(type: "bit", nullable: false),
                    PushNotifications = table.Column<bool>(type: "bit", nullable: false),
                    SMSNotifications = table.Column<bool>(type: "bit", nullable: false),
                    NewsletterSubscription = table.Column<bool>(type: "bit", nullable: false),
                    PromotionalOffers = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSettings", x => x.NotificationSettingsId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_LinkedSocialAccountId",
                table: "UserProfiles",
                column: "LinkedSocialAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_NotificationSettingsId",
                table: "UserProfiles",
                column: "NotificationSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkedSocialAccount_UserProfileUserId",
                table: "LinkedSocialAccount",
                column: "UserProfileUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_LinkedSocialAccount_LinkedSocialAccountId",
                table: "UserProfiles",
                column: "LinkedSocialAccountId",
                principalTable: "LinkedSocialAccount",
                principalColumn: "LinkedSocialAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_NotificationSettings_NotificationSettingsId",
                table: "UserProfiles",
                column: "NotificationSettingsId",
                principalTable: "NotificationSettings",
                principalColumn: "NotificationSettingsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_LinkedSocialAccount_LinkedSocialAccountId",
                table: "UserProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_NotificationSettings_NotificationSettingsId",
                table: "UserProfiles");

            migrationBuilder.DropTable(
                name: "LinkedSocialAccount");

            migrationBuilder.DropTable(
                name: "NotificationSettings");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_LinkedSocialAccountId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_NotificationSettingsId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "LinkedSocialAccountId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "NotificationSettingsId",
                table: "UserProfiles");
        }
    }
}
