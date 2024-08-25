namespace Prods.Models.ViewModel
{
    public class PaymentVM
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public string StripeToken { get; set; }

        public LoginSignup User { get; set; }
    }
}
