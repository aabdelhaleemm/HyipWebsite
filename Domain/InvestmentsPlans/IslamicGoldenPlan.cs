using Domain.Common;

namespace Domain.InvestmentsPlans
{
    public class IslamicGoldenPlan : IInvestmentsPlans
    {
        public bool IsAvailable => true;
        public double ProfitPercent { get; set; } = 10;
        public double MinProfitPercent => 10;
        public double MaxProfitPercent => 12.5;
        public int ReturnProfitDays => 7;
        public int TotalPlanDurationInDays => 126;
        public double MinimumInvestmentAmount => 750;
        public double? MaximumInvestmentAmount => 1999;

        public double CalculateProfit(double amount)
        {
            return ProfitPercent / 100 * amount;
        }
    }
}