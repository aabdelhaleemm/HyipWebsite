using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Admin.Command.AddAdmin
{
    public class AddAdminCommand : IRequest<Unit>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
    public class AddAdminCommandHandler : IRequestHandler<AddAdminCommand,Unit>
    {
        private readonly UserManager<Domain.Entities.Admin> _userManager;

        public AddAdminCommandHandler(UserManager<Domain.Entities.Admin> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }
        public async Task<Unit> Handle(AddAdminCommand request, CancellationToken cancellationToken)
        {
            var adminExist = await _userManager.FindByEmailAsync(request.Email);
            if (adminExist is not null)
                throw new CantAddEntityException("Email Already Exist!");
            
            var result = await _userManager.CreateAsync(new Domain.Entities.Admin()
            {
                Email = request.Email,
                UserName = request.UserName
            }, request.Password);
            if (!result.Succeeded)
                throw new CantAddEntityException(result.Errors.ToList()[0].Description);
            return Unit.Value;
        }
    }
}