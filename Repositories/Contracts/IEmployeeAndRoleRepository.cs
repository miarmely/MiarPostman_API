using Entities.RelationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IEmployeeAndRoleRepository : IRepositoryBase<EmployeeAndRole>
    {
        public void CreateEmployeeAndRole(EmployeeAndRole entity);
        public IQueryable<EmployeeAndRole> FindAllEmployeesAndRoles(bool trackChanges);
        public IQueryable<EmployeeAndRole> FindByEmployeeId(int employeeId, bool trackChanges);
        public EmployeeAndRole FindByEmployeeAndRoleId(int employeeId, int roleId, bool trackChanges);
        public void DeleteEmployeeAndRole(EmployeeAndRole empAndRole);
        public void DeleteEmployeeAndRoles(IEnumerable<EmployeeAndRole> empAndRole);
    }
}