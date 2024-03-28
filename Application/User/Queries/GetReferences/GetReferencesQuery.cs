using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.User.Queries.GetReferences.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.User.Queries.GetReferences
{
    public class GetReferencesQuery : IRequest<ReferencesDto>
    {
    }

    public class GetReferencesQueryHandler : IRequestHandler<GetReferencesQuery, ReferencesDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetReferencesQueryHandler(IApplicationDbContext applicationDbContext,
            ICurrentUserService currentUserService, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<ReferencesDto> Handle(GetReferencesQuery request, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Users.AsNoTracking()
                .Where(x => x.Id == _currentUserService.UserId)
                .ProjectTo<ReferencesDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}