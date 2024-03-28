using System;

namespace Application.Withdraws.Queries.GetWithdraws
{
    public class WithdrawDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public double Amount { get; set; }
        public string WithdrawAccount { get; set; }
        public string WithdrawMethod { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime Created { get; set; }
        public int? LastModifiedBy { get; set; }
        public double UserBalance { get; set; }
    }
}