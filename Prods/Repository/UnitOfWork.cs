using Prods.Data;
using Prods.Repository.IRepository;

namespace Prods.Repository
{
    public class UnitOfWork :IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IJournalRepository Journal { get; private set;}
        public IProductRepository Product { get; private set; }
        public ICustomerRepository Customer { get; private set; }
        public IOrderRepository Order { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Journal = new JournalRepository(_db);
            Product = new ProductRepository(_db);
            Customer = new CustomerRepository(_db);
            Order = new OrderRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
