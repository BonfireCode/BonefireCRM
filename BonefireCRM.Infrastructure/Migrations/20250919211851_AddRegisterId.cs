using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BonefireCRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRegisterId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "RegisterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "RegisterId",
                table: "Users",
                newName: "PasswordHash");
        }
    }
}
