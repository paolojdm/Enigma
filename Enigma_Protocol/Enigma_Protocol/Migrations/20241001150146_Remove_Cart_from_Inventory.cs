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
            // Check and drop foreign key constraint if it exists
            migrationBuilder.Sql(@"
        IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Carts_Inventory_InventoryId')
        BEGIN
            ALTER TABLE [Carts] DROP CONSTRAINT [FK_Carts_Inventory_InventoryId]
        END
    ");

            // Check and drop index if it exists
            migrationBuilder.Sql(@"
        IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Carts_InventoryId')
        BEGIN
            DROP INDEX [IX_Carts_InventoryId] ON [Carts]
        END
    ");

            // Drop the InventoryId column only if it exists
            migrationBuilder.Sql(@"
        IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Carts' AND COLUMN_NAME = 'InventoryId')
        BEGIN
            ALTER TABLE [Carts] DROP COLUMN [InventoryId]
        END
    ");
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
