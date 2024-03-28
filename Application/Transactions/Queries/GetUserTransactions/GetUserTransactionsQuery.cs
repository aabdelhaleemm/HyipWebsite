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

namespace Application.Transactions.Queries.GetUserTransactions
{
    public class GetUserTransactionsQuery : IRequest<PaginatedList<TransactionsDto>>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }

    public class
        GetUserTransactionsQueryHandler : IRequestHandler<GetUserTransactionsQuery, PaginatedList<TransactionsDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetUserTransactionsQueryHandler(IApplicationDbContext applicationDbContext,
            ICurrentUserService currentUserService, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<PaginatedList<TransactionsDto>> Handle(GetUserTransactionsQuery request,
            CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Transactions.AsNoTracking()
                .Where(x => x.UserId == _currentUserService.UserId)
                .OrderByDescending(x => x.Id)
                .ProjectTo<TransactionsDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.Page, request.Size);
        }
    }
}