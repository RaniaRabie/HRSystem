namespace HRSystem.DAL.Models.Base
{
    public abstract class SoftDeletableEntity : AuditableEntity
    {
        public bool IsDeleted { get; set; } = false;
        public string? DeletedBy { get; set; } 
        public DateTime? DeletedAt { get; set; }
    }
}
