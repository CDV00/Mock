using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class seeddb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908753582"),
                column: "ConcurrencyStamp",
                value: "bfe9407a-f6eb-4a26-811c-4c5d0a245cb3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f522"),
                column: "ConcurrencyStamp",
                value: "60d2f2e8-e165-486d-9c44-a3f1a16e228b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f582"),
                column: "ConcurrencyStamp",
                value: "a28317ef-5515-415e-b681-7a265747f60c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908752532"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "UserName" },
                values: new object[] { "b0d9069d-9af9-4ee0-a661-926016830fe5", new DateTime(2022, 5, 20, 13, 39, 18, 477, DateTimeKind.Utc).AddTicks(5570), "AQAAAAEAACcQAAAAEGK3K52/7dsDRJGlu0eUIsIoMqj6mO1Rj7nqVuOfm3QgrMdSTOvgry/a7eIHym1AuQ==", "admin123" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "UserName" },
                values: new object[] { "76b88853-137e-4612-ad7e-7eb569deb316", new DateTime(2022, 5, 20, 13, 37, 38, 313, DateTimeKind.Utc).AddTicks(7251), "AQAAAAEAACcQAAAAELvYrcgSRTgTC+R719AAaQpxPImzXibEQ5wNkm9Qsduu0uYIdp85fkMEOYQeYFF4GQ==", null });
        }
    }
}
