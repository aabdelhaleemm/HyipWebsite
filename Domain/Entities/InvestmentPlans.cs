using System;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class InvestmentPlans : AuditableEntity
    {
        public InvestmentPlans(InvestmentsPlan name, double minProfitPercent, double maxProfitPercent,
            double currentProfitPercent)
        {
            if (minProfitPercent < 0 || minProfitPercent > maxProfitPercent ||
                currentProfitPercent > maxProfitPercent || currentProfitPercent < minProfitPercent)
                throw new AggregateException("Wrong Investment data");

            Name = name;
            MinProfitPercent = minProfitPercent;
            MaxProfitPercent = maxProfitPercent;
            CurrentProfitPercent = currentProfitPercent;
        }

        public int Id { get; set; }
        public InvestmentsPlan Name { get; private set; }
        public double MinProfitPercent { get; private set; }
        public double MaxProfitPercent { get; private set; }
        public double CurrentProfitPercent { get; private set; }

        public void ChangeCurrentProfitPercent(double newPercent)
        {
            var investmentPlans = InvestmentPlanFactory.CreatePlan(Name) ??
                                  throw new ArgumentException("Cannot detect the plan");
            if (newPercent > investmentPlans.MaxProfitPercent || newPercent < investmentPlans.MinProfitPercent)
                throw new ArgumentException("Not valid profit percent");
            CurrentProfitPercent = newPercent;
        }
    }
}