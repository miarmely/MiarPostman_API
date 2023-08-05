using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Services.Contracts;
using Entities.DataModels;
using Entities.ViewModels;


namespace EmployeeManagement_Sql.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;

        private readonly IServiceManager _manager;

       
        public EmployeeController(ILogger<EmployeeController> logger, IServiceManager manager)
        {
            _logger = logger;
            _manager = manager;
        }


        [HttpPost]
        public IActionResult AddOneEmployee([FromBody] EmployeeView employeeView)
        {
            try
            {
                // convert view to data models
                var employee = _manager
                    .DataConverterService
                    .ConvertToEmployee(employeeView);

                // create employee
                _manager
                    .EmployeeService
                    .CreateEmployee(employee);

                // set id
                employeeView.Id = employee.Id;

                // create employee and role
                _manager
                    .EmployeeAndRoleService
                    .CreateEmpAndRole(employeeView);

                _logger.LogInformation($"New Employee With id:{employee.Id} Created.");
                return Ok(employeeView);
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
                var employeeList = _manager
                    .EmployeeService
                    .GetAllEmployees(false)
                    .ToList();

                var employeeViewList =_manager
                    .ViewConverterService
                    .MultipleConvertToEmployeeView(employeeList);

                var entity = _manager
                    .EmployeeAndRoleService
                    .FillRoles(employeeViewList);

                return Ok(entity);
            }

            catch (Exception ex)
            {
                // when dataase is empty
                if (ex.Message.Equals("Empty Database"))
                    return NotFound("Empty Database.");

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
                var employee = _manager
                    .EmployeeService
                    .GetEmployeeById(id, false);

                var employeeView = _manager
                    .ViewConverterService
                    .ConvertToEmployeeView(employee);
                
                var entity = _manager
                    .EmployeeAndRoleService
                    .FillRole(employeeView);

                return Ok(entity);
            }

            catch (Exception ex)
            {
                // when id not found
                if (ex.Message.Equals("Not Matched"))
                    return NotFound($"Id:{id} Not Found.");

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
                var employeeList = _manager
                    .EmployeeService
                    .GetEmployeesByCondition(id, fullName, lastName, job, salary, roles, registerDate, false)
                    .ToList();

                var employeeViewList = _manager
                    .ViewConverterService
                    .MultipleConvertToEmployeeView(employeeList);
                    
                var entity = _manager
                    .EmployeeAndRoleService
                    .FillRoles(employeeViewList);

                return Ok(entity);
            }

            catch (Exception ex)
            {
                switch (ex.Message)
                {
                    case "Empty Database":
                        return NotFound("Empty Database.");
                    case "Not Matched":
                        return NotFound("Not Matched With Any Employee.");
                }

                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        [HttpPut]
        public IActionResult UpdateOneEmployee([FromBody] EmployeeView employeeView)
        {
            try
            {
                // convert view to data models
                var employee = _manager
                .DataConverterService
                .ConvertToEmployee(employeeView);

                // update employee
                _manager
                    .EmployeeService
                    .UpdateOneEmployee(employee, false);

                // add register date to view
                employeeView.RegisterDate = employee
                    .RegisterDate
                    .ToString();

                // when roles changed
                if (employeeView.Roles.Count != 0)
                    _manager
                        .EmployeeAndRoleService
                        .UpdateRelations(employeeView);

                _logger.LogWarning($"Employee Who Id:{employee.Id} Updated.");
                return Ok(employeeView);
            }

            catch (Exception ex)
            {
                // when id not matched
                if (ex.Message.Equals("Not Matched"))
                    return NotFound($"Id:{employeeView.Id} Not Found.");

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
                var entity = _manager
                    .EmployeeService
                    .PartiallyUpdateOneEmployee(id, employeePatch, false);

                _logger.LogWarning($"Employee Who Id:{id} Partially Updated.");
                return Ok(entity);
            }

            catch (Exception ex)
            {
                // when id not found
                if (ex.Message.Equals("Not Matched"))
                    return NotFound($"Id:{id} Not Found");

                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneEmployee([FromRoute(Name = "id")] int id)
        {
            try
            {
                // delete employee
                _manager
                    .EmployeeService
                    .DeleteOneEmployee(id);

                // delete empAndRoles
                _manager
                    .EmployeeAndRoleService
                    .DeleteEmpAndRolesByEmployeeId(id);

                _logger.LogWarning($"Employee Who Id:{id} Deleted!..");
                return NoContent();
            }

            catch (Exception ex)
            {
                // when id not matched
                if (ex.Message.Equals("Not Matched"))
                    return NotFound($"Id:{id} Not Matched.");

                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        [HttpDelete]
        public IActionResult MultipleDelete([FromBody] EmployeeView employeeView)
        {
            try
            {
                // get matched employees
                var entity = _manager
                    .EmployeeService
                    .GetEmployeesByCondition(employeeView.Id, employeeView.FullName, employeeView.LastName, employeeView.Job, employeeView.Salary, employeeView.Roles, employeeView.RegisterDate, false)
                    .ToList();

                // delete employee
                _manager
                    .EmployeeService
                    .DeleteEmployees(entity);

                // delete empAndRoles
                _manager
                    .EmployeeAndRoleService
                    .MultiDeleteEmpAndRoles(entity);

                _logger.LogWarning("Some Employees Deleted.");
                return NoContent();
            }

            catch (Exception ex)
            {
                switch (ex.Message)
                {
                    case "Empty Database": return NotFound("Empty Database.");
                    case "Not Matched": return NotFound("Not Matched.");
                }

                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}