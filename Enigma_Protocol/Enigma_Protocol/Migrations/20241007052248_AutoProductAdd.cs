using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Enigma_Protocol.Migrations
{
    /// <inheritdoc />
    public partial class AutoProductAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ImageUrl", "Price", "ProductDescription", "ProductName", "ProductType" },
                values: new object[,]
                {
                    { 1, "/Images/tshirt2.jpg", 24.989999999999998, "A black T-shirt, sizes S-M-L, with high quality printing.", "T-Shirt - Black", "T-Shirt" },
                    { 2, "/Images/tazza2.jpg", 14.99, "A white coffee cup, with high quality printing.", "Cup - White", "Cup" },
                    { 3, "/Images/cap2.jpg", 19.989999999999998, "A black cap, sizes size M, with high quality printing.", "Cap - Black", "Cap" },
                    { 4, "/Images/zzgame.jpg", 19.989999999999998, "A one time access to the Horror Escape Room.", "Escape Room", "Game" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 7, 7, 22, 47, 777, DateTimeKind.Local).AddTicks(641));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 7, 7, 22, 47, 777, DateTimeKind.Local).AddTicks(700));

            migrationBuilder.InsertData(
                table: "Inventory",
                columns: new[] { "Id", "LastUpdated", "ProductID", "QuantityAvailable", "QuantityReserved" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 7, 7, 22, 47, 777, DateTimeKind.Local).AddTicks(834), 1, 102, 30 },
                    { 2, new DateTime(2024, 10, 7, 7, 22, 47, 777, DateTimeKind.Local).AddTicks(837), 3, 95, 28 },
                    { 3, new DateTime(2024, 10, 7, 7, 22, 47, 777, DateTimeKind.Local).AddTicks(839), 4, 69, 16 },
                    { 4, new DateTime(2024, 10, 7, 7, 22, 47, 777, DateTimeKind.Local).AddTicks(840), 4, 9832, 312 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

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
    }
}
