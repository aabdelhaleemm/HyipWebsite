using Domain.Common;

namespace Domain.Entities
{
    public class State : AuditableEntity
    {
        public int Id { get; set; }
        public bool IsWithdrawActive { get; set; }
        public bool IsDepositActive { get; set; }
    }
}