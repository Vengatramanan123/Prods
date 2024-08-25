using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Prods.Models
{
    public class LoginSignup
    {
        [Key]
        public int UserId { get; set; }
        [ValidateNever]
        public string UserName { get; set; }
        [ValidateNever]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [ValidateNever]
        public string PhoneNumber { get; set; }
        [ValidateNever]
        public string Role { get; set; }
    }
}
