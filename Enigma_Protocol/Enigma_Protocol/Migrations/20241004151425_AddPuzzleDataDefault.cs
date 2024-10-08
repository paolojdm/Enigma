using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Enigma_Protocol.Migrations
{
    /// <inheritdoc />
    public partial class AddPuzzleDataDefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Puzzles",
                columns: new[] { "Id", "Answer", "Question", "RoomId" },
                values: new object[,]
                {
                    { 1, "4359", "Enter the code", 1 },
                    { 2, "true", "Reorder the image", 1 },
                    { 3, "tenebre", "Enter the right word", 2 },
                    { 4, "uscita", "Enter the right word", 3 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 4, 17, 14, 25, 415, DateTimeKind.Local).AddTicks(5539));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 4, 17, 14, 25, 415, DateTimeKind.Local).AddTicks(5591));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Puzzles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Puzzles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Puzzles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Puzzles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 2, 17, 25, 59, 310, DateTimeKind.Local).AddTicks(6885));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 2, 17, 25, 59, 310, DateTimeKind.Local).AddTicks(6930));
        }
    }
}
