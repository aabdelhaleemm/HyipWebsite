using Domain.Common;

namespace Domain.InvestmentsPlans
{
    public class IslamicStarterPlan : IInvestmentsPlans
    {
        public bool IsAvailable => true;
        public double ProfitPercent { get; set; } = 5;
        public double MinProfitPercent => 5;
        public double MaxProfitPercent => 7.5;
        public int ReturnProfitDays => 7;
        public int TotalPlanDurationInDays => 28;
        public double MinimumInvestmentAmount => 10;
        public double? MaximumInvestmentAmount => 200;

        public double CalculateProfit(double amount)
        {
            return ProfitPercent / 100 * amount;
        }
    }
}