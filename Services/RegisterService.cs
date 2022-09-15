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

        // This method will recieve a register request and talk to the database
        // It will create the user to the database
        // If user is successfully created, it will return a success message
        public Result RegisterUser(CreateUserDto createDto)
        {
            User user = _mapper.Map<User>(createDto);
            IdentityUser<int> userIdentity = _mapper.Map<IdentityUser<int>>(user);
            
            Task<IdentityResult> identityResult = _userManager.CreateAsync(userIdentity, createDto.Password);

            if (identityResult.Result.Succeeded)
            {
                _userManager.AddToRoleAsync(userIdentity, "REGULAR");
                return Result.Ok().WithSuccess("User Registered");
            }
            return Result.Fail("Fail to Register User");
        }
    }
}
