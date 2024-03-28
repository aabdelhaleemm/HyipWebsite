using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Mapping;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Investment.Queries.GetInvestments
{
    public class GetInvestmentQuery : IRequest<PaginatedList<InvestmentDto>>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }

    public class GetInvestmentQueryHandler : IRequestHandler<GetInvestmentQuery, PaginatedList<InvestmentDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetInvestmentQueryHandler(IApplicationDbContext applicationDbContext,
            ICurrentUserService currentUserService, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<PaginatedList<InvestmentDto>> Handle(GetInvestmentQuery request,
            CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Investments.AsNoTracking()
                .Where(x => x.UserId == _currentUserService.UserId)
                .OrderByDescending(x => x.Id)
                .ProjectTo<InvestmentDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.Page, request.Size);
        }
    }
}