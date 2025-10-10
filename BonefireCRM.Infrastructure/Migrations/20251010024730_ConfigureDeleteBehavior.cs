using System;
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
                name: "FK_Activities_Deals_DealId",
                table: "Activities");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContactId",
                table: "Activities",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Companies_CompanyId",
                table: "Activities",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Deals_DealId",
                table: "Activities",
                column: "DealId",
                principalTable: "Deals",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Companies_CompanyId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Deals_DealId",
                table: "Activities");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContactId",
                table: "Activities",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Companies_CompanyId",
                table: "Activities",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Deals_DealId",
                table: "Activities",
                column: "DealId",
                principalTable: "Deals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
