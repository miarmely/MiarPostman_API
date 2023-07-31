using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Identity.Client;

namespace Services.Contracts
{
    public interface IEmployeeService
    {
        public void CreateEmployee(Employee employee);
        public IEnumerable<Employee> GetAllEmployees(bool trackChanges);
        public Employee GetEmployeeById(int id, bool trackChanges);
        public IEnumerable<Employee> GetEmployeesByCondition(int id, string fullName, string lastName, string job, int salary, string registerDate, List<string> roles, bool trackChanges);
        public void UpdateOneEmployee(int id, ref Employee employee, bool trackChanges);
        public Employee PartiallyUpdateOneEmployee(int id, JsonPatchDocument<Employee> employeePatch, bool trackChanges);
        public void DeleteOneEmployee(int id, bool trackChanges);
    }
}
