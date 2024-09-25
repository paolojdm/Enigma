namespace Enigma_Protocol.Models
{
    public class OrderDetails
    {
        // Parameterless constructor
        public OrderDetails() { }

        // Constructor with parameters
        public OrderDetails(int orderDetailId, int orderID, Order order, int productID, Product product, int quantity, double unitPrice)
        {
            OrderDetailId = orderDetailId;
            OrderID = orderID;
            Order = order;
            ProductID = productID;
            Product = product;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        // Properties
    public int OrderDetailId { get; set; } // Primary Key
    public int OrderID { get; set; } // Foreign Key to Order
    public Order Order { get; set; } // Navigation property to Order
    public int ProductID { get; set; } // Foreign Key to Product
    public Product Product { get; set; } // Navigation property to Product
    public int Quantity { get; set; } // Quantity of the product ordered
    public double UnitPrice { get; set; }
    }
}

