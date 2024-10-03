namespace Enigma_Protocol.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }
        public string ShippingStatus { get; set; }
        public List<OrderDetailViewModel> OrderDetails { get; set; }
    }
}
