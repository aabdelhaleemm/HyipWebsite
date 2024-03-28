using System;

namespace Application.Deposits.Queries.GetDeposits
{
    public class DepositsDto
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime Created { get; set; }
        public string Status { get; set; }
        public string UserWalletId { get;  set; }
        public string DepositMethod { get; set; }
        public string OperationId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string ProofImage { get;  set; }
        public int? LastModifiedBy { get; set; }
    }
}