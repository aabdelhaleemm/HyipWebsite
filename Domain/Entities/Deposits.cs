using System;
using System.ComponentModel;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Deposits : AuditableEntity
    {
        private Deposits()
        {
        }

        public Deposits(User user, double amount, string depositMethod, string operationId,
            string proofImage, string userWalletId) : this()
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            Amount = amount > 0 ? amount : throw new ArgumentException("Amount should be greater than 0");
            DepositMethod = depositMethod ?? throw new ArgumentNullException(nameof(depositMethod));
            Status = Status.Pending;
            OperationId = operationId ?? throw new ArgumentNullException(nameof(operationId));
            UserWalletId = userWalletId ?? throw new ArgumentNullException(nameof(userWalletId));
            ProofImage = proofImage;
            AddTransaction();
        }

        public int Id { get; set; }
        public double Amount { get; private set; }
        public string AdminFeedBack { get; private set; }
        public Status Status { get; private set; }
        public string UserWalletId { get; private set; }
        public string DepositMethod { get; private set; }
        public string OperationId { get; private set; }
        public string ProofImage { get; private set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public int UserId { get; private set; }
        public Transactions Transaction { get; private set; }
        public User User { get; private set; }

        public void ChangeStatus(Status status, string adminFeedBack)
        {
            if (!Enum.IsDefined(typeof(Status), status))
                throw new InvalidEnumArgumentException(nameof(status), (int)status, typeof(Status));
            if (Status == status)
                throw new ArgumentException("Deposit request have the same status!");

            var oldStatus = Status;
            Status = status;
            Transaction.ChangeStatus(status);
            AdminFeedBack = adminFeedBack;
            User.HandleDepositRequestStatusChange(this, oldStatus);
        }

        private void AddTransaction()
        {
            Transaction = new Transactions(User, Amount, this);
        }
    }
}