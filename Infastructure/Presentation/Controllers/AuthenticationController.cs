using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Dtos.IdentityModules;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
