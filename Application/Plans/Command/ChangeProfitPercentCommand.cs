using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Plans.Command
{
    public class ChangeProfitPercentCommand : IRequest<Unit>
    {
        public int PlanId { get; set; }
        public double NewProfitPercent { get; set; }
    }

    public class ChangeProfitPercentCommandHandler : IRequestHandler<ChangeProfitPercentCommand, Unit>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _currentUserService;

        public ChangeProfitPercentCommandHandler(IApplicationDbContext applicationDbContext,
            ICurrentUserService currentUserService)
        {
            _applicationDbContext = applicationDbContext;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(ChangeProfitPercentCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.UserId != 1)
                throw new UnAuthorizedRequest("You cannot update the profit");
            var plan = await _applicationDbContext.InvestmentPlans.FindAsync(request.PlanId);
            plan.ChangeCurrentProfitPercent(request.NewProfitPercent);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken) > 0
                ? Unit.Value
                : throw new CantAddEntityException("Something went wrong please try again!");
        }
    }
}