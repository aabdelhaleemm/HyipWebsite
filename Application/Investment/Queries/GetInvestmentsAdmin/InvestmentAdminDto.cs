using System;
using Domain.Common;
using Domain.Enums;

namespace Application.Investment.Queries.GetInvestmentsAdmin
{
    public class InvestmentAdminDto
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public double TotalProfit { get; set; }
        public string Status { get; set; }
        public string Plan { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? NextProfitEarningDate { get; set; } = null;
        public int Percent
        {
            get
            {
                if (NextProfitEarningDate == null)
                    return 0;
                Enum.TryParse(Plan, out InvestmentsPlan investmentsPlan);
                var investmentPlan = InvestmentPlanFactory.CreatePlan(investmentsPlan);
                var x = (NextProfitEarningDate - DateTime.UtcNow)?.TotalHours;
                return (int)(100- (x * 100 / (investmentPlan.ReturnProfitDays * 24)));
            }
        }
    }
}