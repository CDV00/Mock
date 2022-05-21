using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class seedDataCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_CategoryId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_CategoryId",
                table: "Categories",
                newName: "IX_Categories_ParentId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908753582"),
                column: "ConcurrencyStamp",
                value: "32e4c708-49a7-4295-b02a-891aaa47246e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f522"),
                column: "ConcurrencyStamp",
                value: "0e62c626-53e1-4067-8bb7-742d84109190");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f582"),
                column: "ConcurrencyStamp",
                value: "8a3bf536-8d04-49c7-ab4b-622e58a8b12c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908752532"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "b8c3a32e-0b3c-4849-a05f-f310fdbe002f", new DateTime(2022, 5, 21, 16, 20, 18, 355, DateTimeKind.Utc).AddTicks(9302), "AQAAAAEAACcQAAAAECKpuYx49OWkjAiUak2QdQSfC20P5d0qwhwJ8snkZ/ltNsUFD+0APhu4Tp6s1VQqVg==" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "IsDeleted", "Name", "ParentId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("9e47da69-3d3e-428d-a207-d53908753582"), new DateTime(2022, 5, 21, 16, 20, 18, 311, DateTimeKind.Utc).AddTicks(3036), null, true, false, "Development", null, null, null },
                    { new Guid("9e47da02-3d3e-428d-a207-d53908753582"), new DateTime(2022, 5, 21, 16, 20, 18, 314, DateTimeKind.Utc).AddTicks(4703), null, true, false, "Business", null, null, null },
                    { new Guid("9e47da02-3d3e-248d-a207-d53908753582"), new DateTime(2022, 5, 21, 16, 20, 18, 314, DateTimeKind.Utc).AddTicks(4853), null, true, false, "IT - SoftWare", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "IsDeleted", "Name", "ParentId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("51b4b0a3-cb94-4c78-a510-983fba3a0029"), new DateTime(2022, 5, 21, 16, 20, 18, 314, DateTimeKind.Utc).AddTicks(4201), null, true, false, "Web Developer", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("3ba7b5e5-3a75-4d6f-871a-eb6e191580e7"), new DateTime(2022, 5, 21, 16, 20, 18, 314, DateTimeKind.Utc).AddTicks(4459), null, true, false, "Data Science", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("fa8d6b29-2115-493e-9b86-b511b4c7c7f4"), new DateTime(2022, 5, 21, 16, 20, 18, 314, DateTimeKind.Utc).AddTicks(4673), null, true, false, "Mobile App", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("cb73d5e2-4063-4010-a8f9-7e02ca70076e"), new DateTime(2022, 5, 21, 16, 20, 18, 314, DateTimeKind.Utc).AddTicks(4744), null, true, false, "Finace", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("cbb48a7b-6a87-4f71-8e06-6472e5a23c90"), new DateTime(2022, 5, 21, 16, 20, 18, 314, DateTimeKind.Utc).AddTicks(4808), null, true, false, "Investor", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("b6e657ea-85bc-427f-a1a4-99dac3a29c34"), new DateTime(2022, 5, 21, 16, 20, 18, 314, DateTimeKind.Utc).AddTicks(4834), null, true, false, "Sale", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("2f103ec2-5626-4985-841f-a538cbc3d1b8"), new DateTime(2022, 5, 21, 16, 20, 18, 314, DateTimeKind.Utc).AddTicks(4884), null, true, false, "IT Certification", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null },
                    { new Guid("14f74890-9da5-4be3-a410-ef82409a0c61"), new DateTime(2022, 5, 21, 16, 20, 18, 314, DateTimeKind.Utc).AddTicks(4910), null, true, false, "Network & Security", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null },
                    { new Guid("c2726573-41ef-40b0-a4c6-e1f3220937f3"), new DateTime(2022, 5, 21, 16, 20, 18, 314, DateTimeKind.Utc).AddTicks(4932), null, true, false, "Hard Ware", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories",
                column: "ParentId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("14f74890-9da5-4be3-a410-ef82409a0c61"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("2f103ec2-5626-4985-841f-a538cbc3d1b8"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3ba7b5e5-3a75-4d6f-871a-eb6e191580e7"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("51b4b0a3-cb94-4c78-a510-983fba3a0029"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b6e657ea-85bc-427f-a1a4-99dac3a29c34"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c2726573-41ef-40b0-a4c6-e1f3220937f3"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("cb73d5e2-4063-4010-a8f9-7e02ca70076e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("cbb48a7b-6a87-4f71-8e06-6472e5a23c90"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("fa8d6b29-2115-493e-9b86-b511b4c7c7f4"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da02-3d3e-248d-a207-d53908753582"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da02-3d3e-428d-a207-d53908753582"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da69-3d3e-428d-a207-d53908753582"));

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                newName: "IX_Categories_CategoryId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908753582"),
                column: "ConcurrencyStamp",
                value: "180156c4-226e-436f-bfec-7a4f472ff198");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f522"),
                column: "ConcurrencyStamp",
                value: "3d4d3ac1-457b-4e3f-a228-6de7f7d2a58f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f582"),
                column: "ConcurrencyStamp",
                value: "3fa1e538-9429-4b12-ba46-d3bae75134e5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908752532"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "451bf15b-89ad-4247-9c7e-a06dba2892d1", new DateTime(2022, 5, 21, 15, 43, 11, 321, DateTimeKind.Utc).AddTicks(8082), "AQAAAAEAACcQAAAAEM0bq46wSmsvRuB+gjZrYEL1ncnEljRf2z9VkOEdJsWS8BoYj7F6RJgiaE6gYH8ppA==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_CategoryId",
                table: "Categories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
