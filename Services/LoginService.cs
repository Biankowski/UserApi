using FluentResults;
using Microsoft.AspNetCore.Identity;
using NuGet.Common;
using UsersApi.Data.Requests;

namespace UsersApi.Services
{
    public class LoginService
    {
        private SignInManager<IdentityUser<int>> _signInManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public Result LogUser(LoginRequest request)
        {
            var identityResult = _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);

            if(identityResult.Result.Succeeded)
            {
                var identityUser = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == request.Username.ToUpper());
                var tokenIdentifier = _signInManager.UserManager.GetRolesAsync(identityUser).Result;
                Token token = _tokenService.CreateToken(identityUser, tokenIdentifier.FirstOrDefault("regular"));

                return Result.Ok().WithSuccess(token.Value);
            }
            return Result.Fail("Failed to login");
        }
    }
}
