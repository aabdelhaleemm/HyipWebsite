using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Investment.Command.CancelInvestment
{
    public class CancelInvestmentCommand : IRequest<Unit>
    {
        public int InvestmentId { get; set; }
        public string CancelReason { get; set; }
    }

    public class CancelInvestmentCommandHandler : IRequestHandler<CancelInvestmentCommand, Unit>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public CancelInvestmentCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Unit> Handle(CancelInvestmentCommand request, CancellationToken cancellationToken)
        {
            var investment = await _applicationDbContext.Investments
                .Include(x => x.User)
                .ThenInclude(x => x.Wallet)
                .FirstOrDefaultAsync(x => x.Id == request.InvestmentId, cancellationToken);
            investment.CancelOrFinishInvestment(InvestmentsStatus.Canceled, request.CancelReason);


            return await _applicationDbContext.SaveChangesAsync(cancellationToken) > 0
                ? Unit.Value
                : throw new CantAddEntityException("Something went wrong! please try again");
        }
    }
}