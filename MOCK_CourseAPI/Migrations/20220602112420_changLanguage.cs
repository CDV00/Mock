using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class changLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AudioLanguages_Courses_CourseId",
                table: "AudioLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_AudioLanguages_Languages_LanguageId",
                table: "AudioLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_CloseCaptions_Courses_CourseId",
                table: "CloseCaptions");

            migrationBuilder.DropForeignKey(
                name: "FK_CloseCaptions_Languages_LanguageId",
                table: "CloseCaptions");

            migrationBuilder.DropTable(
                name: "CourseLevels");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CloseCaptions",
                table: "CloseCaptions");

            migrationBuilder.DropIndex(
                name: "IX_CloseCaptions_LanguageId",
                table: "CloseCaptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AudioLanguages",
                table: "AudioLanguages");

            migrationBuilder.DropIndex(
                name: "IX_AudioLanguages_LanguageId",
                table: "AudioLanguages");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "CloseCaptions");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "CloseCaptions");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "AudioLanguages");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "AudioLanguages");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CloseCaptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AudioLanguages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CloseCaptions",
                table: "CloseCaptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AudioLanguages",
                table: "AudioLanguages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AudioLanguageCourses",
                columns: table => new
                {
                    AudioLanguagesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoursesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioLanguageCourses", x => new { x.AudioLanguagesId, x.CoursesId });
                    table.ForeignKey(
                        name: "FK_AudioLanguageCourses_AudioLanguages_AudioLanguagesId",
                        column: x => x.AudioLanguagesId,
                        principalTable: "AudioLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AudioLanguageCourses_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CloseCaptionCourses",
                columns: table => new
                {
                    CloseCaptionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoursesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloseCaptionCourses", x => new { x.CloseCaptionsId, x.CoursesId });
                    table.ForeignKey(
                        name: "FK_CloseCaptionCourses_CloseCaptions_CloseCaptionsId",
                        column: x => x.CloseCaptionsId,
                        principalTable: "CloseCaptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CloseCaptionCourses_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoursesLevel",
                columns: table => new
                {
                    CoursesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesLevel", x => new { x.CoursesId, x.LevelsId });
                    table.ForeignKey(
                        name: "FK_CoursesLevel_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursesLevel_Levels_LevelsId",
                        column: x => x.LevelsId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AudioLanguageCourses_CoursesId",
                table: "AudioLanguageCourses",
                column: "CoursesId");

            migrationBuilder.CreateIndex(
                name: "IX_CloseCaptionCourses_CoursesId",
                table: "CloseCaptionCourses",
                column: "CoursesId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesLevel_LevelsId",
                table: "CoursesLevel",
                column: "LevelsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AudioLanguageCourses");

            migrationBuilder.DropTable(
                name: "CloseCaptionCourses");

            migrationBuilder.DropTable(
                name: "CoursesLevel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CloseCaptions",
                table: "CloseCaptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AudioLanguages",
                table: "AudioLanguages");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CloseCaptions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AudioLanguages");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                table: "CloseCaptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LanguageId",
                table: "CloseCaptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                table: "AudioLanguages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LanguageId",
                table: "AudioLanguages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CloseCaptions",
                table: "CloseCaptions",
                columns: new[] { "CourseId", "LanguageId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AudioLanguages",
                table: "AudioLanguages",
                columns: new[] { "CourseId", "LanguageId" });

            migrationBuilder.CreateTable(
                name: "CourseLevels",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseLevels", x => new { x.CourseId, x.LevelId });
                    table.ForeignKey(
                        name: "FK_CourseLevels_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseLevels_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CloseCaptions_LanguageId",
                table: "CloseCaptions",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_AudioLanguages_LanguageId",
                table: "AudioLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseLevels_LevelId",
                table: "CourseLevels",
                column: "LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AudioLanguages_Courses_CourseId",
                table: "AudioLanguages",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AudioLanguages_Languages_LanguageId",
                table: "AudioLanguages",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CloseCaptions_Courses_CourseId",
                table: "CloseCaptions",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CloseCaptions_Languages_LanguageId",
                table: "CloseCaptions",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
