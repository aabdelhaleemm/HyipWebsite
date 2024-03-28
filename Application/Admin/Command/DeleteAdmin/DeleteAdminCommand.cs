using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Admin.Command.DeleteAdmin
{
    public class DeleteAdminCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteAdminCommandHandler : IRequestHandler<DeleteAdminCommand, bool>
    {
        private readonly UserManager<Domain.Entities.Admin> _userManager;

        public DeleteAdminCommandHandler(UserManager<Domain.Entities.Admin> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == 1)
                throw new CantAddEntityException("Cannot delete the main admin");
            var admin = await _userManager.FindByIdAsync(request.Id.ToString());
            if (admin is null)
                throw new NotFoundException($"Cannot find admin with {request.Id} Id");
            var result = await _userManager.DeleteAsync(admin);
            return result.Succeeded;
        }
    }
}