using System.Collections.Generic;

namespace Application.Chart
{
    public class Chart
    {
        public List<InvestmentChart> InvestmentChart { get; set; }
        public IEnumerable<InvestmentChart> TotalInvestmentsAmount { get; set; }
        public IEnumerable<InvestmentChart> TotalInvestmentsCount { get; set; }
        public double TotalDeposits { get; set; }
        public double TotalWithdraw { get; set; }
        public double TotalInvestment { get; set; }
        public int TotalUsers { get; set; }
        public double TotalProfit { get; set; }
    }
}