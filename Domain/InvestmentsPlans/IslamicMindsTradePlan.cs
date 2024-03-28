using Domain.Common;

namespace Domain.InvestmentsPlans
{
    public class IslamicMindsTradePlan : IInvestmentsPlans
    {
        public bool IsAvailable => true;
        public double ProfitPercent { get; set; } = 0;
        public double MinProfitPercent => 0;
        public double MaxProfitPercent => 1000;
        public int ReturnProfitDays => 30;
        public int TotalPlanDurationInDays => 120;
        public double MinimumInvestmentAmount => 100;
        public double? MaximumInvestmentAmount => 99999999999999;

        public double CalculateProfit(double amount)
        {
            return ProfitPercent / 100 * amount;
        }
    }
}