using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExerciseTracker.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Squats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Weight = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Squats", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Squats",
                columns: new[] { "Id", "Date", "Weight" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Local), 10f },
                    { 2, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Local), 12.5f },
                    { 3, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Local), 15f }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Squats");
        }
    }
}
