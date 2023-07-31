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
            .Where(e => _manager.EmployeeAndRole
                .FindByEmployeeId(e.Id, false)
                .FirstOrDefault(er => roleIdList.Contains(er.RoleId))
             != null);

            return entity;
        }


        public IEnumerable<Employee> GetEmployeesByCondition(int id, string fullName, string lastName, string job, int salary, string registerDate, List<string> roles, bool trackChanges)
        {
            // when database is empty
            if (_manager.Employee.Count == 0)
                throw new Exception("Empty Database");


            var date = registerDate.Equals("-1") ? new DateTime() 
                : Convert.ToDateTime(registerDate);  // new datetime() ~~> i initialized for can use "date" variable.
                
            // search employee table 
            var entity = _manager.Employee.GetEmployeeByCondition(e =>
                (id == -1 ? true : e.Id == id)
                && (fullName.Equals("-1") ? true : e.FullName.Equals(fullName))
                && (lastName.Equals("-1") ? true : e.LastName.Equals(lastName))
                && (job.Equals("-1") ? true : e.Job.Equals(job))
                && (salary == -1 ? true : e.Salary == salary)
                && (registerDate.Equals("-1") ? true
                    : !e.RegisterDate.Day.Equals(date.Day) ? false  // day control
                        : !e.RegisterDate.Month.Equals(date.Month) ? false  // month control
                            : !e.RegisterDate.Year.Equals(date.Year) ? false : true)    // year control
                , false)
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
    }
}