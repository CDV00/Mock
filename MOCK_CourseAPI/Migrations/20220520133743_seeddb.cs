using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class seeddb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908753582"),
                column: "ConcurrencyStamp",
                value: "1ec9755c-5bb0-43ca-bc12-f88d123ebab1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f522"),
                column: "ConcurrencyStamp",
                value: "59d71892-cf9b-4b84-81ea-b7f2ce3f2fe9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f582"),
                column: "ConcurrencyStamp",
                value: "a954c6fa-cf53-4de0-b0ce-906c44eb9de6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908752532"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "NormalizedEmail", "PasswordHash" },
                values: new object[] { "76b88853-137e-4612-ad7e-7eb569deb316", new DateTime(2022, 5, 20, 13, 37, 38, 313, DateTimeKind.Utc).AddTicks(7251), "admin123@gmail.com", "AQAAAAEAACcQAAAAELvYrcgSRTgTC+R719AAaQpxPImzXibEQ5wNkm9Qsduu0uYIdp85fkMEOYQeYFF4GQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908753582"),
                column: "ConcurrencyStamp",
                value: "29841eb1-4e4c-4435-acf1-316192b9028c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f522"),
                column: "ConcurrencyStamp",
                value: "c145289e-9419-4bcd-afd1-043ff1d404e5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f582"),
                column: "ConcurrencyStamp",
                value: "61fb6048-b138-44ac-9264-b52c5b0c9594");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908752532"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "NormalizedEmail", "PasswordHash" },
                values: new object[] { "b47a384f-8369-4e1d-8170-023af9f64875", new DateTime(2022, 5, 20, 12, 48, 16, 313, DateTimeKind.Utc).AddTicks(4492), null, "AQAAAAEAACcQAAAAEKDyc/fBNAAOdL3R8EGYjl8Vk3mROKuomLXmIrsvCnj2koN7NWDbSj9uLvX6cETLLA==" });
        }
    }
}
