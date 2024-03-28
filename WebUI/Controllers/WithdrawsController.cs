using System.Threading;
using System.Threading.Tasks;
using Application.State.Query;
using Application.Withdraws.Command.AddWithdraw;
using Application.Withdraws.Command.ChangeWithdrawStatus;
using Application.Withdraws.Queries.GetWithdraws;
using Application.Withdraws.Queries.GetWithdrawsHistory;
using KhalafTrade.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KhalafTrade.Controllers
{
    [Authorize]
    public class WithdrawsController : BaseController
    {
        [HttpGet("history")]
        [RequestLimit("login", NoOfRequest = 4, Seconds = 30)]
        public async Task<IActionResult> GetHistory([FromQuery] GetWithdrawsHistoryQuery query,
            CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddWithdrawCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Ok();
        }

        [Authorize(Roles = "Admin,Admin2")]
        [HttpGet("{status}")]
        public async Task<IActionResult> Get([FromQuery] GetWithdrawsQuery query, string status,
            CancellationToken cancellationToken)
        {
            query.Status = status;
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [Authorize(Roles = "Admin,Admin2")]
        [HttpPut("WithdrawStatus")]
        public async Task<IActionResult> ChangeStatus(ChangeWithdrawStatusCommand command,
            CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpGet("state")]
        public async Task<IActionResult> IsWithdrawActive()
        {
            return Ok(await Mediator.Send(new GetStateQuery()));
        }
    }
}