using System.ComponentModel.DataAnnotations;

namespace FundAPI.DTO
{
    public class UserDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
