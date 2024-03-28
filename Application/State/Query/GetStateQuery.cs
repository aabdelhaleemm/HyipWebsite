using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.State.Query
{
    public class GetStateQuery : IRequest<Domain.Entities.State>
    {
    }

    public class GetStateQueryHandler : IRequestHandler<GetStateQuery, Domain.Entities.State>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetStateQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Domain.Entities.State> Handle(GetStateQuery request, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.State.AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}