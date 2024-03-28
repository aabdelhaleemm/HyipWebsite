using System.Threading;
using System.Threading.Tasks;
using Application.Deposits.Command.AddDeposit;
using Application.Deposits.Command.ChangeDepositStatus;
using Application.Deposits.Queries.GetDeposits;
using Application.Deposits.Queries.GetDepositsHistory;
using KhalafTrade.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace KhalafTrade.Controllers
{
    [Authorize]
    public class DepositsController : BaseController
    {
        private readonly IMemoryCache _memoryCache;

        public DepositsController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        [HttpGet("history")]
        [RequestLimit("deposithistory", NoOfRequest = 4, Seconds = 30)]
        public async Task<IActionResult> GetHistory([FromQuery]GetDepositsHistoryQuery query,CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpPost]
        [RequestLimit("adddeposit", NoOfRequest = 5, Seconds = 30)]
        public async Task<IActionResult> Add([FromForm] AddDepositCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            _memoryCache.Remove("chart");
            return Ok();
        }

        [Authorize(Roles = "Admin,Admin2")]
        [HttpGet("{status}")]
        public async Task<IActionResult> Get([FromQuery] GetDepositsQuery query, string status,
            CancellationToken cancellationToken)
        {
            query.Status = status;
            return Ok(await Mediator.Send(query, cancellationToken));
        }


        [Authorize(Roles = "Admin,Admin2")]
        [HttpPut("DepositStatus")]
        public async Task<IActionResult> ChangeStatus(ChangeDepositStatusCommand command,
            CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Ok();
        }
    }
}