using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.Investment.Command.AddInvestment
{
    public class AddInvestmentCommand : IRequest<Unit>
    {
        public double Amount { get; set; }
        public string Plan { get; set; }
    }

    public class AddInvestmentCommandHandler : IRequestHandler<AddInvestmentCommand, Unit>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeService _dateTimeService;


        public AddInvestmentCommandHandler(IApplicationDbContext applicationDbContext,
            ICurrentUserService currentUserService, IDateTimeService dateTimeService)
        {
            _applicationDbContext =
                applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
        }

        public async Task<Unit> Handle(AddInvestmentCommand request, CancellationToken cancellationToken)
        {
            var user = await _applicationDbContext.Users
                .Include(x => x.Wallet)
                .Include(x => x.Reference)
                .ThenInclude(x => x.Wallet)
                .FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId, cancellationToken);

            Enum.TryParse(request.Plan, true, out InvestmentsPlan requestedInvestmentsPlan);
            user.AddInvestment(request.Amount, requestedInvestmentsPlan, _dateTimeService.Now);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken) > 0
                ? Unit.Value
                : throw new CantAddEntityException("Cannot create new Investment Please try again or contact admin");
        }
    }
}