namespace Application.Wallet.Queries.GetUserOverview.Dto
{
    public class UserOverviewWalletDto
    {
        public double Balance { get; set; }
        public double TotalDeposit { get; set; }
        public double TotalWithdraw { get;  set; }
        public double TotalProfit { get;  set; }
        public double TotalReferralsEarning { get;  set; }
        public double TotalInvest { get;  set; }

    }
}