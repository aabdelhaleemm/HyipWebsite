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

namespace Application.Withdraws.Queries.GetWithdrawsHistory
{
    public class GetWithdrawsHistoryQuery : IRequest<PaginatedList<WithdrawsHistoryDto>>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }

    public class
        GetWithdrawsHistoryQueryHandler : IRequestHandler<GetWithdrawsHistoryQuery, PaginatedList<WithdrawsHistoryDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetWithdrawsHistoryQueryHandler(IApplicationDbContext applicationDbContext,
            ICurrentUserService currentUserService, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<PaginatedList<WithdrawsHistoryDto>> Handle(GetWithdrawsHistoryQuery request,
            CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Withdraws.AsNoTracking()
                .Where(x => x.UserId == _currentUserService.UserId)
                .ProjectTo<WithdrawsHistoryDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(x => x.Id)
                .PaginatedListAsync(request.Page, request.Size);
        }
    }
}