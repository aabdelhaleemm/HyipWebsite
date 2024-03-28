using System.Linq;
using Application.Admin.Queries.GetAllAdmins;
using Application.Deposits.Queries.GetDeposits;
using Application.Deposits.Queries.GetDepositsHistory;
using Application.Investment.Queries.GetInvestmentDetails;
using Application.Investment.Queries.GetInvestmentDetailsAdmin;
using Application.Investment.Queries.GetInvestments;
using Application.Investment.Queries.GetInvestmentsAdmin;
using Application.PaymentMethods.Query;
using Application.Plans.Query;
using Application.Transactions.Queries.GetUserTransactions;
using Application.Transfer.Query.GetReceivedTransfersHistory;
using Application.Transfer.Query.GetSentTransferHistory;
using Application.User.Queries.GetReferences.Dto;
using Application.User.Queries.GetUserDetails.Dto;
using Application.Wallet.Queries.GetUserOverview.Dto;
using Application.Withdraws.Queries.GetWithdraws;
using Application.Withdraws.Queries.GetWithdrawsHistory;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Common.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Domain.Entities.User, UserOverviewDto>()
                .ForMember(x => x.Transactions,
                    opt
                        => opt.MapFrom(x =>
                            x.Transactions.OrderByDescending(f => f.Id).Take(10)));
            CreateMap<Domain.Entities.Wallet, UserOverviewWalletDto>();
            CreateMap<Domain.Entities.Transactions, UserOverviewTransactionsDto>()
                .ForMember(x => x.Status, opt =>
                    opt.MapFrom(x => x.Status.ToString()))
                .ForMember(x => x.Type, opt =>
                    opt.MapFrom(x => x.Type.ToString()));

            CreateMap<Domain.Entities.Deposits, DepositsHistoryDto>()
                .ForMember(x => x.Status,
                    opt
                        => opt.MapFrom(k => k.Status.ToString()));
            CreateMap<Domain.Entities.Withdraws, WithdrawsHistoryDto>()
                .ForMember(x => x.Status,
                    opt
                        => opt.MapFrom(k => k.Status.ToString()));

            CreateMap<Domain.Entities.Transactions, TransactionsDto>()
                .ForMember(x => x.Status,
                    opt =>
                        opt.MapFrom(k => k.Status.ToString()))
                .ForMember(x => x.Type,
                    opt =>
                        opt.MapFrom(k => k.Type.ToString()));

            CreateMap<Investments, InvestmentDto>()
                .ForMember(x => x.Status,
                    opt =>
                        opt.MapFrom(k => k.Status.ToString()))
                .ForMember(x => x.Plan,
                    opt =>
                        opt.MapFrom(k => k.Plan.ToString()))
                .ForMember(x => x.NextProfitEarningDate,
                    opt =>
                        opt.MapFrom(k =>
                            k.Status == InvestmentsStatus.Canceled || k.Status == InvestmentsStatus.Finished
                                ? null
                                : k.NextProfitEarningDate));


            CreateMap<Investments, InvestmentAdminDto>()
                .ForMember(x => x.Status,
                    opt =>
                        opt.MapFrom(k => k.Status.ToString()))
                .ForMember(x => x.Plan,
                    opt =>
                        opt.MapFrom(k => k.Plan.ToString()))
                .ForMember(x => x.NextProfitEarningDate,
                    opt =>
                        opt.MapFrom(k =>
                            k.Status == InvestmentsStatus.Canceled || k.Status == InvestmentsStatus.Finished
                                ? null
                                : k.NextProfitEarningDate))
                .ForMember(x => x.UserName,
                    opt
                        => opt.MapFrom(k => k.User.UserName));


            CreateMap<Investments, InvestmentDetailsDto>()
                .ForMember(x => x.Transactions,
                    opt
                        => opt.MapFrom(k
                            => k.Transactions.OrderByDescending(o => o.Id)))
                .ForMember(x => x.InvestmentsProfits,
                    opt
                        => opt.MapFrom(x =>
                            x.InvestmentsProfits.OrderByDescending(k => k.Id)));
            CreateMap<Domain.Entities.Withdraws, WithdrawDto>()
                .ForMember(x => x.Status,
                    opt
                        => opt.MapFrom(k => k.Status.ToString()))
                .ForMember(x => x.UserName, 
                    opt =>
                        opt.MapFrom(k => k.User.UserName));

            
            CreateMap<Domain.Entities.Deposits, DepositsDto>()
                .ForMember(x => x.Status,
                    opt
                        => opt.MapFrom(k => k.Status.ToString()))
                .ForMember(x => x.UserName,
                    opt
                        => opt.MapFrom(k => k.User.UserName));

            CreateMap<Domain.Entities.User, UserDto>();
            CreateMap<Domain.Entities.Wallet, UserWalletDto>();

            CreateMap<Domain.Entities.User, UserReferenceDto>()
                .ForMember(x => x.Created,
                    opt
                        => opt.MapFrom(k => k.Wallet.Created));
            CreateMap<Domain.Entities.Transactions, TransactionsReferenceDto>();
            CreateMap<Domain.Entities.User, ReferencesDto>()
                .ForMember(x => x.Transactions,
                    opt =>
                        opt.MapFrom(
                            k => k.Transactions.Where(f => f.Type == TransactionsTypes.ReferenceProfit)));
            CreateMap<Transfers, SentTransfersDto>()
                .ForMember(x => x.RecipientUserName,
                    opt
                        => opt.MapFrom(k => k.Recipient.UserName));

            CreateMap<Transfers, ReceivedTransfersDto>()
                .ForMember(x => x.SenderUserName,
                    opt
                        => opt.MapFrom(k => k.Sender.UserName));

            CreateMap<Domain.Entities.Admin, AdminDto>();
            CreateMap<InvestmentsProfits, InvestmentProfitDto>();
            CreateMap<Domain.Entities.PaymentMethods, DepositMethodsDto>();
            CreateMap<InvestmentPlans, PlansDto>()
                .ForMember(x => x.Name,
                    opt
                        => opt.MapFrom(k => k.Name.ToString()));
        }
    }
}