using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class addlectureattachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachmentUrl",
                table: "Lectures");

            migrationBuilder.CreateTable(
                name: "LectureAttachment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LectureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LectureAttachment_Lectures_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LectureAttachment_LectureId",
                table: "LectureAttachment",
                column: "LectureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LectureAttachment");

            migrationBuilder.AddColumn<string>(
                name: "AttachmentUrl",
                table: "Lectures",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
