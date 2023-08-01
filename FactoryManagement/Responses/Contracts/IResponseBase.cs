using Microsoft.AspNetCore.Mvc;


namespace FactoryManagement.Responses.Contracts
{
    public interface IResponseBase
    {
        public IActionResult NotFound(string message);
        public IActionResult Conflict(string message);
    }
}
