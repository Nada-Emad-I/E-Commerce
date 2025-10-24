using Shared.Dtos.IdentityModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        //Login
        //Take Email , Password
        //return Token , Email , Display Name
        Task<UserDto> LoginAsync(LoginDto loginDto);
        //Register
        //Take Email , Password , UserName , DisplayName , PhoneNumber
        //return Token , Email , Display Name
        Task<UserDto> RegisterDto(RegisterDto registerDto);
    }
}
