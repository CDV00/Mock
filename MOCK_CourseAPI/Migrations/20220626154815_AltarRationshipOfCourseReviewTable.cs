using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class AltarRationshipOfCourseReviewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CourseReviews_EnrollmentId",
                table: "CourseReviews");

            migrationBuilder.CreateIndex(
                name: "IX_CourseReviews_EnrollmentId",
                table: "CourseReviews",
                column: "EnrollmentId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CourseReviews_EnrollmentId",
                table: "CourseReviews");

            migrationBuilder.CreateIndex(
                name: "IX_CourseReviews_EnrollmentId",
                table: "CourseReviews",
                column: "EnrollmentId");
        }
    }
}
