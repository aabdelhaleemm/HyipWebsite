using System.Threading.Tasks;
using Application.PaymentMethods.Query;
using KhalafTrade.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KhalafTrade.Controllers
{
    [Authorize]
    public class DepositController : BaseController
    {
        [HttpGet]
        [RequestLimit("depositmethods", NoOfRequest = 10, Seconds = 30)]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetDepositMethodsQuery()));
        }
    }
}