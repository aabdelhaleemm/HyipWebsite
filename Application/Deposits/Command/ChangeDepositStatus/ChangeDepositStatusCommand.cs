using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Deposits.Command.ChangeDepositStatus
{
    public class ChangeDepositStatusCommand : IRequest<Unit>
    {
        public int DepositId { get; set; }
        public string Status { get; set; }
        public string FeedBack { get; set; }
    }

    /// <summary>
    ///  first find the deposit request then parse the Status to enum
    ///  Get the user wallet
    /// 
    ///  if the new status is accepted we add the amount to user wallet
    ///  if the deposit request status is accepted and the new status is pending or rejected
    ///     we subtract the amount from the user wallet
    ///
    ///  check if this is the first deposit request for the user and its accepted and the user
    ///  have reference we add the reference user the profit
    /// </summary>
    public class ChangeDepositStatusCommandHandler : IRequestHandler<ChangeDepositStatusCommand, Unit>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ChangeDepositStatusCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Unit> Handle(ChangeDepositStatusCommand request, CancellationToken cancellationToken)
        {
            var deposit = await _applicationDbContext.Deposits
                .Include(x => x.User)
                .ThenInclude(x => x.Wallet)
                .Include(x => x.Transaction)
                .FirstOrDefaultAsync(x => x.Id == request.DepositId, cancellationToken);

            Enum.TryParse(request.Status, true, out Status status);
            if (status == 0)
                throw new CannotParseEnum($"Cannot set deposit request to {request.Status} Status please try again");
            deposit.ChangeStatus(status, request.FeedBack);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken) > 0
                ? Unit.Value
                : throw new CantAddEntityException("Something went wrong please try again later!");
        }
    }
}