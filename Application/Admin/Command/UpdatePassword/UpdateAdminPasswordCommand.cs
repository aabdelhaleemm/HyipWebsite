using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Admin.Command.UpdatePassword
{
    public class UpdateAdminPasswordCommand : IRequest<bool>
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class UpdateAdminPasswordCommandHandler : IRequestHandler<UpdateAdminPasswordCommand, bool>
    {
        private readonly UserManager<Domain.Entities.Admin> _adminManager;
        private readonly ICurrentUserService _currentUserService;

        public UpdateAdminPasswordCommandHandler(UserManager<Domain.Entities.Admin> adminManager,
            ICurrentUserService currentUserService)
        {
            _adminManager = adminManager;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(UpdateAdminPasswordCommand request, CancellationToken cancellationToken)
        {
            var admin = await _adminManager.FindByIdAsync(_currentUserService.UserId.ToString());
            if (admin is null)
                throw new NotFoundException("Something went wrong! please contact admin");
            if (!await _adminManager.CheckPasswordAsync(admin, request.CurrentPassword))
                throw new UnAuthorizedRequest("Current Password is wrong");
            var result = await _adminManager.ChangePasswordAsync(admin, request.CurrentPassword, request.NewPassword);
            return result.Succeeded;
        }
    }
}