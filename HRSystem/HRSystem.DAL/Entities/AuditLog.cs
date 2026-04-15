using System;
using System.Collections.Generic;
using System.Text;

namespace HRSystem.DAL.Entities
{
    internal class AuditLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // After I undersatnd the identity of the user, i can add a property for the user id to track who made the change, but for now we will keep it simple and just track the action, entity name, and entity id

        public string Action { get; set; } = null!;
        public string EntityName { get; set; } = null!;
        public string EntityId { get; set; } = null!;

        public DateTime Timestamp { get; set; } = DateTime.Now;

        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
    }
}
}
