using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class FixDiscountCourseDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Discounts_DiscountId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_DiscountId",
                table: "Courses");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                table: "Discounts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_CourseId",
                table: "Discounts",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Courses_CourseId",
                table: "Discounts",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Courses_CourseId",
                table: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_CourseId",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Discounts");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_DiscountId",
                table: "Courses",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Discounts_DiscountId",
                table: "Courses",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id");
        }
    }
}
