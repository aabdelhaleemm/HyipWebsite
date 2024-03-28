using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Plans.Query
{
    public class GetPlansQuery : IRequest<List<PlansDto>>
    {
    }

    public class GetPlansQueryHandler : IRequestHandler<GetPlansQuery, List<PlansDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetPlansQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<List<PlansDto>> Handle(GetPlansQuery request, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.InvestmentPlans.AsNoTracking()
                .ProjectTo<PlansDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}