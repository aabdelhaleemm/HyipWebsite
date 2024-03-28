using System;

namespace Application.Transfer.Query.GetReceivedTransfersHistory
{
    public class ReceivedTransfersDto
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string SenderUserName { get; set; }
        public DateTime Created { get; set; }
    }
}