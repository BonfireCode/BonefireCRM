using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BonefireCRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class change_DealParticipant_index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DealParticipants_DealId_ContactId",
                table: "DealParticipants");

            migrationBuilder.CreateIndex(
                name: "IX_DealParticipants_DealId_ContactId_DealParticipantRoleId",
                table: "DealParticipants",
                columns: new[] { "DealId", "ContactId", "DealParticipantRoleId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DealParticipants_DealId_ContactId_DealParticipantRoleId",
                table: "DealParticipants");

            migrationBuilder.CreateIndex(
                name: "IX_DealParticipants_DealId_ContactId",
                table: "DealParticipants",
                columns: new[] { "DealId", "ContactId" },
                unique: true);
        }
    }
}
