namespace Enigma_Protocol.Models
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int InventoryID { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Quantity { get; set; }

        // Include navigation properties if needed
        public string UserName { get; set; }
        public string ProductName { get; set; }
    }
}
