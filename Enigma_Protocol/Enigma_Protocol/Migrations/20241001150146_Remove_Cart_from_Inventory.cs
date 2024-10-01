using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enigma_Protocol.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Cart_from_Inventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Inventory_InventoryId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_InventoryId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "Carts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InventoryId",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_InventoryId",
                table: "Carts",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Inventory_InventoryId",
                table: "Carts",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "Id");
        }
    }
}
