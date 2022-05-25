using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseAPI.Migrations
{
    public partial class updateanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CloseCaptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CloseCaptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "CloseCaptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CloseCaptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CloseCaptions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "CloseCaptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AudioLanguages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AudioLanguages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "AudioLanguages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AudioLanguages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "AudioLanguages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "AudioLanguages",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CloseCaptions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CloseCaptions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CloseCaptions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CloseCaptions");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CloseCaptions");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "CloseCaptions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AudioLanguages");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AudioLanguages");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AudioLanguages");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AudioLanguages");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "AudioLanguages");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "AudioLanguages");
        }
    }
}
