using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class changecourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_UserId",
                table: "Courses");

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
                value: "a842aafb-b09e-4dd5-ae15-2c11abf9227c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f522"),
                column: "ConcurrencyStamp",
                value: "c411815d-94af-4632-adca-d5efd35cde18");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f582"),
                column: "ConcurrencyStamp",
                value: "52579684-5f6c-4cd5-aa1a-16459dc1fcb7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908752532"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "c24f0862-38fc-4320-b7da-efe65fd6c466", new DateTime(2022, 5, 23, 8, 47, 55, 103, DateTimeKind.Utc).AddTicks(985), "AQAAAAEAACcQAAAAEB/0KzoZLOgt8WzPrsRzCEoI+0Q5wFPbQuJ7HGcgkKRk3n4s5tjnm1SyLqqLJ14mew==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da02-3d3e-248d-a207-d53908753582"),
                column: "CreatedAt",
                value: new DateTime(2022, 5, 23, 8, 47, 55, 36, DateTimeKind.Utc).AddTicks(806));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da02-3d3e-428d-a207-d53908753582"),
                column: "CreatedAt",
                value: new DateTime(2022, 5, 23, 8, 47, 55, 36, DateTimeKind.Utc).AddTicks(671));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da69-3d3e-428d-a207-d53908753582"),
                column: "CreatedAt",
                value: new DateTime(2022, 5, 23, 8, 47, 55, 32, DateTimeKind.Utc).AddTicks(6555));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "IsDeleted", "Name", "ParentId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("5bfc0203-1f2e-4535-9c12-dae56174c205"), new DateTime(2022, 5, 23, 8, 47, 55, 36, DateTimeKind.Utc).AddTicks(274), null, true, false, "Web Developer", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("0dd6d23b-17cc-4e0a-b644-795504e13d45"), new DateTime(2022, 5, 23, 8, 47, 55, 36, DateTimeKind.Utc).AddTicks(604), null, true, false, "Data Science", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("5a96a4a7-d29f-4741-857e-c36480d2e91c"), new DateTime(2022, 5, 23, 8, 47, 55, 36, DateTimeKind.Utc).AddTicks(644), null, true, false, "Mobile App", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("5496116a-6cfd-42ac-9205-5a561719ccb8"), new DateTime(2022, 5, 23, 8, 47, 55, 36, DateTimeKind.Utc).AddTicks(725), null, true, false, "Finace", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("386699bb-be2e-4ee4-927d-0049ab1fa704"), new DateTime(2022, 5, 23, 8, 47, 55, 36, DateTimeKind.Utc).AddTicks(754), null, true, false, "Investor", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("717170f0-dd33-4ba3-8eff-5fa764f822f6"), new DateTime(2022, 5, 23, 8, 47, 55, 36, DateTimeKind.Utc).AddTicks(782), null, true, false, "Sale", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("2d45a37f-cba3-45d1-bbfc-daf19e3dc0c5"), new DateTime(2022, 5, 23, 8, 47, 55, 36, DateTimeKind.Utc).AddTicks(887), null, true, false, "IT Certification", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null },
                    { new Guid("c71f0414-d723-462d-a19b-e1149717e6f5"), new DateTime(2022, 5, 23, 8, 47, 55, 36, DateTimeKind.Utc).AddTicks(916), null, true, false, "Network & Security", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null },
                    { new Guid("96b0af60-413d-4964-92ee-bd59fda87c07"), new DateTime(2022, 5, 23, 8, 47, 55, 36, DateTimeKind.Utc).AddTicks(1180), null, true, false, "Hard Ware", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_UserId",
                table: "Courses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_UserId",
                table: "Courses");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0dd6d23b-17cc-4e0a-b644-795504e13d45"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("2d45a37f-cba3-45d1-bbfc-daf19e3dc0c5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("386699bb-be2e-4ee4-927d-0049ab1fa704"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5496116a-6cfd-42ac-9205-5a561719ccb8"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5a96a4a7-d29f-4741-857e-c36480d2e91c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5bfc0203-1f2e-4535-9c12-dae56174c205"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("717170f0-dd33-4ba3-8eff-5fa764f822f6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("96b0af60-413d-4964-92ee-bd59fda87c07"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c71f0414-d723-462d-a19b-e1149717e6f5"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_UserId",
                table: "Courses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
