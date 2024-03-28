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

namespace Application.Deposits.Queries.GetDepositsHistory
{
    public class GetDepositsHistoryQuery : IRequest<PaginatedList<DepositsHistoryDto>>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }

    public class GetDepositsHistoryQueryHandler : IRequestHandler<GetDepositsHistoryQuery, PaginatedList<DepositsHistoryDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetDepositsHistoryQueryHandler(IApplicationDbContext applicationDbContext,
            ICurrentUserService currentUserService, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<PaginatedList<DepositsHistoryDto>> Handle(GetDepositsHistoryQuery request,
            CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Deposits.AsNoTracking()
                .Where(x => x.UserId == _currentUserService.UserId)
                .ProjectTo<DepositsHistoryDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(x => x.Id)
                .PaginatedListAsync(request.Page, request.Size);
        }
    }
}