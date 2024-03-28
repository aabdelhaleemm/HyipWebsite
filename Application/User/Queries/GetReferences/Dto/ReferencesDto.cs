using System.Collections.Generic;

namespace Application.User.Queries.GetReferences.Dto
{
    public class ReferencesDto
    {
        public string UserName { get; set; }
        public List<UserReferenceDto> ReferenceUsers { get; set; }
        public List<TransactionsReferenceDto> Transactions { get; set; }
    }
}