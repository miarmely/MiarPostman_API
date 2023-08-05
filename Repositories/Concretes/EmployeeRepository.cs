using Entities.DataModels;
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


        public void CreateEmployee(Employee entity) =>
            base.Create(entity);


        public IQueryable<Employee> GetAllEmployees(bool trackChanges) =>
            base.FindAll(trackChanges)
            .OrderBy(e => e.Id);


        public IQueryable<Employee> GetEmployeeByCondition(Expression<Func<Employee, bool>> expression, bool trackChanges) =>
            base.FindWithCondition(expression, trackChanges)
            .OrderBy(e => e.Id);


        public Employee? GetEmployeeById(int id, bool trackChanges) =>
            base.FindWithCondition(e => e.Id == id, trackChanges)
            .FirstOrDefault();


        public void UpdateEmployee(Employee entity) =>
            base.Update(entity);


        public void DeleteOneEmployee(Employee entity) =>
            base.Delete(entity);


        public void DeleteEmployees(List<Employee> entity) =>
            base.MultipleDelete(entity);            
    }
}
