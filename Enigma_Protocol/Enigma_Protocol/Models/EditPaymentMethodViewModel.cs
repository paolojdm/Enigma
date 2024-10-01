namespace Enigma_Protocol.Models
{
    public class EditPaymentMethodViewModel
    {
        public string EditCardType { get; set; }
        public string EditCardOwner { get; set; }
        public string EditCardNumber { get; set; }
        public string EditCardCVC { get; set; }
        public DateTime? EditExpirationDate { get; set; }
    }
}
