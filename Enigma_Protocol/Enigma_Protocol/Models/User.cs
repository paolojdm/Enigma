namespace Enigma_Protocol.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string? ShippingAddress { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
