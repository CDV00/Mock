using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class removeCourseIdForOrder2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Courses_CoursesId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CoursesId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CoursesId",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CoursesId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CoursesId",
                table: "Orders",
                column: "CoursesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Courses_CoursesId",
                table: "Orders",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
