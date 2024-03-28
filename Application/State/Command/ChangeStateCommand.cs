using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.State.Command
{
    public class ChangeStateCommand : IRequest<Unit>
    {
        public bool IsWithdrawActive { get; set; }
        public bool IsDepositActive { get; set; }
    }

    public class ChangeStateCommandHandler : IRequestHandler<ChangeStateCommand, Unit>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ChangeStateCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Unit> Handle(ChangeStateCommand request, CancellationToken cancellationToken)
        {
            var state = await _applicationDbContext.State.FirstOrDefaultAsync();
            if (state is null)
            {
                throw new ArgumentException("Please contact developer");
            }

            state.IsDepositActive = request.IsDepositActive;
            state.IsWithdrawActive = request.IsWithdrawActive;
            return await _applicationDbContext.SaveChangesAsync(cancellationToken) > 0
                ? Unit.Value
                : throw new CantAddEntityException("Something went wrong please try again");
        }
    }
}