using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enigma_Protocol.Migrations
{
    /// <inheritdoc />
    public partial class DropAndCreateColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the foreign key constraint for InventoryID first
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Inventory_InventoryID",
                table: "Carts");

            // Drop the index on InventoryID
            migrationBuilder.DropIndex(
                name: "IX_Carts_InventoryID",
                table: "Carts");

            // Drop the InventoryID column
            migrationBuilder.DropColumn(
                name: "InventoryID",
                table: "Carts");

            // Add the new ProductID column
            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "Carts",
                nullable: false,
                defaultValue: 0); // Adjust the default value as necessary

            // Add the foreign key for the new ProductID column
            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Products_ProductID",
                table: "Carts",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Reverse the changes for rollback
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Products_ProductID",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "Carts");

            // Add back the InventoryID column
            migrationBuilder.AddColumn<int>(
                name: "InventoryID",
                table: "Carts",
                nullable: false,
                defaultValue: 0); // Adjust as necessary

            // Recreate the index on InventoryID
            migrationBuilder.CreateIndex(
                name: "IX_Carts_InventoryID",
                table: "Carts",
                column: "InventoryID");

            // Recreate the foreign key for InventoryID
            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Inventory_InventoryID",
                table: "Carts",
                column: "InventoryID",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
