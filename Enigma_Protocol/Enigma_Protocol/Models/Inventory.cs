namespace Enigma_Protocol.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public int ProductID { get; set; }
        public int QuantityAvailable { get; set; }
        public int QuantityReserved { get; set; }
        public DateTime LastUpdated { get; set; }

        // Navigation Properties
        public Product Product { get; set; }
    }
}
