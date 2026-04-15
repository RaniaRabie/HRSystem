using System;
using System.Collections.Generic;
using System.Text;

namespace HRSystem.DAL.Entities
{
    public class AuditableEntity: BaseEntity
    {
        public string? CreatedBy { get; set; } // FK to User
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string? ModifiedBy { get; set; } // FK to User
        public DateTime? ModifiedOn { get; set; }

    }
}
