using System;
using System.ComponentModel;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Withdraws : AuditableEntity
    {
        private Withdraws()
        {
        }

        public Withdraws(User user, double amount, string withdrawMethod, string withdrawAccount) : this()
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            Amount = amount > 0 ? amount : throw new ArgumentException("Please provide valid amount!");
            WithdrawAccount = withdrawAccount ?? throw new ArgumentNullException(nameof(withdrawAccount));
            WithdrawMethod = withdrawMethod ?? throw new ArgumentNullException(nameof(withdrawMethod));
            Status = Status.Pending;
            UserBalance = user.Wallet.Balance;
            AddTransaction();
        }

        public int Id { get; set; }
        public Status Status { get; private set; }
        public string AdminFeedBack { get; private set; }
        public double Amount { get; private set; }
        public double UserBalance { get; private set; }
        public string WithdrawAccount { get; private set; }
        public Transactions Transaction { get; private set; }
        public string WithdrawMethod { get; private set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public int UserId { get; private set; }
        public User User { get; private set; }


        public void ChangeStatus(Status status, string adminFeedBack)
        {
            ValidateStatus(status);
            var oldStatus = Status;
            Status = status;
            Transaction.ChangeStatus(status);
            AdminFeedBack = adminFeedBack;
            User.HandleWithdrawsRequestStatusChange(this, oldStatus);
        }

        private void ValidateStatus(Status status)
        {
            if (!Enum.IsDefined(typeof(Status), status))
                throw new InvalidEnumArgumentException(nameof(status), (int)status, typeof(Status));
            if (Status == status)
                throw new ArgumentException("Withdraw request have the same status!");
        }

        private void AddTransaction()
        {
            Transaction = new Transactions(User, -Amount, this);
        }
    }
}