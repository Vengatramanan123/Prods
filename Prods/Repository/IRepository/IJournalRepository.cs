using Prods.Models;

namespace Prods.Repository.IRepository
{
    public interface IJournalRepository :IRepository<Journal>
    {
       void Update(Journal journal);
    }
}
