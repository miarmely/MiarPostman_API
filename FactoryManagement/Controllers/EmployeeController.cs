using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Services.Contracts;
using FactoryManagement.Responses.Contracts;
using System.Linq.Expressions;
using Entities.Models;
using Repositories.Migrations;

namespace EmployeeManagement_Sql.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;

        private readonly IServiceManager _manager;

        private readonly IResponseBase _response;


        public EmployeeController(ILogger<EmployeeController> logger, IServiceManager manager, IResponseBase response)
        {
            _logger = logger;
            _manager = manager;
            _response = response;
        }


        [HttpPost]
        public IActionResult AddOneEmployee([FromBody] Employee employee)
        {
            try
            {
                _manager.EmployeeService.CreateEmployee(employee);
                _manager.EmployeeAndRoleService.CreateEmployeeAndRole(employee);

                _logger.LogInformation($"New Employee With id:{employee.Id} Created.");
                return Ok(employee);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            try
            {
                // get employees and fill roles
                var entity = _manager.EmployeeService.GetAllEmployees(false);
                _manager.EmployeeAndRoleService.FillRoles(ref entity);

                return Ok(entity);
            }

            catch (Exception ex)
            {
                // when dataase is empty
                if (ex.Message.Equals("Empty Database"))
                    _response.NotFound("Empty Database.");

                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        [HttpGet("{id:int}")]
        public IActionResult GetOneEmployeeById([FromRoute(Name = "id")] int id)
        {
            try
            {
                // get one employee and fill roles
                var entity = _manager.EmployeeService.GetEmployeeById(id, false);
                _manager.EmployeeAndRoleService.FillRole(ref entity);

                return Ok(entity);
            }

            catch (Exception ex)
            {
                // when id not found
                if (ex.Message.Equals("Not Matched"))
                    return _response.NotFound($"Id:{id} Not Found.");

                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        [HttpGet("condition")]
        public IActionResult GetEmployees([FromQuery(Name = "id")] int? id,
            [FromQuery(Name = "fullName")] string? fullName,
            [FromQuery(Name = "lastName")] string? lastName,
            [FromQuery(Name = "job")] string? job,
            [FromQuery(Name = "salary")] decimal? salary,
            [FromQuery(Name = "registerDate")] string? registerDate,
            [FromQuery(Name = "roles")] List<string> roles)
        {
            // can be connection error with database 
            try
            {
                // get employees and fill 
                var entity = _manager.EmployeeService.GetEmployeesByCondition(id, fullName, lastName, job, salary, roles, registerDate, false);
                _manager.EmployeeAndRoleService.FillRoles(ref entity);

                return Ok(entity);
            }

            catch (Exception ex)
            {
                switch (ex.Message)
                {
                    case "Empty Database":
                        return _response.NotFound("Empty Database.");
                    case "Not Matched":
                        return _response.NotFound("Not Matched With Any Employee.");
                }

                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        [HttpPut]
        public IActionResult UpdateOneEmployee([FromBody] Employee employee)
        {
            try
            {
                _manager.EmployeeService.UpdateOneEmployee(employee.Id, ref employee, false);

                // when roles changed
                if (employee.Roles.Count != 0)
                    _manager.EmployeeAndRoleService.UpdateRelations(employee);

                _logger.LogWarning($"Employee Who Id:{employee.Id} Updated.");
                return Ok(employee);
            }

            catch (Exception ex)
            {
                // when id not matched
                if (ex.Message.Equals("Not Matched"))
                    return _response.NotFound($"Id:{employee.Id} Not Found.");

                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneEmployee([FromRoute(Name = "id")] int id, 
            [FromBody] JsonPatchDocument<Employee> employeePatch)
        {
            try
            {
                var entity = _manager.EmployeeService.PartiallyUpdateOneEmployee(id, employeePatch, false);

                _logger.LogWarning($"Employee Who Id:{id} Partially Updated.");
                return Ok(entity);
            }

            catch (Exception ex)
            {
                // when id not found
                if (ex.Message.Equals("Not Matched"))
                    return _response.NotFound($"Id:{id} Not Found");

                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneEmployee([FromRoute(Name = "id")] int id)
        {
            try
            {
                _manager.EmployeeService.DeleteOneEmployee(id);

                _logger.LogWarning($"Employee Who Id:{id} Deleted!..");
                return NoContent();
            }

            catch (Exception ex)
            {
                // when id not matched
                if (ex.Message.Equals("Not Matched"))
                    return _response.NotFound($"Id:{id} Not Matched.");

                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        [HttpDelete]
        public IActionResult MultipleDelete([FromBody] Employee employee)
        {
            // add role names to list
            var roleNames = new List<string>();
            foreach (var role in employee.Roles)
                roleNames.Append(role.RoleName);

            try
            {
                // get matched employees
                var entity = _manager.EmployeeService.GetEmployeesByCondition(employee.Id, employee.FullName, employee.LastName,
                    employee.Job, employee.Salary, roleNames, employee.RegisterDate.ToString(), false);

                //_manager.EmployeeService.DeleteEmployees(entity);
                //_manager.EmployeeAndRoleService.DeleteEmpAndRolesByEmployeeId(employee.Id);

                _logger.LogWarning("Some Employees Deleted.");
                return NoContent();
            }

            catch (Exception ex)
            {
                switch (ex.Message)
                {
                    case "Empty Database": return _response.NotFound("Empty Database.");
                    case "Not Matched": return _response.NotFound("Not Matched.");
                }

                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}