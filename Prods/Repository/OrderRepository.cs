using Microsoft.EntityFrameworkCore;
using Prods.Data;
using Prods.Models;
using Prods.Repository.IRepository;

namespace Prods.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderRepository(ApplicationDbContext db)  : base(db)
        {
            _db = db;
        }

        public void Update(Order order)
        {
            _db.Update(order);
        }
    }
}
