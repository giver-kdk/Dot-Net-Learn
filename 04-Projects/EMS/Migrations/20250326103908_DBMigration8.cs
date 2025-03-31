using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.Migrations
{
    /// <inheritdoc />
    public partial class DBMigration8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClockOut",
                table: "TimeLogs");

            migrationBuilder.RenameColumn(
                name: "ClockIn",
                table: "TimeLogs",
                newName: "Log");

            migrationBuilder.AddColumn<string>(
                name: "LogType",
                table: "TimeLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogType",
                table: "TimeLogs");

            migrationBuilder.RenameColumn(
                name: "Log",
                table: "TimeLogs",
                newName: "ClockIn");

            migrationBuilder.AddColumn<DateTime>(
                name: "ClockOut",
                table: "TimeLogs",
                type: "datetime2",
                nullable: true);
        }
    }
}
