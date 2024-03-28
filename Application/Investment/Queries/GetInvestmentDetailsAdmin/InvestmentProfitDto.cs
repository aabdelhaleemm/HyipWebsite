using System;

namespace Application.Investment.Queries.GetInvestmentDetailsAdmin
{
    public class InvestmentProfitDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public double Amount { get; set; }
    }
}