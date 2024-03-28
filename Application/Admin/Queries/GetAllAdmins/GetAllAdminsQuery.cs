using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Admin.Queries.GetAllAdmins
{
    public class GetAllAdminsQuery : IRequest<List<AdminDto>>
    {
    }

    public class GetAllAdminsQueryHandler : IRequestHandler<GetAllAdminsQuery, List<AdminDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetAllAdminsQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<List<AdminDto>> Handle(GetAllAdminsQuery request, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Admin.AsNoTracking()
                .ProjectTo<AdminDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}