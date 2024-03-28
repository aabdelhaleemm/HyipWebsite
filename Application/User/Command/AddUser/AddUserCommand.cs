using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User.Command.AddUser
{
    public class AddUserCommand : IRequest<Unit>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ReferenceUserName { get; set; }
        public string Email { get; set; }


    }

    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Unit>
    {
        private readonly UserManager<Domain.Entities.User> _userManager;


        public AddUserCommandHandler(UserManager<Domain.Entities.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var userExist = await _userManager.FindByEmailAsync(request.Email);
            if (userExist is not null)
                throw new CantAddEntityException("Email Already Exist!");
            int? referenceId = null;
            if (request.ReferenceUserName is not null)
            {
                var reference = await _userManager.FindByNameAsync(request.ReferenceUserName);
                if (reference is not null)
                    referenceId = reference.Id;
            }

            var result = await _userManager
                .CreateAsync(new Domain.Entities.User(request.Email, request.UserName, referenceId)
                    , request.Password);
            if (!result.Succeeded)
                throw new CantAddEntityException(result.Errors.ToList()[0].Description);

            return Unit.Value;
        }
    }
}