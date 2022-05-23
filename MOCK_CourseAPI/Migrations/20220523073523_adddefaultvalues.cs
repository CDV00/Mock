using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class adddefaultvalues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("23dd28d8-ee0c-4fee-bc21-44dec994505c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("58e06a1e-efcb-4f1e-831a-6aa8cbc350ed"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6547ae09-a4ac-46f5-a2dc-d19f3d2a922c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8435092a-8e83-4a4d-a204-626e86f0e980"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a5396b1a-f298-4320-9829-7b8803aaf5c9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a7f23a07-6980-4177-8f6a-904e394ecaba"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("aaf12d2d-5720-4178-bbd2-6c077427276e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b586d450-a2e4-4401-8a62-b2733139bfca"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("eacb10b0-721b-463b-bba5-74fb5c62eca5"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908753582"),
                column: "ConcurrencyStamp",
                value: "a5eb17ca-b928-4b13-8365-22c43b430666");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f522"),
                column: "ConcurrencyStamp",
                value: "73a80ce8-74ba-477e-bc50-804edc57e15f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f582"),
                column: "ConcurrencyStamp",
                value: "8a4c9559-b9f2-46ca-832b-d3a000caca0f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908752532"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "2423b30e-8b01-4f42-b214-e12e82f01119", new DateTime(2022, 5, 23, 7, 35, 21, 400, DateTimeKind.Utc).AddTicks(9754), "AQAAAAEAACcQAAAAEMW0LuZCFsgG75FHzkCXhzG1gRGaZooMCKajjh0utmr3h+Egdd8/JZB4eFc0jRF5WQ==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da02-3d3e-248d-a207-d53908753582"),
                column: "CreatedAt",
                value: new DateTime(2022, 5, 23, 7, 35, 21, 363, DateTimeKind.Utc).AddTicks(3343));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da02-3d3e-428d-a207-d53908753582"),
                column: "CreatedAt",
                value: new DateTime(2022, 5, 23, 7, 35, 21, 363, DateTimeKind.Utc).AddTicks(3289));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da69-3d3e-428d-a207-d53908753582"),
                column: "CreatedAt",
                value: new DateTime(2022, 5, 23, 7, 35, 21, 361, DateTimeKind.Utc).AddTicks(2417));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "IsDeleted", "Name", "ParentId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("adaa568f-53a4-43f4-932c-d96278e07d43"), new DateTime(2022, 5, 23, 7, 35, 21, 363, DateTimeKind.Utc).AddTicks(3083), null, true, false, "Web Developer", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("e7f41da1-f0ab-4092-9de0-3e2853184fae"), new DateTime(2022, 5, 23, 7, 35, 21, 363, DateTimeKind.Utc).AddTicks(3259), null, true, false, "Data Science", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("f52e2ea8-1876-4c0f-b05d-fd5d9b0cc7a9"), new DateTime(2022, 5, 23, 7, 35, 21, 363, DateTimeKind.Utc).AddTicks(3280), null, true, false, "Mobile App", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("5a48880f-c082-4f7e-bfb4-ca4e4490b78f"), new DateTime(2022, 5, 23, 7, 35, 21, 363, DateTimeKind.Utc).AddTicks(3311), null, true, false, "Finace", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("1ed1f0b4-d891-44c0-9b54-a1f94d484795"), new DateTime(2022, 5, 23, 7, 35, 21, 363, DateTimeKind.Utc).AddTicks(3322), null, true, false, "Investor", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("9447546e-98f2-454b-8fb9-892b67d3383b"), new DateTime(2022, 5, 23, 7, 35, 21, 363, DateTimeKind.Utc).AddTicks(3333), null, true, false, "Sale", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("2b6b5a53-f147-4ac3-81b1-74494c2afc92"), new DateTime(2022, 5, 23, 7, 35, 21, 363, DateTimeKind.Utc).AddTicks(3379), null, true, false, "IT Certification", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null },
                    { new Guid("ac0fbc1b-2e8d-47ce-9c8a-fdcdbaf46bc1"), new DateTime(2022, 5, 23, 7, 35, 21, 363, DateTimeKind.Utc).AddTicks(3391), null, true, false, "Network & Security", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null },
                    { new Guid("4751bbfa-c0d4-4e66-978f-f620404271ef"), new DateTime(2022, 5, 23, 7, 35, 21, 363, DateTimeKind.Utc).AddTicks(3401), null, true, false, "Hard Ware", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1ed1f0b4-d891-44c0-9b54-a1f94d484795"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("2b6b5a53-f147-4ac3-81b1-74494c2afc92"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4751bbfa-c0d4-4e66-978f-f620404271ef"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5a48880f-c082-4f7e-bfb4-ca4e4490b78f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9447546e-98f2-454b-8fb9-892b67d3383b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ac0fbc1b-2e8d-47ce-9c8a-fdcdbaf46bc1"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("adaa568f-53a4-43f4-932c-d96278e07d43"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e7f41da1-f0ab-4092-9de0-3e2853184fae"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f52e2ea8-1876-4c0f-b05d-fd5d9b0cc7a9"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908753582"),
                column: "ConcurrencyStamp",
                value: "bb9f3a19-daf1-4efa-9f73-f15e982484c8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f522"),
                column: "ConcurrencyStamp",
                value: "bf9ed0ad-4095-4b47-b1d1-06b9d6e404f6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f582"),
                column: "ConcurrencyStamp",
                value: "edff3d28-178d-41e1-a417-0de63fb40c29");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908752532"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "23441f67-2304-4a9a-b158-88cd7b58be95", new DateTime(2022, 5, 23, 7, 30, 58, 292, DateTimeKind.Utc).AddTicks(7453), "AQAAAAEAACcQAAAAEAtizTB97P/cuXKtdr0LxbqQVyiILl1bovXHzu2WBivPIem05B+3JtmDrM/cg5r5JQ==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da02-3d3e-248d-a207-d53908753582"),
                column: "CreatedAt",
                value: new DateTime(2022, 5, 23, 7, 30, 58, 247, DateTimeKind.Utc).AddTicks(8213));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da02-3d3e-428d-a207-d53908753582"),
                column: "CreatedAt",
                value: new DateTime(2022, 5, 23, 7, 30, 58, 247, DateTimeKind.Utc).AddTicks(8118));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da69-3d3e-428d-a207-d53908753582"),
                column: "CreatedAt",
                value: new DateTime(2022, 5, 23, 7, 30, 58, 245, DateTimeKind.Utc).AddTicks(47));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "IsDeleted", "Name", "ParentId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("6547ae09-a4ac-46f5-a2dc-d19f3d2a922c"), new DateTime(2022, 5, 23, 7, 30, 58, 247, DateTimeKind.Utc).AddTicks(7824), null, true, false, "Web Developer", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("58e06a1e-efcb-4f1e-831a-6aa8cbc350ed"), new DateTime(2022, 5, 23, 7, 30, 58, 247, DateTimeKind.Utc).AddTicks(8068), null, true, false, "Data Science", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("8435092a-8e83-4a4d-a204-626e86f0e980"), new DateTime(2022, 5, 23, 7, 30, 58, 247, DateTimeKind.Utc).AddTicks(8099), null, true, false, "Mobile App", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("a7f23a07-6980-4177-8f6a-904e394ecaba"), new DateTime(2022, 5, 23, 7, 30, 58, 247, DateTimeKind.Utc).AddTicks(8156), null, true, false, "Finace", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("eacb10b0-721b-463b-bba5-74fb5c62eca5"), new DateTime(2022, 5, 23, 7, 30, 58, 247, DateTimeKind.Utc).AddTicks(8177), null, true, false, "Investor", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("aaf12d2d-5720-4178-bbd2-6c077427276e"), new DateTime(2022, 5, 23, 7, 30, 58, 247, DateTimeKind.Utc).AddTicks(8197), null, true, false, "Sale", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("23dd28d8-ee0c-4fee-bc21-44dec994505c"), new DateTime(2022, 5, 23, 7, 30, 58, 247, DateTimeKind.Utc).AddTicks(8257), null, true, false, "IT Certification", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null },
                    { new Guid("b586d450-a2e4-4401-8a62-b2733139bfca"), new DateTime(2022, 5, 23, 7, 30, 58, 247, DateTimeKind.Utc).AddTicks(8278), null, true, false, "Network & Security", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null },
                    { new Guid("a5396b1a-f298-4320-9829-7b8803aaf5c9"), new DateTime(2022, 5, 23, 7, 30, 58, 247, DateTimeKind.Utc).AddTicks(8297), null, true, false, "Hard Ware", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null }
                });
        }
    }
}
