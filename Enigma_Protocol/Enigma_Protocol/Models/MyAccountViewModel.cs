namespace Enigma_Protocol.Models
{
    public class MyAccountViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? ShippingAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public int SolvedPuzzlesCount { get; set; } // Statistic for solved puzzles
    }
}
