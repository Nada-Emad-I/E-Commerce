using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Dtos.IdentityModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager):ApiBaseController
    {
        //Login
        [HttpPost("Login")]//POST : baseUrl/api/Authentication/login
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user=await _serviceManager.authenticationService.LoginAsync(loginDto);
            return Ok(user);
        }
        //Register
        [HttpPost("Register")]//POST : baseUrl/api/Authentication/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _serviceManager.authenticationService.RegisterDto(registerDto);
            return Ok(user);
        }
        //CheckEmail
        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var result=await _serviceManager.authenticationService.CheckEmailAsync(email);
            return Ok(result);
        }
        //Get CurrentUser
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var appUser =await _serviceManager.authenticationService.GetCurrentUserAsync(email!);
            return Ok(appUser);
        }
        //Get CurrentUserAddress
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address =await _serviceManager.authenticationService.GetCurrentUserAddressAsync(email!);
            return Ok(address);
        }
        //Update CurrentUserAddress
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto address)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var UpdatedAddress =await _serviceManager.authenticationService.UpdateCurrentUserAddressAsync(email!, address);
            return Ok(UpdatedAddress);
        }
    }
    
}
