using Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;


namespace FactoryManagement.Controllers
{
    [ApiController]
    [Route("api/role")]
    public class RoleController : ControllerBase
    {
        private readonly IServiceManager _manager;

        private readonly ILogger<Role> _logger;


        public RoleController(IServiceManager manager, ILogger<Role> logger)
        {
            _manager = manager;
            _logger = logger;
        }
            

        [HttpPost]
        public IActionResult CreateRole([FromBody] Role role)
        {
            try
            {
                _manager.RoleSevice.CreateRole(role);
                
                _logger.LogInformation($"Role That Id:{role.Id} Created.");
                return Ok(role);
            }

            catch (Exception ex)
            {
                // when role is already exists
                if (ex.Message.Equals("Conflict"))
                    return BadRequest($"Role Name That:{role.RoleName} Is Already Exists.");

                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        [HttpGet]
        public IActionResult GetAllRoles()
        {
            try
            {
                var entity = _manager.RoleSevice.GetAllRoles(false);

                _logger.LogInformation("All Roles Displayed.");
                return Ok(entity);
            }

            catch (Exception ex)
            {
                // when database is empty
                if (ex.Message.Equals("Empty Database"))
                    return NotFound("Empty Database.");

                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
