using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Enigma_Protocol.Migrations
{
    /// <inheritdoc />
    public partial class PlayerProgress_TIME_Health : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CurrentRoomTime",
                table: "PlayerProgress",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Current_Lives_Puzzle",
                table: "PlayerProgress",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Current_Lives_Room",
                table: "PlayerProgress",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RoomStartTime",
                table: "PlayerProgress",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "RoomDescription", "RoomName" },
                values: new object[,]
                {
                    { 1, "A misterious bedroom.", "Bedroom" },
                    { 2, "A strange dining room.", "Dining room" },
                    { 3, "An ancient armoury.", "Armoury" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "CurrentRoomTime",
                table: "PlayerProgress");

            migrationBuilder.DropColumn(
                name: "Current_Lives_Puzzle",
                table: "PlayerProgress");

            migrationBuilder.DropColumn(
                name: "Current_Lives_Room",
                table: "PlayerProgress");

            migrationBuilder.DropColumn(
                name: "RoomStartTime",
                table: "PlayerProgress");
        }
    }
}
