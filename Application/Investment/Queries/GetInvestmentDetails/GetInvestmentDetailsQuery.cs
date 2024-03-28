using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Investment.Queries.GetInvestmentDetails
{
    public class GetInvestmentDetailsQuery : IRequest<InvestmentDetailsDto>
    {
        public int InvestmentId { get; set; }
    }

    public class GetInvestmentDetailsQueryHandler : IRequestHandler<GetInvestmentDetailsQuery, InvestmentDetailsDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetInvestmentDetailsQueryHandler(IApplicationDbContext applicationDbContext,
            ICurrentUserService currentUserService, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<InvestmentDetailsDto> Handle(GetInvestmentDetailsQuery request,
            CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Investments.AsNoTracking()
                .Where(x => x.Id == request.InvestmentId && x.UserId == _currentUserService.UserId)
                .ProjectTo<InvestmentDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}