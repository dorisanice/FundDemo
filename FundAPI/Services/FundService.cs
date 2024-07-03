using FundAPI.DTO;
using FundAPI.Interfaces;
using Newtonsoft.Json;

namespace FundAPI.Services
{
    public class FundService : IFundService
    {
        public IEnumerable<FundDto> GetFunds(string query)
        {
            List<FundDto> Funds = new List<FundDto>();
            var json = File.ReadAllText("data.json");
            Funds = JsonConvert.DeserializeObject<List<FundDto>>(json);
          
            return Funds.Where(slt => slt.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            slt.Ticker.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            slt.Exchange.Contains(query, StringComparison.OrdinalIgnoreCase));
        }
    }
}
