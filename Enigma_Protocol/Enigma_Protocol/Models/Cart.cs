namespace Enigma_Protocol.Models
{
    public class Cart
    {
        public Cart(int cartId, int userID, int bookID, DateTime createdAt, int quantity)
        {
            CartId = cartId;
            UserID = userID;
            BookID = bookID;
            CreatedAt = createdAt;
            Quantity = quantity;
        }

        public int CartId { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Quantity { get; set; }
    }
}
