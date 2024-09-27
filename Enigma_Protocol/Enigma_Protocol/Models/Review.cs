namespace Enigma_Protocol.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsVisible { get; set; }  // Admins can hide reviews

        // Navigation Properties
        public Product Product { get; set; }
        public User User { get; set; }
    }
}
