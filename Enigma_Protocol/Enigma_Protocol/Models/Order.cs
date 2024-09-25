namespace Enigma_Protocol.Models
{
    public class Order
    {
        public Order(int orderId, int userID, DateTime orderDate,
            double totalAmount, string shippingAddress, string shippingStatus,
            string trackingNumber, DateTime updatedAt)
        {
            OrderId = orderId;
            UserID = userID;
            OrderDate = orderDate;
            TotalAmount = totalAmount;
            ShippingAddress = shippingAddress;
            ShippingStatus = shippingStatus;
            TrackingNumber = trackingNumber;
            UpdatedAt = updatedAt;
        }

       public int OrderId { get; set; }
       public int UserID { get; set; }
       public DateTime OrderDate { get; set; }
       public double TotalAmount { get; set; }
       public string ShippingAddress { get; set; }
       public string ShippingStatus { get; set; }
       public string TrackingNumber { get; set; }
       public DateTime UpdatedAt { get; set; }
       public List<OrderDetails> OrdersDetails { get; set; }
    }
}
