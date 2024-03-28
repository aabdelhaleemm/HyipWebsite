using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.AuthCode.Command
{
    public class GenerateAuthCodeCommand : IRequest<Unit>
    {
        public string RecipientUserName { get; set; }
    }

    public class GenerateAuthCodeCommandHandler : IRequestHandler<GenerateAuthCodeCommand, Unit>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ISendGridEmailService _sendGridEmailService;
        private readonly ICurrentUserService _currentUserService;

        public GenerateAuthCodeCommandHandler(IApplicationDbContext applicationDbContext,
            ISendGridEmailService sendGridEmailService, ICurrentUserService currentUserService)
        {
            _applicationDbContext = applicationDbContext;
            _sendGridEmailService = sendGridEmailService;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(GenerateAuthCodeCommand request, CancellationToken cancellationToken)
        {
            var recipient = await _applicationDbContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserName == request.RecipientUserName, cancellationToken);
            if (recipient is null)
                throw new ArgumentException("Cannot find the recipient account!");
            var user = await _applicationDbContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId, cancellationToken);

            if (user is null)
                throw new ArgumentException("Cannot find user account!");
            var authCodeEntity = new Domain.Entities.AuthCode(_currentUserService.UserId);
            await _applicationDbContext.AuthCode.AddAsync(authCodeEntity, cancellationToken);
            if (await _applicationDbContext.SaveChangesAsync(cancellationToken) <= 0)
            {
                throw new CantAddEntityException("Something went wrong! Please try again");
            }

            await _sendGridEmailService.SendAuthCodeAsync(user.Email, user.UserName, authCodeEntity.Code);
            return Unit.Value;
        }
    }
}