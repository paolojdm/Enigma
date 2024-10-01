namespace Enigma_Protocol.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double Price { get; set; }
        public string ProductType { get; set; }
        public string ImageUrl {  get; set; }

        // Navigation Properties
        public ICollection<Inventory> Inventories { get; set; } = [];
        public ICollection<OrderDetail> OrderDetails { get; set; } = [];
        public ICollection<Review> Reviews { get; set; } = [];
    }
}
