using System.ComponentModel.DataAnnotations;

namespace Enigma_Protocol.Models
{
    public class EditProfileViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Shipping address is required")]
        public string? ShippingAddress { get; set; }
    }
}
