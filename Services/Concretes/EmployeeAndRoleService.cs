using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concretes
{
    public class EmployeeAndRoleService : IEmployeeAndRoleService
    {
        private readonly IRepositoryManager _manager;


        public EmployeeAndRoleService(IRepositoryManager manager) =>
            _manager = manager;

       
        public void FillRoles(ref IEnumerable<Employee> employees)
        {
            employees = employees
                .ToList()  //  disconnect from database to close the DataReader.
                .Select(e => new Employee
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    LastName = e.LastName,
                    Job = e.Job,
                    RegisterDate = e.RegisterDate,
                    Salary = e.Salary,
                    Roles = AddRoles(e.Id)
                });
        }
        

        public void FillRole(ref Employee employee)
        {
            employee.Roles = AddRoles(employee.Id);
        }


        private List<string> AddRoles(int employeeId)
        {
            // get employee and role relationships
            var empAndRoles = _manager.EmployeeAndRole
                .FindByEmployeeId(employeeId, false)
                .ToList();  //  disconnect from database to close the DataReader.

            var roleNames = new List<string>();

            // add roles to list
            foreach (var empAndRole in empAndRoles)
                roleNames.Add(
                    _manager.Role.GetById(empAndRole.RoleId, false)
                    .RoleName);

            return roleNames;
        }
    }
}
