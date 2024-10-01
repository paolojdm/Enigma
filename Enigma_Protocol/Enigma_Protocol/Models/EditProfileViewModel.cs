using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Enigma_Protocol.Models
{
    public class EditProfileViewModel
    {
        [HiddenInput(DisplayValue = false)] // This hides the field from the form and prevents validation
        public string UserName { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string Email { get; set; }

        public string? ShippingAddress { get; set; }

        // Payment Information
        public string? CardType { get; set; }
        public string? CardOwner { get; set; }
        public string? CardNumber { get; set; }
        public int? CardCVC { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
