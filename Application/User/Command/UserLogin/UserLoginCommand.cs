using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User.Command.UserLogin
{
    public class UserLoginCommand : IRequest<string>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, string>
    {
        private readonly UserManager<Domain.Entities.User> _userManager;
        private readonly IJwtService _jwtService;

        public UserLoginCommandHandler(UserManager<Domain.Entities.User> userManager, IJwtService jwtService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        }
        public async Task<string> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user is null)
                throw new UnAuthorizedRequest("Wrong username or password!");
            var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isValidPassword)
                throw new UnAuthorizedRequest("Wrong username or password!");
            return _jwtService.GenerateToken(user.Id, user.UserName, "User");
            
        }
    }
}