using FundAPI.DTO;

namespace FundAPI.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(UserLoginDto user);

    }
}
