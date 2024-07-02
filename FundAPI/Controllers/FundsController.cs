using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FundsController : BaseAPIController
    {
        private readonly DataService _dataService;

        public FundsController(DataService dataService) 
        {
            _dataService = dataService;
        }
        [HttpGet]
        public IActionResult Get([FromQuery] string query)
        {
            var funds = _dataService.GetFunds(query);
            return Ok(funds);
        }
    }
}
