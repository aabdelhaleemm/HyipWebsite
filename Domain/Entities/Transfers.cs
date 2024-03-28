using Domain.Common;
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Domain.Entities
{
    public class Transfers : AuditableEntity
    {
        private Transfers()
        {
        }

        public Transfers(User sender, User recipient, double amount) : this()
        {
            Amount = amount;
            Recipient = recipient;
            Sender = sender;
            AddTransaction();
        }

        public int Id { get; set; }
        public double Amount { get; private set; }
        public int RecipientId { get; private set; }
        public User Recipient { get; private set; }
        public int SenderId { get; private set; }
        public User Sender { get; private set; }
        public Transactions SenderTransaction { get; private set; }
        public Transactions RecipientTransaction { get; private set; }


        private void AddTransaction()
        {
            SenderTransaction = new Transactions(Sender, -Amount, this);
            RecipientTransaction = new Transactions(Amount, Recipient, this);
        }
    }
}