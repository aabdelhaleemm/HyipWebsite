using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User.Command.UpdateUser
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly UserManager<Domain.Entities.User> _userManager;

        public UpdateUserCommandHandler(UserManager<Domain.Entities.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user is null)
                throw new NotFoundException("Cannot find the user! ");
            await _userManager.SetEmailAsync(user, request.Email);
            if (string.IsNullOrEmpty(request.Password)) 
                return Unit.Value;
            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, request.Password);

            return Unit.Value;
        }
    }
}