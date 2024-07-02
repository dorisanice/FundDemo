using FundAPI.DTO;
using FundAPI.Entities;
using FundAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace FundAPI.Controllers
{
    public class AccountController(ITokenService tokenService): BaseAPIController
    {
        [HttpPost("register")] //account/register
        public ActionResult<UserDto> Register(UserLoginDto userLoginDto)
        {
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = userLoginDto.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userLoginDto.Password)),
                PasswordSalt = hmac.Key,
            };


            return new UserDto
            {
                Username = userLoginDto.Username,
                Token = tokenService.CreateToken(user)
            }; 

        }

        [HttpPost("login")]
        public  ActionResult<UserDto> Login(UserLoginDto loginDto)
        {
            var user = new AppUser
            {
                UserName = loginDto.Username,
            };

            if (loginDto.Username == "user" && loginDto.Password == "password")
            {

            }
            else return Unauthorized("Invalid username");
            return new UserDto
            {
                Username = loginDto.Username,
                Token = tokenService.CreateToken(user)
            };

        }
    }
}
