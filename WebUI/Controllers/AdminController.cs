using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Admin.Command.AddAdmin;
using Application.Admin.Command.DeleteAdmin;
using Application.Admin.Command.LoginAdmin;
using Application.Admin.Command.UpdateAdmin;
using Application.Admin.Command.UpdatePassword;
using Application.Admin.Queries.GetAllAdmins;
using Application.Chart;
using Application.State.Command;
using Application.State.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace KhalafTrade.Controllers
{
    [Authorize(Roles = "Admin,Admin2")]
    public class AdminController : BaseController
    {
        private readonly IMemoryCache _memoryCache;

        public AdminController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        [AllowAnonymous]
        //[Authorize(Roles = "Admin2")]
        [HttpPost]
        public async Task<IActionResult> Add(AddAdminCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(AdminLoginCommand command, CancellationToken cancellationToken)
        {
            return Ok(new { token = await Mediator.Send(command, cancellationToken) });
        }

        [HttpGet("chart")]
        public async Task<IActionResult> GetChart(CancellationToken cancellationToken)
        {
            if (!_memoryCache.TryGetValue("chart", out Chart chart))
            {
                chart = await Mediator.Send(new GetChartQuery(), cancellationToken);
                _memoryCache.Set("chart", chart, TimeSpan.FromDays(1));
            }

            return Ok(chart);
        }

        [Authorize(Roles = "Admin2")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAllAdminsQuery(), cancellationToken));
        }

        [Authorize(Roles = "Admin2")]
        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateAdminCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        [Authorize(Roles = "Admin2")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new DeleteAdminCommand() { Id = id }, cancellationToken));
        }

        [HttpPut("updatePassword")]
        public async Task<IActionResult> UpdatePassword(UpdateAdminPasswordCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [Authorize(Roles = "Admin2")]
        [HttpGet("state")]
        public async Task<IActionResult> GetState()
        {
            return Ok(await Mediator.Send(new GetStateQuery()));
        }
        [Authorize(Roles = "Admin2")]
        [HttpPut("state")]
        public async Task<IActionResult> ChangeState(ChangeStateCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }
    }
}