using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovaLite.ToDo.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assignee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    Number = table.Column<int>(type: "int", nullable: false),
                    AssigneeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reminder = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false),
                    AttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignment", x => new { x.Number, x.AssigneeId });
                    table.ForeignKey(
                        name: "FK_Assignment_Assignee_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "Assignee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assignment_Attachment_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Step",
                columns: table => new
                {
                    Number = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignmentNumber = table.Column<int>(type: "int", nullable: false),
                    AssigneeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Completed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Step", x => new { x.Number, x.AssignmentNumber, x.AssigneeId });
                    table.ForeignKey(
                        name: "FK_Step_Assignment_AssignmentNumber_AssigneeId",
                        columns: x => new { x.AssignmentNumber, x.AssigneeId },
                        principalTable: "Assignment",
                        principalColumns: new[] { "Number", "AssigneeId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_AssigneeId",
                table: "Assignment",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_AttachmentId",
                table: "Assignment",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Step_AssignmentNumber_AssigneeId",
                table: "Step",
                columns: new[] { "AssignmentNumber", "AssigneeId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Step");

            migrationBuilder.DropTable(
                name: "Assignment");

            migrationBuilder.DropTable(
                name: "Assignee");

            migrationBuilder.DropTable(
                name: "Attachment");
        }
    }
}
