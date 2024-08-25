using Prods.Data;
using Prods.Models;
using Prods.Repository.IRepository;

namespace Prods.Repository
{
    public class JournalRepository : Repository<Journal>,IJournalRepository
    {
        private ApplicationDbContext _context;
        public JournalRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Journal journal)
        {
            _context.Update(journal);
        }
    }
}
