using FundAPI.DTO;

namespace FundAPI.Interfaces
{
    public interface IFundService
    {
        IEnumerable<FundDto> GetFunds(string query);
    }
}
