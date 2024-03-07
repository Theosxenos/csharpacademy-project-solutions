using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftsLogger.Api.Migrations
{
    /// <inheritdoc />
    public partial class Updateshiftsmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Shifts");

            migrationBuilder.AddColumn<int>(
                name: "MinutesDuration",
                table: "Shifts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinutesDuration",
                table: "Shifts");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "Shifts",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
