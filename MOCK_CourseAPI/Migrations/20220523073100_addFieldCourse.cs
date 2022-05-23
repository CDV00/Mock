using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class addFieldCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0b8d9398-e58e-40d4-b2b2-efe639587370"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("2e4cb814-e770-4f58-9caf-1379a97a93e0"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3dbeacd1-01bc-47d7-9b98-96e3e0b43742"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("795c2988-d982-48f1-94d9-710707e872ce"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("869962fd-554a-4044-8951-8166e12b5c52"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1ade942-c1ea-42a8-a410-247d36f65c2a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ba0250e8-8c17-42e5-a636-bbf4103948f8"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f70b8987-f1b6-40a9-8b11-2956335be2eb"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f8a0fd14-8984-467a-bf47-84b1fd91d3a2"));

            migrationBuilder.AddColumn<bool>(
                name: "IsPreview",
                table: "Lessons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VideoPoster",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPrice",
                table: "Courses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "RequireEnroll",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequireLogin",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsPreview",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "VideoPoster",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "DiscountPrice",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "RequireEnroll",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "RequireLogin",
                table: "Courses");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908753582"),
                column: "ConcurrencyStamp",
                value: "a78b518f-1d1d-4267-b66a-a7ee2ac13c70");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f522"),
                column: "ConcurrencyStamp",
                value: "a49f9b43-1d82-431b-abfe-c7f2629056df");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f582"),
                column: "ConcurrencyStamp",
                value: "fef04b93-bd52-439e-b5c3-becc74f3a49f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908752532"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "306cc86a-354a-4b74-a68a-0ca6ef5dc945", new DateTime(2022, 5, 23, 6, 59, 57, 657, DateTimeKind.Utc).AddTicks(4703), "AQAAAAEAACcQAAAAEGCyFeTnZDuG0YVCa1VLzaS8+mmwO4my1ITrPc0bg97UvvU35xZYHnKnlu1uNRy95A==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da02-3d3e-248d-a207-d53908753582"),
                column: "CreatedAt",
                value: new DateTime(2022, 5, 23, 6, 59, 57, 625, DateTimeKind.Utc).AddTicks(8625));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da02-3d3e-428d-a207-d53908753582"),
                column: "CreatedAt",
                value: new DateTime(2022, 5, 23, 6, 59, 57, 625, DateTimeKind.Utc).AddTicks(8531));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da69-3d3e-428d-a207-d53908753582"),
                column: "CreatedAt",
                value: new DateTime(2022, 5, 23, 6, 59, 57, 623, DateTimeKind.Utc).AddTicks(4620));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "IsDeleted", "Name", "ParentId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("f70b8987-f1b6-40a9-8b11-2956335be2eb"), new DateTime(2022, 5, 23, 6, 59, 57, 625, DateTimeKind.Utc).AddTicks(8168), null, true, false, "Web Developer", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("869962fd-554a-4044-8951-8166e12b5c52"), new DateTime(2022, 5, 23, 6, 59, 57, 625, DateTimeKind.Utc).AddTicks(8479), null, true, false, "Data Science", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("f8a0fd14-8984-467a-bf47-84b1fd91d3a2"), new DateTime(2022, 5, 23, 6, 59, 57, 625, DateTimeKind.Utc).AddTicks(8516), null, true, false, "Mobile App", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("795c2988-d982-48f1-94d9-710707e872ce"), new DateTime(2022, 5, 23, 6, 59, 57, 625, DateTimeKind.Utc).AddTicks(8569), null, true, false, "Finace", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("3dbeacd1-01bc-47d7-9b98-96e3e0b43742"), new DateTime(2022, 5, 23, 6, 59, 57, 625, DateTimeKind.Utc).AddTicks(8589), null, true, false, "Investor", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("0b8d9398-e58e-40d4-b2b2-efe639587370"), new DateTime(2022, 5, 23, 6, 59, 57, 625, DateTimeKind.Utc).AddTicks(8609), null, true, false, "Sale", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("a1ade942-c1ea-42a8-a410-247d36f65c2a"), new DateTime(2022, 5, 23, 6, 59, 57, 625, DateTimeKind.Utc).AddTicks(8648), null, true, false, "IT Certification", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null },
                    { new Guid("ba0250e8-8c17-42e5-a636-bbf4103948f8"), new DateTime(2022, 5, 23, 6, 59, 57, 625, DateTimeKind.Utc).AddTicks(8694), null, true, false, "Network & Security", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null },
                    { new Guid("2e4cb814-e770-4f58-9caf-1379a97a93e0"), new DateTime(2022, 5, 23, 6, 59, 57, 625, DateTimeKind.Utc).AddTicks(8713), null, true, false, "Hard Ware", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null }
                });
        }
    }
}
