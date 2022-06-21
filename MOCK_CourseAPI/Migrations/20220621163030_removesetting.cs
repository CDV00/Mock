using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class removesetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizSettings");

            migrationBuilder.AddColumn<bool>(
                name: "IsShowTime",
                table: "Quizs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte>(
                name: "PassingScore",
                table: "Quizs",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<long>(
                name: "QuestionsLimit",
                table: "Quizs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TimeLimit",
                table: "Quizs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsShowTime",
                table: "Quizs");

            migrationBuilder.DropColumn(
                name: "PassingScore",
                table: "Quizs");

            migrationBuilder.DropColumn(
                name: "QuestionsLimit",
                table: "Quizs");

            migrationBuilder.DropColumn(
                name: "TimeLimit",
                table: "Quizs");

            migrationBuilder.CreateTable(
                name: "QuizSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsGradable = table.Column<bool>(type: "bit", nullable: false),
                    IsShowTime = table.Column<bool>(type: "bit", nullable: false),
                    PassingScore = table.Column<byte>(type: "tinyint", nullable: false),
                    QuestionsLimit = table.Column<long>(type: "bigint", nullable: false),
                    QuizId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeLimit = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizSettings_Quizs_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizSettings_QuizId",
                table: "QuizSettings",
                column: "QuizId",
                unique: true);
        }
    }
}
