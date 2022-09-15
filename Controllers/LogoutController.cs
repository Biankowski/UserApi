using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Services;

namespace UsersApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private LogoutService _logoutService;
        public LogoutController(LogoutService logoutService)
        {
            _logoutService = logoutService;
        }

        // Logout route
        [HttpPost]
        public IActionResult LogOutUser()
        {
            Result result = _logoutService.LogOutUser();
            if(result.IsFailed)
            {
                return Unauthorized(result.Errors);
            }
            return Ok(result.Reasons);
        }
    }
}
