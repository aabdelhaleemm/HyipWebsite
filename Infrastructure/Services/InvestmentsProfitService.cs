using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Common;
using Hangfire;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class InvestmentsProfitService : IInvestmentsProfitService
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IBackgroundJobClient _backgroundJobClient;


        public InvestmentsProfitService(IApplicationDbContext applicationDbContext,
            IBackgroundJobClient backgroundJobClient)
        {
            _applicationDbContext = applicationDbContext;
            _backgroundJobClient = backgroundJobClient;
        }

        public void ScheduleProfitEarning(int investmentId, int nextProfitEarningDurationInDays)
        {
            _backgroundJobClient.Schedule(() =>
                    AddInvestmentProfit(investmentId),
                TimeSpan.FromDays(nextProfitEarningDurationInDays));
        }


        // Load the investment then load the investment plan percent from db then send it to the method
        public async Task AddInvestmentProfit(int investmentId)
        {
            var investment = await _applicationDbContext.Investments
                .Include(x => x.User)
                .ThenInclude(x => x.Wallet)
                .FirstOrDefaultAsync(x => x.Id == investmentId);
            var investmentPlanProfitPercent = await _applicationDbContext.InvestmentPlans.AsNoTracking()
                .Where(x => x.Name == investment.Plan)
                .Select(x => x.CurrentProfitPercent)
                .FirstOrDefaultAsync();
            var plan = InvestmentPlanFactory.CreatePlan(investment.Plan);
            plan.ProfitPercent = investmentPlanProfitPercent;
            investment.AddProfit(plan);

            await _applicationDbContext.SaveChangesAsync(CancellationToken.None);
        }
    }
}