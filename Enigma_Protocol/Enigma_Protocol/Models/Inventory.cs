namespace Enigma_Protocol.Models
{
    public class Inventory
    {
        public Inventory(int inventoryId, int productId, string productType, int quantityAvailable,
            int quantityReserved, DateTime lastUpdated, Product product)
        {
            InventoryId = inventoryId;
            ProductId = productId;
            ProductType = productType;
            QuantityAvailable = quantityAvailable;
            QuantityReserved = quantityReserved;
            LastUpdated = lastUpdated;
            Product = product;
        }

        public int InventoryId { get; set; }  // Primary Key
        public int ProductId { get; set; }    // Foreign Key to the Product (could be Room, Item, etc.)
        public string ProductType { get; set; }  // Type of product (e.g., "Room", "Item", etc.)
        public int QuantityAvailable { get; set; }  // Number of units currently available
        public int QuantityReserved { get; set; }   // Units currently reserved (but not yet completed)
        public DateTime LastUpdated { get; set; }   // Timestamp of the last update
        public Product Product { get; set; }
    }

}
