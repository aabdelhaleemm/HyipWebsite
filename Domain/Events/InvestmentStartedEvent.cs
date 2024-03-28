using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class InvestmentStartedEvent : DomainEvent
    {
        public InvestmentStartedEvent(Investments investment)
        {
            Investment = investment;
        }

        public Investments Investment { get;  }
    }
}