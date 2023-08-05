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
        void CreateEmployeeAndRole(EmployeeAndRole entity);
        IQueryable<EmployeeAndRole> FindAllEmployeesAndRoles(bool trackChanges);
        IQueryable<EmployeeAndRole> FindByEmployeeId(int employeeId, bool trackChanges);
        EmployeeAndRole FindByEmployeeAndRoleId(int employeeId, int roleId, bool trackChanges);
        void DeleteEmployeeAndRole(EmployeeAndRole empAndRole);
        void DeleteEmployeeAndRoles(List<EmployeeAndRole> empAndRole);
    }
}