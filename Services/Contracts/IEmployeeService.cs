using Entities.DataModels;
using Entities.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Identity.Client;

namespace Services.Contracts
{
    public interface IEmployeeService
    {
        void CreateEmployee(Employee employee);
        IEnumerable<Employee> GetAllEmployees(bool trackChanges);
        Employee GetEmployeeById(int id, bool trackChanges);
        IEnumerable<Employee> GetEmployeesByCondition(int? id, string? fullName, string? lastName, string? job, decimal? salary, List<string> roles, string? registerDate, bool trackChanges);
        void UpdateOneEmployee(Employee employee, bool trackChanges);
        Employee PartiallyUpdateOneEmployee(int id, JsonPatchDocument<Employee> employeePatch, bool trackChanges);
        void DeleteOneEmployee(int id);
        void DeleteEmployees(List<Employee> entity);
    }
}
