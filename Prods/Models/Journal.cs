using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace Prods.Models
{
    public class Journal
    {
        [Key]
        public int JournalId { get; set; }
        [Required]
        public string Name { get; set; }
        public int OrderId { get; set; }
       // public ICollection<Product> Products { get; set; }
    }
}
