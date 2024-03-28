using Domain.Common;

namespace Domain.InvestmentsPlans
{
    public class IslamicPearlPlan : IInvestmentsPlans
    {
        public bool IsAvailable => true;
        public double ProfitPercent { get; set; } = 15;
        public double MinProfitPercent => 15;
        public double MaxProfitPercent => 17.5;
        public int ReturnProfitDays => 7;
        public int TotalPlanDurationInDays => 168;
        public double MinimumInvestmentAmount => 5000;
        public double? MaximumInvestmentAmount => 14999;

        public double CalculateProfit(double amount)
        {
            return ProfitPercent / 100 * amount;
        }
    }
}