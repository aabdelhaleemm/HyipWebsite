using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class Transactions : AuditableEntity
    {
        private Transactions()
        {
        }

        public Transactions(User user, double amount) : this()
        {
            Amount = amount;
            User = user;
        }

        // ReSharper disable once UnusedParameter.Local
        public Transactions(User user, double amount, string status) : this(user, amount)
        {
            Type = TransactionsTypes.ReferenceProfit;
            Status = Status.Accepted;
        }

        public Transactions(User user, double amount, Deposits deposit)
            : this(user, amount)
        {
            Deposit = deposit;
            Type = TransactionsTypes.DepositRequest;
            Status = Status.Pending;
        }

        public Transactions(User user, double amount, Withdraws withdraw)
            : this(user, amount)
        {
            Withdraw = withdraw;
            Type = TransactionsTypes.WithdrawRequest;
            Status = Status.Pending;
        }

        //Sender
        public Transactions(User user, double amount, Transfers transfers)
            : this(user, amount)
        {
            SenderTransfers = transfers;
            Type = TransactionsTypes.MoneyTransfer;
            Status = Status.Accepted;
        }
        //Recipient
        public Transactions(double amount,User user, Transfers transfers)
            : this(user, amount)
        {
            RecipientTransfers = transfers;
            Type = TransactionsTypes.MoneyTransfer;
            Status = Status.Accepted;
        }

        public Transactions(User user, double amount, Investments investment, TransactionsTypes transactionType)
            : this(user, amount)
        {
            if (!Enum.IsDefined(typeof(TransactionsTypes), transactionType))
                throw new InvalidEnumArgumentException(nameof(transactionType), (int)transactionType,
                    typeof(TransactionsTypes));
            Investment = investment;
            Type = transactionType;
            Status = Status.Accepted;
        }

        public Transactions(User user, double amount, InvestmentsProfits investmentsProfit)
            : this(user, amount)
        {
            Status = Status.Accepted;
            InvestmentsProfit = investmentsProfit;
            Type = TransactionsTypes.InvestmentProfit;
        }

        public int Id { get; set; }
        public double Amount { get; private set; }
        public TransactionsTypes Type { get; private set; }
        public Status Status { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; }
        public int? DepositId { get; private set; }

        public Transfers RecipientTransfers { get; private set; }
        public int? RecipientTransferId { get; private set; }

        public Transfers SenderTransfers { get; private set; }
        public int? SenderTransferId { get; private set; }

        public Deposits Deposit { get; private set; }
        public int? WithdrawId { get; private set; }
        public Withdraws Withdraw { get; private set; }
        public Investments Investment { get; private set; }
        public int? InvestmentId { get; private set; }
        public int? InvestmentProfitId { get; private set; }
        public InvestmentsProfits InvestmentsProfit { get; private set; }

        internal void ChangeStatus(Status status)
        {
            Status = status;
        }
    }
}