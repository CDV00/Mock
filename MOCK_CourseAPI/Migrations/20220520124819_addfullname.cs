using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class addfullname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Fullname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

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
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "b47a384f-8369-4e1d-8170-023af9f64875", new DateTime(2022, 5, 20, 12, 48, 16, 313, DateTimeKind.Utc).AddTicks(4492), "AQAAAAEAACcQAAAAEKDyc/fBNAAOdL3R8EGYjl8Vk3mROKuomLXmIrsvCnj2koN7NWDbSj9uLvX6cETLLA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fullname",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908753582"),
                column: "ConcurrencyStamp",
                value: "6f220217-2ed3-4cd4-a18f-5775b9ffc285");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f522"),
                column: "ConcurrencyStamp",
                value: "a8597b46-98e5-4c84-9d4f-0b8cd4e45fa8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f582"),
                column: "ConcurrencyStamp",
                value: "fad41c8d-0c64-4a3c-9a37-fa1fa562db36");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908752532"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "1f9a0327-c05b-4cac-a926-72c145dfe745", new DateTime(2022, 5, 20, 12, 47, 27, 631, DateTimeKind.Utc).AddTicks(1611), "AQAAAAEAACcQAAAAEIZsNdPtcjyIyZDR+Uu8XaKTg8eXtDR9c3uQHKQrlzzelwFkvDGRbWx0JkHqyVkGlA==" });
        }
    }
}
