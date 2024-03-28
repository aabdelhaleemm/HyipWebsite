using System;

namespace Application.Transfer.Query.GetSentTransferHistory
{
    public class SentTransfersDto
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string RecipientUserName { get; set; }
        public DateTime Created { get; set; }
    }
}