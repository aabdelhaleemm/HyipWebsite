using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mapping;
using Application.Common.Models;
using Application.Transactions.Queries.GetUserTransactions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Queries.GetUserTransactionsForAdmin
{
    public class GetUserTransactionsAdminQuery : IRequest<PaginatedList<TransactionsDto>>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 20;
        public string UserName { get; set; }
    }

    public class
        GetUserTransactionsAdminQueryHandler : IRequestHandler<GetUserTransactionsAdminQuery,
            PaginatedList<TransactionsDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<Domain.Entities.User> _userManager;

        public GetUserTransactionsAdminQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper,
            UserManager<Domain.Entities.User> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<PaginatedList<TransactionsDto>> Handle(GetUserTransactionsAdminQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                throw new NotFoundException("Cannot find user with this User Name");
            return await _applicationDbContext.Transactions.AsNoTracking()
                .Where(x => x.UserId == user.Id)
                .OrderByDescending(x => x.Id)
                .ProjectTo<TransactionsDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.Page, request.Size);
        }
    }
}