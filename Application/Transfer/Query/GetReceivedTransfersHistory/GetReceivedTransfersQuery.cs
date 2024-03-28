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

namespace Application.Transfer.Query.GetReceivedTransfersHistory
{
    public class GetReceivedTransfersQuery : IRequest<PaginatedList<ReceivedTransfersDto>>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }

    public class
        GetReceivedTransfersQueryHandler : IRequestHandler<GetReceivedTransfersQuery,
            PaginatedList<ReceivedTransfersDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetReceivedTransfersQueryHandler(IApplicationDbContext applicationDbContext,
            ICurrentUserService currentUserService, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ReceivedTransfersDto>> Handle(GetReceivedTransfersQuery request,
            CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Transfers.AsNoTracking()
                .Where(x => x.RecipientId == _currentUserService.UserId)
                .OrderByDescending(x=>x.Id)
                .ProjectTo<ReceivedTransfersDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.Page, request.Size);
        }
    }
}