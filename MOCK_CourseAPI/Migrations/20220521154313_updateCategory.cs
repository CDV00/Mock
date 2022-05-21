using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class updateCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentcategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "TotalTime",
                table: "Sections");

            migrationBuilder.RenameColumn(
                name: "ParentcategoryId",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentcategoryId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_CategoryId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "ParentcategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_CategoryId",
                table: "Categories",
                newName: "IX_Categories_ParentcategoryId");

            migrationBuilder.AddColumn<int>(
                name: "TotalTime",
                table: "Sections",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentcategoryId",
                table: "Categories",
                column: "ParentcategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
