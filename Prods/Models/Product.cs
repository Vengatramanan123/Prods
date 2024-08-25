using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prods.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [ValidateNever]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [ValidateNever]
        public int Quantity { get; set; }
        public int? JournalId { get; set; }
        [ForeignKey("JournalId")]
        [ValidateNever]
        public Journal Journal { get; set; }
    }
}

