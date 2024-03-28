using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Wallet.Queries.GetUserOverview.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Wallet.Queries.GetUserOverview
{
    public class GetUserOverviewQuery : IRequest<UserOverviewDto>
    {
    }

    public class GetUserOverviewQueryHandler : IRequestHandler<GetUserOverviewQuery, UserOverviewDto>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetUserOverviewQueryHandler(ICurrentUserService currentUserService,
            IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<UserOverviewDto> Handle(GetUserOverviewQuery request, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Users.AsNoTracking()
                .Where(x => x.Id == _currentUserService.UserId)
                .ProjectTo<UserOverviewDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}