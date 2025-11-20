using Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly RepositoryContext _context;

        public RepositoryBase(RepositoryContext context) => _context = context;

        public IQueryable<T> FindAll(bool trackChanges) =>
            trackChanges ?
            _context.Set<T>() : _context.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Func<T, bool> condition, bool trackChanges) =>
            trackChanges ?
            _context.Set<T>().Where(condition).AsQueryable() : _context.Set<T>().AsNoTracking().Where(condition).AsQueryable();

        public void Create(T entity) => _context.Set<T>().Add(entity);
        
        public void Update(T entity) => _context.Set<T>().Update(entity);

        public void Delete(T entity) => _context.Set<T>().Remove(entity);
    }
}
