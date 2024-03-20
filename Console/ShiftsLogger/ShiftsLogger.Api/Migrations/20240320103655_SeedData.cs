using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShiftsLogger.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Workers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Henk" },
                    { 2, "Ingrid" },
                    { 3, "Jan" },
                    { 4, "Helena" }
                });

            migrationBuilder.InsertData(
                table: "Shifts",
                columns: new[] { "Id", "Duration", "EndShift", "StartShift", "WorkerId" },
                values: new object[,]
                {
                    { 1, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2024, 3, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2024, 3, 15, 17, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, new TimeSpan(0, 3, 0, 0, 0), new DateTime(2024, 3, 15, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 15, 18, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2024, 3, 16, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 16, 12, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 5, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2024, 3, 16, 17, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 16, 15, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 6, new TimeSpan(0, 3, 0, 0, 0), new DateTime(2024, 3, 16, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 16, 18, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 7, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2024, 3, 17, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 8, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2024, 3, 17, 17, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 17, 15, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 9, new TimeSpan(0, 3, 0, 0, 0), new DateTime(2024, 3, 17, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 17, 18, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 10, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2024, 3, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 11, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2024, 3, 15, 17, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 12, new TimeSpan(0, 3, 0, 0, 0), new DateTime(2024, 3, 15, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 15, 18, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 13, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2024, 3, 16, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 16, 12, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 14, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2024, 3, 16, 17, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 16, 15, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 15, new TimeSpan(0, 3, 0, 0, 0), new DateTime(2024, 3, 16, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 16, 18, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 16, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2024, 3, 17, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 17, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2024, 3, 17, 17, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 17, 15, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 18, new TimeSpan(0, 3, 0, 0, 0), new DateTime(2024, 3, 17, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 17, 18, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 19, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2024, 3, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 20, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2024, 3, 15, 17, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 21, new TimeSpan(0, 3, 0, 0, 0), new DateTime(2024, 3, 15, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 15, 18, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 22, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2024, 3, 16, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 16, 12, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 23, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2024, 3, 16, 17, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 16, 15, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 24, new TimeSpan(0, 3, 0, 0, 0), new DateTime(2024, 3, 16, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 16, 18, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 25, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2024, 3, 17, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 26, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2024, 3, 17, 17, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 17, 15, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 27, new TimeSpan(0, 3, 0, 0, 0), new DateTime(2024, 3, 17, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 17, 18, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 28, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2024, 3, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 29, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2024, 3, 15, 17, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 30, new TimeSpan(0, 3, 0, 0, 0), new DateTime(2024, 3, 15, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 15, 18, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 31, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2024, 3, 16, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 16, 12, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 32, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2024, 3, 16, 17, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 16, 15, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 33, new TimeSpan(0, 3, 0, 0, 0), new DateTime(2024, 3, 16, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 16, 18, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 34, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2024, 3, 17, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 35, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2024, 3, 17, 17, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 17, 15, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 36, new TimeSpan(0, 3, 0, 0, 0), new DateTime(2024, 3, 17, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 17, 18, 0, 0, 0, DateTimeKind.Unspecified), 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Workers",
                keyColumn: "Id",
                keyValue: 4);

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
    }
}
