using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class setdefaultvalue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908753582"),
                column: "ConcurrencyStamp",
                value: "4ff0baa6-9f48-4c12-8c9c-4e1c4244da61");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f522"),
                column: "ConcurrencyStamp",
                value: "46a296b1-4073-4ed9-a6e1-8bca97d8f225");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f582"),
                column: "ConcurrencyStamp",
                value: "b6717b9a-0b2c-4678-81d8-760fb9011934");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908752532"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "44990306-bc46-43ec-b334-04ec127aa2be", new DateTime(2022, 5, 20, 19, 37, 7, 739, DateTimeKind.Utc).AddTicks(445), "AQAAAAEAACcQAAAAEGnbaabpuAOVlNUv9aXOpIMkNaEU4td+b6qJtD9erJQb7TiILatRLLXdcs+CO0k2xg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
