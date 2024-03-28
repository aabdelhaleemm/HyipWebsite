using System.Threading;
using System.Threading.Tasks;
using Application.Investment.Command.AddInvestment;
using Application.Investment.Command.CancelInvestment;
using Application.Investment.Queries.GetInvestmentDetails;
using Application.Investment.Queries.GetInvestmentDetailsAdmin;
using Application.Investment.Queries.GetInvestments;
using Application.Investment.Queries.GetInvestmentsAdmin;
using KhalafTrade.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace KhalafTrade.Controllers
{
    [Authorize]
    public class InvestmentsController : BaseController
    {
        private readonly IMemoryCache _memoryCache;

        public InvestmentsController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetInvestmentQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpGet("details/{id}")]
        [RequestLimit("InvestmentsDetails", NoOfRequest = 8, Seconds = 30)]
        public async Task<IActionResult> GetDetails(int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetInvestmentDetailsQuery() { InvestmentId = id }, cancellationToken));
        }

        [HttpPost]
        [RequestLimit("addinvestment", NoOfRequest = 4, Seconds = 30)]
        public async Task<IActionResult> Add(AddInvestmentCommand command, CancellationToken cancellationToken)
        {
            
            await Mediator.Send(command, cancellationToken);
            _memoryCache.Remove("chart");
            return Ok();
        }

        [Authorize(Roles = "Admin,Admin2")]
        [HttpGet("{status}")]
        public async Task<IActionResult> Get([FromQuery] GetInvestmentsAdminQuery query, string status,
            CancellationToken cancellationToken)
        {
            query.Status = status;
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [Authorize(Roles = "Admin,Admin2")]
        [HttpGet("admin/details/{id}")]
        public async Task<IActionResult> GetDetailsForAdmin(int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetInvestmentDetailsAdminQuery { InvestmentId = id }, cancellationToken));
        }

        [Authorize(Roles = "Admin2")]
        [HttpPut("cancel")]
        public async Task<IActionResult> CancelInvestment(CancelInvestmentCommand command,
            CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Ok();
        }
    }
}