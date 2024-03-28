using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PaymentMethods.Query
{
    public class GetDepositMethodsQuery : IRequest<List<DepositMethodsDto>>
    {
    }

    public class GetDepositMethodsQueryHandler : IRequestHandler<GetDepositMethodsQuery, List<DepositMethodsDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetDepositMethodsQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<List<DepositMethodsDto>> Handle(GetDepositMethodsQuery request,
            CancellationToken cancellationToken)
        {
            var state = await _applicationDbContext.State.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (!state.IsDepositActive)
            {
                return null;
            }

            return await _applicationDbContext.PaymentMethods.AsNoTracking()
                .ProjectTo<DepositMethodsDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}