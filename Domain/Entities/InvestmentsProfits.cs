using System;
using Domain.Common;

namespace Domain.Entities
{
    public class InvestmentsProfits : AuditableEntity
    {
        private InvestmentsProfits()
        {
            // Amount = profit,
            // InvestmentsId = investment.Id,
            // UserId = investment.UserId
        }

        public InvestmentsProfits(User user, Investments investments, double amount) : this()
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            Investment = investments ?? throw new ArgumentNullException(nameof(investments));
            Amount = amount;
            AddTransaction();
        }

        public int Id { get; set; }
        public double Amount { get; private set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public int InvestmentsId { get; private set; }
        public Investments Investment { get; private set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public int UserId { get; private set; }
        public Transactions Transaction { get; private set; }
        public User User { get; private set; }

        private void AddTransaction()
        {
            Transaction = new Transactions(User, Amount, this);
        }
    }
}