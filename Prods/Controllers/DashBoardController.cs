using Microsoft.AspNetCore.Mvc;
using Prods.Data;
using Prods.Models;
using Prods.Models.ViewModel;
using Prods.Repository.IRepository;

namespace Prods.Controllers
{
    
        public class DashBoardController : Controller
        {
            private readonly IDashBoardRepository _dashboardService;
            private readonly IUnitOfWork _context;

            public DashBoardController(IDashBoardRepository dashboardService, IUnitOfWork context)
            {
                _dashboardService = dashboardService;
                 _context = context;
            }

            public IActionResult Index()
            {
                var summary = _dashboardService.GetDashboardSummary();

                var dashboardViewModel = new DashBoardVM
                {
                    ProductCount = summary.ProductCount,
                    JournalCount = summary.JournalCount,
                    TotalCount = summary.TotalCount,
                    OrderCount = summary.OrderCount,
                    orders = summary.orders,
                    products = summary.products
                };

                return View(dashboardViewModel);
            }
        }
    
}
