using FundAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom.Compiler;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace FundAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseAPIController
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto login)
        {
            if (login.Username == "user" && login.Password == "password")
            {
                var token = GenerateJWTToken();
                return Ok(new { token });
            }

            return Unauthorized();
        }

        private string GenerateJWTToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "user")
            };

            var token = new JwtSecurityToken(_configuration["Jwt:issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
                
                
        }
    }
}