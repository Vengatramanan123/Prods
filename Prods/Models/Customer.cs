using System.ComponentModel.DataAnnotations;

namespace Prods.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string ContactNumber { get;set; }
        public string Address { get; set; }
        public string IdNumber { get; set; }
    }
}
