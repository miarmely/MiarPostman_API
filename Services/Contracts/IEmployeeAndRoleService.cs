using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IEmployeeAndRoleService
    {
        public void CreateEmployeeAndRole(Employee employee);
        public void FillRoles(ref IEnumerable<Employee> employees);
        public void FillRole(ref Employee employee);
        public void UpdateRelations(Employee employeeOnQuery);
    }
}
