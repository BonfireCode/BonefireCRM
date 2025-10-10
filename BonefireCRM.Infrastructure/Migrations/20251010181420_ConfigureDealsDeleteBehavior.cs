using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BonefireCRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureDealsDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Users_UserId",
                table: "Deals");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Users_UserId",
                table: "Deals",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Users_UserId",
                table: "Deals");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Users_UserId",
                table: "Deals",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
