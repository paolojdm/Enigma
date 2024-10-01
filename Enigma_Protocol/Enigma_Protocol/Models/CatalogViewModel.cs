namespace Enigma_Protocol.Models
{
    public class CatalogViewModel
    {
       
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double Price { get; set; }
        public string ProductType { get; set; }
        public int QuantityAvailable { get; set; }
        public string ImageUrl { get; set; } // Ensure this property is present
    }
}
