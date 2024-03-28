using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User.Command.RequestResetPassword
{
    public class RequestResetPasswordCommand : IRequest<Unit>
    {
        public string Email { get; set; }
    }

    public class RequestResetPasswordCommandHandler : IRequestHandler<RequestResetPasswordCommand, Unit>
    {
        private readonly UserManager<Domain.Entities.User> _userManager;
        private readonly ISendGridEmailService _sendGridEmailService;

        public RequestResetPasswordCommandHandler(UserManager<Domain.Entities.User> userManager,
            ISendGridEmailService sendGridEmailService)
        {
            _userManager = userManager;
            _sendGridEmailService = sendGridEmailService;
        }

        public async Task<Unit> Handle(RequestResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return Unit.Value;
            var result = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _sendGridEmailService.SendResetPasswordEmailAsync(user.Email, user.UserName,
                HttpUtility.UrlEncode(result));
            return Unit.Value;
        }
    }
}