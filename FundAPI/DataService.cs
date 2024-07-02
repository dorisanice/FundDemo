using FundAPI.DTO;
using System.Net;
//using System.Text.Json.Serialization;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Diagnostics.SymbolStore;

namespace FundAPI
{
    public class DataService
    {
        public List<FundDto> Funds { get; set; }
        public DataService() 
        { 
            
        }
        private void LoadData()
        {
            var json = File.ReadAllText("data.json");
            Funds = JsonConvert.DeserializeObject<List<FundDto>>(json);

        }

        public IEnumerable<FundDto> GetFunds(string query)
        {
            return Funds.Where(slt => slt.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            slt.Ticker.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            slt.Exchange.Contains(query, StringComparison.OrdinalIgnoreCase));
        }
    }
}
