using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Withdraws.Command.ChangeWithdrawStatus
{
    public class ChangeWithdrawStatusCommand : IRequest<Unit>
    {
        public int WithdrawId { get; set; }
        public string Status { get; set; }
        public string FeedBack { get; set; }
    }

    public class ChangeWithdrawStatusCommandHandler : IRequestHandler<ChangeWithdrawStatusCommand, Unit>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ChangeWithdrawStatusCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Unit> Handle(ChangeWithdrawStatusCommand request, CancellationToken cancellationToken)
        {
            var withdraw = await _applicationDbContext.Withdraws
                .Include(x => x.User)
                .ThenInclude(x => x.Wallet)
                .Include(x => x.Transaction)
                .FirstOrDefaultAsync(x => x.Id == request.WithdrawId, cancellationToken);

            Enum.TryParse(request.Status, true, out Status status);
            if (status == 0)
                throw new CannotParseEnum("Not Valid Status");

            withdraw.ChangeStatus(status, request.FeedBack);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken) > 0
                ? Unit.Value
                : throw new CannotParseEnum(
                    $"Cannot change Withdraw request with Id: {request.WithdrawId} status something went wrong");
        }
    }
}