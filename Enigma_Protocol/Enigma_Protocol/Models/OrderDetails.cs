namespace Enigma_Protocol.Models
{
    public class OrderDetails
    {
        public OrderDetails(int orderDetailId, int orderID, Order order, int productId, Product product, int quantity, double unitPrice)
        {
            OrderDetailId = orderDetailId;
            OrderID = orderID;
            Order = order;
            ProductId = productId;
            Product = product;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public int OrderDetailId { get; set; }
        public int OrderID { get; set; }  // Foreign Key to Order
        public Order Order { get; set; }  // Proprietà di navigazione a Order

        public int ProductId { get; set; }  // Foreign Key to Product
        public Product Product { get; set; }  // Proprietà di navigazione a Product

        public int Quantity { get; set; }  // Quantità del prodotto ordinato
        public double UnitPrice { get; set; }  
    }
}

