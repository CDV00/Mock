using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class fixmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "b0d9069d-9af9-4ee0-a661-926016830fe5", new DateTime(2022, 5, 20, 13, 39, 18, 477, DateTimeKind.Utc).AddTicks(5570), "AQAAAAEAACcQAAAAEGK3K52/7dsDRJGlu0eUIsIoMqj6mO1Rj7nqVuOfm3QgrMdSTOvgry/a7eIHym1AuQ==" });
        }
    }
}
