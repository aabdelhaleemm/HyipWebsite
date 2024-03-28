using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Admin.Command.LoginAdmin
{
    public class AdminLoginCommand : IRequest<string>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class AdminLoginCommandHandler : IRequestHandler<AdminLoginCommand, string>
    {
        private readonly UserManager<Domain.Entities.Admin> _userManager;
        private readonly IJwtService _jwtService;

        public AdminLoginCommandHandler(UserManager<Domain.Entities.Admin> userManager, IJwtService jwtService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        }

        public async Task<string> Handle(AdminLoginCommand request, CancellationToken cancellationToken)
        {
            var admin = await _userManager.FindByNameAsync(request.UserName);
            if (admin is null)
                throw new UnAuthorizedRequest("Email Or password is wrong");
            var isValidPassword = await _userManager.CheckPasswordAsync(admin, request.Password);
            if (!isValidPassword)
                throw new UnAuthorizedRequest("Email Or password is wrong");
            return _jwtService.GenerateToken(admin.Id, admin.UserName, admin.Id == 1 ? "Admin2" : "Admin");
        }
    }
}