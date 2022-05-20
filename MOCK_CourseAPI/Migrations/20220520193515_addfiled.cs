using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class addfiled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "Courses",
                newName: "ThumbnailUrl");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Courses",
                newName: "ShortDescription");

            migrationBuilder.AddColumn<int>(
                name: "CourseLevel",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Learn",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Requirement",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "View",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908753582"),
                column: "ConcurrencyStamp",
                value: "d0e2abc8-7387-45fe-a8b9-34c18d27f352");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f522"),
                column: "ConcurrencyStamp",
                value: "3f7b1df8-f5ca-4247-bc81-ccbeb6f5f754");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f582"),
                column: "ConcurrencyStamp",
                value: "c2beadf7-0d85-47b0-8f07-24d0358526eb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908752532"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "f1db1acf-d4ac-413d-a54b-c700329b5542", new DateTime(2022, 5, 20, 19, 35, 13, 439, DateTimeKind.Utc).AddTicks(6861), "AQAAAAEAACcQAAAAEBIm7uchiQIObU4PWfBXwuWks8Ljqq7tXt/ihlGGzLjkPSYkinp5UHWSa88ZZU9Taw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseLevel",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Learn",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Requirement",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "View",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "ThumbnailUrl",
                table: "Courses",
                newName: "Summary");

            migrationBuilder.RenameColumn(
                name: "ShortDescription",
                table: "Courses",
                newName: "ImageUrl");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908753582"),
                column: "ConcurrencyStamp",
                value: "ac8e4900-4cf0-4207-89b9-35bb0f12ea58");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f522"),
                column: "ConcurrencyStamp",
                value: "1064fe10-c64f-4060-abd4-b0dff01dab5b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f582"),
                column: "ConcurrencyStamp",
                value: "79527eae-cb8b-402d-982e-f5c81ecf2aae");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908752532"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "4e1c59c4-65e6-41d0-905d-4b0427872b1f", new DateTime(2022, 5, 20, 17, 20, 33, 924, DateTimeKind.Utc).AddTicks(9713), "AQAAAAEAACcQAAAAEBghfa5ciwO1nR73ceBUwqSsVNblMUFbqhLvOTd5WYX11yxEtDXSF+wkA7qMm9WPyw==" });
        }
    }
}
