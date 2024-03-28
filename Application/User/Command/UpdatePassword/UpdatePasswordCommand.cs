using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User.Command.UpdatePassword
{
    public class UpdatePasswordCommand : IRequest<bool>
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, bool>
    {
        private readonly UserManager<Domain.Entities.User> _userManager;
        private readonly ICurrentUserService _currentUserService;

        public UpdatePasswordCommandHandler(UserManager<Domain.Entities.User> userManager,
            ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(_currentUserService.UserId.ToString());
            if (user is null)
                throw new NotFoundException("Something went wrong! please contact admin");
            if (!await _userManager.CheckPasswordAsync(user, request.CurrentPassword))
                throw new UnAuthorizedRequest("Current Password is wrong");
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            return result.Succeeded;
        }
    }
}