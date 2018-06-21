using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleMvc.Data.Migrations
{
    public partial class ChangeNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Notes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Notes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Notes");
        }
    }
}
