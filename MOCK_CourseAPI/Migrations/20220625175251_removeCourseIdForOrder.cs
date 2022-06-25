using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class removeCourseIdForOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //               name: "FK_Orders_Courses_CoursesId",
            //               table: "Orders");

            //migrationBuilder.DropColumn(
            //   name: "CoursesId",
            //   table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
