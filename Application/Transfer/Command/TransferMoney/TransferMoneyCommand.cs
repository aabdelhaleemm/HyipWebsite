using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Transfer.Command.TransferMoney
{
    public class TransferMoneyCommand : IRequest<bool>
    {
        public int Code { get; set; }
        public string RecipientUserName { get; set; }
        public double Amount { get; set; }
    }

    public class TransferMoneyCommandHandler : IRequestHandler<TransferMoneyCommand, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeService _dateTimeService;

        public TransferMoneyCommandHandler(IApplicationDbContext applicationDbContext,
            ICurrentUserService currentUserService, IDateTimeService dateTimeService)
        {
            _applicationDbContext = applicationDbContext;
            _currentUserService = currentUserService;
            _dateTimeService = dateTimeService;
        }

        public async Task<bool> Handle(TransferMoneyCommand request, CancellationToken cancellationToken)
        {
            var authCode = await ValidateCode(request.Code, cancellationToken);
            var sender = await _applicationDbContext.Users
                .Include(x => x.Wallet)
                .FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId, cancellationToken);
            var recipient = await _applicationDbContext.Users
                .Include(x => x.Wallet)
                .FirstOrDefaultAsync(x => x.UserName == request.RecipientUserName.ToLower(), cancellationToken);
            sender.TransferMoney(recipient, request.Amount);
            await DeleteExpiredAuthCodes(cancellationToken);
            _applicationDbContext.AuthCode.Remove(authCode);
            if (await _applicationDbContext.SaveChangesAsync(cancellationToken) > 0)
                return true;
            throw new CantAddEntityException("Something went wrong! Please try again");
        }

        private async Task DeleteExpiredAuthCodes(CancellationToken cancellationToken)
        {
            var auth = await _applicationDbContext.AuthCode
                .Where(x => x.ExpireAt.CompareTo(_dateTimeService.Now) < 1)
                .ToListAsync(cancellationToken);
            _applicationDbContext.AuthCode.RemoveRange(auth);
        }

        private async Task<Domain.Entities.AuthCode> ValidateCode(int code, CancellationToken cancellationToken)
        {
            var authCodeEntity = await _applicationDbContext.AuthCode.AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == _currentUserService.UserId && x.Code == code,
                    cancellationToken);
            if (authCodeEntity is null)
                throw new ArgumentException("Not valid code!");
            if (authCodeEntity.ExpireAt.CompareTo(_dateTimeService.Now) < 1)
                throw new ArgumentException("Code is expired");
            return authCodeEntity;
        }
    }
}