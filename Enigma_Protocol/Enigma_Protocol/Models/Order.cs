namespace Enigma_Protocol.Models
{
    public class Order
    {
        // Parameterless constructor
        public Order()
        {
            OrdersDetails = new List<OrderDetails>(); // Initialize the OrdersDetails collection
        }

        // Constructor with parameters
        public Order(int orderId, int userID, DateTime orderDate,
                     double totalAmount, string shippingAddress,
                     string shippingStatus, string trackingNumber,
                     DateTime updatedAt)
        {
            OrderId = orderId;
            UserID = userID;
            OrderDate = orderDate;
            TotalAmount = totalAmount;
            ShippingAddress = shippingAddress;
            ShippingStatus = shippingStatus;
            TrackingNumber = trackingNumber;
            UpdatedAt = updatedAt;
            OrdersDetails = []; // Initialize the OrdersDetails collection
        }

        // Properties
        public int OrderId { get; set; }  // Primary Key
        public int UserID { get; set; }  // Foreign Key to User
        public DateTime OrderDate { get; set; }  // Date of the order
        public double TotalAmount { get; set; }  // Total amount of the order
        public string ShippingAddress { get; set; }  // Address for shipping
        public string ShippingStatus { get; set; }  // Current shipping status
        public string TrackingNumber { get; set; }  // Tracking number for the shipment
        public DateTime UpdatedAt { get; set; }  // Last updated date

        public User User { get; set; }  // Add this line to connect Order to User
        public ICollection<OrderDetails> OrdersDetails { get; set; }  // Collection of order details
    }

}
