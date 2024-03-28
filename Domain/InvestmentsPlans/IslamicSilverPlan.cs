using Domain.Common;

namespace Domain.InvestmentsPlans
{
    public class IslamicSilverPlan : IInvestmentsPlans
    {
        public bool IsAvailable => true;
        public double ProfitPercent { get; set; } = 7.5;
        public double MinProfitPercent => 7.5;
        public double MaxProfitPercent => 10;
        public int ReturnProfitDays => 7;
        public int TotalPlanDurationInDays => 112;
        public double MinimumInvestmentAmount => 75;
        public double? MaximumInvestmentAmount => 749;

        public double CalculateProfit(double amount)
        {
            return ProfitPercent / 100 * amount;
        }
    }
}