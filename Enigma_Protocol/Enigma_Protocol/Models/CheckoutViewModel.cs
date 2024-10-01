namespace Enigma_Protocol.Models
{
    public class CheckoutViewModel
    {
        public User User { get; set; }
        public IEnumerable<CartViewModel> CartItems { get; set; }
    }
}
