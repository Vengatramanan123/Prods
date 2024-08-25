using Prods.Data;
using Prods.Models.ViewModel;
using Prods.Repository.IRepository;
using Prods.Models;

namespace Prods.Repository
{
    public class DashBoardRepository :IDashBoardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public DashBoardRepository(ApplicationDbContext context,IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public DashBoardVM GetDashboardSummary()
        {
            var totalProducts = _context.Products.Count();
            var totalJournals = _context.Journals.Count();
            var orderCount = _context.Orders.Count();
            List<Order> orders;
            List<Product> products;

            return new DashBoardVM
            {
                ProductCount = totalProducts,
                JournalCount = totalJournals,
                OrderCount = orderCount,
                TotalCount = totalJournals + totalProducts,
                orders = _unitOfWork.Order.GetAll().ToList(),
                products = _unitOfWork.Product.GetAll().ToList(),
            };
        }
    }
}
