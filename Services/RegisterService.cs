using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsersApi.Data.Dtos;
using UsersApi.Models;

namespace UsersApi.Services
{
    public class RegisterService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;
        private RoleManager<IdentityRole<int>> _roleManager;


        public RegisterService(IMapper mapper, UserManager<IdentityUser<int>> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public  Result RegisterUser(CreateUserDto createDto)
        {
            User user = _mapper.Map<User>(createDto);
            IdentityUser<int> userIdentity = _mapper.Map<IdentityUser<int>>(user);
            
            Task<IdentityResult> identityResult = _userManager.CreateAsync(userIdentity, createDto.Password);
            
            _userManager.AddToRoleAsync(userIdentity, "regular");

            if (identityResult.Result.Succeeded)
            {
                return Result.Ok();
            }
            return Result.Fail("Fail to Register User");
        }
    }
}
