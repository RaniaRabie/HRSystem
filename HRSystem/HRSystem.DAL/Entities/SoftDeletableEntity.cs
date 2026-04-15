using System;
using System.Collections.Generic;
using System.Text;

namespace HRSystem.DAL.Entities
{
    public class SoftDeletableEntity : AuditableEntity
    {
        public bool IsDeleted { get; set; } = false;
        public string? DeletedBy { get; set; } // FK to User
        public DateTime? DeletedAt { get; set; }
    }
}
