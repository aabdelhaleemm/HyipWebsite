using System;
using System.Collections.Generic;
using System.ComponentModel;
using Domain.Common;
using Domain.Enums;
using Domain.Events;

namespace Domain.Entities
{
    public class Investments : AuditableEntity, IHasDomainEvent
    {
        private Investments()
        {
        }

        public Investments(User user, double amount, InvestmentsPlan plan, DateTime currentDateTime) : this()
        {
            var investmentPlan = GetInvestmentPlan(plan);
            TotalProfitShouldBeAddedCount = investmentPlan.TotalPlanDurationInDays / investmentPlan.ReturnProfitDays;
            TotalAddedProfitCount = 0;
            User = user ?? throw new ArgumentNullException(nameof(user));
            ValidateInvestmentAmount(amount, investmentPlan);
            Amount = amount;
            Status = InvestmentsStatus.Running;
            Plan = plan;
            StartDate = currentDateTime;
            EndDate = currentDateTime + TimeSpan.FromDays(investmentPlan.TotalPlanDurationInDays);
            NextProfitEarningDate = currentDateTime + TimeSpan.FromDays(investmentPlan.ReturnProfitDays);
            DomainEvents.Add(new InvestmentStartedEvent(this));
            AddTransaction(TransactionsTypes.InvestmentStart, -Amount);
        }


        public int Id { get; set; }
        public double Amount { get; private set; }
        public double TotalProfit { get; private set; }
        public int TotalProfitShouldBeAddedCount { get; private set; }
        public int TotalAddedProfitCount { get; private set; }
        public string CancelReason { get; private set; }
        public InvestmentsStatus Status { get; private set; }
        public InvestmentsPlan Plan { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public DateTime? NextProfitEarningDate { get; private set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public int UserId { get; private set; }
        public User User { get; private set; }
        private readonly List<Transactions> _transactions = new();
        public IReadOnlyCollection<Transactions> Transactions => _transactions.AsReadOnly();
        private readonly List<InvestmentsProfits> _investmentsProfits = new();
        public IReadOnlyCollection<InvestmentsProfits> InvestmentsProfits => _investmentsProfits;

        public List<DomainEvent> DomainEvents { get; set; } = new();

        private void AddTransaction(TransactionsTypes type)
        {
            _transactions.Add(new Transactions(User, Amount, this, type));
        }

        private void AddTransaction(TransactionsTypes type, double amount)
        {
            _transactions.Add(new Transactions(User, amount, this, type));
        }

        public void CancelOrFinishInvestment(InvestmentsStatus status, string cancelReason)
        {
            ValidateStatus(status);
            Status = status;
            CancelReason = cancelReason;
            AddTransactionBasedOnStatus(status);
            NextProfitEarningDate = null;
            User.HandleCancelOrFinishInvestment(this);
        }

        private void AddTransactionBasedOnStatus(InvestmentsStatus status)
        {
            if (status == InvestmentsStatus.Finished)
                AddTransaction(TransactionsTypes.InvestmentFinished);
            else if (status == InvestmentsStatus.Canceled)
                AddTransaction(TransactionsTypes.InvestmentCanceled);
        }

        private void ValidateStatus(InvestmentsStatus status)
        {
            if (!Enum.IsDefined(typeof(InvestmentsStatus), status))
                throw new InvalidEnumArgumentException(nameof(status), (int)status, typeof(InvestmentsStatus));
            if (status is InvestmentsStatus.Running)
                throw new ArgumentException("Wrong investment status");
            if (Status is not InvestmentsStatus.Running)
                throw new ArgumentException("Investment is cancelled or finished!");
        }

        public void AddProfit(IInvestmentsPlans investmentPlan)
        {
            if (NextProfitEarningDate is null)
                return;
            ValidateAddingProfit();
            var profit = investmentPlan.CalculateProfit(Amount);
            var investmentProfit = new InvestmentsProfits(User, this, profit);
            TotalProfit += profit;
            _investmentsProfits.Add(investmentProfit);
            NextProfitEarningDate = NextProfitEarningDate?.AddDays(investmentPlan.ReturnProfitDays);
            User.HandleInvestmentProfitAdding(this, profit);
            TotalAddedProfitCount += 1;
            if (TotalAddedProfitCount != TotalProfitShouldBeAddedCount) return;
            CancelOrFinishInvestment(InvestmentsStatus.Finished, "");
        }

        private void ValidateAddingProfit()
        {
            if (Status is not InvestmentsStatus.Running)
                throw new ArgumentException("Investment is cancelled or finished!");
            if (NextProfitEarningDate is null)
            {
                return;
            }

            var time = DateTime.UtcNow;
            if (DateTime.Compare(NextProfitEarningDate ?? time, time) > 0 &&
                TotalAddedProfitCount >= TotalProfitShouldBeAddedCount)
                throw new ArgumentException("Investment is finished!");
        }

        private void ValidateInvestmentAmount(double amount, IInvestmentsPlans investmentPlan)
        {
            if (amount < investmentPlan.MinimumInvestmentAmount)
                throw new ArgumentException("Amount is less than the limit for this plan!");
            if (investmentPlan.MaximumInvestmentAmount is null) return;
            if (amount > investmentPlan.MaximumInvestmentAmount)
                throw new ArgumentException("Amount is more than the limit for this plan!");
        }

        private static IInvestmentsPlans GetInvestmentPlan(InvestmentsPlan plan)
        {
            if (!Enum.IsDefined(typeof(InvestmentsPlan), plan))
                throw new InvalidEnumArgumentException(nameof(plan), (int)plan, typeof(InvestmentsPlan));
            var investmentPlan = InvestmentPlanFactory.CreatePlan(plan) ??
                                 throw new ArgumentException("Cannot detect investment plan");
            if (!investmentPlan.IsAvailable)
                throw new ArgumentException("We no longer accept this investment plan");
            return investmentPlan;
        }
    }
}