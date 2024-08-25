using Prods.Models.ViewModel;

namespace Prods.Repository.IRepository
{
    public interface IDashBoardRepository
    {
        DashBoardVM GetDashboardSummary();
    }
}
