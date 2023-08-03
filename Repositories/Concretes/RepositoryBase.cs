using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repositories.Contracts;
using Repositories.EFCore;

namespace Repositories.Concrete
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly RepositoryContext _context;

        public int Count => _context.Set<T>().Count();


        public RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }


        public void Create(T entity) => _context.Set<T>().Add(entity);

        
        public IQueryable<T> FindAll(bool trackChanges) =>
            trackChanges ? 
            _context.Set<T>() : 
            _context.Set<T>().AsNoTracking();


        public IQueryable<T> FindWithCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            trackChanges ? 
            _context.Set<T>().Where(expression) : 
            _context.Set<T>().Where(expression).AsNoTracking();


        public void Update(T entity) => 
            _context.Set<T>()
            .Update(entity);


        public void Delete(T entity) => 
            _context.Set<T>()
            .Remove(entity);


        public void MultipleDelete(IEnumerable<T> entity) =>
            _context.Set<T>()
            .RemoveRange(entity);
    }
}
// Note: If you use "AsNoTracking()", your query will more fast. So i use trackChanges.