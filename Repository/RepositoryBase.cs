using Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository
{
    public class RepositoryBase<TContext, T> : IRepositoryBase<T> 
        where T : class 
        where TContext: DbContext
    {
        private readonly TContext _context;
        internal DbSet<T> dbSet;

        public RepositoryBase(TContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }

        public IQueryable<T> FindAll(bool trackChanges) =>
            trackChanges ?
            dbSet : dbSet.AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition, bool trackChanges) => trackChanges ?
                dbSet.Where(condition) : dbSet.AsNoTracking().Where(condition);

        public void Create(T entity) => dbSet.Add(entity);
        
        public void Update(T entity) => dbSet.Update(entity);

        public void Delete(T entity) => dbSet.Remove(entity);

        public IQueryable<T> FindAllByConditionWithIncludes(Expression<Func<T, bool>>? condition, bool trackChanges, string? includes = null)
        {
            IQueryable<T> query = dbSet;
            if (condition != null)
            {
                query = query.Where(condition);
            }
            if (!string.IsNullOrEmpty(includes))
            {
                foreach (var includeProperty in includes.Split([','], StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return trackChanges ? query : query.AsNoTracking();
        }

        public IQueryable<T> FindByConditionWithIncludes(Expression<Func<T, bool>> condition, bool trackChanges, string? includes = null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includes))
            {
                foreach (var includeProperty in includes.Split([','], StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return trackChanges ? query.Where(condition) : query.Where(condition).AsNoTracking();
        }
    }
}
