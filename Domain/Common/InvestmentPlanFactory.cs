using Domain.Enums;
using Domain.InvestmentsPlans;

namespace Domain.Common
{
    public static class InvestmentPlanFactory
    {
        public static IInvestmentsPlans CreatePlan(InvestmentsPlan investmentPlan)
        {
            return investmentPlan switch
            {
                InvestmentsPlan.IslamicStarterPlan => new IslamicStarterPlan(),
                InvestmentsPlan.IslamicSilverPlan => new IslamicSilverPlan(),
                InvestmentsPlan.IslamicGoldenPlan => new IslamicGoldenPlan(),
                InvestmentsPlan.IslamicDiamondPlan => new IslamicDiamondPlan(),
                InvestmentsPlan.IslamicPearlPlan => new IslamicPearlPlan(),
                InvestmentsPlan.IslamicPlatinumPlan => new IslamicPlatinumPlan(),
                InvestmentsPlan.IslamicMindsTradePlan => new IslamicMindsTradePlan(),
                _ => null
            };
        }
    }
}