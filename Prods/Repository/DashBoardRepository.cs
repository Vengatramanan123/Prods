using Prods.Data;
using Prods.Models.ViewModel;
using Prods.Repository.IRepository;
using Prods.Models;

namespace Prods.Repository
{
    public class DashBoardRepository :IDashBoardRepository
    {
        private readonly ApplicationDbContext _context;

        public DashBoardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public DashBoardVM GetDashboardSummary()
        {
            var totalProducts = _context.Products.Count();
            var totalJournals = _context.Journals.Count();

            return new DashBoardVM
            {
                ProductCount = totalProducts,
                JournalCount = totalJournals,
                TotalCount = totalJournals + totalProducts
            };
        }
    }
}
