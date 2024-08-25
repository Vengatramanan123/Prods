using Microsoft.AspNetCore.Mvc;
using Prods.Models.ViewModel;
using Prods.Repository.IRepository;

namespace Prods.Controllers
{
    
        public class DashBoardController : Controller
        {
            private readonly IDashBoardRepository _dashboardService;

            public DashBoardController(IDashBoardRepository dashboardService)
            {
                _dashboardService = dashboardService;
            }

            public IActionResult Index()
            {
                var summary = _dashboardService.GetDashboardSummary();

                var dashboardViewModel = new DashBoardVM
                {
                    ProductCount = summary.ProductCount,
                    JournalCount = summary.JournalCount,
                    TotalCount = summary.TotalCount,
                    
                };

                return View(dashboardViewModel);
            }
        }
    
}
