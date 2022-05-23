using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class seeddatacourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPrice",
                table: "Courses",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908753582"),
                column: "ConcurrencyStamp",
                value: "7d9bda57-5e31-41fe-af3c-9a9acdc3b7e8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f522"),
                column: "ConcurrencyStamp",
                value: "1030ba48-8cee-4fec-9b78-e8b9612d67c1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f582"),
                column: "ConcurrencyStamp",
                value: "c2721e89-9de5-4bac-9360-7334d103633f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908752532"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "f6c2702b-c720-4e94-9492-6ccf1f5cb7f8", new DateTime(2022, 5, 23, 8, 55, 30, 279, DateTimeKind.Utc).AddTicks(5306), "AQAAAAEAACcQAAAAECs6nfDI3qx0M0Uj+jiejZxzwG2YRgMeRH1Xfriex75a+ZlUhlSfTqHUZ+xnbEeHXA==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da02-3d3e-248d-a207-d53908753582"),
                column: "CreatedAt",
                value: new DateTime(2022, 5, 23, 8, 55, 30, 179, DateTimeKind.Utc).AddTicks(3214));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da02-3d3e-428d-a207-d53908753582"),
                column: "CreatedAt",
                value: new DateTime(2022, 5, 23, 8, 55, 30, 179, DateTimeKind.Utc).AddTicks(3058));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da69-3d3e-428d-a207-d53908753582"),
                column: "CreatedAt",
                value: new DateTime(2022, 5, 23, 8, 55, 30, 175, DateTimeKind.Utc).AddTicks(817));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "IsDeleted", "Name", "ParentId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("01c6f561-5c40-405a-ac7d-9c19cbe8417f"), new DateTime(2022, 5, 23, 8, 55, 30, 179, DateTimeKind.Utc).AddTicks(3301), null, true, false, "Hard Ware", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null },
                    { new Guid("1ffd3243-9fc9-4b20-8743-3800cbec5e8c"), new DateTime(2022, 5, 23, 8, 55, 30, 179, DateTimeKind.Utc).AddTicks(3247), null, true, false, "IT Certification", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null },
                    { new Guid("e61e4bb9-ef99-43f0-b853-03c03608d371"), new DateTime(2022, 5, 23, 8, 55, 30, 179, DateTimeKind.Utc).AddTicks(3186), null, true, false, "Sale", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("b02d05e6-5ac6-4514-b622-a408f42691cd"), new DateTime(2022, 5, 23, 8, 55, 30, 179, DateTimeKind.Utc).AddTicks(3136), null, true, false, "Investor", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("33e1f8ca-cc29-4686-b9a7-52ba49bdda7d"), new DateTime(2022, 5, 23, 8, 55, 30, 179, DateTimeKind.Utc).AddTicks(3106), null, true, false, "Finace", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("058e6530-bd60-4592-bf71-fda31c6216f1"), new DateTime(2022, 5, 23, 8, 55, 30, 179, DateTimeKind.Utc).AddTicks(3033), null, true, false, "Mobile App", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("56adab52-8f5d-4c8b-b51c-451dea4dd790"), new DateTime(2022, 5, 23, 8, 55, 30, 179, DateTimeKind.Utc).AddTicks(2994), null, true, false, "Data Science", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("bf316b76-007e-4553-84ef-f1ec2812fa12"), new DateTime(2022, 5, 23, 8, 55, 30, 179, DateTimeKind.Utc).AddTicks(3274), null, true, false, "Network & Security", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null },
                    { new Guid("f5232d4e-5c17-40b4-8658-dad8d7622868"), new DateTime(2022, 5, 23, 8, 55, 30, 179, DateTimeKind.Utc).AddTicks(2659), null, true, false, "Web Developer", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "CourseLevel", "CreatedAt", "CreatedBy", "Description", "DiscountPrice", "IsActive", "IsDeleted", "Learn", "PreviewVideoUrl", "Price", "RequireEnroll", "RequireLogin", "Requirement", "ShortDescription", "ThumbnailUrl", "Title", "UpdatedAt", "UpdatedBy", "UserId", "View" },
                values: new object[] { new Guid("7244ed41-a870-4451-b93b-40457d4de898"), new Guid("29304f68-b4b7-4e3c-8151-04b03ba03cd2"), 0, new DateTime(2022, 5, 23, 8, 55, 30, 202, DateTimeKind.Utc).AddTicks(8319), null, null, 0m, true, false, null, null, 0m, false, false, null, null, null, "New Course", null, null, new Guid("9e59da69-3d3e-428d-a207-d53908752532"), 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("01c6f561-5c40-405a-ac7d-9c19cbe8417f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("058e6530-bd60-4592-bf71-fda31c6216f1"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1ffd3243-9fc9-4b20-8743-3800cbec5e8c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("33e1f8ca-cc29-4686-b9a7-52ba49bdda7d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("56adab52-8f5d-4c8b-b51c-451dea4dd790"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b02d05e6-5ac6-4514-b622-a408f42691cd"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("bf316b76-007e-4553-84ef-f1ec2812fa12"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e61e4bb9-ef99-43f0-b853-03c03608d371"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f5232d4e-5c17-40b4-8658-dad8d7622868"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("7244ed41-a870-4451-b93b-40457d4de898"));

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPrice",
                table: "Courses",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

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
        }
    }
}
