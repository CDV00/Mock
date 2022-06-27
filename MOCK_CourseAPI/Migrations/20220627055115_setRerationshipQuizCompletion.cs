using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class setRerationshipQuizCompletion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizCompletions_Quizs_QuizId",
                table: "QuizCompletions");

            migrationBuilder.CreateIndex(
                name: "IX_QuizCompletions_QuizId",
                table: "QuizCompletions",
                column: "QuizId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizCompletions_Quizs_QuizId",
                table: "QuizCompletions",
                column: "QuizId",
                principalTable: "Quizs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizCompletions_Quizs_QuizId",
                table: "QuizCompletions");

            migrationBuilder.DropIndex(
                name: "IX_QuizCompletions_QuizId",
                table: "QuizCompletions");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizCompletions_Quizs_QuizId",
                table: "QuizCompletions",
                column: "QuizId",
                principalTable: "Quizs",
                principalColumn: "Id");
        }
    }
}
