using Domain.Common;

namespace Domain.Entities
{
    public class PaymentMethods : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string WalletId { get; set; }
        public int Minimum { get; set; }
        public double Charge { get; set; }
        public string LogoUrl { get; set; }
    }
}