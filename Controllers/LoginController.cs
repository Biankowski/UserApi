using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Data.Requests;
using UsersApi.Services;

namespace UsersApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult LogUser(LoginRequest request)
        {
            Result result = _loginService.LogUser(request);
            if(result.IsFailed)
            {
                return Unauthorized(result.Errors);
            }
            return Ok(result.Reasons);
        }
    }
}
