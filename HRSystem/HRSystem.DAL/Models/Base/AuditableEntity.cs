namespace HRSystem.DAL.Models.Base
{
    public abstract class AuditableEntity: BaseEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string? ModifiedBy { get; set; } 
        public DateTime? ModifiedAt { get; set; }

    }
}
