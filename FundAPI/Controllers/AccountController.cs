using FundAPI.DTO;
using FundAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FundAPI.Controllers
{
    public class AccountController(ITokenService tokenService): BaseAPIController
    {
        [HttpPost("login")]
        public  ActionResult<UserDto> Login(UserLoginDto loginDto)
        {
            var user = new UserLoginDto
            {
                Username = loginDto.Username,
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
