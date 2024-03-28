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

namespace Application.Deposits.Queries.GetDeposits
{
    public class GetDepositsQuery : IRequest<PaginatedList<DepositsDto>>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 20;
        public string Status { get; set; } = "All";
    }

    public class GetDepositsQueryHandler : IRequestHandler<GetDepositsQuery, PaginatedList<DepositsDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetDepositsQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedList<DepositsDto>> Handle(GetDepositsQuery request,
            CancellationToken cancellationToken)
        {
            if (request.Status.ToLower() == "all")
            {
                return await _applicationDbContext.Deposits.AsNoTracking()
                    .OrderByDescending(x => x.Id)
                    .ProjectTo<DepositsDto>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.Page, request.Size);
            }

            Enum.TryParse(request.Status, true, out Status status);
            if (status == 0)
                throw new CannotParseEnum("Invalid Deposit Status!");
            return await _applicationDbContext.Deposits.AsNoTracking()
                .Where(x => x.Status == status)
                .OrderByDescending(x => x.Id)
                .ProjectTo<DepositsDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.Page, request.Size);
        }
    }
}