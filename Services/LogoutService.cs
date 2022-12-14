using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace UsersApi.Services
{
    public class LogoutService
    {
        private SignInManager<IdentityUser<int>> _signinManager;

        public LogoutService(SignInManager<IdentityUser<int>> signinManager)
        {
            _signinManager = signinManager;
        }

        // This method will just logout user
        public Result LogOutUser()
        {
            var identityResult = _signinManager.SignOutAsync();
            if(identityResult.IsCompletedSuccessfully)
            {
                return Result.Ok().WithSuccess("User logged out");
            }
            return Result.Fail("Failed to logout");
        }
    }
}
