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

        //Check Email
        //Take Email
        //Return Boolean
        Task<bool> CheckEmailAsync(string email);
        //Get Current User
        //Take Email
        //Return UserDto(Token , Email , Display Name)
        Task<UserDto> GetCurrentUserAsync(string email);
        //Get Current User Address
        //Take Email
        //Return AddressDto
        Task<AddressDto> GetCurrentUserAddressAsync(string email);
        //Update Current User Address
        //Take Email,AddressDto
        //Return AddressDto
        Task<AddressDto> UpdateCurrentUserAddressAsync(string email,AddressDto addressDto);
    }
}
