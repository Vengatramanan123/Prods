using Prods.Data;
using Prods.Models;
using Prods.Repository.IRepository;

namespace Prods.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product product)
        {
            _context.Update(product);
        }
    }
}
