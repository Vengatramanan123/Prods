using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prods.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [ValidateNever]
        public int Id { get; set; }  

        [ForeignKey("Id")]
        [ValidateNever]
        public Product Product { get; set; }
        [ValidateNever]
        public string ProductName { get; set; }
        [ValidateNever]
        public int quantity { get; set; }
        [ValidateNever]
        public string OrderStatus { get; set; }
        [ValidateNever]
        public int Price { get; set; }
        [ValidateNever]
        public int Total { get; set; }

        public int JournalId { get; set; }
        [ForeignKey("JournalId")]
        [ValidateNever]
        public Journal Journal { get; set; }

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public LoginSignup LoginSignup { get; set; }
    }
}
