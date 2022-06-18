using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class AddCol_Leacture_and_FixTypeQuesion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QuizSettings_QuizId",
                table: "QuizSettings");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Lectures",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileUrl",
                table: "Lectures",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "Lectures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QuizSettings_QuizId",
                table: "QuizSettings",
                column: "QuizId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QuizSettings_QuizId",
                table: "QuizSettings");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "FileUrl",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "Lectures");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_QuizSettings_QuizId",
                table: "QuizSettings",
                column: "QuizId");
        }
    }
}
