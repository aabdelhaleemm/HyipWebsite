using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Admin.Command.UpdateAdmin
{
    public class UpdateAdminCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UpdateAdminCommandHandler : IRequestHandler<UpdateAdminCommand, Unit>
    {
        private readonly UserManager<Domain.Entities.Admin> _adminManager;

        public UpdateAdminCommandHandler(UserManager<Domain.Entities.Admin> adminManager)
        {
            _adminManager = adminManager;
        }

        public async Task<Unit> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = await _adminManager.FindByIdAsync(request.Id.ToString());
            if (admin is null)
                throw new NotFoundException("Cannot find the admin account! ");
            await _adminManager.SetEmailAsync(admin, request.Email);
            if (string.IsNullOrEmpty(request.Password))
                return Unit.Value;
            await _adminManager.RemovePasswordAsync(admin);
            await _adminManager.AddPasswordAsync(admin, request.Password);

            return Unit.Value;
        }
    }
}