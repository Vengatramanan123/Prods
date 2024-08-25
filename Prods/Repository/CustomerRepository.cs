using Prods.Data;
using Prods.Models;
using Prods.Repository.IRepository;

namespace Prods.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Customer customer)
        {
            _context.Update(customer);
        }
    }
}
