using System.Threading;
using System.Threading.Tasks;
using Application.Wallet.Queries.GetBalance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KhalafTrade.Controllers
{
    [Authorize]
    public class WalletController : BaseController
    {
        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetBalanceQuery(), cancellationToken));
        }
    }
}