using Microsoft.EntityFrameworkCore;
using Prods.Data;
using Prods.Repository.IRepository;
using System.Linq.Expressions;

namespace Prods.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> Set;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.Set = _db.Set<T>();
        }
        public void Add(T entity)
        {
            Set.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> data)
        {
            IQueryable<T> info = Set;
            info = info.Where(data);
            return info.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> info = Set;
            return info.ToList();
        }

        public void Remove(T entity)
        {
            Set.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            Set.RemoveRange(entity);
        }
    }
}
