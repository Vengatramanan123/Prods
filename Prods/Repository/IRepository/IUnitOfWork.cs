using Prods.Data;

namespace Prods.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IJournalRepository Journal { get; }
        IProductRepository Product { get; }
        ICustomerRepository Customer { get; }
        IOrderRepository Order { get; }

        void Save();
    }
}
