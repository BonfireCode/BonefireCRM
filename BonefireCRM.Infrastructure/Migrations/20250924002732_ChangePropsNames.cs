using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BonefireCRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangePropsNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DealParticipantRoles_Users_UserId",
                table: "DealParticipantRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Contacts_ContactId",
                table: "Deals");

            migrationBuilder.RenameColumn(
                name: "ContactId",
                table: "Deals",
                newName: "PrimaryContactId");

            migrationBuilder.RenameIndex(
                name: "IX_Deals_ContactId",
                table: "Deals",
                newName: "IX_Deals_PrimaryContactId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "DealParticipantRoles",
                newName: "RegisteredByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_DealParticipantRoles_UserId",
                table: "DealParticipantRoles",
                newName: "IX_DealParticipantRoles_RegisteredByUserId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DealParticipantRoles_Users_RegisteredByUserId",
                table: "DealParticipantRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Contacts_PrimaryContactId",
                table: "Deals");

            migrationBuilder.RenameColumn(
                name: "PrimaryContactId",
                table: "Deals",
                newName: "ContactId");

            migrationBuilder.RenameIndex(
                name: "IX_Deals_PrimaryContactId",
                table: "Deals",
                newName: "IX_Deals_ContactId");

            migrationBuilder.RenameColumn(
                name: "RegisteredByUserId",
                table: "DealParticipantRoles",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DealParticipantRoles_RegisteredByUserId",
                table: "DealParticipantRoles",
                newName: "IX_DealParticipantRoles_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DealParticipantRoles_Users_UserId",
                table: "DealParticipantRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Contacts_ContactId",
                table: "Deals",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
