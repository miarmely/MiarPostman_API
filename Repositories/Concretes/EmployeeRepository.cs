using Entities.Models;
using Microsoft.EntityFrameworkCore.Internal;
using Repositories.Concretes;
using Repositories.Contracts;
using Repositories.EFCore;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Repositories.Concrete
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext context) : base(context)
        {}


        public void CreateEmployee(Employee entity) => Create(entity);


        public void DeleteEmployee(Employee entity) => Delete(entity);


        public IQueryable<Employee> GetAllEmployees(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(e => e.Id);


        public IQueryable<Employee> GetEmployeeByCondition(Expression<Func<Employee, bool>> expression, bool trackChanges) =>
            base.FindWithCondition(expression, trackChanges)
            .OrderBy(e => e.Id);


        public Employee? GetEmployeeById(int id, bool trackChanges) =>
            FindWithCondition(e => e.Id == id, trackChanges)
            .FirstOrDefault();


        public void UpdateEmployee(Employee entity) => Update(entity);
        
    }
}
