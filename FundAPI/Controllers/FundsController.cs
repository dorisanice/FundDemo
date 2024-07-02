using FundAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundAPI.Controllers
{
    [Authorize]
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class FundsController(IFundService _fundService) : BaseAPIController
    {
        [HttpGet]
        public IActionResult GetFunds([FromQuery] string query)
        {
            var funds = _fundService.GetFunds(query);
            return Ok(funds);
        }
    }
}
