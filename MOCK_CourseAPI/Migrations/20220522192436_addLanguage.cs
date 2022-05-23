using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class addLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("178c33e6-6d99-4c65-a140-f8d7cab7ceec"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22be68bb-305e-49c5-8be2-1a941df33338"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("43897d96-b828-4bc8-9596-e64321f17c86"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("44807556-f76d-4487-9d56-14d127ff0a4a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4c647bc6-782d-4c6d-9d57-39e6207b8d7e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7109340e-d4ed-45f3-8eb2-cbcbf6323b93"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7a70ebdc-8364-4b07-957b-c0c8352046be"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9ef3dde9-3281-43d9-a927-1a5901f63ef1"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a84b4339-3992-4c31-8751-574b3ac6bded"));

            migrationBuilder.RenameColumn(
                name: "Instroduction",
                table: "AspNetUsers",
                newName: "ProfileLink");

            migrationBuilder.AddColumn<string>(
                name: "HeadLine",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "audioLanguages",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audioLanguages", x => new { x.CourseId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_audioLanguages_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_audioLanguages_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908753582"),
                column: "ConcurrencyStamp",
                value: "8717b15f-0b37-4c57-be6a-42a52e3d21ca");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f522"),
                column: "ConcurrencyStamp",
                value: "d36ef623-acf0-4731-a0bc-ecc478a2b5a4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f582"),
                column: "ConcurrencyStamp",
                value: "715cfb42-ac9c-4500-8523-d6396e84cb46");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908752532"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "4f394783-f6ca-4ad3-8119-342c534ee06c", new DateTime(2022, 5, 22, 19, 24, 35, 185, DateTimeKind.Utc).AddTicks(5351), "AQAAAAEAACcQAAAAEEf/xxoGq+k517ECLhozJD75pku+KpLg6L31mn90KUCekX+ZlnILoRtSM82GP69+mw==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da02-3d3e-248d-a207-d53908753582"),
                column: "CreatedAt",
                value: new DateTime(2022, 5, 22, 19, 24, 35, 157, DateTimeKind.Utc).AddTicks(1760));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da02-3d3e-428d-a207-d53908753582"),
                column: "CreatedAt",
                value: new DateTime(2022, 5, 22, 19, 24, 35, 157, DateTimeKind.Utc).AddTicks(1717));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da69-3d3e-428d-a207-d53908753582"),
                column: "CreatedAt",
                value: new DateTime(2022, 5, 22, 19, 24, 35, 155, DateTimeKind.Utc).AddTicks(3921));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "IsDeleted", "Name", "ParentId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("6373f39b-7049-4997-b4b7-b1c451474d23"), new DateTime(2022, 5, 22, 19, 24, 35, 157, DateTimeKind.Utc).AddTicks(1538), null, true, false, "Web Developer", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("31a4a026-6ed9-4fac-a78b-bdad8593d704"), new DateTime(2022, 5, 22, 19, 24, 35, 157, DateTimeKind.Utc).AddTicks(1693), null, true, false, "Data Science", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("d96f1c89-fdf1-40ed-b80e-cc2cc1b7b92a"), new DateTime(2022, 5, 22, 19, 24, 35, 157, DateTimeKind.Utc).AddTicks(1707), null, true, false, "Mobile App", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("5563d518-96d3-4412-8c87-bfb05c9957ac"), new DateTime(2022, 5, 22, 19, 24, 35, 157, DateTimeKind.Utc).AddTicks(1736), null, true, false, "Finace", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("163433d0-57d7-442d-bb55-d294e5326c2f"), new DateTime(2022, 5, 22, 19, 24, 35, 157, DateTimeKind.Utc).AddTicks(1743), null, true, false, "Investor", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("0cb68005-bb52-4ee7-a293-52670ac7d1a6"), new DateTime(2022, 5, 22, 19, 24, 35, 157, DateTimeKind.Utc).AddTicks(1752), null, true, false, "Sale", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("29304f68-b4b7-4e3c-8151-04b03ba03cd2"), new DateTime(2022, 5, 22, 19, 24, 35, 157, DateTimeKind.Utc).AddTicks(1791), null, true, false, "IT Certification", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null },
                    { new Guid("1d100f08-c490-44fd-8103-2e594eeff99d"), new DateTime(2022, 5, 22, 19, 24, 35, 157, DateTimeKind.Utc).AddTicks(1800), null, true, false, "Network & Security", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null },
                    { new Guid("6526816f-0bd3-4975-b943-68ffe5b50278"), new DateTime(2022, 5, 22, 19, 24, 35, 157, DateTimeKind.Utc).AddTicks(1807), null, true, false, "Hard Ware", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_audioLanguages_LanguageId",
                table: "audioLanguages",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audioLanguages");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0cb68005-bb52-4ee7-a293-52670ac7d1a6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("163433d0-57d7-442d-bb55-d294e5326c2f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1d100f08-c490-44fd-8103-2e594eeff99d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("29304f68-b4b7-4e3c-8151-04b03ba03cd2"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("31a4a026-6ed9-4fac-a78b-bdad8593d704"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5563d518-96d3-4412-8c87-bfb05c9957ac"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6373f39b-7049-4997-b4b7-b1c451474d23"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6526816f-0bd3-4975-b943-68ffe5b50278"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d96f1c89-fdf1-40ed-b80e-cc2cc1b7b92a"));

            migrationBuilder.DropColumn(
                name: "HeadLine",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ProfileLink",
                table: "AspNetUsers",
                newName: "Instroduction");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908753582"),
                column: "ConcurrencyStamp",
                value: "e2a878b6-565d-47bb-88b6-7df6fc5a31da");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f522"),
                column: "ConcurrencyStamp",
                value: "e6da178b-fce6-4f99-b9ec-9098130c518c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f582"),
                column: "ConcurrencyStamp",
                value: "d5c1fc41-b3b1-410f-9594-94a40c50569e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908752532"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "8b44e3b9-cadf-404a-9c30-9e82b6f61f6a", new DateTime(2022, 5, 22, 15, 4, 27, 717, DateTimeKind.Utc).AddTicks(4017), "AQAAAAEAACcQAAAAEKfiXLqj4rCwna5g4ley2qNRB9PGjXZ97LcDru/IrSAb7P8M9hYlXf990zKiRsXWqg==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da02-3d3e-248d-a207-d53908753582"),
                column: "CreatedAt",
                value: new DateTime(2022, 5, 22, 15, 4, 27, 662, DateTimeKind.Utc).AddTicks(360));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da02-3d3e-428d-a207-d53908753582"),
                column: "CreatedAt",
                value: new DateTime(2022, 5, 22, 15, 4, 27, 662, DateTimeKind.Utc).AddTicks(261));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da69-3d3e-428d-a207-d53908753582"),
                column: "CreatedAt",
                value: new DateTime(2022, 5, 22, 15, 4, 27, 658, DateTimeKind.Utc).AddTicks(9533));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "IsDeleted", "Name", "ParentId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("7a70ebdc-8364-4b07-957b-c0c8352046be"), new DateTime(2022, 5, 22, 15, 4, 27, 661, DateTimeKind.Utc).AddTicks(9951), null, true, false, "Web Developer", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("22be68bb-305e-49c5-8be2-1a941df33338"), new DateTime(2022, 5, 22, 15, 4, 27, 662, DateTimeKind.Utc).AddTicks(210), null, true, false, "Data Science", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("44807556-f76d-4487-9d56-14d127ff0a4a"), new DateTime(2022, 5, 22, 15, 4, 27, 662, DateTimeKind.Utc).AddTicks(241), null, true, false, "Mobile App", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("178c33e6-6d99-4c65-a140-f8d7cab7ceec"), new DateTime(2022, 5, 22, 15, 4, 27, 662, DateTimeKind.Utc).AddTicks(299), null, true, false, "Finace", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("4c647bc6-782d-4c6d-9d57-39e6207b8d7e"), new DateTime(2022, 5, 22, 15, 4, 27, 662, DateTimeKind.Utc).AddTicks(321), null, true, false, "Investor", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("a84b4339-3992-4c31-8751-574b3ac6bded"), new DateTime(2022, 5, 22, 15, 4, 27, 662, DateTimeKind.Utc).AddTicks(341), null, true, false, "Sale", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("7109340e-d4ed-45f3-8eb2-cbcbf6323b93"), new DateTime(2022, 5, 22, 15, 4, 27, 662, DateTimeKind.Utc).AddTicks(387), null, true, false, "IT Certification", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null },
                    { new Guid("43897d96-b828-4bc8-9596-e64321f17c86"), new DateTime(2022, 5, 22, 15, 4, 27, 662, DateTimeKind.Utc).AddTicks(482), null, true, false, "Network & Security", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null },
                    { new Guid("9ef3dde9-3281-43d9-a927-1a5901f63ef1"), new DateTime(2022, 5, 22, 15, 4, 27, 662, DateTimeKind.Utc).AddTicks(507), null, true, false, "Hard Ware", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null }
                });
        }
    }
}
