using System.Threading;
using System.Threading.Tasks;
using Application.Plans.Command;
using Application.Plans.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KhalafTrade.Controllers
{
    [Authorize(Roles = "Admin2")]
    public class PlansController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllPlans(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetPlansQuery(), cancellationToken));
        }

        [HttpPut]
        public async Task<IActionResult> ChangeProfitPercent(ChangeProfitPercentCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }
    }
}