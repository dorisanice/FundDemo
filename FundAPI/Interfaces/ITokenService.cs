using FundAPI.Entities;

namespace FundAPI.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);

    }
}
