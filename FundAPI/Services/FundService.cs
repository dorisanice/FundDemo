using FundAPI.DTO;
using FundAPI.Interfaces;
using Newtonsoft.Json;

namespace FundAPI.Services
{
    public class FundService : IFundService
    {
        public List<FundDto> Funds { get; set; }
     
        public IEnumerable<FundDto> GetFunds(string query)
        {
            var json = File.ReadAllText("data.json");
            Funds = JsonConvert.DeserializeObject<List<FundDto>>(json);
          
            return Funds.Where(slt => slt.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            slt.Ticker.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            slt.Exchange.Contains(query, StringComparison.OrdinalIgnoreCase));
        }
    }
}
