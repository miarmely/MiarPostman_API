using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;


namespace Repositories.Contracts
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        public void CreateEmployee(Employee employee);
        public Employee? GetEmployeeById(int id, bool trackChanges);
        public IQueryable<Employee> GetEmployeeByCondition(Expression<Func<Employee, bool>> expression, bool trackChanges);
        public IQueryable<Employee> GetAllEmployees(bool trackChanges);
        public void UpdateEmployee(Employee employee);
        public void DeleteEmployee(Employee employee);
    }
}
