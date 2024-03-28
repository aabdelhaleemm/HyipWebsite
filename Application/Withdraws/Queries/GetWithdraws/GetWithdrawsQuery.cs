﻿using System;
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

namespace Application.Withdraws.Queries.GetWithdraws
{
    public class GetWithdrawsQuery : IRequest<PaginatedList<WithdrawDto>>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 20;
        public string Status { get; set; }
    }

    public class GetWithdrawsQueryHandler : IRequestHandler<GetWithdrawsQuery, PaginatedList<WithdrawDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetWithdrawsQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedList<WithdrawDto>> Handle(GetWithdrawsQuery request,
            CancellationToken cancellationToken)
        {
            if (request.Status.ToLower() == "all")
            {
                return await _applicationDbContext.Withdraws.AsNoTracking()
                    .OrderByDescending(x => x.Id)
                    .ProjectTo<WithdrawDto>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.Page, request.Size);
            }

            Enum.TryParse(request.Status, true, out Status status);
            if (status == 0)
                throw new CannotParseEnum("Invalid Withdraw Status!");
            return await _applicationDbContext.Withdraws.AsNoTracking()
                .Where(x => x.Status == status)
                .OrderByDescending(x => x.Id)
                .ProjectTo<WithdrawDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.Page, request.Size);
        }
    }
}