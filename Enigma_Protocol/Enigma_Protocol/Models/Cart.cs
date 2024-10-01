namespace Enigma_Protocol.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; } // Changed from InventoryID to ProductID
        public DateTime CreatedAt { get; set; }
        public int Quantity { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public Product Product { get; set; } // Changed from Inventory to Product
    }
}