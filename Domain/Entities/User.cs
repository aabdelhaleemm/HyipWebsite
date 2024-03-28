using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public sealed class User : IdentityUser<int>
    {
        private User()
        {
        }

        public User(string email, string userName, int? referenceId) : this()
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            ReferenceId = referenceId;
            Wallet = new Wallet(this);
        }

        public int? ReferenceId { get; private set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public User Reference { get; private set; }

        public Wallet Wallet { get; private set; }

        private readonly List<User> _referenceUsers = new();
        public IReadOnlyCollection<User> ReferenceUsers => _referenceUsers.AsReadOnly();

        private readonly List<Deposits> _deposits = new();
        public IReadOnlyCollection<Deposits> Deposits => _deposits.AsReadOnly();


        private readonly List<Transfers> _sentTransfers = new();
        public IReadOnlyCollection<Transfers> SentTransfers => _sentTransfers.AsReadOnly();
        private readonly List<Transfers> _receivedTransfers = new();
        public IReadOnlyCollection<Transfers> ReceivedTransfers => _receivedTransfers.AsReadOnly();


        private readonly List<Withdraws> _withdraws = new();
        public IReadOnlyCollection<Withdraws> Withdraws => _withdraws.AsReadOnly();

        private readonly List<Investments> _investments = new();
        public IReadOnlyCollection<Investments> Investments => _investments.AsReadOnly();

        // ReSharper disable once CollectionNeverUpdated.Local
        private readonly List<InvestmentsProfits> _investmentsProfits = new();
        public IReadOnlyCollection<InvestmentsProfits> InvestmentsProfits => _investmentsProfits;

        //
        private readonly List<Transactions> _transactions = new();
        public IReadOnlyCollection<Transactions> Transactions => _transactions.AsReadOnly();

        public void TransferMoney(User recipient, double amount)
        {
            if (amount > Wallet.Balance)
                throw new ArgumentException("No enough balance!");
            if (recipient is null)
                throw new ArgumentException("Wrong recipient username!");
            var transferEntity = new Transfers(this, recipient, amount);
            _sentTransfers.Add(transferEntity);
            Wallet.HandleTransferMoney(-amount);
            recipient.Wallet.HandleTransferMoney(amount);
        }

        internal void HandleCancelOrFinishInvestment(Investments investment)
        {
            if (investment is null || !Investments.Contains(investment))
                throw new ArgumentNullException(nameof(investment));
            Wallet.HandleCancelOrFinishInvestment(this, investment.Amount);
        }

        internal void HandleInvestmentProfitAdding(Investments investments, double profit)
        {
            if (investments == null || !Investments.Contains(investments))
                throw new ArgumentNullException(nameof(investments));
            Wallet.HandleInvestmentProfitAdding(this, profit);
        }

        public void AddInvestment(double requestAmount, InvestmentsPlan requestedInvestmentsPlan,
            DateTime currentDateTime)
        {
            if (requestAmount > Wallet.Balance)
                throw new ArgumentException("Investment amount more than user balance!");
            var investment = new Investments(this, requestAmount, requestedInvestmentsPlan, currentDateTime);
            _investments.Add(investment);
            CheckForReferenceProfit(investment);
            Wallet.HandleAddInvestment(this, requestAmount);
        }

        public void AddWithdrawRequest(double requestAmount, string requestWithdrawMethod,
            string requestWithdrawAccount)
        {
            if (requestAmount > Wallet.Balance)
                throw new ArgumentException("Withdraw amount more than wallet balance!");
            _withdraws.Add(new Withdraws(this, requestAmount, requestWithdrawMethod, requestWithdrawAccount));
            Wallet.HandleAddWithdrawRequest(this, requestAmount);
        }

        public void AddDepositRequest(double amount, string depositMethod, string operationId,
            string userWalletId, string proofImage)
        {
            _deposits.Add(new Deposits(this, amount, depositMethod, operationId, proofImage,
                userWalletId));
        }

        internal void HandleWithdrawsRequestStatusChange(Withdraws withdraws, Status oldStatus)
        {
            if (withdraws == null || !Withdraws.Contains(withdraws))
                throw new ArgumentNullException(nameof(withdraws));
            if (!Enum.IsDefined(typeof(Status), oldStatus))
                throw new InvalidEnumArgumentException(nameof(oldStatus), (int)oldStatus, typeof(Status));
            ChangeBalance(oldStatus, withdraws);
        }

        private void ChangeBalance(Status oldStatus, Withdraws withdraws)
        {
            var newStatus = withdraws.Status;
            switch (oldStatus)
            {
                case Status.Pending when newStatus is Status.Accepted:
                    Wallet.HandlePendingWithdrawWhenAccepted(this, withdraws.Amount);
                    break;
                case Status.Pending when newStatus is Status.Rejected:
                    Wallet.HandlePendingWithdrawWhenRejected(this, withdraws.Amount);
                    break;
                case Status.Accepted when newStatus is Status.Pending:
                    Wallet.HandleAcceptedWithdrawWhenPending(this, withdraws.Amount);
                    break;
                case Status.Accepted when newStatus is Status.Rejected:
                    Wallet.HandleAcceptedWithdrawWhenRejected(this, withdraws.Amount);
                    break;
                case Status.Rejected when newStatus is Status.Pending:
                    Wallet.HandleRejectedWithdrawWhenPending(this, withdraws.Amount);
                    break;
                case Status.Rejected when newStatus is Status.Accepted:
                    Wallet.HandleRejectedWithdrawWhenAccepted(this, withdraws.Amount);
                    break;
            }
        }

        internal void HandleDepositRequestStatusChange(Deposits deposit, Status oldStatus)
        {
            if (deposit == null || !Deposits.Contains(deposit))
                throw new ArgumentNullException(nameof(deposit));
            if (!Enum.IsDefined(typeof(Status), oldStatus))
                throw new InvalidEnumArgumentException(nameof(oldStatus), (int)oldStatus, typeof(Status));
            ChangeBalance(oldStatus, deposit);
        }

        private void CheckForReferenceProfit(Investments investment)
        {
            if (Wallet.TotalInvest != 0 || ReferenceId is null ||
                investment.Status is not InvestmentsStatus.Running || investment.Amount < 100) return;
            var amount = investment.Amount switch
            {
                >= 10 and < 100 => 5,
                >= 100 and < 500 => 10,
                >= 500 and <1000 => 20,
                >= 1000 and <5000 => 30,
                >= 5000 => 50,
                _ => 0
            };

            Reference.Wallet.AddReferenceProfit(Reference, amount);
            Reference._transactions.Add(new Transactions(Reference, amount, ""));
        }

        private void ChangeBalance(Status oldStatus, Deposits deposit)
        {
            if (oldStatus is Status.Accepted)
                Wallet.HandelAddDeposit(this, -deposit.Amount);

            else if (deposit.Status is Status.Accepted)
                Wallet.HandelAddDeposit(this, deposit.Amount);
        }
    }
}