using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExerciseTracker.Migrations
{
    /// <inheritdoc />
    public partial class ChangeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Squats");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Squats",
                newName: "Duration");

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Squats",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                table: "Squats",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStart",
                table: "Squats",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Squats",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Comments", "DateEnd", "DateStart", "Duration" },
                values: new object[] { "Set PR at 50kg", new DateTime(2024, 3, 5, 20, 14, 19, 373, DateTimeKind.Local).AddTicks(458), new DateTime(2024, 3, 5, 17, 14, 19, 373, DateTimeKind.Local).AddTicks(417), new TimeSpan(0, 3, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Squats",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Comments", "DateEnd", "DateStart", "Duration" },
                values: new object[] { "Quit after 3 reps", new DateTime(2024, 3, 6, 17, 29, 19, 373, DateTimeKind.Local).AddTicks(468), new DateTime(2024, 3, 6, 17, 14, 19, 373, DateTimeKind.Local).AddTicks(467), new TimeSpan(0, 0, 15, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Squats",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Comments", "DateEnd", "DateStart", "Duration" },
                values: new object[] { "", new DateTime(2024, 3, 7, 19, 14, 19, 373, DateTimeKind.Local).AddTicks(472), new DateTime(2024, 3, 7, 17, 14, 19, 373, DateTimeKind.Local).AddTicks(471), new TimeSpan(0, 2, 0, 0, 0) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Squats");

            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "Squats");

            migrationBuilder.DropColumn(
                name: "DateStart",
                table: "Squats");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Squats",
                newName: "Date");

            migrationBuilder.AddColumn<float>(
                name: "Weight",
                table: "Squats",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.UpdateData(
                table: "Squats",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "Weight" },
                values: new object[] { new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Local), 10f });

            migrationBuilder.UpdateData(
                table: "Squats",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "Weight" },
                values: new object[] { new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Local), 12.5f });

            migrationBuilder.UpdateData(
                table: "Squats",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Date", "Weight" },
                values: new object[] { new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Local), 15f });
        }
    }
}
