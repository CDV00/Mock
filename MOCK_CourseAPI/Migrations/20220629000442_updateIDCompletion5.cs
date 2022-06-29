using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class updateIDCompletion5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizCompletions",
                table: "QuizCompletions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LectureCompletions",
                table: "LectureCompletions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignmentCompletions",
                table: "AssignmentCompletions");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "QuizCompletions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "LectureCompletions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "AssignmentCompletions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizCompletions",
                table: "QuizCompletions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LectureCompletions",
                table: "LectureCompletions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignmentCompletions",
                table: "AssignmentCompletions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_QuizCompletions_QuizId",
                table: "QuizCompletions",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureCompletions_LectureId",
                table: "LectureCompletions",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentCompletions_AssignmentId",
                table: "AssignmentCompletions",
                column: "AssignmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizCompletions",
                table: "QuizCompletions");

            migrationBuilder.DropIndex(
                name: "IX_QuizCompletions_QuizId",
                table: "QuizCompletions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LectureCompletions",
                table: "LectureCompletions");

            migrationBuilder.DropIndex(
                name: "IX_LectureCompletions_LectureId",
                table: "LectureCompletions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignmentCompletions",
                table: "AssignmentCompletions");

            migrationBuilder.DropIndex(
                name: "IX_AssignmentCompletions_AssignmentId",
                table: "AssignmentCompletions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "QuizCompletions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "LectureCompletions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AssignmentCompletions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizCompletions",
                table: "QuizCompletions",
                columns: new[] { "QuizId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_LectureCompletions",
                table: "LectureCompletions",
                columns: new[] { "LectureId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignmentCompletions",
                table: "AssignmentCompletions",
                columns: new[] { "AssignmentId", "UserId" });
        }
    }
}
