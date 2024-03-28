using System.Collections.Generic;

namespace Application.Wallet.Queries.GetUserOverview.Dto
{
    public class UserOverviewDto
    {
        public UserOverviewWalletDto Wallet { get; set; }
        public IEnumerable<UserOverviewTransactionsDto> Transactions { get; set; }
    }
}