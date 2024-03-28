using System;

namespace Application.Withdraws.Queries.GetWithdrawsHistory
{
    public class WithdrawsHistoryDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string AdminFeedBack { get; set; }
        public double Amount { get; set; }
        public string WithdrawMethod { get; set; }
        public DateTime Created { get; set; }
    }
}