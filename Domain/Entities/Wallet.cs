using System;
using Domain.Common;

namespace Domain.Entities
{
    public class Wallet : AuditableEntity
    {
        private Wallet()
        {
        }

        public Wallet(User user) : this()
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            Balance = 0;
            TotalDeposit = 0;
            TotalWithdraw = 0;
            TotalProfit = 0;
            TotalReferralsEarning = 0;
            TotalInvest = 0;
        }

        public int Id { get; set; }
        public double Balance { get; private set; }
        public double TotalDeposit { get; private set; }
        public double TotalWithdraw { get; private set; }
        public double TotalProfit { get; private set; }

        public double TotalInvest { get; private set; }

        public double TotalReferralsEarning { get; private set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public int UserId { get; private set; }
        public User User { get; private set; }

        internal void AddReferenceProfit(User user, double profit)
        {
            ValidateUser(user);
            Balance += profit;
            TotalReferralsEarning += profit;
        }

        internal void HandleTransferMoney(double amount)
        {
            Balance += amount;
        }

        internal void HandlePendingWithdrawWhenAccepted(User user, double amount)
        {
            ValidateUser(user);
            TotalWithdraw += amount;
        }

        internal void HandlePendingWithdrawWhenRejected(User user, double amount)
        {
            ValidateUser(user);
            Balance += amount;
        }

        internal void HandleAcceptedWithdrawWhenPending(User user, double amount)
        {
            ValidateUser(user);
            TotalWithdraw -= amount;
        }

        internal void HandleAcceptedWithdrawWhenRejected(User user, double amount)
        {
            ValidateUser(user);
            Balance += amount;
            TotalWithdraw -= amount;
        }

        internal void HandleRejectedWithdrawWhenPending(User user, double amount)
        {
            ValidateUser(user);
            Balance -= amount;
        }

        internal void HandleRejectedWithdrawWhenAccepted(User user, double amount)
        {
            ValidateUser(user);
            Balance -= amount;
            TotalWithdraw += amount;
        }

        internal void HandleAddInvestment(User user, double amount)
        {
            ValidateUser(user);
            TotalInvest += amount;
            Balance -= amount;
        }

        internal void HandleCancelOrFinishInvestment(User user, double amount)
        {
            ValidateUser(user);
            Balance += amount;
        }

        internal void HandleInvestmentProfitAdding(User user, double profit)
        {
            ValidateUser(user);
            Balance += profit;
            TotalProfit += profit;
        }

        internal void HandleAddWithdrawRequest(User user, double amount)
        {
            ValidateUser(user);
            Balance -= amount;
        }

        internal void HandelAddDeposit(User user, double amount)
        {
            ValidateUser(user);
            Balance += amount;
            TotalDeposit += amount;
        }

        private void ValidateUser(User user)
        {
            if (User != user) throw new ArgumentException("User Not Valid");
        }
    }
}