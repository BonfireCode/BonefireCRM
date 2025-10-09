using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BonefireCRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Companies_CompanyId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Contacts_ContactId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Deals_DealId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Users_UserId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_LifecycleStages_LifecycleStageId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_DealParticipantRoles_Users_RegisteredByUserId",
                table: "DealParticipantRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Contacts_PrimaryContactId",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "ReminderId",
                table: "FollowUpReminders");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Companies_CompanyId",
                table: "Activities",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Contacts_ContactId",
                table: "Activities",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Deals_DealId",
                table: "Activities",
                column: "DealId",
                principalTable: "Deals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Users_UserId",
                table: "Activities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_LifecycleStages_LifecycleStageId",
                table: "Contacts",
                column: "LifecycleStageId",
                principalTable: "LifecycleStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DealParticipantRoles_Users_RegisteredByUserId",
                table: "DealParticipantRoles",
                column: "RegisteredByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Contacts_PrimaryContactId",
                table: "Deals",
                column: "PrimaryContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Companies_CompanyId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Contacts_ContactId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Deals_DealId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Users_UserId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_LifecycleStages_LifecycleStageId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_DealParticipantRoles_Users_RegisteredByUserId",
                table: "DealParticipantRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Contacts_PrimaryContactId",
                table: "Deals");

            migrationBuilder.AddColumn<int>(
                name: "ReminderId",
                table: "FollowUpReminders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Companies_CompanyId",
                table: "Activities",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Contacts_ContactId",
                table: "Activities",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Deals_DealId",
                table: "Activities",
                column: "DealId",
                principalTable: "Deals",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Users_UserId",
                table: "Activities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_LifecycleStages_LifecycleStageId",
                table: "Contacts",
                column: "LifecycleStageId",
                principalTable: "LifecycleStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DealParticipantRoles_Users_RegisteredByUserId",
                table: "DealParticipantRoles",
                column: "RegisteredByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Contacts_PrimaryContactId",
                table: "Deals",
                column: "PrimaryContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
