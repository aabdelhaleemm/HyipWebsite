using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Investment.Queries.GetInvestmentDetails;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Investment.Queries.GetInvestmentDetailsAdmin
{
    public class GetInvestmentDetailsAdminQuery : IRequest<InvestmentDetailsDto>
    {
        public int InvestmentId { get; set; }
    }

    public class
        GetInvestmentDetailsAdminQueryHandler : IRequestHandler<GetInvestmentDetailsAdminQuery, InvestmentDetailsDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetInvestmentDetailsAdminQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<InvestmentDetailsDto> Handle(GetInvestmentDetailsAdminQuery request,
            CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Investments.AsNoTracking()
                .Where(x => x.Id == request.InvestmentId)
                .ProjectTo<InvestmentDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}