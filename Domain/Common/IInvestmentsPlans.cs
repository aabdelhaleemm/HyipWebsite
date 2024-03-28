namespace Domain.Common
{
    public interface IInvestmentsPlans
    {
        public bool IsAvailable { get; }
        public double ProfitPercent { get; set; }
        public double MinProfitPercent { get; }
        public double MaxProfitPercent { get; }
        public int ReturnProfitDays { get; } // Ex: every 7 days we calculate the profit
        public int TotalPlanDurationInDays { get; }
        public double MinimumInvestmentAmount { get; }
        public double? MaximumInvestmentAmount { get; }

        double CalculateProfit(double amount);
    }
}