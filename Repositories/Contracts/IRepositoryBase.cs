using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;


namespace Repositories.Contracts
{
    public interface IRepositoryBase<T>
    {
        int Count { get; }
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindWithCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void MultipleDelete(List<T> entity);
    }
}