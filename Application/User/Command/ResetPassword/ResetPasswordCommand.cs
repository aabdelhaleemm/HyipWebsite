using System.Threading;
using System.Threading.Tasks;
using System.Web;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User.Command.ResetPassword
{
    public class ResetPasswordCommand : IRequest<bool>
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly UserManager<Domain.Entities.User> _userManager;

        public ResetPasswordCommandHandler(UserManager<Domain.Entities.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            var token = HttpUtility.UrlDecode(request.Token);
            var result = await _userManager.ResetPasswordAsync(user, token, request.Password);
            return result.Succeeded;
        }
    }
}