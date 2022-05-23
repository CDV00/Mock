using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class addCloseCaptionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_audioLanguages_Courses_CourseId",
                table: "audioLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_audioLanguages_Language_LanguageId",
                table: "audioLanguages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_audioLanguages",
                table: "audioLanguages");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("098a0add-2149-42a5-a917-ad100ae36386"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("23a73f21-a261-4f4b-93ca-5891920f3ef9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3e6d0a31-52b4-4a9e-9607-ea9ba24d13de"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8d3fa2d7-48c3-4ec8-a48a-591c39d0b90f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e4d3c3d-f14f-4157-a735-482cf8fc8648"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c50a01ee-821f-4269-b207-01a077931ba9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f43b19ff-5540-4378-ad6b-5ca877187da6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f597e979-b5cd-4d54-83ef-07330b7c62a6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("fb6fbc75-df67-4872-b62d-c7f14090a388"));

            migrationBuilder.RenameTable(
                name: "audioLanguages",
                newName: "AudioLanguages");

            migrationBuilder.RenameIndex(
                name: "IX_audioLanguages_LanguageId",
                table: "AudioLanguages",
                newName: "IX_AudioLanguages_LanguageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AudioLanguages",
                table: "AudioLanguages",
                columns: new[] { "CourseId", "LanguageId" });

            migrationBuilder.CreateTable(
                name: "CloseCaptions",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloseCaptions", x => new { x.CourseId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_CloseCaptions_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CloseCaptions_Language_LanguageId",
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

            migrationBuilder.CreateIndex(
                name: "IX_CloseCaptions_LanguageId",
                table: "CloseCaptions",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AudioLanguages_Courses_CourseId",
                table: "AudioLanguages",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AudioLanguages_Language_LanguageId",
                table: "AudioLanguages",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AudioLanguages_Courses_CourseId",
                table: "AudioLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_AudioLanguages_Language_LanguageId",
                table: "AudioLanguages");

            migrationBuilder.DropTable(
                name: "CloseCaptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AudioLanguages",
                table: "AudioLanguages");

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

            migrationBuilder.RenameTable(
                name: "AudioLanguages",
                newName: "audioLanguages");

            migrationBuilder.RenameIndex(
                name: "IX_AudioLanguages_LanguageId",
                table: "audioLanguages",
                newName: "IX_audioLanguages_LanguageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_audioLanguages",
                table: "audioLanguages",
                columns: new[] { "CourseId", "LanguageId" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908753582"),
                column: "ConcurrencyStamp",
                value: "3a89c074-7f9f-43f4-bfa8-818297971910");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f522"),
                column: "ConcurrencyStamp",
                value: "5759c7f1-58d9-4b65-83b1-ae6e49d367f0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d5390875f582"),
                column: "ConcurrencyStamp",
                value: "75c56dff-d489-4734-ab69-0f73b78de309");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e59da69-3d3e-428d-a207-d53908752532"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "eb2c9a0f-14a9-43a1-a37b-67dc03f468f4", new DateTime(2022, 5, 23, 4, 33, 1, 344, DateTimeKind.Utc).AddTicks(8181), "AQAAAAEAACcQAAAAEHhd9pXDZbqS4T53nHxAxEXX3CLvdHKMek/d5mXTR1XxhkfVRBu7jSFsCA6uIOulOg==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da02-3d3e-248d-a207-d53908753582"),
                column: "CreatedAt",
                value: new DateTime(2022, 5, 23, 4, 33, 1, 302, DateTimeKind.Utc).AddTicks(4688));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da02-3d3e-428d-a207-d53908753582"),
                column: "CreatedAt",
                value: new DateTime(2022, 5, 23, 4, 33, 1, 302, DateTimeKind.Utc).AddTicks(4623));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e47da69-3d3e-428d-a207-d53908753582"),
                column: "CreatedAt",
                value: new DateTime(2022, 5, 23, 4, 33, 1, 300, DateTimeKind.Utc).AddTicks(2405));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "IsDeleted", "Name", "ParentId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("f597e979-b5cd-4d54-83ef-07330b7c62a6"), new DateTime(2022, 5, 23, 4, 33, 1, 302, DateTimeKind.Utc).AddTicks(4309), null, true, false, "Web Developer", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("c50a01ee-821f-4269-b207-01a077931ba9"), new DateTime(2022, 5, 23, 4, 33, 1, 302, DateTimeKind.Utc).AddTicks(4593), null, true, false, "Data Science", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("3e6d0a31-52b4-4a9e-9607-ea9ba24d13de"), new DateTime(2022, 5, 23, 4, 33, 1, 302, DateTimeKind.Utc).AddTicks(4613), null, true, false, "Mobile App", new Guid("9e47da69-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("fb6fbc75-df67-4872-b62d-c7f14090a388"), new DateTime(2022, 5, 23, 4, 33, 1, 302, DateTimeKind.Utc).AddTicks(4659), null, true, false, "Finace", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("f43b19ff-5540-4378-ad6b-5ca877187da6"), new DateTime(2022, 5, 23, 4, 33, 1, 302, DateTimeKind.Utc).AddTicks(4669), null, true, false, "Investor", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("8d3fa2d7-48c3-4ec8-a48a-591c39d0b90f"), new DateTime(2022, 5, 23, 4, 33, 1, 302, DateTimeKind.Utc).AddTicks(4678), null, true, false, "Sale", new Guid("9e47da02-3d3e-428d-a207-d53908753582"), null, null },
                    { new Guid("23a73f21-a261-4f4b-93ca-5891920f3ef9"), new DateTime(2022, 5, 23, 4, 33, 1, 302, DateTimeKind.Utc).AddTicks(4702), null, true, false, "IT Certification", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null },
                    { new Guid("9e4d3c3d-f14f-4157-a735-482cf8fc8648"), new DateTime(2022, 5, 23, 4, 33, 1, 302, DateTimeKind.Utc).AddTicks(4735), null, true, false, "Network & Security", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null },
                    { new Guid("098a0add-2149-42a5-a917-ad100ae36386"), new DateTime(2022, 5, 23, 4, 33, 1, 302, DateTimeKind.Utc).AddTicks(4745), null, true, false, "Hard Ware", new Guid("9e47da02-3d3e-248d-a207-d53908753582"), null, null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_audioLanguages_Courses_CourseId",
                table: "audioLanguages",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_audioLanguages_Language_LanguageId",
                table: "audioLanguages",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
