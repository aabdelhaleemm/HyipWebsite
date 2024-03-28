using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Withdraws.Command.AddWithdraw
{
    public class AddWithdrawCommand : IRequest<Unit>
    {
        public double Amount { get; set; }
        public string WithdrawAccount { get; set; }
        public string WithdrawMethod { get; set; }
    }

    public class AddWithdrawCommandHandler : IRequestHandler<AddWithdrawCommand, Unit>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _currentUserService;

        public AddWithdrawCommandHandler(IApplicationDbContext applicationDbContext,
            ICurrentUserService currentUserService)
        {
            _applicationDbContext = applicationDbContext;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(AddWithdrawCommand request, CancellationToken cancellationToken)
        {
            var user = await _applicationDbContext.Users
                .Include(x => x.Wallet)
                .FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId, cancellationToken);
            user.AddWithdrawRequest(request.Amount, request.WithdrawMethod, request.WithdrawAccount);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken) > 0
                ? Unit.Value
                : throw new CantAddEntityException("Something went wrong please try again!");
        }
    }
}