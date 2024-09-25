namespace Enigma_Protocol.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int InventoryID { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Quantity { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public Inventory Inventory { get; set; }
    }
}
