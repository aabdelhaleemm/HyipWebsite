using Domain.Common;

namespace Domain.InvestmentsPlans
{
    public class IslamicPlatinumPlan : IInvestmentsPlans
    {
        public bool IsAvailable => true;
        public double ProfitPercent { get; set; } = 17.5;
        public double MinProfitPercent => 17.5;
        public double MaxProfitPercent => 20;
        public int ReturnProfitDays => 7;
        public int TotalPlanDurationInDays => 196;
        public double MinimumInvestmentAmount => 15000;
        public double? MaximumInvestmentAmount => null;

        public double CalculateProfit(double amount)
        {
            return ProfitPercent / 100 * amount;
        }
    }
}