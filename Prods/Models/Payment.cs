namespace Prods.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public double Amount { get; set; }
        public string? Currency { get; set; }
        public string? Description { get; set; }
        public string? StripeChargeId { get; set; }
        public string? Status { get; set; }
        public DateTime? PaymentDate { get; set; }

        public Order Order { get; set; }
    }
}
