using HRSystem.DAL.Models.Base;

namespace HRSystem.DAL.Models.Entities
{
    public class AuditLog: BaseEntity
    {
        
        // here we track the user who made the change, the action (Create, Update, Delete), the entity name, and the entity id
        public string  UserWhoMadeTheChange{ get; set; } = null!;
        public string Action { get; set; } = null!;
        public string EntityName { get; set; } = null!;
        public string EntityId { get; set; } = null!;

        public DateTime Timestamp { get; set; } = DateTime.Now;

        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
    }
}

