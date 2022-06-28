using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class addlectureattachment2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LectureAttachment_Lectures_LectureId",
                table: "LectureAttachment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LectureAttachment",
                table: "LectureAttachment");

            migrationBuilder.RenameTable(
                name: "LectureAttachment",
                newName: "LectureAttachments");

            migrationBuilder.RenameIndex(
                name: "IX_LectureAttachment_LectureId",
                table: "LectureAttachments",
                newName: "IX_LectureAttachments_LectureId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LectureAttachments",
                table: "LectureAttachments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LectureAttachments_Lectures_LectureId",
                table: "LectureAttachments",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LectureAttachments_Lectures_LectureId",
                table: "LectureAttachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LectureAttachments",
                table: "LectureAttachments");

            migrationBuilder.RenameTable(
                name: "LectureAttachments",
                newName: "LectureAttachment");

            migrationBuilder.RenameIndex(
                name: "IX_LectureAttachments_LectureId",
                table: "LectureAttachment",
                newName: "IX_LectureAttachment_LectureId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LectureAttachment",
                table: "LectureAttachment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LectureAttachment_Lectures_LectureId",
                table: "LectureAttachment",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
