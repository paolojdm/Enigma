namespace Enigma_Protocol.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingStatus { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
