using System.Collections.Generic;
using Application.Investment.Queries.GetInvestmentDetailsAdmin;
using Application.Transactions.Queries.GetUserTransactions;

namespace Application.Investment.Queries.GetInvestmentDetails
{
    public class InvestmentDetailsDto
    {
        public List<TransactionsDto> Transactions { get; set; }
        public List<InvestmentProfitDto> InvestmentsProfits { get; set; }
    }
}