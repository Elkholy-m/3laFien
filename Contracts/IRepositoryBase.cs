using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition, bool trackChanges);
        IQueryable<T> FindAllByConditionWithIncludes(Expression<Func<T, bool>>? condition, bool trackChanges, string? includes = null);
        IQueryable<T> FindByConditionWithIncludes(Expression<Func<T, bool>> condition, bool trackChanges, string? includes = null);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
