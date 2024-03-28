using System;


namespace Application.Deposits.Queries.GetDepositsHistory
{
    public class DepositsHistoryDto
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime Created { get; set; }
        public string AdminFeedBack { get; set; }
        public string Status { get; set; }
        public string DepositMethod { get; set; }
    }
}