using System.Collections.Specialized;

namespace Prods.Utilites
{
    public static class OrderStatus
    {
        public const string orderplaced = "Payment Completed";
        public const string ordershipped = "Order Shipped";
        public const string ordercancelled = "Order Cancelled";
        public const string orderplacedpaymentpending = "Order Placed. Payment Pending";
        public const string paymentreceived = "Payment Received";
    }
}
