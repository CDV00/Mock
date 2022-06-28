using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class addIdForQuizCompletion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizCompletions",
                table: "QuizCompletions");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "QuizCompletions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizCompletions",
                table: "QuizCompletions",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizCompletions",
                table: "QuizCompletions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "QuizCompletions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizCompletions",
                table: "QuizCompletions",
                columns: new[] { "QuizId", "UserId" });
        }
    }
}
