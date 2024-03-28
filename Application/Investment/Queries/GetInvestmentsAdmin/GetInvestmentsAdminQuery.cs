using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mapping;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Investment.Queries.GetInvestmentsAdmin
{
    public class GetInvestmentsAdminQuery : IRequest<PaginatedList<InvestmentAdminDto>>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 20;
        public string Status { get; set; }
    }

    public class
        GetInvestmentsAdminQueryHandler : IRequestHandler<GetInvestmentsAdminQuery, PaginatedList<InvestmentAdminDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetInvestmentsAdminQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedList<InvestmentAdminDto>> Handle(GetInvestmentsAdminQuery request,
            CancellationToken cancellationToken)
        {
            if (request.Status.ToLower() == "all")
            {
                return await _applicationDbContext.Investments.AsNoTracking()
                    .OrderByDescending(x => x.Id)
                    .ProjectTo<InvestmentAdminDto>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.Page, request.Size);
            }

            Enum.TryParse(request.Status, true, out InvestmentsStatus status);
            if (status == 0)
                throw new CannotParseEnum("Invalid Investment Status!");
            return await _applicationDbContext.Investments.AsNoTracking()
                .Where(x => x.Status == status)
                .OrderByDescending(x => x.Id)
                .ProjectTo<InvestmentAdminDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.Page, request.Size);
        }
    }
}