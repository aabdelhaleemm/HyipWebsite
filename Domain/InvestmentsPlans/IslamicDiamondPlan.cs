using Domain.Common;

namespace Domain.InvestmentsPlans
{
    public class IslamicDiamondPlan : IInvestmentsPlans
    {
        public bool IsAvailable => true;
        public double ProfitPercent { get; set; } = 12.5;
        public double MinProfitPercent => 12.5;
        public double MaxProfitPercent => 15;
        public int ReturnProfitDays => 7;
        public int TotalPlanDurationInDays => 140;
        public double MinimumInvestmentAmount => 2000;
        public double? MaximumInvestmentAmount => 4999;

        public double CalculateProfit(double amount)
        {
            return ProfitPercent / 100 * amount;
        }
    }
}