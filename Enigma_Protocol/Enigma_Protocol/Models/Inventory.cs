using System;
using System.Collections.Generic;
namespace Enigma_Protocol.Models
{
    public class Inventory
    {
        public Inventory()
        {
            // Initialize collections if applicable
        }

        // Constructor with parameters
        public Inventory(int inventoryId, int productID, string productType,
                         int quantityAvailable, int quantityReserved,
                         DateTime lastUpdated, Product product)
        {
            InventoryId = inventoryId;
            ProductID = productID;
            ProductType = productType;
            QuantityAvailable = quantityAvailable;
            QuantityReserved = quantityReserved;
            LastUpdated = lastUpdated;
            Product = product;
        }

        // Properties
        public int InventoryId { get; set; }  // Primary Key
        public int ProductID { get; set; }    // Foreign Key to the Product
        public string ProductType { get; set; }  // Type of product
        public int QuantityAvailable { get; set; }  // Number of units available
        public int QuantityReserved { get; set; }   // Units reserved
        public DateTime LastUpdated { get; set; }   // Last update timestamp
        public Product Product { get; set; }  // Navigation property to Product

        // Optionally, if there are multiple inventories for a product
        public ICollection<Cart> Carts { get; set; } // Navigation property to Cart
        public ICollection<Inventory> InventoryEntries { get; set; }  // Collection of inventory entries
    }
}
