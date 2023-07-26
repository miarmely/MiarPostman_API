using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using Repositories.Contracts;
using Repositories.Migrations;
using Services.Contracts;
using System.IO.Pipes;

namespace Services.Concretes
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _manager;
        private readonly EmployeeAndRoleService EmployeeAndRoleService;


        public EmployeeService(IRepositoryManager manager)
        {
            _manager = manager;
            EmployeeAndRoleService = new EmployeeAndRoleService(manager);
        }


        private void AddEmployeeAndRole(Employee employee)
        {
            // view roles of one employee
            employee.Roles.ForEach(roleName =>
            {
                var role = _manager.Role.GetByRoleName(roleName, false);
                var empAndRole = new EmployeeAndRole()
                {
                    EmployeeId = employee.Id,
                    RoleId = role.Id
                };

                _manager.EmployeeAndRole.CreateEmployeeAndRole(empAndRole);
                _manager.Save();
            });
        }


        public void CreateEmployee(Employee employee)
        {

            // add employee to database.
            _manager.Employee.CreateEmployee(employee);
            _manager.Save();

            // update EmployeeAndRole table
            AddEmployeeAndRole(employee);
        }


        public void DeleteOneEmployee(int id, bool trackChanges)
        {
            var entity = _manager.Employee.GetEmployeeById(id, false);

            // when id not matched
            if (entity is null)
                throw new Exception($"Id Not Found");

            // delete
            _manager.Employee.DeleteEmployee(entity);
            _manager.Save();
        }


        private IEnumerable<Employee> ControlOfRoleCondition(IEnumerable<Employee> employees, List<string> roles)
        {
            var roleIdList = new List<int>();

            // add role ids to list
            foreach (var roleName in roles)
                roleIdList.Add(
                    _manager.Role.GetByRoleName(roleName, false)
                    .Id);

            // get employees that matched any role.
                var entity = employees
                .ToList()  // disconnect from employee
                .Where(e =>_manager.EmployeeAndRole
                    .FindByEmployeeId(e.Id, false)
                    .FirstOrDefault(er => roleIdList.Contains(er.RoleId))
                 != null);

            return entity;
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


        private IEnumerable<Employee> FillRoles(IEnumerable<Employee> entity)
        {
            return entity
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


        public IEnumerable<Employee> GetEmployees(int id, string fullName, string lastName, string job, int salary, string registerDate, List<string> roles, bool trackChanges)
        {
            // when database is empty
            if (_manager.Employee.Count == 0)
                throw new Exception("Empty Database");

            IEnumerable <Employee> entity;

            // all employee display
            if (id == -1 
                && fullName.Equals("-1") 
                && lastName.Equals("-1") 
                && job.Equals("-1") 
                && salary == -1 
                && roles.Count == 0
                && registerDate.Equals("-1"))
                    entity = _manager.Employee.GetAllEmployees(false);

            // display by condition
            else
            {
                var date = registerDate.Equals("-1") ? new DateTime() 
                    : Convert.ToDateTime(registerDate);  // new datetime() ~~> i initialized for can use "date" variable.
                
                // search employee table 
                entity = _manager.Employee.GetEmployeeByCondition(e =>
                    (id == -1 ? true : e.Id == id)
                    && (fullName.Equals("-1") ? true : e.FullName.Equals(fullName))
                    && (lastName.Equals("-1") ? true : e.LastName.Equals(lastName))
                    && (job.Equals("-1") ? true : e.Job.Equals(job))
                    && (salary == -1 ? true : e.Salary == salary)
                    && (registerDate.Equals("-1") ? true
                        : !e.RegisterDate.Day.Equals(date.Day) ? false  // day control
                            : !e.RegisterDate.Month.Equals(date.Month) ? false  // month control
                                : !e.RegisterDate.Year.Equals(date.Year) ? false : true)    // year control
                    , false);

                //when there is role condition.
                if (roles.Count != 0)
                    entity = ControlOfRoleCondition(entity, roles);
            }

            // when nothing matched
            if (entity.Count() == 0)
                throw new Exception("Not Matched");

            return FillRoles(entity);
        }


        public Employee GetOneEmployeeById(int id, bool trackChanges)
        {
            var entity = _manager.Employee.GetEmployeeById(id, trackChanges);

            // when id not matched
            if (entity is null)
                throw new Exception("Id Not Found");

            return entity;
        }


        public void UpdateOneEmployee(int id, Employee employee, bool trackChanges)
        {
            var entity = _manager.Employee.GetEmployeeById(id, trackChanges);

            // when id not Found
            if (entity is null)
                throw new Exception("Id Not Found");

            // update
            employee.Id = id;
            _manager.Employee.UpdateEmployee(employee);
            _manager.Save();
        }


        public Employee PartiallyUpdateOneEmployee(int id, JsonPatchDocument<Employee> employeePatch, bool trackChanges)
        {
            var entity = _manager.Employee.GetEmployeeById(id, trackChanges);

            // when id not Found
            if (entity is null)
                throw new Exception("Id Not Found");

            employeePatch.ApplyTo(entity);
            _manager.Save();

            return entity;
        }
    }
}