using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class setRelationShipLectureCompletion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LectureCompletions_Lectures_LectureId",
                table: "LectureCompletions");

            migrationBuilder.CreateIndex(
                name: "IX_LectureCompletions_LectureId",
                table: "LectureCompletions",
                column: "LectureId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureCompletions_Lectures_LectureId",
                table: "LectureCompletions",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LectureCompletions_Lectures_LectureId",
                table: "LectureCompletions");

            migrationBuilder.DropIndex(
                name: "IX_LectureCompletions_LectureId",
                table: "LectureCompletions");

            migrationBuilder.AddForeignKey(
                name: "FK_LectureCompletions_Lectures_LectureId",
                table: "LectureCompletions",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id");
        }
    }
}
