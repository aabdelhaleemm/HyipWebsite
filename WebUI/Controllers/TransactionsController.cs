using System.Threading;
using System.Threading.Tasks;
using Application.Transactions.Queries.GetUserTransactions;
using Application.Transactions.Queries.GetUserTransactionsForAdmin;
using KhalafTrade.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KhalafTrade.Controllers
{
    [Authorize]
    public class TransactionsController : BaseController
    {
        [HttpGet]
        [RequestLimit("Transactions", NoOfRequest = 4, Seconds = 30)]
        public async Task<IActionResult> Get([FromQuery] GetUserTransactionsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [Authorize(Roles = "Admin,Admin2")]
        [HttpGet("history")]
        public async Task<IActionResult> GetHistory([FromQuery] GetUserTransactionsAdminQuery query,
            CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }
    }
}