using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Entities.DataModels;


namespace Repositories.Contracts
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        void CreateEmployee(Employee employee);
        Employee? GetEmployeeById(int id, bool trackChanges);
        IQueryable<Employee> GetEmployeeByCondition(Expression<Func<Employee, bool>> expression, bool trackChanges);
        IQueryable<Employee> GetAllEmployees(bool trackChanges);
        void UpdateEmployee(Employee employee);
        void DeleteOneEmployee(Employee employee);
        void DeleteEmployees(List<Employee> entity);
    }
}
