using HRSystem.DAL.Models.Entities;

namespace HRSystem.DAL.Configurations
{
    internal class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Action).IsRequired();
            builder.Property(a => a.EntityName).IsRequired();
        }
    }
}
