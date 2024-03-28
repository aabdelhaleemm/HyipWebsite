using System;

namespace Application.Transactions.Queries.GetUserTransactions
{
    public class TransactionsDto
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
    }
}