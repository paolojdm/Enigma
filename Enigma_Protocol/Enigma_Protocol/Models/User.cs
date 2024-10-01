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
        public bool IsAdmin { get; set; }

        // Payment Information
        public string? CardType { get; set; }
        public string? CardOwner { get; set; }
        public string? CardNumber { get; set; }
        public int? CardCVC { get; set; }
        public DateTime? ExpirationDate { get; set; }


        // Navigation Properties
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<PlayerProgress> playerProgresses { get; set; }
        public ICollection<Review> Reviews { get; set; }  // Add this line
    }
}
