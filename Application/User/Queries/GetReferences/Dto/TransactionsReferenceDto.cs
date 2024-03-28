using System;

namespace Application.User.Queries.GetReferences.Dto
{
    public class TransactionsReferenceDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public double Amount { get; set; }
    }
}