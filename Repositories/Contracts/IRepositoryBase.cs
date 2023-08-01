using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;


namespace Repositories.Contracts
{
    public interface IRepositoryBase<T>
    {
        public int Count { get; }
        public IQueryable<T> FindAll(bool trackChanges);
        public IQueryable<T> FindWithCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        public void Create(T entity);
        public void Update(T entity);
        public void Delete(T entity);
    }
}