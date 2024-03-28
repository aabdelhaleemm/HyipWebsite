using System;

namespace Application.User.Queries.GetUserDetails.Dto
{
    public class UserWalletDto
    {
        public DateTime Created { get; set; }
        public double Balance { get; set; }
        public double TotalDeposit { get; set; }
        public double TotalWithdraw { get; set; }
        public double TotalProfit { get; set; }
        public double TotalInvest { get; set; }
        public double TotalReferralsEarning { get; set; }
    }
}