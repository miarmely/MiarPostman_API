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


        public EmployeeService(IRepositoryManager manager)
        {
            _manager = manager;
        }


        public void CreateEmployee(Employee employee)
        {
            // add employee to database.
            _manager.Employee.CreateEmployee(employee);
            _manager.Save();
        }


        public IEnumerable<Employee> GetAllEmployees(bool trackChanges)
        {
            var entity = _manager.Employee.GetAllEmployees(trackChanges);

            // when database is empty
            if (entity is null)
                throw new Exception("Empty Database");

            return entity;
        }


        public Employee GetEmployeeById(int id, bool trackChanges)
        {
            var entity = _manager.Employee.GetEmployeeById(id, trackChanges);

            // when not found
            if (entity is null)
                throw new Exception("Not Matched");

            return entity;
        }


        public IEnumerable<Employee> GetEmployeesByCondition(int? id, string? fullName, string? lastName, string? job, decimal? salary, List<string> roles, string? registerDate, bool trackChanges)
        {
            // when database is empty
            if (_manager.Employee.Count == 0)
                throw new Exception("Empty Database");

            // set format of registerDate
            if (registerDate is not null)
                registerDate = Convert
                .ToDateTime(registerDate)
                .ToString("dd.MM.yyyy");

            // search employee table 
            var entity = _manager.Employee.GetEmployeeByCondition(e =>
                (id == null ? true : e.Id == id)
                && (fullName == null ? true : e.FullName.Equals(fullName))
                && (lastName == null ? true : e.LastName.Equals(lastName))
                && (job == null ? true : e.Job.Equals(job))
                && (salary == null ? true : e.Salary == salary)
                && (registerDate == null ? true
                    : e.RegisterDate
                    .ToString("dd.MM.yyyy")
                    .Equals(registerDate))
                , trackChanges)
                .AsEnumerable();

            //when there is role condition.
            if (roles.Count != 0)
                entity = ControlOfRoleCondition(entity, roles);

            // when nothing matched
            if (entity.Count() == 0)
                throw new Exception("Not Matched");

            return entity;
        }


        public Employee GetOneEmployeeById(int id, bool trackChanges)
        {
            var entity = _manager.Employee.GetEmployeeById(id, trackChanges);

            // when id not matched
            if (entity is null)
                throw new Exception("Not Matched");

            return entity;
        }


        public void UpdateOneEmployee(int id, ref Employee employee, bool trackChanges)
        {
            var entity = _manager.Employee.GetEmployeeById(id, trackChanges);

            // when id not Found
            if (entity is null)
                throw new Exception("Not Matched");

            // update
            employee.RegisterDate = entity.RegisterDate;
            _manager.Employee.UpdateEmployee(employee);
            _manager.Save();
        }


        public Employee PartiallyUpdateOneEmployee(int id, JsonPatchDocument<Employee> employeePatch, bool trackChanges)
        {
            var entity = _manager.Employee.GetEmployeeById(id, trackChanges);

            // when id not Found
            if (entity is null)
                throw new Exception("Not Matched");

            employeePatch.ApplyTo(entity);
            _manager.Save();

            return entity;
        }


        public void DeleteOneEmployee(int id)
        {
            var entity = _manager.Employee.GetEmployeeById(id, false);

            // when id not matched
            if (entity is null)
                throw new Exception("Not Matched");

            // delete
            _manager.Employee.DeleteOneEmployee(entity);
            _manager.Save();
        }


        public void DeleteEmployees(IEnumerable<Employee> entity)
        {
            _manager.Employee.DeleteEmployees(entity);
            _manager.Save();
        }


        private IEnumerable<Employee> ControlOfRoleCondition(IEnumerable<Employee> employees, List<string> roles)
        {
            var roleIdList = new List<int>();

            // add role ids to list
            foreach (var roleName in roles)
                roleIdList.Add(
                    _manager.Role
                    .GetByRoleName(roleName, false)
                    .Id);

            // get employees that matched any role.
            var entity = employees
                .ToList()  // disconnect from employee
                .Where(e => _manager.EmployeeAndRole
                    .FindByEmployeeId(e.Id, false)
                    .Any(er => roleIdList.Contains(er.RoleId)));

            return entity;
        }
    }
}