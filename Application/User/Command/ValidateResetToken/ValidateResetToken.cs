using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User.Command.ValidateResetToken
{
    public class ValidateResetToken : IRequest<bool>
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }

    public class ValidateResetTokenHandler : IRequestHandler<ValidateResetToken, bool>
    {
        private readonly UserManager<Domain.Entities.User> _userManager;

        public ValidateResetTokenHandler(UserManager<Domain.Entities.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(ValidateResetToken request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return false;
            var purpose = UserManager<Domain.Entities.User>.ResetPasswordTokenPurpose;
            var validate = await
                _userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultProvider, purpose, request.Token);
            return validate;
        }
    }
}