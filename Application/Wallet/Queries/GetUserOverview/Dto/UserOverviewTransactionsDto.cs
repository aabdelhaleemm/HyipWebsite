using System;

namespace Application.Wallet.Queries.GetUserOverview.Dto
{
    public class UserOverviewTransactionsDto
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
    }
}