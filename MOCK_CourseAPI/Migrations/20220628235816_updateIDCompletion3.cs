using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class updateIDCompletion3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QuizCompletions_QuizId",
                table: "QuizCompletions");

            migrationBuilder.DropIndex(
                name: "IX_LectureCompletions_LectureId",
                table: "LectureCompletions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignmentCompletions",
                table: "AssignmentCompletions");

            migrationBuilder.DropIndex(
                name: "IX_AssignmentCompletions_AssignmentId",
                table: "AssignmentCompletions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignmentCompletions",
                table: "AssignmentCompletions",
                columns: new[] { "AssignmentId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_QuizCompletions_QuizId",
                table: "QuizCompletions",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureCompletions_LectureId",
                table: "LectureCompletions",
                column: "LectureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QuizCompletions_QuizId",
                table: "QuizCompletions");

            migrationBuilder.DropIndex(
                name: "IX_LectureCompletions_LectureId",
                table: "LectureCompletions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignmentCompletions",
                table: "AssignmentCompletions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignmentCompletions",
                table: "AssignmentCompletions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_QuizCompletions_QuizId",
                table: "QuizCompletions",
                column: "QuizId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LectureCompletions_LectureId",
                table: "LectureCompletions",
                column: "LectureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentCompletions_AssignmentId",
                table: "AssignmentCompletions",
                column: "AssignmentId",
                unique: true);
        }
    }
}
