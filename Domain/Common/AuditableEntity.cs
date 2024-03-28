using System;

namespace Domain.Common
{
    public class AuditableEntity
    {
        public DateTime Created { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public int LastModifiedBy { get; set; }
    }
}