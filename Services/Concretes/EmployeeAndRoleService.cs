using Entities.Models;
using Entities.RelationModels;
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


        public void CreateEmployeeAndRole(Employee employee)
        {
            employee.Roles.ForEach(roleWithoutId =>
            {
                var role = _manager.Role.GetByRoleName(roleWithoutId.RoleName, false);

                // create employeeAndRole
                _manager.EmployeeAndRole.CreateEmployeeAndRole(new EmployeeAndRole()
                {
                    EmployeeId = employee.Id,
                    RoleId = role.Id
                });

                _manager.Save();
            });
        }


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


        public void UpdateRelations(Employee employeeOnQuery)
        {
            var empAndRoles = _manager.EmployeeAndRole
                .FindByEmployeeId(employeeOnQuery.Id, false)
                .ToList();

            // when exists on database but not exists on query
            foreach (var empAndRole in empAndRoles)
            {
                var roleName = _manager.Role.GetById(empAndRole.RoleId, false)
                    .RoleName;

                // delete empAndRole
                if (!employeeOnQuery.Roles.Any(r => r.RoleName
                        .Equals(roleName)))
                    _manager.EmployeeAndRole.DeleteEmployeeAndRole(empAndRole);
            }

            // when exists on query but not exists on database
            foreach (var roleOnQuery in employeeOnQuery.Roles)
            {
                var roleId = _manager.Role.GetByRoleName(roleOnQuery.RoleName, false)
                    .Id;

                // add employeeAndRole
                if (!empAndRoles.Any(e => e.RoleId == roleId))
                    _manager.EmployeeAndRole.CreateEmployeeAndRole(new EmployeeAndRole()
                    {
                        EmployeeId = employeeOnQuery.Id,
                        RoleId = roleId
                    });
            }

            _manager.Save();
        }


        private List<Role> AddRoles(int employeeId)
        {
            // get employeeAndRole with matched employee
            var empAndRoles = _manager.EmployeeAndRole
                .FindByEmployeeId(employeeId, false)
                .ToList();  //  disconnect from database to close the DataReader.

            var roles = new List<Role>();

            foreach (var empAndRole in empAndRoles)
                roles.Add(
                    _manager.Role.GetById(empAndRole.RoleId, false));

            return roles;
        }
    }
}
