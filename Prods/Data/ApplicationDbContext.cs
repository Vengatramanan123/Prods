using Microsoft.EntityFrameworkCore;
using Prods.Models;

namespace Prods.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Journal> Journals { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<LoginSignup> LoginSignups { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}
