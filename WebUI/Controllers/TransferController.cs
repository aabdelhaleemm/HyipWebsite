using System.Threading;
using System.Threading.Tasks;
using Application.AuthCode.Command;
using Application.Transfer.Command.TransferMoney;
using Application.Transfer.Query.GetReceivedTransfersHistory;
using Application.Transfer.Query.GetSentTransferHistory;
using KhalafTrade.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KhalafTrade.Controllers
{
    [Authorize]
    public class TransferController : BaseController
    {
        [HttpPost("request")]
        [RequestLimit("requestCode", NoOfRequest = 4, Seconds = 300)]
        public async Task<IActionResult> RequestCode(GenerateAuthCodeCommand command,CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Transfer(TransferMoneyCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [HttpGet("received")]
        [RequestLimit("receivedtransfer", NoOfRequest = 5, Seconds = 30)]
        public async Task<IActionResult> GetReceived([FromQuery] GetReceivedTransfersQuery query,
            CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpGet("sent")]
        [RequestLimit("senttransfer", NoOfRequest = 5, Seconds = 30)]
        public async Task<IActionResult> GetSent([FromQuery] GetSentTransfersQuery query,
            CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }
    }
}