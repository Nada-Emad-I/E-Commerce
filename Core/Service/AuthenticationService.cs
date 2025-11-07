using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModules;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.Dtos.IdentityModules;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager
        ,IConfiguration _configuration,IMapper _mapper) : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string email)
        {
            var user =await _userManager.FindByEmailAsync(email);
            return user is not null;
        }
        public async Task<UserDto> GetCurrentUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user is null)
            {
                throw new UserNotFoundException(email);
            }
            return new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await CreateTokenAsync(user)
            };
        }

        public async Task<AddressDto> GetCurrentUserAddressAsync(string email)
        {
            var user = await _userManager.Users.Include(U => U.address)
                                               .FirstOrDefaultAsync(U => U.Email == email);
            if (user is null) throw new UserNotFoundException(email);
            if (user.address is null) throw new AddressNotFoundException(user.UserName!);
            return _mapper.Map<Address, AddressDto>(user.address);
        }

        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto)
        {
            var user = await _userManager.Users.Include(U => U.address)
                                               .FirstOrDefaultAsync(U => U.Email == email);
            if (user is null) throw new UserNotFoundException(email);
            if (user.address is not null)//Update
            {
                user.address.FirstName = addressDto.FirstName;
                user.address.LastName = addressDto.LastName;
                user.address.City = addressDto.City;
                user.address.Country = addressDto.Country;
                user.address.Street = addressDto.Street;
            }
            else//Add new Address
                user.address = _mapper.Map<AddressDto, Address>(addressDto);
            await _userManager.UpdateAsync(user);
            return _mapper.Map<Address, AddressDto>(user.address);
        }

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
                    Token =await CreateTokenAsync(user)
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
                    Token =await CreateTokenAsync(user)
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

            var SecretKet = _configuration.GetSection("JWTOptions")["SecretKey"];
            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKet));
            var Creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["JWTOptions:Issuer"],
                audience: _configuration["JWTOption:,Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
