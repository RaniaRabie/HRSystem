
using HRSystem.DAL.Enums;

namespace HRSystem.DAL.Entities
{
    public class EmailLog: AuditableEntity
    {
        public string ToEmail { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;

        public DateTime SentAt { get; set; }

        public EmailStatus Status { get; set; }

        // we can add user id if we want to track who sent the email, but for now we will keep it simple
    }
}
