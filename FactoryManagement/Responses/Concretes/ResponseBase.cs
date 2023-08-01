using Microsoft.AspNetCore.Mvc;
using FactoryManagement.Responses.Contracts;


namespace FactoryManagement.Responses.Concretes
{
    public class ResponseBase : ControllerBase, IResponseBase
    {
        public IActionResult NotFound(string message)
        {
            return NotFound(new
            {
                statusCode = 404,
                message = message
            }); ;
        }

        public IActionResult Conflict(string message)
        {
            return BadRequest(new
            {
                statusCode = 400,
                message = message
            });
        }
    }
}
