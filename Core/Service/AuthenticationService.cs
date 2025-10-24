using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModules;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;
using Shared.Dtos.IdentityModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager) : IAuthenticationService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            //check if email is exists
            var user=await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) throw new UserNotFoundException(loginDto.Email);

            //Check Password
            var isPasswordValid=await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (isPasswordValid) {
                return new UserDto()
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token = "Token -- TODo"
                };
            }
            else
            {
                throw new UnAutherizedException();
            }
        }

        public async Task<UserDto> RegisterDto(RegisterDto registerDto)
        {
            //Mapping Rigester Dto=> ApplicationUser
            var user = new ApplicationUser()
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber
            };

            //create user
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                return new UserDto()
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token = CreateTokenAsync(user)
                }; 
            }
            else
                throw new BadRequestException(result.Errors.Select(E => E.Description).ToList());
        }

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.Name,user.UserName!),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };
            var roles =await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return "Token -- TODO";
        }
    }
}
