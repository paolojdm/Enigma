using System;
using System.Net.NetworkInformation;

namespace Enigma_Protocol.Models
{
    public class Cart
    {
        // Parameterless constructor
        public Cart()
        {
            // You can initialize collections here if needed
        }

        // Constructor with parameters
        public Cart(int cartId, int userID, int inventoryID, DateTime createdAt, int quantity)
        {
            CartId = cartId;
            UserID = userID;
            InventoryID = inventoryID;
            CreatedAt = createdAt;
            Quantity = quantity;
        }

        // Properties
        public int CartId { get; set; } // Primary Key
        public int UserID { get; set; } // Foreign Key to User
        public int InventoryID { get; set; } // Foreign Key to Inventory
        public DateTime CreatedAt { get; set; }
        public int Quantity { get; set; }

        // Navigation properties
        public User User { get; set; } // Navigation property to User
        public Inventory Inventory { get; set; } // Navigation property to Inventory
    }
}
