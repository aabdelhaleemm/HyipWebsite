using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Wallet.Queries.GetBalance
{
    public class GetBalanceQuery : IRequest<double>
    {
    }

    public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, double>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _currentUserService;

        public GetBalanceQueryHandler(IApplicationDbContext applicationDbContext,
            ICurrentUserService currentUserService)
        {
            _applicationDbContext = applicationDbContext;
            _currentUserService = currentUserService;
        }

        public async Task<double> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Wallets.AsNoTracking()
                .Where(x => x.UserId == _currentUserService.UserId)
                .Select(x => x.Balance)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}