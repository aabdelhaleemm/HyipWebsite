using System.Threading;
using System.Threading.Tasks;
using Application.User.Command.AddUser;
using Application.User.Command.RequestResetPassword;
using Application.User.Command.ResetPassword;
using Application.User.Command.UpdatePassword;
using Application.User.Command.UpdateUser;
using Application.User.Command.UserLogin;
using Application.User.Command.ValidateResetToken;
using Application.User.Queries.GetAll;
using Application.User.Queries.GetReferences;
using Application.User.Queries.GetUserDetails;
using Application.Wallet.Queries.GetUserOverview;
using KhalafTrade.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KhalafTrade.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Add(AddUserCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [RequestLimit("login", NoOfRequest = 4, Seconds = 40)]
        public async Task<IActionResult> Login(UserLoginCommand command, CancellationToken cancellationToken)
        {
            var token = await Mediator.Send(command, cancellationToken);
            return Ok(new { token });
        }

        [HttpGet("overview")]
        [RequestLimit("overview", NoOfRequest = 5, Seconds = 10)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetUserOverviewQuery(), cancellationToken));
        }

        [Authorize(Roles = "Admin,Admin2")]
        [HttpGet("details")]
        public async Task<IActionResult> GetDetails([FromQuery] GetUserDetailsQuery query,
            CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [Authorize(Roles = "Admin,Admin2")]
        [HttpGet("usersList")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllUsersQuery query,
            CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpPut("updatePassword")]
        [RequestLimit("updatePassword", NoOfRequest = 2, Seconds = 30)]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordCommand command,
            CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }


        [Authorize(Roles = "Admin,Admin2")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("requestReset")]
        [RequestLimit("requestReset", NoOfRequest = 2, Seconds = 300)]
        public async Task<IActionResult> Reset(RequestResetPasswordCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("reset")]
        public async Task<IActionResult> ResetPass(ResetPasswordCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        [AllowAnonymous]
        [HttpGet("validate")]
        public async Task<IActionResult> ValidateToken([FromQuery] ValidateResetToken validateResetToken)
        {
            return Ok(await Mediator.Send(validateResetToken));
        }

        [HttpGet("references")]
        [RequestLimit("references", NoOfRequest = 5, Seconds = 30)]
        public async Task<IActionResult> GetReferences()
        {
            return Ok(await Mediator.Send(new GetReferencesQuery()));
        }
    }
}