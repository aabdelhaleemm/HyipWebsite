using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using Domain.Common;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Investment.EventsHandlers
{
    public class InvestmentStartedEventHandler : INotificationHandler<DomainEventNotification<InvestmentStartedEvent>>
    {
        private readonly IInvestmentsProfitService _investmentsProfitService;
        private readonly ILogger<InvestmentStartedEventHandler> _logger;

        public InvestmentStartedEventHandler(IInvestmentsProfitService investmentsProfitService,
            ILogger<InvestmentStartedEventHandler> logger)
        {
            _investmentsProfitService = investmentsProfitService;
            _logger = logger;
        }


#pragma warning disable 1998
        public async Task Handle(DomainEventNotification<InvestmentStartedEvent> notification,
            CancellationToken cancellationToken)
        {
            var investment = notification.DomainEvent.Investment;
            var plan = InvestmentPlanFactory.CreatePlan(investment.Plan);
            var counter = 0;
            for (var i = 0; i < plan.TotalPlanDurationInDays / plan.ReturnProfitDays; i++)
            {
                _investmentsProfitService.ScheduleProfitEarning(investment.Id,
                    plan.ReturnProfitDays + counter);
                counter += plan.ReturnProfitDays;
            }

            _logger.LogInformation("Investment id ({Id}) has been set next profit earning date : {Date}",
                investment.Id, investment.NextProfitEarningDate);
        }
    }
}