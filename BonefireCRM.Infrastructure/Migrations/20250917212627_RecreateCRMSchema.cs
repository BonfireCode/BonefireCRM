using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BonefireCRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RecreateCRMSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Contacts_ContactId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Users_AssignedToId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Companies_CompanyId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_CreatedByUserId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Contacts_PrimaryContactId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Users_AssignedToUserId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowUpReminders_Contacts_ContactId",
                table: "FollowUpReminders");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowUpReminders_Deals_DealId",
                table: "FollowUpReminders");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowUpReminders_Users_AssignedToUserId",
                table: "FollowUpReminders");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowUpReminders_Users_CreatedByUserId",
                table: "FollowUpReminders");

            migrationBuilder.DropTable(
                name: "ContactDeal");

            migrationBuilder.DropTable(
                name: "ContactTag");

            migrationBuilder.DropTable(
                name: "Interactions");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_FollowUpReminders_AssignedToUserId",
                table: "FollowUpReminders");

            migrationBuilder.DropIndex(
                name: "IX_FollowUpReminders_ContactId",
                table: "FollowUpReminders");

            migrationBuilder.DropIndex(
                name: "IX_FollowUpReminders_DealId",
                table: "FollowUpReminders");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_CreatedByUserId",
                table: "Contacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_AssignedToId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastLoginDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AssignedToUserId",
                table: "FollowUpReminders");

            migrationBuilder.DropColumn(
                name: "CompletedDateTime",
                table: "FollowUpReminders");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "FollowUpReminders");

            migrationBuilder.DropColumn(
                name: "DealId",
                table: "FollowUpReminders");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "FollowUpReminders");

            migrationBuilder.DropColumn(
                name: "Stage",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "AssignedToId",
                table: "Assignments");

            migrationBuilder.RenameTable(
                name: "Assignments",
                newName: "Activities");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "FollowUpReminders",
                newName: "DueDate");

            migrationBuilder.RenameColumn(
                name: "ReminderDateTime",
                table: "FollowUpReminders",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Priority",
                table: "FollowUpReminders",
                newName: "ReminderId");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "FollowUpReminders",
                newName: "ActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_FollowUpReminders_CreatedByUserId",
                table: "FollowUpReminders",
                newName: "IX_FollowUpReminders_ActivityId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Deals",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "EstimatedValue",
                table: "Deals",
                newName: "StageId");

            migrationBuilder.RenameColumn(
                name: "AssignedToUserId",
                table: "Deals",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Deals_AssignedToUserId",
                table: "Deals",
                newName: "IX_Deals_CompanyId");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Contacts",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Contacts",
                newName: "LifecycleStageId");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Activities",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_ContactId",
                table: "Activities",
                newName: "IX_Activities_ContactId");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "FollowUpReminders",
                type: "TEXT",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "PrimaryContactId",
                table: "Deals",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpectedCloseDate",
                table: "Deals",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Deals",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Deals",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Contacts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Contacts",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Contacts",
                type: "TEXT",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Contacts",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Companies",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Companies",
                type: "TEXT",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsCompleted",
                table: "Activities",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "Activities",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "ActivityType",
                table: "Activities",
                type: "TEXT",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Activities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CallTime",
                table: "Activities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Activities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DealId",
                table: "Activities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Activities",
                type: "TEXT",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email_Subject",
                table: "Activities",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Activities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsIncoming",
                table: "Activities",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Meeting_Notes",
                table: "Activities",
                type: "TEXT",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Meeting_Subject",
                table: "Activities",
                type: "TEXT",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Activities",
                type: "TEXT",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SentAt",
                table: "Activities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Activities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Activities",
                type: "TEXT",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Activities",
                table: "Activities",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DealContactRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    RegisteredByUserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealContactRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DealContactRoles_Users_RegisteredByUserId",
                        column: x => x.RegisteredByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LifecycleStages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifecycleStages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pipelines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    IsDefault = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pipelines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DealContacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DealId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ContactId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DealContactRoleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DealContacts_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DealContacts_DealContactRoles_DealContactRoleId",
                        column: x => x.DealContactRoleId,
                        principalTable: "DealContactRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DealContacts_Deals_DealId",
                        column: x => x.DealId,
                        principalTable: "Deals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PipelineStages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    OrderIndex = table.Column<int>(type: "INTEGER", nullable: false),
                    IsClosedWon = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsClosedLost = table.Column<bool>(type: "INTEGER", nullable: false),
                    PipelineId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PipelineStages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PipelineStages_Pipelines_PipelineId",
                        column: x => x.PipelineId,
                        principalTable: "Pipelines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpReminders_DueDate",
                table: "FollowUpReminders",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_Deals_StageId",
                table: "Deals",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_Deals_UserId",
                table: "Deals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Email",
                table: "Contacts",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_LifecycleStageId",
                table: "Contacts",
                column: "LifecycleStageId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_UserId",
                table: "Contacts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_CompanyId",
                table: "Activities",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_DealId",
                table: "Activities",
                column: "DealId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_DueDate",
                table: "Activities",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_SentAt",
                table: "Activities",
                column: "SentAt");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_UserId",
                table: "Activities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DealContactRoles_RegisteredByUserId",
                table: "DealContactRoles",
                column: "RegisteredByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DealContacts_ContactId",
                table: "DealContacts",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_DealContacts_DealContactRoleId",
                table: "DealContacts",
                column: "DealContactRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_DealContacts_DealId_ContactId",
                table: "DealContacts",
                columns: new[] { "DealId", "ContactId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PipelineStages_PipelineId_OrderIndex",
                table: "PipelineStages",
                columns: new[] { "PipelineId", "OrderIndex" },
                unique: true);

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
                name: "FK_Contacts_Companies_CompanyId",
                table: "Contacts",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_LifecycleStages_LifecycleStageId",
                table: "Contacts",
                column: "LifecycleStageId",
                principalTable: "LifecycleStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_UserId",
                table: "Contacts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Companies_CompanyId",
                table: "Deals",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Contacts_PrimaryContactId",
                table: "Deals",
                column: "PrimaryContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_PipelineStages_StageId",
                table: "Deals",
                column: "StageId",
                principalTable: "PipelineStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Users_UserId",
                table: "Deals",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowUpReminders_Activities_ActivityId",
                table: "FollowUpReminders",
                column: "ActivityId",
                principalTable: "Activities",
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
                name: "FK_Contacts_Companies_CompanyId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_LifecycleStages_LifecycleStageId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_UserId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Companies_CompanyId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Contacts_PrimaryContactId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Deals_PipelineStages_StageId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Users_UserId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowUpReminders_Activities_ActivityId",
                table: "FollowUpReminders");

            migrationBuilder.DropTable(
                name: "DealContacts");

            migrationBuilder.DropTable(
                name: "LifecycleStages");

            migrationBuilder.DropTable(
                name: "PipelineStages");

            migrationBuilder.DropTable(
                name: "DealContactRoles");

            migrationBuilder.DropTable(
                name: "Pipelines");

            migrationBuilder.DropIndex(
                name: "IX_FollowUpReminders_DueDate",
                table: "FollowUpReminders");

            migrationBuilder.DropIndex(
                name: "IX_Deals_StageId",
                table: "Deals");

            migrationBuilder.DropIndex(
                name: "IX_Deals_UserId",
                table: "Deals");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_Email",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_LifecycleStageId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_UserId",
                table: "Contacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Activities",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_CompanyId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_DealId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_DueDate",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_SentAt",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_UserId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "FollowUpReminders");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ActivityType",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Body",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "CallTime",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "DealId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Email_Subject",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "IsIncoming",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Meeting_Notes",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Meeting_Subject",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "SentAt",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Activities");

            migrationBuilder.RenameTable(
                name: "Activities",
                newName: "Assignments");

            migrationBuilder.RenameColumn(
                name: "ReminderId",
                table: "FollowUpReminders",
                newName: "Priority");

            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "FollowUpReminders",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "FollowUpReminders",
                newName: "ReminderDateTime");

            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "FollowUpReminders",
                newName: "CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_FollowUpReminders_ActivityId",
                table: "FollowUpReminders",
                newName: "IX_FollowUpReminders_CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Deals",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "StageId",
                table: "Deals",
                newName: "EstimatedValue");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Deals",
                newName: "AssignedToUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Deals_CompanyId",
                table: "Deals",
                newName: "IX_Deals_AssignedToUserId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Contacts",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "LifecycleStageId",
                table: "Contacts",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Assignments",
                newName: "Title");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_ContactId",
                table: "Assignments",
                newName: "IX_Assignments_ContactId");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginDate",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "AssignedToUserId",
                table: "FollowUpReminders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedDateTime",
                table: "FollowUpReminders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContactId",
                table: "FollowUpReminders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DealId",
                table: "FollowUpReminders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "FollowUpReminders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PrimaryContactId",
                table: "Deals",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpectedCloseDate",
                table: "Deals",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "Stage",
                table: "Deals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                table: "Contacts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "Contacts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Contacts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Companies",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsCompleted",
                table: "Assignments",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "Assignments",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AssignedToId",
                table: "Assignments",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ContactDeal",
                columns: table => new
                {
                    AssociatedContactsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DealId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDeal", x => new { x.AssociatedContactsId, x.DealId });
                    table.ForeignKey(
                        name: "FK_ContactDeal_Contacts_AssociatedContactsId",
                        column: x => x.AssociatedContactsId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactDeal_Deals_DealId",
                        column: x => x.DealId,
                        principalTable: "Deals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ContactId = table.Column<Guid>(type: "TEXT", nullable: true),
                    DealId = table.Column<Guid>(type: "TEXT", nullable: true),
                    LoggedByUserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InteractionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    Summary = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interactions_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Interactions_Deals_DealId",
                        column: x => x.DealId,
                        principalTable: "Deals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Interactions_Users_LoggedByUserId",
                        column: x => x.LoggedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ColorHex = table.Column<string>(type: "TEXT", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactTag",
                columns: table => new
                {
                    ContactsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TagsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTag", x => new { x.ContactsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_ContactTag_Contacts_ContactsId",
                        column: x => x.ContactsId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpReminders_AssignedToUserId",
                table: "FollowUpReminders",
                column: "AssignedToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpReminders_ContactId",
                table: "FollowUpReminders",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpReminders_DealId",
                table: "FollowUpReminders",
                column: "DealId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CreatedByUserId",
                table: "Contacts",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_AssignedToId",
                table: "Assignments",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDeal_DealId",
                table: "ContactDeal",
                column: "DealId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactTag_TagsId",
                table: "ContactTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_ContactId",
                table: "Interactions",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_DealId",
                table: "Interactions",
                column: "DealId");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_LoggedByUserId",
                table: "Interactions",
                column: "LoggedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Contacts_ContactId",
                table: "Assignments",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Users_AssignedToId",
                table: "Assignments",
                column: "AssignedToId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Companies_CompanyId",
                table: "Contacts",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_CreatedByUserId",
                table: "Contacts",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Contacts_PrimaryContactId",
                table: "Deals",
                column: "PrimaryContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Users_AssignedToUserId",
                table: "Deals",
                column: "AssignedToUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FollowUpReminders_Contacts_ContactId",
                table: "FollowUpReminders",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FollowUpReminders_Deals_DealId",
                table: "FollowUpReminders",
                column: "DealId",
                principalTable: "Deals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FollowUpReminders_Users_AssignedToUserId",
                table: "FollowUpReminders",
                column: "AssignedToUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FollowUpReminders_Users_CreatedByUserId",
                table: "FollowUpReminders",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
