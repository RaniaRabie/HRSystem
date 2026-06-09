using HRSystem.DAL.Models.Base;

namespace HRSystem.DAL.Models.Entities
{
    public class EmailLog: AuditableEntity
    {
        // We log the email details such as who sent it, to whom, the subject, body, and the status of the email (sent successfully or failed)
        public string UserwhoSentTheEmail { get; set; } = null!;
        public string ToEmail { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;

        public DateTime SentAt { get; set; }

        public EmailStatus Status { get; set; }


    }
}
