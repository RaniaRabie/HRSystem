
using HRSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRSystem.DAL.Configurations
{
    internal class EmailLogConfiguration : IEntityTypeConfiguration<EmailLog>
    {
        public void Configure(EntityTypeBuilder<EmailLog> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.ToEmail).IsRequired();
            builder.Property(e => e.Subject).IsRequired();
        }
    }
}
